using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameMgr : MonoBehaviour
{
    public AnimationCurve scaleAnimCurve;
    public static GameMgr Instance => _instance;

    public Image imageBigCard;
    public Image imageTimerBg;

    public Card[] cards;

    public Sprite[] cardTexturesChin;
    public Sprite[] cardTexturesEng;
    public Sprite[] cardTextures => PlayerPrefs.GetInt(LangSettings.KEY_LANG, 0) == 0 ? cardTexturesChin : cardTexturesEng;

    public Sprite cardBackTexture;

    private static GameMgr _instance = null;
    public Image imageCorrectAns;
    public Sprite correctTextureChin;
    public Sprite correctTextureEng;


    //public Image imageWrongAns;

    public TextMeshProUGUI textCorrectCount;

    public TextMeshProUGUI textTime;

    List<Card> cardsClicked = new List<Card>();

    int correctCount = 0;

    public float timeLimit = 60;

    float gameTime = 0;
    float lastTimeLeft = -1;


    bool isGameEnded = false;


    public AudioSource sfxWin;
    public AudioSource sfxLose;
    public AudioSource sfxCorrect;
    public AudioSource sfxWrong;
    public AudioSource bgmTimeUp;


    void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(_instance.gameObject);
        }

        _instance = this;
        imageCorrectAns.sprite = PlayerPrefs.GetInt(LangSettings.KEY_LANG, 0) == 0 ? correctTextureChin : correctTextureEng;
        imageCorrectAns.transform.localScale = Vector3.zero;
        //imageWrongAns.transform.localScale = Vector3.zero;

        textCorrectCount.text = "0";
    }

    void Reset()
    {
        int cardCount = cards.Length;

        HashSet<int> cardImageIndexSet = new HashSet<int>();
        for (int i = 0; i < cardCount; ++i)
        {
            cardImageIndexSet.Add(i);
        }

        HashSet<int> cardIndexSet = new HashSet<int>();
        for (int i = 0; i < cardCount; ++i)
        {
            cardIndexSet.Add(i);
        }

        while(cardIndexSet.Count > 0)
        {
            int cardIndex;

            {
                int index;
                index = UnityEngine.Random.Range(0, cardImageIndexSet.Count);
                cardIndex = cardImageIndexSet.ElementAt(index);

                //Debug.Log("cardIndex: " + cardIndex);
                //Debug.Log("value: " + cardImageIndexSet.ElementAt(index));
                //Debug.Log(" ");
                cardImageIndexSet.Remove(cardImageIndexSet.ElementAt(index));
            }

            {
                int index;

                index = UnityEngine.Random.Range(0, cardIndexSet.Count);
                cards[cardIndexSet.ElementAt(index)].cardIndex = cardIndex;
                cardIndexSet.Remove(cardIndexSet.ElementAt(index));

                index = UnityEngine.Random.Range(0, cardIndexSet.Count);
                cards[cardIndexSet.ElementAt(index)].cardIndex = cardIndex;
                cardIndexSet.Remove(cardIndexSet.ElementAt(index));
            }
        }

        for (int i = 0; i < cards.Length; ++i)
        {
            cards[i].ResetTexture();
        }
    }


    void Start()
    {
        Reset();
    }
    void EndGame()
    {
        if (isGameEnded) return;
        isGameEnded = true;

        if (bgmTimeUp.isPlaying)
        {
            bgmTimeUp.Stop();
        }

        StartCoroutine(GoToNextPage(3f));
    }

    IEnumerator GoToNextPage(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("04_Overview");
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameEnded) return;


        gameTime += Time.deltaTime;


        if(gameTime > 60.0f)
        {
            gameTime = 60.0f;

            sfxLose.Play();
            textTime.text = "0";
            EndGame();
            return;
        }

        float timeLeft = (float)Math.Round(timeLimit - gameTime);
        if (lastTimeLeft < 0) lastTimeLeft = timeLeft;

        imageTimerBg.fillAmount = (timeLimit - gameTime) / timeLimit;
        if (timeLeft <= timeLimit * .3f)
        {
            Color newCol;
            if (ColorUtility.TryParseHtmlString("#ED1C24", out newCol))
                imageTimerBg.color = newCol;
        }
        else if (timeLeft <= timeLimit * .6f)
        {
            Color newCol;
            if (ColorUtility.TryParseHtmlString("#F7941D", out newCol))
                imageTimerBg.color = newCol;
        }
        else
        {
            Color newCol;
            if (ColorUtility.TryParseHtmlString("#8DC63F", out newCol))
                imageTimerBg.color = newCol;
        }

        bool hurryUp = timeLeft <= 10;
        if(hurryUp && !bgmTimeUp.isPlaying)
        {
            bgmTimeUp.Play();
        }

        if(lastTimeLeft != timeLeft && hurryUp)
        {
            textTime.color = Color.red;
            textTime.transform
                .DOScale(0.03f, 0.2f)
                .SetLoops(2, LoopType.Yoyo)
            ;
        }
        lastTimeLeft = timeLeft;

        textTime.text = timeLeft.ToString();
    }


    public Sprite GetTargetTexture(int cardIndex)
    {
        return cardTextures[cardIndex];
    }

    public Sprite GetOriTexture()
    {
        return cardBackTexture;
    }

    public void OnCardClicked(int index)
    {
        if (isGameEnded) return;


        if (cards[index].IsFlipped || cards[index].IsFlipping) return;


        if (cardsClicked.Count >= 2) return;


        cardsClicked.Add(cards[index]);


        cards[index].OnClick();


        if (cardsClicked.Count == 2)
        {
            if (cardsClicked[0].cardIndex == cardsClicked[1].cardIndex)
            {
                cardsClicked[0].MakeCorrect();
                cardsClicked[1].MakeCorrect();


                StartCoroutine(ShowCorrectUI());

                ++correctCount;


                //Debug.Log("card index: " + cardsClicked[0].cardIndex);
                imageBigCard.gameObject.SetActive(true);
                imageBigCard.sprite = GetTargetTexture(cardsClicked[0].cardIndex);


                cardsClicked.Clear();


                textCorrectCount.text = correctCount.ToString();


                if (correctCount >= 4)
                {
                    sfxWin.Play();
                    EndGame();
                }
                else
                {
                    sfxCorrect.Play();
                }
            }
            else
            {
                //StartCoroutine(ShowCorrectUI(false));
                sfxWrong.Play();

                StartCoroutine(ResetFlippedCards());
            }
        }
    }

    public IEnumerator ResetFlippedCards()
    {
        yield return new WaitForSeconds(1.1f);


        for (int i = 0; i < cards.Length; ++i)
        {
            if(cards[i].IsFlipped && !cards[i].IsCorrected) cards[i].ResetFlip();
        }

        cardsClicked.Clear();
    }

    IEnumerator ShowCorrectUI()
    {
        Image imageTarget = imageCorrectAns;

        float timeLimit = 2f;
        float time = 0f;
        float rotRange = 30f;

        while(time < timeLimit)
        {
            time += Time.deltaTime;

            //float scale = Mathf.SmoothStep(0, 1f, scaleAnimCurve.Evaluate(time / timeLimit));
            float scale = scaleAnimCurve.Evaluate(time / timeLimit);


            imageTarget.transform.localScale = new Vector3(scale, scale, scale);


            float rot = Mathf.PingPong(time * 100f, rotRange / 2f) - rotRange / 2f;


            imageTarget.transform.rotation = Quaternion.Euler(0f, 0f, rot);


            yield return null;
        }


        imageBigCard.gameObject.SetActive(false);
        imageTarget.transform.localScale = Vector3.zero;
    }

    IEnumerator GoToNextStage()
    {
        yield return new WaitForSeconds(1.0f);
    }
}
