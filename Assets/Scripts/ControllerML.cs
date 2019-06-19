using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class ControllerML : MonoBehaviour
{

    void Awake()
	{
		MLInput.Start();
        MLInput.OnControllerButtonUp += OnButtonUp;

    }

    void OnDestroy()
    {
        //MLInput.OnControllerButtonDown -= OnButtonDown;
        MLInput.OnControllerButtonUp -= OnButtonUp;
        MLInput.Stop();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnButtonUp(byte controller_id, MLInputControllerButton button)
    {
        Debug.Log("Input detected.");
        if (button == MLInputControllerButton.HomeTap)
        {
            Debug.Log("Home button pressed.");

        }
    }
}
