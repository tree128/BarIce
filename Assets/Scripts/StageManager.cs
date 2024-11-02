using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Transform CutStage;
    public Transform CrushStage;
    public Transform PourStage;
    public Stage CurrentStage;
    public Customer Customer;
    public CutIce CutIce;
    public CrushIceRoot CrushIceRoot;
    public BoxCollider2D CutterPngCollider;
    public bool CutterPngColliderBool_Customer;
    public bool CutterPngColliderBool_CutIce;
    public ScoreDisplay ScoreDisplay;
    public AudioSource pourAudio;
    public bool ForStock = false;

    private Camera mCamera;
    private Vector3 cutStage;
    private Vector3 crushStage;
    private Vector3 pourStage;
    private Shine cutterPng_Shine;

    public enum Stage
    {
        Cut,
        Crush,
        Pour
    }

    // Start is called before the first frame update
    void Start()
    {
        mCamera = Camera.main;
        cutterPng_Shine = CutterPngCollider.gameObject.GetComponent<Shine>();
        CutterPngCollider.enabled = false;

        // Cutから始まる
        CurrentStage = Stage.Cut;

        // カメラ位置として使うため z=-10 にする
        cutStage = new Vector3(CutStage.transform.position.x, CutStage.transform.position.y, CutStage.transform.position.z - 10);
        crushStage = new Vector3(CrushStage.transform.position.x, CrushStage.transform.position.y, CrushStage.transform.position.z - 10);
        pourStage = new Vector3(PourStage.transform.position.x, PourStage.transform.position.y, PourStage.transform.position.z - 10);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(CutterPngColliderBool_Customer && CutterPngColliderBool_CutIce)
        {
            CutterPngCollider.enabled = true;
        }
        else
        {
            CutterPngCollider.enabled = false;
        }*/
        if (CutterPngColliderBool_Customer && CutIce.StockNum > 0)
        {
            CutterPngCollider.enabled = true;
            cutterPng_Shine.Set_IsShine(true);
        }
        else if (ForStock)
        {
            CutterPngCollider.enabled = true;
            cutterPng_Shine.Set_IsShine(true);
        }
        else
        {
            CutterPngCollider.enabled = false;
            cutterPng_Shine.Set_IsShine(false);
        }
    }

    // クリアした場合
    public void ChangeToCutStage_Clear()
    {
        StartCoroutine(ChangeStage());
    }

    IEnumerator ChangeStage()
    {
        yield return new WaitUntil(() => Customer.isLeft);
        mCamera.transform.position = cutStage;
        CurrentStage = Stage.Cut;
        CutIce.Init();
        CrushIceRoot.OrderedIce_Destroy();
        ScoreCalculator.Instance.ScoreInit();
        ForStock = false;
    }

    // ストックを増やすために戻りたい場合
    public void ChangeToCutStage()
    {
        ForStock = true;
        mCamera.transform.position = cutStage;
        CurrentStage = Stage.Cut;
    }

    public void ChangeToCrushStage()
    {
        mCamera.transform.position = crushStage;
        CurrentStage = Stage.Crush;
    }

    public void ChangeToPourStage()
    {
        mCamera.transform.position = pourStage;
        CurrentStage = Stage.Pour;
    
        if (CurrentStage == Stage.Pour && pourAudio != null && pourAudio.clip != null)
        {
            pourAudio.Play();
        }

        if (CurrentStage == Stage.Pour)
        {
            int currentScore = ScoreCalculator.Instance.GetCurrentScore();

            if (currentScore < 0)
            {
                ScoreCalculator.Instance.ScoreInit_Crash();
            }

            if (currentScore == ScoreCalculator.Instance.GetTotalScore())
            {
                ScoreCalculator.Instance.AddToCurrentScore(110);
                ScoreCalculator.Instance.AddToTotalScore(110);
            }
        }
    }
}
