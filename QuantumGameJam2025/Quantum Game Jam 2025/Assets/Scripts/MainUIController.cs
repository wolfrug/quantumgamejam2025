using InkEngine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using InkEngine;
using System.Collections.Generic;
using System.IO;
using Unity.Collections;

public class MainUIController : MonoBehaviour
{

    public Animator m_mainGameplayPanelAnimator;
    public Animator m_chatboxAnimator;
    public DissolveObject m_textOnlyObject;
    public DissolveObject m_imageOnlyObject;
    public DissolveObject m_textAndImageObject;

    public TMP_InputField m_textInputField;
    public Button m_submitButton;
    public TextMeshProUGUI m_attemptsLeftText;
    public GameObject m_attemptParent;
    public TextMeshProUGUI m_titleText;

    public int m_maxAttempts = 10;
    public int m_attemptsMade = 0;

    private InkStoryData m_listenTarget;

    private bool chatboxIsUp = false;

    [SerializeField] private DissolveObject m_currentDissolveObject;

    void Awake()
    {
        m_submitButton.onClick.AddListener(() => OnClickSubmit());
        GlobalEvents.OnSubmittedAnswerDone += GlobalEvents_OnSubmittedAnswerDone;
        GlobalEvents.OnObjectComplete += GlobalEvents_OnObjectComplete;
        if (m_listenTarget == null)
        {
            m_listenTarget = Resources.LoadAll<InkStoryData>("InkStoryData")[0];
        }
        m_listenTarget.m_textFunctionFoundEvent.AddListener(OnClickedDissolveText);
        DeactivateAllDissolveObjects();
    }
    void OnDestroy()
    {
        GlobalEvents.OnSubmittedAnswerDone -= GlobalEvents_OnSubmittedAnswerDone;
        GlobalEvents.OnObjectComplete -= GlobalEvents_OnObjectComplete;
    }

    public void ClickedMinimize()
    {
        m_mainGameplayPanelAnimator.SetBool("isActive", false);
    }

    public void ToggleChatbox()
    {
        m_chatboxAnimator.SetBool("isActive", !chatboxIsUp);
        chatboxIsUp = !chatboxIsUp;
    }

    public void OnClickedDissolveText(InkDialogueLine line, InkTextVariable variable)
    {
        if (variable.variableName == "OBJECTIVE")
        {
            DissolveObject targetObject = m_textOnlyObject;
            string targetKnot = variable.VariableArguments[0];
            string type = variable.VariableArguments[1];
            string array = m_listenTarget.InkStory.variablesState[targetKnot + "_array"] as string;
            float difficulty = float.Parse(InkArrays.GetStringByKey("difficulty", array));
            int attemptsMade = int.Parse(InkArrays.GetStringByKey("attempts", array));
            bool isFinished = int.Parse(InkArrays.GetStringByKey("completed", array)) > 0;
            Debug.Log("Difficulty parsed: " + difficulty);
            string text = line.displayText;
            List<string> triggerWords = new List<string>(InkArrays.DeSerializeString(InkArrays.GetStringByKey("triggerWords", array)));
            List<string> usedTriggerWords = new List<string>(InkArrays.DeSerializeString(InkArrays.GetStringByKey("usedTriggerWords", array)));
            List<string> usedAnyWords = new List<string>(InkArrays.DeSerializeString(InkArrays.GetStringByKey("usedAllWords", array)));
            Sprite targetSprite = null;
            switch (type)
            {
                case "TEXT_ONLY":
                    {
                        targetObject = m_textOnlyObject;
                        break;
                    }
                case "IMAGE_ONLY":
                    {
                        targetObject = m_imageOnlyObject;
                        targetSprite = m_listenTarget.m_defaultTextVariables.m_sprites.Find((x) => x.id == variable.VariableArguments[3]).sprite;
                        break;
                    }
                case "TEXT_IMAGE":
                    {
                        targetObject = m_textAndImageObject;
                        targetSprite = m_listenTarget.m_defaultTextVariables.m_sprites.Find((x) => x.id == variable.VariableArguments[3]).sprite;
                        break;
                    }
            }
            m_attemptsMade = attemptsMade;
            m_mainGameplayPanelAnimator.SetBool("isActive", true);
            SetDissolveObject(targetObject, text, triggerWords, targetKnot, difficulty, usedTriggerWords, usedAnyWords, targetSprite);
            m_titleText.SetText(variable.VariableArguments[2]);
            if (isFinished)
            {
                m_attemptParent.SetActive(false);
                m_titleText.color = Color.green;
            }
            else
            {
                m_attemptParent.SetActive(true);
                m_titleText.color = Color.black;
            }
            UpdateAttempts();
        }
    }

