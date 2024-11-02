using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IceSprites_Set : MonoBehaviour
{
    public float IceScale;
    public Color Color1;
    public Color Color2;

    private Sprite[] iceSpriteArray;
    private SpriteRenderer[] rendererArray;
    private List<IceInfo> iceInfoList;
    private List<IceInfo> sortedIceInfoList;
    private Vector3 cupPos;
    private Pour pour;
    private Vector3 iceScale;
    private Rigidbody2D rb;

    private class IceInfo
    {
        public SpriteRenderer SpriteRenderer;
        public int xPos;
        public int yPos;

        public IceInfo(SpriteRenderer SpriteRenderer, int xPos, int yPos)
        {
            this.SpriteRenderer = SpriteRenderer;
            this.xPos = xPos;
            this.yPos = yPos;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        iceSpriteArray = Resources.LoadAll<Sprite>("Sprites/ice/");
        rendererArray = gameObject.GetComponentsInChildren<SpriteRenderer>();
        iceInfoList = new List<IceInfo>();
        sortedIceInfoList = new List<IceInfo>();
        foreach(SpriteRenderer renderer in rendererArray)
        {
            iceInfoList.Add(new IceInfo(renderer, Mathf.FloorToInt(renderer.gameObject.transform.localPosition.x), Mathf.FloorToInt(renderer.gameObject.transform.localPosition.y)));
        }
        Comparison<IceInfo> cY = new Comparison<IceInfo>(CompareY);
        Comparison<IceInfo> cX = new Comparison<IceInfo>(CompareX);
        iceInfoList.Sort(cY);
        List<IceInfo> iList = new List<IceInfo>();
        foreach (IceInfo i in iceInfoList)
        {
            //Debug.LogFormat("{0}({1}, {2})", i.SpriteRenderer.gameObject.transform.GetSiblingIndex(), i.xPos, i.yPos);
            iList.Add(i);
            if(iList.Count == 17)
            {
                iList.Sort(cX);
                sortedIceInfoList.AddRange(iList);
                iList.Clear();
            }
        }

        // スプライトをセット
        int count = 0;
        foreach (IceInfo i in sortedIceInfoList)
        {
            i.SpriteRenderer.sprite = iceSpriteArray[count];
            count++;
        }

        // Hierarchyでの順番をソート
        count = 0;
        foreach(IceInfo i in sortedIceInfoList)
        {
            //Debug.LogFormat("{0}({1}, {2})", i.SpriteRenderer.gameObject.transform.GetSiblingIndex(), i.xPos, i.yPos);
            i.SpriteRenderer.gameObject.transform.SetSiblingIndex(count);
            count++;
        }

        pour = GetComponentInParent<CrushIceRoot>().Pour;
        cupPos = pour.gameObject.transform.position;
        iceScale = new Vector3(IceScale, IceScale, IceScale);

        //rb = GetComponent<Rigidbody2D>();
        //rb.isKinematic = true;

        pour.Color1 = Color1;
        pour.Color2 = Color2;
    }

    private void OnDisable()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        rb.isKinematic = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    static int CompareY(IceInfo a, IceInfo b)
    {
        // 降順
        return b.yPos - a.yPos;
    }

    static int CompareX(IceInfo a, IceInfo b)
    {
        // 昇順
        return a.xPos - b.xPos;
    }

    public void MoveIce()
    {
        gameObject.transform.position = cupPos;
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.transform.localScale = iceScale;
        rb.isKinematic = false;
    }
}
