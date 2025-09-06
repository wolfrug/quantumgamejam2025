using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DissolveObject : MonoBehaviour
{
    public TextMeshProUGUI m_text;
    public Image m_image;
    public DissolveSet m_setter;


    public bool Active
    {
        get
        {
            return gameObject.activeSelf;
        }
        set
        {
            gameObject.SetActive(value);
        }
    }
}
