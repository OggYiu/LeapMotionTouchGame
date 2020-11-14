using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LangSettings : MonoBehaviour
{
    public static string KEY_LANG = "lang";
    public MeshRenderer meshRenderer;
    public Texture spriteChin;
    public Texture spriteEng;

    private void Awake()
    {
        if(spriteChin != null && spriteEng != null)
        {
            Texture targetTexture = PlayerPrefs.GetInt(KEY_LANG, 0) == 0 ? spriteChin : spriteEng;
            meshRenderer.material.SetTexture("_MainTex", targetTexture);
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

    public void OnEngButtonClicked()
    {
        PlayerPrefs.SetInt("lang", 1);
    }

    public void OnChinButtonClicked()
    {
        PlayerPrefs.SetInt("lang", 0);
    }
}
