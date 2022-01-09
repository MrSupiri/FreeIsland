using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;


public class Cardinal : MonoBehaviour
{
    [SerializeField]
    public int NumberOfActiveNPC = 0;
    [SerializeField]
    public int RequiredNumberOfNPC = 2;
    //public readonly Vector3 SPAWN_POINT = new(-35, 0, -40);
    public readonly Vector3 SPAWN_POINT = new(0, 0, 20);
    public static List<Mission> missions = new();
    private Kitchen kitchen;
    private UnityEngine.Object[] listOfNPC;
    private int NumberOfPeopleInQueue = 0;
    public GameObject clock;
    private Text _clock;
    private Transform taskList;
#nullable enable
    public static Mission? ActiveMission;
#nullable disable
    void Start()
    {
        listOfNPC = Resources.LoadAll("NPCs", typeof(GameObject));
        kitchen = GameObject.Find("/Cafe/OrderLocation").GetComponent<Kitchen>();
        taskList = GameObject.Find("/HUD/Mission/TaskList").transform;
        _clock = clock.GetComponent<Text>();
        StartCoroutine(SpawnNPCs());
    }

    // Update is called once per frame
    void Update()
    {
        NumberOfPeopleInQueue = kitchen.queue.Count;
        _clock.text = DateTime.Now.ToString("hh:mm tt");
        
        if (ActiveMission == null)
        {
            var activeMission = missions.FirstOrDefault(m => m.Order.Equals(Kitchen.PickedOrder));
            if (activeMission != null)
            {
                activeMission.Message = $"Handover the order to {activeMission.CustomerName}";
                ActiveMission = activeMission;
                missions.ForEach(missions => missions.Rendered = false);
                foreach (Transform child in taskList)
                {
                    Destroy(child.gameObject);
                }
            }
        }


    }

    IEnumerator SpawnNPCs()
    {
        yield return new WaitForSeconds(2);
        while (true)
        {
            yield return new WaitWhile(() => NumberOfActiveNPC >= RequiredNumberOfNPC || NumberOfPeopleInQueue >= 5);

            GameObject character = Instantiate(listOfNPC[UnityEngine.Random.Range(0, listOfNPC.Length)] as GameObject, SPAWN_POINT, Quaternion.identity);

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

public class Mission
{
    public string ID;
    public string Message;
    public string CustomerName;
    public bool Rendered = false;
    public float Reward;
    public Order Order;
}