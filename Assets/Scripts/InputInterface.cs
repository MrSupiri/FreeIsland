using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInterface : MonoBehaviour
{
    // Start is called before the first frame update

    PlayerAction control;

    public Dictionary<string, bool> onButtonHold = new Dictionary<string, bool>();
    public Dictionary<string, bool> onButtonDown = new Dictionary<string, bool>();
    public Dictionary<string, bool> onButtonUp = new Dictionary<string, bool>();

    private void Awake()
    {
        control = new PlayerAction();
    }

    private void OnEnable()
    {
        control.Enable();
    }

    private void OnDisable()
    {
        control.Disable();
    }

    void Start()
    {
      foreach(var item in control.DummyAnimation.Get().actions){
            onButtonHold.Add(item.name, false);
            onButtonDown.Add(item.name, false);
            onButtonUp.Add(item.name, false);

            item.performed += delegate { onButtonHold[item.name] = true; };
            item.canceled += delegate { onButtonHold[item.name] = false; };

            item.performed += delegate { onButtonDown[item.name] = true; };
            item.canceled += delegate { onButtonDown[item.name] = false; };

            item.performed += delegate { onButtonUp[item.name] = true; };
            item.canceled += delegate { onButtonUp[item.name] = false; };
        }  
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in control.DummyAnimation.Get().actions)
        {
            onButtonDown[item.name] = item.triggered;
        }
    }

    private void LateUpdate()
    {
        foreach (var item in control.DummyAnimation.Get().actions)
        {
            onButtonUp[item.name] = item.triggered;
        }
    }
}
