using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{

    public DissolveObject m_textOnlyObject;
    public DissolveObject m_imageOnlyObject;
    public DissolveObject m_textAndImageObject;

    public TMP_InputField m_textInputField;
    public Button m_submitButton;
    public TextMeshProUGUI m_attemptsLeftText;

    public int m_maxAttempts = 10;
    public int m_attemptsMade = 0;

    [SerializeField] private DissolveObject m_currentDissolveObject;

    void Awake()
    {
        m_submitButton.onClick.AddListener(() => OnClickSubmit());
    }

    void OnClickSubmit()
    {
        if (m_maxAttempts > m_attemptsMade && GetAnswer() != "")
        {
            GlobalEvents.SendOnSubmitAnswer(new SubmitAnswerEventArgs { successful = true, answer = GetAnswer(), currentTarget = m_currentDissolveObject });
            m_attemptsMade++;
            m_textInputField.text = "";
            UpdateAttempts();
        }
        else
        {
            GlobalEvents.SendOnSubmitAnswer(new SubmitAnswerEventArgs { successful = false, answer = GetAnswer(), currentTarget = m_currentDissolveObject });
        }
    }

    string GetAnswer()
    {
        return m_textInputField.text;
    }

    void UpdateAttempts()
    {
        m_attemptsLeftText.SetText(string.Format("{0}/{1}", m_attemptsMade, m_maxAttempts));
    }
}
