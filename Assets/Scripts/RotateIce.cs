using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIce : MonoBehaviour
{
    public CrushIceRoot CrushIceRoot;
    public float rotateRate = 1f;
    public PicCutterManager_Tilemap PicCutterManager_Tilemap;
    public UIManager UIManager;

    private Collider2D myCollider;
    private SpriteRenderer myRenderer;
    private bool isRotate = false;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = gameObject.GetComponent<Collider2D>();
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.GameOverImage.enabled)
        {
            isRotate = false;
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider == myCollider)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    myRenderer.color = new Color(0.6f, 0.6f, 0.6f);
                    PicCutterManager_Tilemap.CutterColliderPause = true;
                    isRotate = true;
                }
                /*if (Input.GetMouseButton(0))
                {
                    CrushIceRoot.transform.Rotate(0, 0, rotateRate);
                }*/
            }


            if (Input.GetMouseButtonUp(0))
            {
                myRenderer.color = Color.white;
                PicCutterManager_Tilemap.CutterColliderPause = false;
                isRotate = false;
            }
        }

        if (isRotate)
        {
            CrushIceRoot.transform.Rotate(0, 0, rotateRate);
        }

    }
}
