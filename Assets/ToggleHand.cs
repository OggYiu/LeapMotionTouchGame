using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleHand : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MyLeapSettings settings = FindObjectOfType<MyLeapSettings>();
        HandModelManager handModelManager = GetComponent<HandModelManager>();
        if (settings.enableHand)
        {
            handModelManager.EnableGroup("Rigged Hands");
        }
        else
        {
            handModelManager.DisableGroup("Rigged Hands");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
