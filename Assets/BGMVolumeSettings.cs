using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMVolumeSettings : MonoBehaviour
{
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        MyLeapSettings settings = FindObjectOfType<MyLeapSettings>();

        audioSource.volume = settings.bgmVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
