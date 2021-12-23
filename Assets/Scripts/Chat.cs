using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;

public class Chat : MonoBehaviour
{
    private Text replyTextBox;
    private TMPro.TMP_InputField inputField;
    private PlayerInput playerInput;
    private Animator animator;
    private CinemachineTargetGroup targetGroup;
    private System.Random rng = new System.Random();

    public string NPCName = "NPC";

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
    }

    private void OnEnable()
    {
        if (playerInput == null || inputField == null) return;
        targetGroup.m_Targets[1] = new CinemachineTargetGroup.Target { target = transform.parent.parent.parent, weight = 2, radius = 1 };
        playerInput.enabled = false;
        inputField.ActivateInputField();
    }

    private void OnDisable()
    {
        playerInput.enabled = true;
    }

    public void OnSubmit(string request)
    {
        replyTextBox.text = NPCName + ": " + Shuffle(request);
        inputField.SetTextWithoutNotify("");
        //animator.SetInteger("TalkAnim", rng.Next(1, 3));
        //StartCoroutine(ResetAnimation());
        inputField.ActivateInputField();
    }

    IEnumerator ResetAnimation()
    {
        yield return new WaitForSeconds(2);
        if (animator.GetInteger("TalkAnim") != 0)
        {
            animator.SetInteger("TalkAnim", 0);
        }
    }

    public string Shuffle(string str)
    {
        char[] array = str.ToCharArray();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            var value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        return new string(array);
    }
}
