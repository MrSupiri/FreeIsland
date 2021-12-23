using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navigate : MonoBehaviour
{
    private Transform target;
    private NavMeshAgent agent;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("/Navigation/RandomPlace").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = transform.parent.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.position = transform.position;
        transform.parent.rotation = transform.rotation;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        agent.destination = target.position;


        animator.SetFloat("Velocity", agent.acceleration);
        Debug.Log($"{agent.acceleration}");
    }
}
