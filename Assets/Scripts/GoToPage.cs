using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToPage : MonoBehaviour
{
    public string targetPage = "";
    float delay = 0.1f;
    float countDown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(countDown > 0 && targetPage.Length > 0)
        {
            countDown -= Time.deltaTime;
            if(countDown <= 0)
            {
                SceneManager.LoadScene(targetPage);
            }
        }
    }

    public void Go(string pageName)
    {
        if (targetPage.Length > 0) return;
        targetPage = pageName;
        countDown = delay;
    }
}
