using UnityEngine;
using UnityEngine.AI;

public class Navigate : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private GameObject talkUI;
    private Transform orderLoaction;

    public Vector3 targetLocation;
    public NavLocationType navLocationType;
    private Kitchen kitchen;
    private int queueNumber = 0;

    void Start()
    {
        kitchen = GameObject.Find("/Cafe/OrderLocation").GetComponent<Kitchen>();
        agent = GetComponent<NavMeshAgent>();
        animator = transform.parent.GetComponent<Animator>();
        talkUI = transform.Find("TalkTrigger/Canvas").gameObject;
        orderLoaction = GameObject.Find("/Cafe/OrderLocation").transform;
    }

    void Update()
    {
        if (targetLocation == null) return;

        if (talkUI.activeInHierarchy)
        {
            agent.isStopped = true;
            animator.SetFloat("Velocity", 0);
            return;
        }
        else
        {
            agent.isStopped = false;
        }


        transform.parent.SetPositionAndRotation(transform.position, transform.rotation);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        animator.SetFloat("Velocity", agent.velocity.magnitude);

        agent.destination = targetLocation;


        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            //Debug.Log($"{agent.remainingDistance} {agent.stoppingDistance}");

            switch (navLocationType)
            {
                case NavLocationType.Order:
                    transform.LookAt(orderLoaction);
                    //Stop();
                    break;
                case NavLocationType.FreeSpot:
                    transform.LookAt(orderLoaction);
                    //Stop();
                    break;
            }
        }


    }

    public void NavigateTo(NavLocationType locationType)
    {
        if (agent != null)
            agent.isStopped = false;
        navLocationType = locationType;

        if(kitchen == null)
        {
            kitchen = GameObject.Find("/Cafe/OrderLocation").GetComponent<Kitchen>();
        }

        switch (navLocationType)
        {
            case NavLocationType.Order:
                targetLocation = new Vector3(10, 0, -1.4f + kitchen.queue.Count + 2);
                queueNumber = kitchen.queue.Count;
                break;
            case NavLocationType.FreeSpot:
                targetLocation = FindFreeSpot();
                break;
            case NavLocationType.Leave:
                targetLocation = new Vector3(-35, 0, -40);
                break;
        }
    }

    public void UpdateQueuePos()
    {
        if (navLocationType != NavLocationType.Order) return;
        queueNumber -= 1;
        targetLocation = new Vector3(10, 0, 0.6f + queueNumber);
    }

    private Vector3 FindFreeSpot()
    {
        return new Vector3(Random.Range(-3f, 25f), 0, Random.Range(10f, 20f));
    }
}

public enum NavLocationType
{
    Order,
    FreeSpot,
    Leave
}

// -3, 0, 20
// 20, 0, 10