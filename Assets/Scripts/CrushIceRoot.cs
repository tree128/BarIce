using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushIceRoot : MonoBehaviour
{
    /// <summary>
    /// �������ꂽ�X�𐶐�����B
    /// </summary>
    
    public StageManager StageManager;
    public Customer Customer;
    public Transform IceParent;
    // �q�I�u�W�F�N�g�֎Q�Ƃ��邽�߂ɕێ�
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

    // �N���b�N���ɌĂяo���Ď��s
    public void OrderedIce_Instantiate()
    {
        /*
        if(IceParent.childCount == 0)
        {
            // ���q����ƕR�Â��p�^�[��
            /*customerName = Customer.Animator.runtimeAnimatorController.name;
            string path = folderPath + customerName + "_Ice";
            GameObject orderedIcePrefab = (GameObject)Resources.Load(path);
            orderedIce = Instantiate(orderedIcePrefab, gameObject.transform.position, Quaternion.identity, IceParent);
            

            // ���S�����_���p�^�[��
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

    // ���g���C�{�^���N���b�N���ɌĂяo���Ď��s
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
