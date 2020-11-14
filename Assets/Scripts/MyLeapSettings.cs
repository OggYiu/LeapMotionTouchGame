using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLeapSettings : MonoBehaviour
{
    public MyLeapSettingsScriptableObject settings;


    public InteractionButton[] interactionButtons;

    public float bgmVolume => settings.bgmVolume;
    public bool enableHand => settings.enableHand;

    private void Awake()
    {
        foreach (InteractionButton button in interactionButtons)
        {
            button.SetMinHeight(settings.minHeight);
            button.SetMaxHeight(settings.maxHeight);
            button.springForce = settings.springForce;


            button.restingHeight = settings.restingHeight;


            Vector3 pos = button.transform.position;
            pos.z = settings.btnPosZ;
            button.transform.position = pos;


            Vector3 scale = button.transform.localScale;
            scale.z = settings.btnScaleZ;
            button.transform.localScale = scale;

            button.transform.rotation = Quaternion.Euler(settings.btnRotX, 0, 0);
        }
    }
}
