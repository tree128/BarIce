using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutIce : MonoBehaviour
{
    public ParticleSystem Point5;
    public GameObject CutIceObj;
    public StageManager StageManager;
    public UIManager UIManager;
    public int StockNum = 0;
    public int StockNumMax = 20;

    private bool isCut1;
    private bool isCut2;
    private bool isCut3;
    private bool isCut4;
    private BoxCollider2D point5Collider;
    private SpriteRenderer[] cutIceSprites = new SpriteRenderer[4];
    private CutClick[] cutClickArray = new CutClick[5];

    // Start is called before the first frame update
    void Start()
    {
        point5Collider = Point5.gameObject.GetComponentInChildren<BoxCollider2D>();
        cutIceSprites = CutIceObj.GetComponentsInChildren<SpriteRenderer>();
        cutClickArray = gameObject.GetComponentsInChildren<CutClick>();
        StageManager.CutterPngColliderBool_CutIce = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCut1 && isCut2 && isCut3 && isCut4)
        {
            Point5.gameObject.SetActive(true);
        }

    }

    public void True_isCut1()
    {
        isCut1 = true;
    }
    public void True_isCut2()
    {
        isCut2 = true;
    }
    public void True_isCut3()
    {
        isCut3 = true;
    }
    public void True_isCut4()
    {
        isCut4 = true;
    }

    public void True_isCut5()
    {
        isCut1 = false;
        isCut2 = false;
        isCut3 = false;
        isCut4 = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        point5Collider.enabled = false;
        foreach (SpriteRenderer s in cutIceSprites)
        {
            s.enabled = true;
        }

        StageManager.CutterPngColliderBool_CutIce = true;
        StockNum += 4;
        StockNum = Mathf.Clamp(StockNum, 0, StockNumMax);
        UIManager.StockNumUpdate();
        UIManager.StockMoveText.text = "+4";
        UIManager.StockMoveAnimator.SetTrigger("StockMove");

        StartCoroutine(WaitAndInit());
    }

    // 包丁クリック時に実行
    public void MoveToCrushStage()
    {
        if (!StageManager.ForStock)
        {
            StockNum -= 1;
            UIManager.StockNumUpdate();
            UIManager.StockMoveText.text = "-1";
            UIManager.StockMoveAnimator.SetTrigger("StockMove");
        }
        else
        {
            StageManager.ForStock = false;
        }       
    }

    public void Init()
    {
        isCut1 = false;
        isCut2 = false;
        isCut3 = false;
        isCut4 = false;
        point5Collider.enabled = true;
        StageManager.CutterPngColliderBool_CutIce = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        foreach (SpriteRenderer s in cutIceSprites)
        {
            s.enabled = false;
        }

        foreach(CutClick c in cutClickArray)
        {
            c.Init();
        }
    }

    IEnumerator WaitAndInit()
    {
        yield return new WaitForSeconds(2f);
        Init();
    }
}
