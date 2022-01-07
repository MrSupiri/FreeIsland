using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public GameObject clock;
    public Transform miniMapCamara;
    public Transform player;
    Text _clock;
    // Start is called before the first frame update
    void Start()
    {
        _clock = clock.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _clock.text = DateTime.Now.ToString("hh:mm tt");
    }

    private void LateUpdate()
    {
        //Vector3 miniMapNewPos = player.position;
        //miniMapNewPos.y = miniMapCamara.position.y;
        //miniMapCamara.position = miniMapNewPos;

    }
}
