using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class DissolveSet : MonoBehaviour
{
    [Range(0f, 1f)]
    public float location = 0f; // 0 = fully visible, 1 = fully dissolved

    public List<Texture2D> noiseTextures = new List<Texture2D> { };

    Material mat;
    Image uiImage;

    void Awake()
    {
        uiImage = GetComponent<Image>();
        uiImage.material = new Material(uiImage.material);
        mat = uiImage.materialForRendering;
        mat.SetTexture("_FadeTex", noiseTextures[Random.Range(0, noiseTextures.Count)]);
        mat.EnableKeyword("_FADE_AMOUNT");
    }

    public void SetLocation(float set)
    {
        location = Mathf.Clamp(set, 0f, 1f);
    }
    public float Location
    {
        set
        {
            location = Mathf.Clamp(value, 0f, 1f);
        }
        get
        {
            return location;
        }
    }


    void LateUpdate()
    {
        if (mat != null)
        {
            mat = uiImage.materialForRendering;
            mat.SetFloat("_FadeAmount", location);
            //MaskUtilities.NotifyStencilStateChanged(GetComponent<Mask>());
        }
    }
}