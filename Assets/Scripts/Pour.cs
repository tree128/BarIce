using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Pour : MonoBehaviour
{
    public StageManager StageManager;
    public UIManager UIManager;
    public Material WaveMaterial;
    public ParticleSystem KiraEffect;
    public Material DefaultMat;
    public Collider2D TargetCollider;
    public bool IsFinished = false;
    public AudioSource soundEffectAudioSource;
    public Color Color1;
    public Color Color2;
    public GameObject GlassBottom;
    public GameObject GlassTop;
    public Button PourButton;

    private SpriteRenderer spriteRenderer;
    [SerializeField] private float pourSpeed = 0.001f;
    [SerializeField]private float ratio = 0f;
    private bool isSetMat = false;
    [SerializeField] private AnimationCurve scoreMap = AnimationCurve.Linear(0f, 1f, 1f, 50f);
    [SerializeField]private float score;
    private int pourCountDown = 2;
    private Vector3 bottomStartPos;
    private float bottomMoveThresholdYPos;
    private float bottomMoveDistance;
    [SerializeField]private float iceMoveRate = 0f;
    private float glassHight;
    private float surfaceYPos;
    private float iceTopYPos;
    private float glassTopYPos;
    private bool isPouring = false;
    // Start is called before the first frame update
    void Start()
    {
        soundEffectAudioSource = gameObject.GetComponent<AudioSource>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        bottomStartPos = GlassBottom.transform.position;
        glassTopYPos = GlassTop.transform.position.y;
        glassHight = glassTopYPos - bottomStartPos.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFinished || UIManager.GameOverImage.enabled)
        {
            PourButton.interactable = false;
        }
        else
        {
            PourButton.interactable = true;
        }

        if(isPouring && UIManager.GameOverImage.enabled)
        {
            ButtonUp();
        }
        /*if (!UIManager.GameOverImage.enabled)
        {
            if (ratio < 1 && IsFinished == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
                if (hit.collider == TargetCollider)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        pourCountDown -= 1;
                        UIManager.PourCountTextUpdate(pourCountDown);
                        if (!UIManager.PourButtonIntearactable)
                        {
                            UIManager.PourButtonSetIntearactable();
                        }
                    }*/

                    //if (Input.GetMouseButton(0))
                    if(isPouring)
                    {
                        if (!isSetMat)
                        {
                            spriteRenderer.material = WaveMaterial;
                            WaveMaterial.SetFloat("_Ratio", 0f);
                            WaveMaterial.SetColor("_Color1", Color1);
                            WaveMaterial.SetColor("_Color2", Color2);
                            isSetMat = true;
                            IceMoveSetting();
                        }

                        ratio += pourSpeed;
                        WaveMaterial.SetFloat("_Ratio", ratio);
                        surfaceYPos = bottomStartPos.y + glassHight * ratio;
                        Vector3 glass = GlassBottom.transform.position;
                        if (iceTopYPos < surfaceYPos && glass.y < bottomMoveThresholdYPos)
                        {
                            if(iceMoveRate == 0)
                            {
                                iceMoveRate = bottomMoveDistance / (1 / pourSpeed) / (1 - ratio);
                            }
                            glass.y += iceMoveRate;
                            GlassBottom.transform.position = glass;
                        }
                    }

                    /*if (ScoreCalculator.Instance.GetCurrentScore() < 0)
                    {
                        ScoreCalculator.Instance.ScoreInit();
                    }

                }
                if (pourCountDown == 0 && Input.GetMouseButtonUp(0))
                {
                    UIManager.PourButton.onClick.Invoke();
                }
            }
        }*/

        // 水面での氷の浮き沈み
        //if(iceTopYPos < surfaceYPos && !Input.GetMouseButton(0))
        if(iceTopYPos < surfaceYPos && !isPouring)
        {
            float sin = Mathf.Sin(Time.time * 2f);
            GlassBottom.transform.position = new Vector3(GlassBottom.transform.position.x, GlassBottom.transform.position.y + sin * 0.001f, GlassBottom.transform.position.z);
        }
    }

    // Finishボタンクリック時に実行
    public void CheckScore()
    {
        IsFinished = true;
        score = scoreMap.Evaluate(ratio);
        ScoreCalculator.Instance.Multiply(score);
        ScoreCalculator.Instance.AddToTotalScore(10000-ScoreCalculator.Instance.GetTotalScore());
        if (score == 3.0f)
        {
            PerfectScoreEffect();
            if(ScoreCalculator.Instance.GetCurrentScore() == 9000)
            {
                ScoreCalculator.Instance.AddToCurrentScore(1000);
            }
        }
    }

    private void PerfectScoreEffect()
    {
        KiraEffect.Play();

        if (soundEffectAudioSource != null)
        {
            soundEffectAudioSource.Play(); 
        }
        else
        {
            Debug.LogWarning("AudioSource not assigned. Sound effect will not be played.");
        }
    }   

    public void Init()
    {
        ratio = 0f;
        spriteRenderer.material = DefaultMat;
        isSetMat = false;
        WaveMaterial.SetFloat("_Ratio", 0f);
        IsFinished = false;
        pourCountDown = 2;
        UIManager.PourCountTextUpdate(pourCountDown);
        GlassBottom.transform.position = bottomStartPos;
        iceMoveRate = 0f;
        surfaceYPos = bottomStartPos.y;
    }

    private void IceMoveSetting()
    {
        GameObject icePrefab = FindFirstObjectByType<IceSprites_Set>().gameObject;
        if (icePrefab.transform.childCount > 0)
        {
            Crush_Tilemap topIce = icePrefab.GetComponentInChildren<Crush_Tilemap>();
            Transform iceParent = topIce.transform.parent.gameObject.transform;
            Crush_Tilemap bottomIce = iceParent.GetChild(iceParent.childCount - 1).GetComponent<Crush_Tilemap>();
            iceTopYPos = topIce.transform.position.y;
            float iceBottomPos = bottomIce.transform.position.y;
            float iceHight = iceTopYPos - iceBottomPos;
            bottomMoveThresholdYPos = glassTopYPos - iceHight * 0.65f;
            bottomMoveDistance = bottomMoveThresholdYPos - bottomStartPos.y;
        }
    }

    // Pourボタンが押された時に実行
    public void ButtonDown()
    {
        if (!UIManager.GameOverImage.enabled && !IsFinished)
        {
            pourCountDown -= 1;
            UIManager.PourCountTextUpdate(pourCountDown);
            if (!UIManager.FinishButtonIntearactable)
            {
                UIManager.FinishButtonSetIntearactable();
            }

            if (ScoreCalculator.Instance.GetCurrentScore() < 0)
            {
                ScoreCalculator.Instance.ScoreInit();
            }
            isPouring = true;
        }
    }

    // Pourボタンが離された時に実行
    public void ButtonUp()
    {
        isPouring = false;
        if (pourCountDown == 0 && !IsFinished)
        {
            UIManager.FinishButton.onClick.Invoke();
        }
    }
}
