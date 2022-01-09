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
    private GameObject NPC;
    private Cardinal cardinal;
    void Start()
    {
        kitchen = GameObject.Find("/Cafe/OrderLocation").GetComponent<Kitchen>();
        cardinal = GameObject.Find("/Cardinal").GetComponent<Cardinal>();
        agent = GetComponent<NavMeshAgent>();
        animator = transform.parent.GetComponent<Animator>();
        talkUI = transform.Find("TalkTrigger/Canvas").gameObject;
        orderLoaction = GameObject.Find("/Cafe/OrderLocation").transform;
        NPC = transform.parent.gameObject;

        InvokeRepeating("CheckDestination", 2.0f, 1f);
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

    }

    private void CheckDestination()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            //Debug.Log($"{agent.remainingDistance} {agent.stoppingDistance}");

            switch (navLocationType)
            {
                case NavLocationType.Order:
                    LookAt(transform, orderLoaction);
                    break;
                case NavLocationType.FreeSpot:
                    LookAt(transform, orderLoaction);
                    break;
                case NavLocationType.Leave:
                    Destroy(NPC);
                    cardinal.NumberOfActiveNPC -= 1;
                    break;
            }
        }
    }

    public void NavigateTo(NavLocationType locationType)
    {
        if (agent != null)
            agent.isStopped = false;

        if(kitchen == null)
        {
            kitchen = GameObject.Find("/Cafe/OrderLocation").GetComponent<Kitchen>();
        }

        switch (locationType)
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

        navLocationType = locationType;
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

    public static void LookAt(Transform transform, Transform target, int damping = 1)
    {
        var lookPos = target.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, damping);
    }
}

public enum NavLocationType
{
    Order,
    FreeSpot,
    Leave
}

// UnityEditor.TransformWorldPlacementJSON:{"position":{"x":8.489999771118164,"y":0.0,"z":-0.6180000305175781},"rotation":{"x":0.0,"y":0.0,"z":0.0,"w":1.0},"scale":{"x":1.0,"y":1.0,"z":1.0}}