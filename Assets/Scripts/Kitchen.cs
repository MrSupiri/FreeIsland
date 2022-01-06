using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
    // Start is called before the first frame update
    public Queue<Navigate> queue;
    public bool Ordering = false;
    public event Action OnChange;

    void Start()
    {
        queue = new();
    }

    public void JoinQueue(Navigate npcNavigator)
    {
        OnChange += npcNavigator.UpdateQueuePos;
        queue.Enqueue(npcNavigator);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == queue.Peek().gameObject && !Ordering)
        {
            Ordering = true;
            StartCoroutine(PlaceTheOrder());
        }
    }

    IEnumerator PlaceTheOrder()
    {
        yield return new WaitForSeconds(1);

        var npc = queue.Peek().transform.parent;

        npc.LookAt(transform);

        var animator = npc.GetComponent<Animator>();
        animator.SetLayerWeight(animator.GetLayerIndex("Talking"), 1);

        animator.SetInteger("Talk", UnityEngine.Random.Range(5, 8));
        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 5));
        
        animator.SetInteger("Talk", UnityEngine.Random.Range(5, 8));
        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 5));
        
        animator.SetInteger("Talk", UnityEngine.Random.Range(5, 8));
        yield return new WaitForSeconds(UnityEngine.Random.Range(2, 5));
        
        animator.SetLayerWeight(animator.GetLayerIndex("Talking"), 0);

        queue.Peek().NavigateTo(NavLocationType.FreeSpot);

        yield return new WaitForSeconds(2);
        var npcNavigator = queue.Dequeue();
        OnChange.Invoke();
        Ordering = false;
    }
}