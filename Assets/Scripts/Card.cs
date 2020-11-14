using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public AnimationCurve animCurve;


    public Image targetObject;


    public Sprite textureOri;
    public Sprite textureTarget;


    public int cardIndex = 0;


    bool _isFlipping = false;
    bool _isFlipped = false;
    bool _isCorrected = false;


    public bool IsFlipping => _isFlipping;
    public bool IsFlipped => _isFlipped;
    public bool IsCorrected => _isCorrected;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (!_isFlipping)
        {
            StartCoroutine(Filp(_isFlipped, animCurve));
        }
    }

    IEnumerator Filp(bool isInvert, AnimationCurve curve)
    {
        _isFlipping = true;

        float time = 0;
        float minimum = 0f;
        float maximum = 180f;

        float origin = minimum;
        float target = maximum;

        float timeLimit = 0.5f;

        while (time <= timeLimit)
        {
            time += Time.deltaTime;

            float value = Mathf.SmoothStep(origin, target, curve.Evaluate(time / timeLimit));

            Sprite targetTexture;

            if (value >= 90)
            {
                targetTexture = _isFlipped ? textureOri : textureTarget;
            }
            else
            {
                targetTexture = _isFlipped ? textureTarget : textureOri;
            }


            targetObject.sprite = targetTexture;
            
            
            if (time >= timeLimit)
            {
                _isFlipping = false;
                _isFlipped = !_isFlipped;
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            }

            transform.rotation = Quaternion.Euler(0f, value >= 90? maximum - value : value, 0f);


            yield return null;
        }

        //isFlipping = false;
        //isFlipped = !isFlipped;
    }


    public void ResetFlip()
    {
        _isFlipped = true;

        StartCoroutine(Filp(_isFlipped, animCurve));
    }


    public void ResetTexture()
    {
        textureOri = GameMgr.Instance.GetOriTexture();
        textureTarget = GameMgr.Instance.GetTargetTexture(cardIndex);

        targetObject.sprite = textureOri;
    }

    public void MakeCorrect()
    {
        _isCorrected = true;
    }
}
