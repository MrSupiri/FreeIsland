using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Networking;
using System;
using Unity.Collections;

public class Chat : MonoBehaviour
{
    private Text replyTextBox;
    private TMPro.TMP_InputField inputField;
    private PlayerInput playerInput;
    private Animator animator;
    private CinemachineTargetGroup targetGroup;

    public string NPCName = "NPC";
    public string Pronoun = "he";
    public string Mood = "in bad mood and don't want to talk to anyone";
    public string Order = "two burgers and fries";
    [ReadOnly]
    public string Reaction = "";
    [ReadOnly]
    public float Stratification = 0;
    public void Start()
    {
        replyTextBox = transform.Find("InputField/Reply").gameObject.GetComponent<Text>();
        inputField = transform.Find("InputField").gameObject.GetComponent<TMPro.TMP_InputField>();
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
        animator = transform.parent.parent.parent.gameObject.GetComponent<Animator>();
        targetGroup = GameObject.Find("/Player/TargetGroup").GetComponent<CinemachineTargetGroup>();

        if (NPCName == "NPC" || NPCName == "")
        {
            NPCName = transform.parent.parent.parent.gameObject.name;
        }

        inputField.placeholder.GetComponent<TMPro.TextMeshProUGUI>().text  = "Press F to talk to " + NPCName;

        playerInput.enabled = false;
        inputField.ActivateInputField();

        targetGroup.m_Targets[1] = new CinemachineTargetGroup.Target { target = transform.parent.parent.parent, weight = 2, radius = 1 };

        Stratification = UnityEngine.Random.Range(-2, 4);
        animator.SetLayerWeight(animator.GetLayerIndex("Talking"), 1);
    }
    public float GetTip(float value)
    {
        return Math.Max(value * Stratification, value);
    }
    private void OnEnable()
    {
        if (playerInput == null || inputField == null) return;
        targetGroup.m_Targets[1] = new CinemachineTargetGroup.Target { target = transform.parent.parent.parent, weight = 2, radius = 1 };
        playerInput.enabled = false;
        if (gameObject.activeInHierarchy)
            StartCoroutine(DelayedEnable());
        animator.SetLayerWeight(animator.GetLayerIndex("Talking"), 1);
    }

    /// <summary>
    /// Hack to delay the enable till parent is enabled
    /// </summary>
    /// <returns></returns>
    IEnumerator DelayedEnable()
    {
        yield return new WaitForSeconds(1);
        inputField.ActivateInputField();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        playerInput.enabled = true;
        ResetTextBox(false);
        animator.SetLayerWeight(animator.GetLayerIndex("Talking"), 0);

    }

    public void OnSubmit(string request)
    {
        if (string.IsNullOrWhiteSpace(request))
        {
            ResetTextBox();
            return;
        };
        inputField.interactable = false;
        animator.SetBool("Thinking", true);
        if (gameObject.activeInHierarchy)
            StartCoroutine(GenerateReply(request));
    }

    private void UpdateTextBox(string request)
    {
        replyTextBox.text = NPCName + ": " + request;
        ResetTextBox();
    }

    private void ResetTextBox(bool Wait = true)
    {
        inputField.interactable = true;
        inputField.SetTextWithoutNotify("");
        inputField.Select();
        if (Wait)
        {
            if (gameObject.activeInHierarchy)
                StartCoroutine(ResetAnimation());
        }
        else
            animator.SetInteger("Talk", 0);
    }
    IEnumerator ResetAnimation()
    {
        yield return new WaitForSeconds(4);
        animator.SetInteger("Talk", 0);
    }

    IEnumerator SetAnimationLayer(bool revert = false)
    {
        var index = animator.GetLayerIndex("Talking");
        yield return new WaitForSeconds(0.1f);
        for (float i = 0; i < 1;)
        {
            animator.SetLayerWeight(index, i);
            if (revert)
                i -= 0.1f;
            else
                i -= 0.1f;
        }
    }

    IEnumerator GenerateReply(string request)
    {
        var query = new Query
        {
            name = NPCName,
            order = Order,
            pronoun = Pronoun,
            mood = $"{NPCName} is in bad mood and don't want to talk to anyone",
            prompt = request,
            apiKey = "Lp8G4DoEyKXYZscko2ADXNiLxrDH",
            mock = true
        };
        string json = JsonUtility.ToJson(query);
        var uwr = new UnityWebRequest("https://asia-southeast1-iconicto.cloudfunctions.net/free-island-nlp", "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();
        animator.SetBool("Thinking", false);
        if (uwr.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Getting a respond to the query: " + uwr.error);
            ResetTextBox();
        }
        else
        {
            var reponse = JsonUtility.FromJson<NLPReponse>(uwr.downloadHandler.text);
            this.Stratification += reponse.score;
            this.Reaction = reponse.mood;
            this.MapAnimation(reponse.mood);
            this.UpdateTextBox(reponse.reply);
        }
        uwr.Dispose();
    }

    private void MapAnimation(string mood)
    {
        switch (mood)
        {
            case "happiness":
                animator.SetInteger("Talk", 1);
                break;
            case "love":
                animator.SetInteger("Talk", 2);
                break;
            case "sadness":
                animator.SetInteger("Talk", 3);
                break;
            case "anger":
                animator.SetInteger("Talk", 4);
                break;
            case "neutrality":
                animator.SetInteger("Talk", UnityEngine.Random.Range(5, 8));
                break;
        }
        Debug.Log($"{mood} {animator.GetInteger("Talk")}");
    }
}

[Serializable]
public class NLPReponse
{
    public string reply = null;
    //["happiness", "sadness", "love", "anger", "neutrality"]
    public string mood = null;
    public float score = 0;
}

[Serializable]
public class Query
{
    public string name;
    public string order;
    public string pronoun;
    public string mood;
    public string prompt;
    public string apiKey;
    public bool mock;
}