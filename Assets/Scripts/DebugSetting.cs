using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class DebugSetting : MonoBehaviour
{
    public MeshRenderer[] renderers;
    public int activateCount = 0;
    public int targetActivateCount = 5;

    private void Awake()
    {
        Reset();
    }

    public void Reset()
    {
        bool isDeubg = false;

//#if UNITY_EDITOR
        isDeubg = IsDebug();
        //#endif

        for (int i = 0; i < renderers.Length; ++i)
        {
            renderers[i].enabled = isDeubg;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TryToggleDebug()
    {
        ++activateCount;

        if(activateCount >= targetActivateCount)
        {
            bool isDebug = IsDebug();
            SetDebug(!isDebug);
            activateCount = 0;
            Reset();
        }
    }

    public bool IsDebug()
    {
        return PlayerPrefs.GetInt("IsDebug", 0) == 1;
    }

    public void SetDebug(bool isDebug)
    {
        PlayerPrefs.SetInt("IsDebug", isDebug? 1 : 0);
    }
}
