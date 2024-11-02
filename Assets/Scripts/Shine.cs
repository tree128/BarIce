using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shine : MonoBehaviour
{
    public Material ShineMaterial;
    public Texture TargetTexture;
    public bool IsShine;
    public StageManager StageManager;
    public StageManager.Stage BelongStage;
    public UIManager UIManager;

    private SpriteRenderer myRenderer;
    [ColorUsage(false, true)]private Color emissionColor = new Color(1f, 1f, 1f);
    
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myRenderer.material = ShineMaterial;
        myRenderer.material.SetTexture("_EmissionMap", TargetTexture);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsShine && StageManager.CurrentStage == BelongStage && !UIManager.GameOverImage.enabled)
        {
            float t = Mathf.Abs(Mathf.Sin(Time.time*1.2f));
            emissionColor = new Color(0.7f + t, 0.7f+t, 0.7f+t);
            myRenderer.material.SetColor("_EmissionColor", emissionColor);
        }
        else
        {
            myRenderer.material.SetColor("_EmissionColor", new Color(1f, 1f, 1f));
        }
    }

    public void Set_IsShine(bool ShineBool)
    {
        IsShine = ShineBool;
    }

}
