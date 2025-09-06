using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class DissolveSet : MonoBehaviour
{
    [Range(0f, 1f)]
    public float location = 0f; // 0 = fully visible, 1 = fully dissolved

    private float lerpLocation = 0f;

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
        lerpLocation = location;
        mat = uiImage.materialForRendering;
        lerpLocation = Mathf.Lerp(lerpLocation, location, Time.deltaTime * 2f);
        mat.SetFloat("_FadeAmount", lerpLocation);
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
            if (location > lerpLocation || location < lerpLocation)
            {
                mat = uiImage.materialForRendering;
                lerpLocation = Mathf.Lerp(lerpLocation, location, Time.deltaTime * 2f);
                mat.SetFloat("_FadeAmount", lerpLocation);
                Debug.Log("Slerping location from " + lerpLocation + " to " + location);
                //MaskUtilities.NotifyStencilStateChanged(GetComponent<Mask>());
            }
        }
        else
        {
            Debug.LogError("Mat is null!");
            mat = uiImage.materialForRendering;
        }
    }
}