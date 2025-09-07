using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FolderButton : MonoBehaviour
{
    public string m_folderName = "Folder #1";
    public string m_folderID;
    public Button m_selfButton;
    public string m_password = "";
    public TextMeshProUGUI m_folderNameText;
    public GameObject m_lockedSprite;
    public GameObject m_unlockedSprite;
    public Animator m_folderPanelAnimator;
    public TMP_InputField m_passwordInputField;

    private bool m_locked;
    public bool Locked
    {
        get
        {
            return m_locked;
        }
        set
        {
            m_locked = value;
            m_lockedSprite.SetActive(value);
            m_unlockedSprite.SetActive(!value);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        m_folderNameText.SetText(m_folderName);
        m_selfButton.onClick.AddListener(OnClickedFolder);
        m_passwordInputField.onEndEdit.AddListener(OnInputEnded);
        m_passwordInputField.onDeselect.AddListener(OnDeselectInputField);
        Locked = m_password != "";
        ShowPasswordInput(false);
    }

    protected virtual void OnDestroy()
    {

    }

    public virtual void OpenFolder(bool open)
    {
        if (!Locked)
        {
            m_folderPanelAnimator.SetBool("isActive", open);
        }
    }

    public virtual void ShowPasswordInput(bool show)
    {
        m_passwordInputField.gameObject.SetActive(show);
    }

    public virtual bool ShowingPasswordInput
    {
        get
        {
            return m_passwordInputField.gameObject.activeSelf;
        }
    }

    protected virtual void OnClickedFolder()
    {
        GlobalEvents.SendOnClickedFolder(new DesktopUIEventsArgs { folderButton = this });
        if (Locked)
        {
            ShowPasswordInput(!ShowingPasswordInput);
            if (ShowingPasswordInput)
            {
                m_passwordInputField.Select();
            }
        }
        else
        {
            OpenFolder(true);
        }
    }

    protected virtual void OnDeselectInputField(string input)
    {
        m_passwordInputField.text = "";
        ShowPasswordInput(false);
    }

    protected virtual void OnInputEnded(string input)
    {
        if (input == m_password)
        {
            Locked = false;
            ShowPasswordInput(false);
            OpenFolder(true);
            GlobalEvents.SendOnUnlockedFolder(new DesktopUIEventsArgs { folderButton = this });
        }
        else
        {
            m_passwordInputField.text = "";
        }
    }
}
