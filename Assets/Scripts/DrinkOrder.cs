using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkOrder : MonoBehaviour
{
    /// <summary>
    /// �\�������^�C�~���O�łǂ̂��q���񂪗��X�����̂����擾���A�Ή�����Sprite���Z�b�g����B
    /// </summary>

    public CrushIceRoot CrushIceRoot;
    public Customer Customer;

    private SpriteRenderer myRenderer;
    [SerializeField]private string folderPath = "Sprites/Orders/";
    private string customerName;
    private Sprite[] orderArray;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        orderArray = Resources.LoadAll<Sprite>(folderPath);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnBecameVisible()
    {
        Debug.Log("Order");
        // ���q����ƕR�Â��p�^�[��
        /*customerName = Customer.Animator.runtimeAnimatorController.name;
        string path = folderPath + customerName + "_Order";
        myRenderer.sprite = Resources.Load<Sprite>(path);*/

        // ���S�����_���p�^�[��
        myRenderer.sprite = orderArray[Random.Range(0, orderArray.Length)];
    }

}
