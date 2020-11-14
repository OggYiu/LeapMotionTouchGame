using Leap.Unity.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/MyLeapSettingsScriptableObject", order = 1)]

public class MyLeapSettingsScriptableObject : ScriptableObject
{
    public float minHeight;
    public float maxHeight;
    public float restingHeight;
    public float springForce;
    public float btnPosZ;
    public float btnScaleZ;
    public float btnRotX;
    public float btnActivateDelay;
    public float bgmVolume;
    public float soundVolume;
    public bool enableHand;
}
