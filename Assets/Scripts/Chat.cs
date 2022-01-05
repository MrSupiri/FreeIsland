using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.Networking;
using System;

public class Chat : MonoBehaviour
{
    private Text replyTextBox;
    private TMPro.TMP_InputField inputField;
    private PlayerInput playerInput;
    private Animator animator;
    private CinemachineTargetGroup targetGroup;

    public string NPCName = "NPC";
    public string Mood = "";
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

        Stratification = RandomFloat(-2, 3);
        //animator.SetLayerWeight(0, 0);
        //animator.SetLayerWeight(1, 1);
    }

    private void OnEnable()
    {
        if (playerInput == null || inputField == null) return;
        targetGroup.m_Targets[1] = new CinemachineTargetGroup.Target { target = transform.parent.parent.parent, weight = 2, radius = 1 };
        playerInput.enabled = false;
        StartCoroutine(DelayedEnable());
        //animator.SetLayerWeight(0, 0);
        //animator.SetLayerWeight(1, 1);
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
        //animator.SetLayerWeight(0, 1);
        //animator.SetLayerWeight(1, 0);
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
        StartCoroutine(GenerateReply("Alex", "two burgers", "he", "Alex is in bad mood and don't want to talk to anyone", request));
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
            StartCoroutine(ResetAnimation());
        else
            animator.SetInteger("Talk", 0);
    }
    IEnumerator ResetAnimation()
    {
        yield return new WaitForSeconds(2);
        animator.SetInteger("Talk", 0);
    }

    IEnumerator GenerateReply(string name, string order, string pronoun, string mood, string request)
    {
        var query = new Query
        {
            name = name,
            order = order,
            pronoun = pronoun,
            mood = mood,
            prompt = request,
            apiKey = "Lp8G4DoEyKXYZscko2ADXNiLxrDH"
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
            this.Mood = reponse.mood;
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
                animator.SetInteger("Talk", Convert.ToInt32(RandomFloat(5,7)));
                break;
        }
    }

    static float RandomFloat(float min, float max)
    {
        System.Random random = new();
        double val = (random.NextDouble() * (max - min) + min);
        return (float)val;
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
}