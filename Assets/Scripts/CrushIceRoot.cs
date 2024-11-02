using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushIceRoot : MonoBehaviour
{
    /// <summary>
    /// 注文された氷を生成する。
    /// </summary>
    
    public StageManager StageManager;
    public Customer Customer;
    public Transform IceParent;
    // 子オブジェクトへ参照するために保持
    public GameObject Cutter;
    public Pour Pour;

    //[SerializeField]private string folderPath = "Prefabs/Tilemap/";
    //private string customerName;
    //private GameObject[] iceArray;
    private GameObject orderedIcePrefab;
    private GameObject orderedIce;
    private bool isRetry = false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] iceArray = Resources.LoadAll<GameObject>("Prefabs/Tilemap/");
        foreach (var item in iceArray)
        {
            GameObject icePrefab = Instantiate(item, IceParent);
            icePrefab.SetActive(false);
        }
    }

    // クリック時に呼び出して実行
    public void OrderedIce_Instantiate()
    {
        /*
        if(IceParent.childCount == 0)
        {
            // お客さんと紐づけパターン
            /*customerName = Customer.Animator.runtimeAnimatorController.name;
            string path = folderPath + customerName + "_Ice";
            GameObject orderedIcePrefab = (GameObject)Resources.Load(path);
            orderedIce = Instantiate(orderedIcePrefab, gameObject.transform.position, Quaternion.identity, IceParent);
            

            // 完全ランダムパターン
            if (!isRetry)
            {
                orderedIcePrefab = iceArray[Random.Range(0, iceArray.Length)];
            }
            orderedIce = Instantiate(orderedIcePrefab, gameObject.transform.position, Quaternion.identity, IceParent);
        }
        */
        if (!StageManager.ForStock)
        {
            if (!isRetry)
            {
                orderedIce = IceParent.GetChild(Random.Range(0, IceParent.childCount)).gameObject;
            }
            orderedIce.SetActive(true);
        }
    }

    public void OrderedIce_Destroy()
    {
        if(orderedIce != null)
        {
            //Destroy(orderedIce);
            orderedIce.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
    }

    // リトライボタンクリック時に呼び出して実行
    public void Retry()
    {
        isRetry = true;
        //StartCoroutine(Destroy_Instanciate());
        OrderedIce_Destroy();
        OrderedIce_Instantiate();
        isRetry = false;
    }

    IEnumerator Destroy_Instanciate()
    {
        OrderedIce_Destroy();
        yield return new WaitUntil(() => IceParent.childCount == 0);
        OrderedIce_Instantiate();
        isRetry = false;
    }

    public void MoveIce()
    {
        IceSprites_Set ice = GetComponentInChildren<IceSprites_Set>();
        ice.MoveIce();
    }
}
