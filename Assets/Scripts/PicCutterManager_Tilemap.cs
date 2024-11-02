using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PicCutterManager_Tilemap: MonoBehaviour
{
    public Image PicImage;
    public Image CutterImage;
    public Image CutterGuideImage;
    public Image OtherImage;
    public Canvas Canvas;
    public StageManager StageManager;
    public bool CutterColliderPause = false;
    public bool IsGameOver = false;

    private Image mouseImage;
    private RectTransform rectTransform;
    private Vector2 mousePos;
    private ParticleSystem cutEffect;
    private BoxCollider2D cutterCollider;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = Canvas.GetComponent<RectTransform>();
        cutEffect = CutterImage.GetComponentInChildren<ParticleSystem>();
        cutterCollider = gameObject.GetComponentInChildren<BoxCollider2D>();
        cutterCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver || StageManager.CurrentStage == StageManager.Stage.Pour)
        {
            PicImage.enabled = false;
            CutterImage.enabled = false;
            CutterGuideImage.enabled = false;
            mouseImage = OtherImage;
        }
        else
        {
            if (StageManager.CurrentStage == StageManager.Stage.Cut)
            {
                PicImage.enabled = true;
                CutterImage.enabled = false;
                CutterGuideImage.enabled = false;
                mouseImage = PicImage;
            }
            else if (StageManager.CurrentStage == StageManager.Stage.Crush)
            {
                PicImage.enabled = false;
                CutterImage.enabled = true;
                CutterGuideImage.enabled = true;
                mouseImage = CutterImage;
                if (Input.GetMouseButton(0) && !CutterColliderPause)
                {
                    cutterCollider.enabled = true;
                }
                else
                {
                    cutterCollider.enabled = false;
                }
            }
        }
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, Canvas.worldCamera, out mousePos);
        mouseImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(mousePos.x, mousePos.y);
    }

    public void PlayCutEffect(Vector3 pos)
    {
        cutEffect.transform.position = pos;
        cutEffect.Play();
    }
}
