using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTransition : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int currentState = animator.GetInteger("TalkAnim");
        if (currentState != 1 && Input.GetKey(KeyCode.Alpha1))
        {
            animator.SetInteger("TalkAnim", 1);
        }
        else if (currentState != 2 && Input.GetKey(KeyCode.Alpha2))
        {
            animator.SetInteger("TalkAnim", 2);
        }
        else if (currentState != 3 && Input.GetKey(KeyCode.Alpha3))
        {
            animator.SetInteger("TalkAnim", 3);
        }
        else
        {
            animator.SetInteger("TalkAnim", 0);
        }
    }
}
