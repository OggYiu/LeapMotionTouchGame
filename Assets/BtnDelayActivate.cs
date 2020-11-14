using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnDelayActivate : MonoBehaviour
{
    InteractionButton targetBtn;
    MyLeapSettingsScriptableObject settings;
    float delay = 0f;

    private void Awake()
    {
        MyLeapSettings leapSettings = FindObjectOfType<MyLeapSettings>();
        if(leapSettings != null)
        {
            delay = leapSettings.settings.btnActivateDelay;
        }

        targetBtn = GetComponent<InteractionButton>();
        targetBtn.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(delay > 0)
        {
            delay -= Time.deltaTime;
            if(delay <= 0)
            {
                delay = 0f;
                targetBtn.enabled = true;
            }
        }
    }
}
