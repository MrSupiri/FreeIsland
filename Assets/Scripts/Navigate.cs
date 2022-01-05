using UnityEngine;
using UnityEngine.AI;

public class Navigate : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.SetPositionAndRotation(transform.position, transform.rotation);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        //animator.SetFloat("Velocity", agent.velocity.magnitude);

        if(target != null)
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
