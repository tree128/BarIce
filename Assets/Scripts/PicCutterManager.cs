using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicCutterManager : MonoBehaviour
{
    public GameObject PicPrefab;
    public GameObject CutterPrefab;
    public Transform PicDefaultPos;
    public Transform CutterDefaultPos;
    public StageManager StageManager;
    public CrushIceRoot CrushIceRoot;

    private Camera camera;
    private Quaternion rot;
    private GameObject pic;
    public GameObject cutter;
    private Vector2 mousePos;
    private Vector2 screenPos;
    [SerializeField] private float pic_offset_x;
    [SerializeField] private float pic_offset_y;
    [SerializeField] private float cutter_offset_x;
    [SerializeField] private float cutter_offset_y;
    [SerializeField] private ParticleSystem cutEffect;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        rot = Quaternion.AngleAxis(-25f, new Vector3(0.0f, 0.0f, 1.0f));
        pic = Instantiate(PicPrefab, PicDefaultPos.position, rot, gameObject.transform);
        cutter = Instantiate(CutterPrefab, CutterDefaultPos.position, Quaternion.identity, gameObject.transform);
        cutEffect = cutter.GetComponentInChildren<ParticleSystem>();
        CrushIceRoot.Cutter = cutter;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        screenPos = camera.ScreenToWorldPoint(mousePos);

        if (StageManager.CurrentStage == StageManager.Stage.Cut)
        {
            screenPos.x += pic_offset_x;
            screenPos.y += pic_offset_y;
            pic.transform.position = screenPos;
            cutter.transform.position = CutterDefaultPos.transform.position;
        }
        else if(StageManager.CurrentStage == StageManager.Stage.Crush)
        {
            screenPos.x += cutter_offset_x;
            screenPos.y += cutter_offset_y;
            cutter.transform.position = screenPos;
            pic.transform.position = PicDefaultPos.transform.position;
        }
        else
        {
            pic.transform.position = PicDefaultPos.transform.position;
            cutter.transform.position = CutterDefaultPos.transform.position;
        }
    }

    public void PlayCutEffect()
    {
        cutEffect.Play();
    }
}
