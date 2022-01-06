using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cardinal : MonoBehaviour
{
    public int NumberOfActiveNPC = 0;
    public int RequiredNumberOfNPC = 2;
    //public readonly Vector3 SPAWN_POINT = new(-35, 0, -40);
    public readonly Vector3 SPAWN_POINT = new(0, 0, 20);
    private Kitchen kitchen;
    private Object[] listOfNPC;
    private int NumberOfPeopleInQueue = 0;
    void Start()
    {
        listOfNPC = Resources.LoadAll("NPCs", typeof(GameObject));
        kitchen = GameObject.Find("/Cafe/OrderLocation").GetComponent<Kitchen>();
        StartCoroutine(SpawnNPCs());
    }

    // Update is called once per frame
    void Update()
    {
        NumberOfPeopleInQueue = kitchen.queue.Count;
    }

    IEnumerator SpawnNPCs()
    {
        yield return new WaitForSeconds(2);
        while (true)
        {
            yield return new WaitWhile(() => NumberOfActiveNPC >= RequiredNumberOfNPC || NumberOfPeopleInQueue >= 5);

            GameObject character = Instantiate(listOfNPC[Random.Range(0, listOfNPC.Length)] as GameObject, SPAWN_POINT, Quaternion.identity);

            character.name = character.name.Replace("(Clone)","");

            Navigate navigator = character.transform.Find("NPC").GetComponent<Navigate>();

            navigator.NavigateTo(NavLocationType.Order);
            character.transform.parent = GameObject.Find("NPCs").transform;
            kitchen.JoinQueue(navigator);


            NumberOfActiveNPC += 1;

            yield return new WaitForSeconds(2);
        }
    }
}
