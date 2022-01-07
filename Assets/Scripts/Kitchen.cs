using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitchen : MonoBehaviour
{

    public Queue<Navigate> queue = new();
    public bool Ordering = false;
    public event Action OnChange;
    public Queue<Order> OnGoingOrders = new();
    public Queue<Order> CookedOrders = new();
    public ChefStatus ChefCurrentStatus = ChefStatus.Idle;
#nullable enable
    private Order? cookingOrder;
#nullable disable

    private void OnTriggerEnter(Collider collider)
    {
        if (queue.Count == 0) return;
        if (collider.gameObject == queue.Peek().gameObject && !Ordering)
        {
            Ordering = true;
            StartCoroutine(PlaceTheOrder());
        }
    }

    private void Update()
    {
        if (queue.Count == 0)
        {
            if (OnGoingOrders.Count > 0 && ChefCurrentStatus != ChefStatus.Cooking) {
                ChefCurrentStatus = ChefStatus.GoingToKitchen;
            }
        }

        if (cookingOrder == null && ChefCurrentStatus == ChefStatus.Cooking)
        {
            if (OnGoingOrders.Count != 0)
            {
                StartCoroutine(PrepOrder(OnGoingOrders.Dequeue()));
            }
        }

        if (queue.Count == 0 && OnGoingOrders.Count == 0 && cookingOrder == null)
        {
            ChefCurrentStatus = ChefStatus.Idle;
        }

    }
    public void JoinQueue(Navigate npcNavigator)
    {
        OnChange += npcNavigator.UpdateQueuePos;
        queue.Enqueue(npcNavigator);
    }

    IEnumerator PrepOrder(Order order){
        cookingOrder = order;
        yield return new WaitForSeconds(order.TimeToCook);
        Cardinal.missions.Add(new Mission { Message = $"{order.Customer.name}'s order is Ready!", Location = order.Customer.transform, Reward = order.Price});
        CookedOrders.Enqueue(cookingOrder);
        cookingOrder = null;
    }

    IEnumerator PlaceTheOrder()
    {
        yield return new WaitForSeconds(1);

        ChefCurrentStatus = ChefStatus.GoingToCounter;

        yield return new WaitWhile(() => ChefCurrentStatus != ChefStatus.TakingOrder  );


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
        OnGoingOrders.Enqueue(new Order {
            Price = UnityEngine.Random.Range(10f, 30f),
            TimeToCook = UnityEngine.Random.Range(10, 20),
            Customer = npcNavigator.gameObject
        });
        ChefCurrentStatus = ChefStatus.Idle;
    }
}

public enum ChefStatus
{
    GoingToKitchen,
    GoingToCounter,
    Cooking,
    TakingOrder,
    Idle
}

public class Order
{
    public float Price;
    public int TimeToCook;
    public GameObject Customer;
}
