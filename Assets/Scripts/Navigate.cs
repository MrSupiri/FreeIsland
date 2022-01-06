using UnityEngine;
using UnityEngine.AI;

public class Navigate : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    private GameObject talkUI;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = transform.parent.GetComponent<Animator>();
        talkUI = transform.Find("TalkTrigger/Canvas").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (talkUI.activeInHierarchy)
        {
            agent.isStopped = true;
            animator.SetFloat("Velocity", 0);
            return;
        }

        transform.parent.SetPositionAndRotation(transform.position, transform.rotation);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        animator.SetFloat("Velocity", agent.velocity.magnitude);

        if (target != null)
        {
            agent.isStopped = false;
            agent.destination = target.transform.position;
        }
        else
        {
            agent.isStopped = true;
        }
    }
}
