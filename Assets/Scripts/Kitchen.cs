using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Kitchen : MonoBehaviour
{

    public Queue<Navigate> queue = new();
    public bool Ordering = false;
    public event Action OnChange;
    public Queue<Order> OnGoingOrders = new();
    public List<Order> CookedOrders = new();
    public ChefStatus ChefCurrentStatus = ChefStatus.Idle;
#nullable enable
    public Order? cookingOrder;
    public static Order? PickedOrder;
#nullable disable
    private GameObject foodTray;
    private Order[] counter = new Order[3] { null, null, null };
    private void Start()
    {
        foodTray = (GameObject) Resources.Load("FoodTray", typeof(GameObject));
    }

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
                StartCoroutine(PrepOrder(OnGoingOrders.Peek()));
            }
        }

        if (queue.Count == 0 && OnGoingOrders.Count == 0 && cookingOrder == null)
        {
            ChefCurrentStatus = ChefStatus.Idle;
        }


        for(var i=0; i < Math.Min(CookedOrders.Count, 3); i++)
        {
            if (CookedOrders[i].PickedUp)
            {
                PickedOrder = CookedOrders[i];
                CookedOrders.RemoveAt(i);
                counter[i] = null;
                continue;
            }
            
            if (!counter.Contains(CookedOrders[i]))
            {
                
                var index = Array.IndexOf(counter, null);
                if (index < 0) continue;
                int offset = index % 3;
                counter[index] = CookedOrders[i];
                CookedOrders[i].FoodTray.transform.position = new(8.814f - (0.55f * offset), 1.085f, 1.244f);
                CookedOrders[i].FoodTray.SetActive(true);
            }

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
        Cardinal.missions.Add(new Mission { 
            ID = Guid.NewGuid().ToString(),
            Message = $"{order.Customer.transform.parent.name}'s order is Ready!",
            CustomerName = order.Customer.transform.parent.name,
            Reward = order.Price,
            Order = order
        });




        order.FoodTray = Instantiate(foodTray, Vector3.zero, Quaternion.identity);
        order.FoodTray.transform.parent = transform.parent;
        order.FoodTray.name = $"{order.Customer.transform.parent.name} Order";
        order.FoodTray.SetActive(false);


        OnGoingOrders.Dequeue();
        CookedOrders.Add(cookingOrder);
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
    public GameObject FoodTray;
    public bool PickedUp = false;
}
