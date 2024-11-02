using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public Pour Pour;
    public Transform DoorPos;
    public GameObject[] Chairs = new GameObject[2];
    public SpriteRenderer SpeechBubble;
    public SpriteRenderer DrinkOrder;
    public SpriteRenderer AngryMark;
    public bool isLeft = false; // �ޓX������
    public StageManager StageManager;
    public UIManager UIManager;
    public PicCutterManager_Tilemap PicCutterManager_Tilemap;
    public Animator Animator;
    public Vector2Int WaitTime_CustomerEnter = new Vector2Int(10, 15);
    public Vector2Int WaitTime_Order = new Vector2Int(3, 5);
    public Vector2Int WaitTime_Drink = new Vector2Int(15, 25);
    public bool IsGameOver = false;
    public float WalkSpeed = 1f;
    public AudioSource BellInSE;
    public AudioSource BellOutSE;
    public AudioSource OrderSE;

    private RuntimeAnimatorController[] customerArray;
    private List<RuntimeAnimatorController> customerList;
    private SpriteRenderer sRenderer;
    private Vector3 currentPos;
    private Vector3 selectedChairPos;
    private Vector3 targetPos;
    private bool isMove = false; // �ړ�����
    private bool isEntering = false; // ���X����
    private bool isLeaving = false;  // �ޓX����
    private int waitSecond;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = DoorPos.position;
        sRenderer = gameObject.GetComponent<SpriteRenderer>();
        Animator = gameObject.GetComponent<Animator>();
        sRenderer.enabled = false;
        SpeechBubble.enabled = false;
        DrinkOrder.enabled = false;
        AngryMark.enabled = false;
        StageManager.CutterPngColliderBool_Customer = false;

        customerArray = Resources.LoadAll<RuntimeAnimatorController>("AnimController"); // �z�񂪕Ԃ��Ă���
        customerList = new List<RuntimeAnimatorController>(customerArray); // �v�f����������ǉ������肷�邽�߃��X�g��

        StartCoroutine(CustomerEnter());
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver)
        {
            StartCoroutine(GameOver());
        }

        if (isMove)
        {
            if(gameObject.transform.position == targetPos)
            {
                isMove = false;
                if (isEntering)
                {
                    StartCoroutine(OrderThinking());
                    Animator.SetBool("IsSit", true);
                    StartCoroutine(RendererOnOff());
                    isEntering = false;
                }
                if (isLeaving)
                {
                    Debug.Log("�ޓX");
                    //GetComponents<AudioSource>()[1].Play();
                    BellOutSE.Play();
                    sRenderer.enabled = false;
                    isLeaving = false;
                    isLeft = true;
                    UIManager.ConsumeNumInit();
                    UIManager.StockNumUpdate();
                }
                
            }

            // �ړI�n�֌�����
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, Time.deltaTime * WalkSpeed);
        }

    }

    IEnumerator CustomerEnter()
    {
        isLeft = false;
        StageManager.CutterPngColliderBool_Customer = false;
        waitSecond = Random.Range(WaitTime_CustomerEnter.x, WaitTime_CustomerEnter.y + 1);
        Debug.LogFormat("���X�҂��F{0}�b", waitSecond);
        yield return new WaitForSeconds(waitSecond);
        gameObject.transform.Rotate(0, 180, 0);

        // ���q����������_���Őݒ�
        Animator.runtimeAnimatorController = customerList[Random.Range(0, customerList.Count)];

        // ����֎q�����ߖړI�n�ɐݒ肷��
        //elapsedTime = 0f;
        currentPos = gameObject.transform.position;
        selectedChairPos = Chairs[Random.Range(0, Chairs.Length)].transform.position;
        targetPos = new Vector3(selectedChairPos.x, currentPos.y, currentPos.z); // x���W�݈̂ړ�
        isEntering = true;
        isMove = true;
        
        sRenderer.enabled = true;
        //GetComponents<AudioSource>()[0].Play();
        BellInSE.Play();
    }

    IEnumerator OrderThinking()
    {
        waitSecond = Random.Range(WaitTime_Order.x, WaitTime_Order.y + 1);
        Debug.LogFormat("�����l����:{0}�b", waitSecond);
        yield return new WaitForSeconds(waitSecond);
        SpeechBubble.enabled = true;
        DrinkOrder.enabled = true;
        UIManager.Canvas_Time.enabled = true;
        UIManager.CountDown();
        StageManager.CutterPngColliderBool_Customer = true;
        //GetComponents<AudioSource>()[2].Play();
        OrderSE.Play();
    }

    // �N���b�N�����s
    public void CustomerChange()
    {
        StartCoroutine(Drink_Leave_Enter());
    }

    IEnumerator Drink_Leave_Enter()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("����񋟁ASE");
        SpeechBubble.enabled = false;
        DrinkOrder.enabled = false;
        UIManager.Canvas_Time.enabled = false;

        waitSecond = Random.Range(WaitTime_Drink.x, WaitTime_Drink.y + 1);
        Debug.LogFormat("���ݒ�:{0}�b", waitSecond);
        yield return new WaitForSeconds(waitSecond);
        Debug.Log("���ݏI���");
        StartCoroutine(CustomerLeave_Enter());
    }

    IEnumerator RendererOnOff()
    {
        sRenderer.enabled = false;
        if (isEntering)
        {
            yield return new WaitUntil(() => Animator.GetCurrentAnimatorStateInfo(0).IsName("Sit"));
            gameObject.transform.position = selectedChairPos;
            sRenderer.enabled = true;
        }
        else if (isLeaving)
        {
            yield return new WaitUntil(() => Animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"));
            sRenderer.enabled = true;
        }
    }

    IEnumerator GameOver()
    {
        Debug.Log("GAMEOVER");
        IsGameOver = false;
        PicCutterManager_Tilemap.IsGameOver = true;
        DrinkOrder.enabled = false;
        AngryMark.enabled = true;
        UIManager.Canvas_Time.enabled = false;
        UIManager.GameOver_InOperable();
        yield return new WaitForSeconds(3f);
        StartCoroutine(CustomerLeave_Enter());
    }

    IEnumerator CustomerLeave_Enter()
    {
        gameObject.transform.Rotate(0, 180, 0);
        currentPos = new Vector3(gameObject.transform.position.x, DoorPos.position.y, gameObject.transform.position.z);
        gameObject.transform.position = currentPos;
        targetPos = DoorPos.position;
        isLeaving = true;
        isMove = true;
        Animator.SetBool("IsSit", false);
        StartCoroutine(RendererOnOff());
        StageManager.ChangeToCutStage_Clear();
        yield return new WaitUntil(() => isLeft);
        Pour.Init();
        AngryMark.enabled = false;
        SpeechBubble.enabled = false;
        UIManager.GameOver_Operable();
        PicCutterManager_Tilemap.IsGameOver = false;
        StartCoroutine(CustomerEnter());
    }
}