    void OnClickSubmit()
    {
        if (m_currentDissolveObject != null && m_maxAttempts > m_attemptsMade && GetAnswer() != "")
        {
            GlobalEvents.SendOnSubmitAnswer(new SubmitAnswerEventArgs { successful = true, answer = GetAnswer(), currentTarget = m_currentDissolveObject });
        }
        else
        {
            GlobalEvents.SendOnSubmitAnswer(new SubmitAnswerEventArgs { successful = false, answer = GetAnswer(), currentTarget = m_currentDissolveObject });
        }
    }

    void GlobalEvents_OnSubmittedAnswerDone(SubmitAnswerEventArgs args)
    {
        if (args.currentTarget == m_currentDissolveObject && args.coherenceIncrease != 0f)
        {
            m_attemptsMade++;
            m_textInputField.text = "";
            UpdateInk();
            UpdateAttempts();
        }
    }
    void GlobalEvents_OnObjectComplete(SubmitAnswerEventArgs args)
    {
        if (args.currentTarget == m_currentDissolveObject)
        {
            string currentDictionary = m_listenTarget.InkStory.variablesState[m_currentDissolveObject.m_targetKnot + "_array"] as string;
            string updatedDictionary = InkArrayFunctions.AddStringDictionary("completed", "1", currentDictionary);
            m_listenTarget.InkStory.variablesState[m_currentDissolveObject.m_targetKnot + "_array"] = updatedDictionary;
            m_attemptParent.SetActive(false);
            m_titleText.color = Color.green;
            UpdateInk();
        }
    }

    void UpdateInk()
    {
        string currentDictionary = m_listenTarget.InkStory.variablesState[m_currentDissolveObject.m_targetKnot + "_array"] as string;
        string updatedDictionary = InkArrayFunctions.AddStringDictionary("attempts", m_attemptsMade.ToString(), currentDictionary);
        updatedDictionary = InkArrayFunctions.AddStringDictionary("difficulty", m_currentDissolveObject.m_setter.Location.ToString(), updatedDictionary);
        updatedDictionary = InkArrayFunctions.AddStringDictionary("usedTriggerWords", InkArrays.SerializeStrings(m_currentDissolveObject.m_usedTriggerWords), updatedDictionary);
        updatedDictionary = InkArrayFunctions.AddStringDictionary("usedAllWords", InkArrays.SerializeStrings(m_currentDissolveObject.m_usedAllWords), updatedDictionary);
        updatedDictionary = InkArrayFunctions.AddStringDictionary("triggerWords", InkArrays.SerializeStrings(m_currentDissolveObject.m_triggerWords), updatedDictionary);
        m_listenTarget.InkStory.variablesState[m_currentDissolveObject.m_targetKnot + "_array"] = updatedDictionary;
    }

    string GetAnswer()
    {
        return m_textInputField.text;
    }

    void UpdateAttempts()
    {
        m_attemptsLeftText.SetText(string.Format("{0}/{1}", m_attemptsMade, m_maxAttempts));
    }
    void DeactivateAllDissolveObjects()
    {
        m_textOnlyObject.gameObject.SetActive(false);
        m_textAndImageObject.gameObject.SetActive(false);
        m_imageOnlyObject.gameObject.SetActive(false);
    }

    public void SetDissolveObject(DissolveObject target, string text, List<string> triggerWords, string targetKnot, float location, List<string> usedTriggerWords, List<string> usedAnyWords, Sprite targetsprite)
    {
        DeactivateAllDissolveObjects();
        if (target != null)
        {
            target.gameObject.SetActive(true);
            target.Init(text, triggerWords, targetKnot, location, usedTriggerWords, usedAnyWords, targetsprite);
        }
        m_currentDissolveObject = target;
    }
}
