using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int TimeLimit = 30;
    public Image TimeLimitImage;
    public Image MaskImage;
    public Image GameOverImage;
    public Text StockText;
    public Text StockMoveText;
    public Text ConsumeNumText;
    public SpriteRenderer SpeechBubble;
    public Canvas Canvas_Time;
    public Canvas Canvas_Stock;
    public Canvas Canvas_Crush;
    public Canvas Canvas_Pour;
    public Text PourCountText;
    public Button FinishButton;
    public bool FinishButtonIntearactable = false;
    public Button RetryButton;
    public StageManager StageManager;
    public Customer Customer;
    public CutIce CutIce;
    public Animator StockMoveAnimator;

    private RectTransform maskRect;
    private float time;
    private bool isCountDown = false;
    private int stockNum;
    private int consumeNum =-1;

    // Start is called before the first frame update
    void Start()
    {
        //timeLimitRect = TimeLimitImage.GetComponent<RectTransform>();
        maskRect = MaskImage.GetComponent<RectTransform>();
        time = 0;
        Canvas_Time.enabled = false;
        GameOverImage.enabled = false;
        StockNumUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeLimitImage.fillAmount == 1f)
        {
            Customer.IsGameOver = true;
            time = 0;
            TimeLimitImage.fillAmount = 0;
            isCountDown = false;
        }

        if (isCountDown)
        {
            time += Time.deltaTime;
            TimeLimitImage.fillAmount = time / TimeLimit;
        }

        if (StageManager.CurrentStage == StageManager.Stage.Cut)
        {
            Canvas_Stock.enabled = true;
            //pourCanvasVisible = false;
        }

        if (StageManager.CurrentStage == StageManager.Stage.Crush)
        {
            Canvas_Crush.enabled = true;
        }
        else
        {
            Canvas_Crush.enabled = false;
        }

        if (StageManager.CurrentStage == StageManager.Stage.Pour)
        {           
            Canvas_Pour.enabled = true;
            Canvas_Stock.enabled = false;
            if (FinishButtonIntearactable)
            {
                FinishButton.interactable = true;
            }
            else
            {
                FinishButton.interactable = false;
            }
        }
        else
        {
            Canvas_Pour.enabled = false;
        }
    }

    public void FinishButtonSetIntearactable()
    {
        FinishButtonIntearactable = true;
    }
    
    public void FinishButtonSetNotIntearactable()
    {
        FinishButtonIntearactable = false;
    }

    public void CountDown()
    {
        time = 0;
        maskRect.position = RectTransformUtility.WorldToScreenPoint(Camera.main, SpeechBubble.transform.position);
        isCountDown = true;
    }

    public void GameOver_InOperable()
    {
        GameOverImage.enabled = true;
        FinishButtonSetNotIntearactable();
        RetryButton.interactable = false;
    }

    public void GameOver_Operable()
    {
        GameOverImage.enabled = false;
        RetryButton.interactable = true;
    }

    // PourStageのボタンクリックで実行
    public void IsCountDown_False()
    {
        isCountDown = false;
    }

    public void StockNumUpdate()
    {
        stockNum = CutIce.StockNum;
        StockText.text = "x" + stockNum.ToString();
        if(stockNum < Mathf.Abs(consumeNum))
        {
            StockText.color = Color.red;
            RetryButton.interactable = false;
        }
        else
        {
            RetryButton.interactable = true;
            if (stockNum == CutIce.StockNumMax)
            {
                StockText.color = Color.yellow;
            }
            else
            {
                StockText.color = Color.white;
            }
        }

    }

    // リトライボタンクリックで実行
    public void ConsumeAndNumUpdate()
    {
        CutIce.StockNum += consumeNum;
        //StockNumUpdate();

        StockMoveText.text = consumeNum.ToString();
        StockMoveAnimator.SetTrigger("StockMove");

        consumeNum *= 2;
        if(consumeNum >= -16)
        {
            ConsumeNumText.text = consumeNum.ToString();
        }
        StockNumUpdate();

        ScoreCalculator.Instance.ScoreInit();

        if (consumeNum == -16)
        {
            RetryButton.interactable = false;
        }
    }

    public void ConsumeNumInit()
    {
        consumeNum = -1;
        ConsumeNumText.text = consumeNum.ToString();
    }

    public void PourCountTextUpdate(int count)
    {
        PourCountText.text = "あと" + count.ToString() + "回";
    }
}
