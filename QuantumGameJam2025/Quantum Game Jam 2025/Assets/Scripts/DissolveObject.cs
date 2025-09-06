using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
public class DissolveObject : MonoBehaviour
{
    public TextMeshProUGUI m_text;
    public Image m_image;
    public DissolveSet m_setter;
    public List<string> m_triggerWords = new List<string> { };
    public List<string> m_usedTriggerWords = new List<string> { };
    public List<string> m_allWords = new List<string> { };
    public List<string> m_usedAllWords = new List<string> { };
    public string m_targetKnot;

    public Color m_targetWordColor = Color.blue;
    public Color m_anyWordColor = Color.green;
    public Color m_nocolor = Color.black;

    void Awake()
    {
        GlobalEvents.OnSubmittedAnswer += GlobalEvents_OnSubmittedAnswer;
    }
    void OnDestroy()
    {
        GlobalEvents.OnSubmittedAnswer -= GlobalEvents_OnSubmittedAnswer;
    }

    public void Init(string text, List<string> triggerWords, string targetKnot, float currentLocation, List<string> usedTriggerWords, List<string> usedAllWords, Sprite image = null)
    {
        m_text.SetText(text);
        m_setter.SetLocation(currentLocation);
        m_triggerWords = triggerWords;
        m_usedTriggerWords = usedTriggerWords;
        m_usedAllWords = usedAllWords;
        m_targetKnot = targetKnot;
        if (image != null)
        {
            m_image.enabled = true;
            m_image.sprite = image;
        }
        else
        {
            m_image.enabled = false;
        }
        // \w+ = one or more word characters (letters, digits, underscore)
        // If you only want letters, use [A-Za-z]+ instead.
        MatchCollection matches = Regex.Matches(m_text.text, @"\w+");

        m_allWords.Clear();
        foreach (Match match in matches)
        {
            m_allWords.Add(match.Value.ToLower()); // lowercase for case-insensitivity
        }

        foreach (string regularWord in m_usedAllWords)
        {
            HighlightWord(regularWord, m_anyWordColor);
        }
        // So it overrides the green with blue ;)
        foreach (string triggerWord in m_usedTriggerWords)
        {
            HighlightWord(triggerWord, m_targetWordColor);
        }

    }

    void GlobalEvents_OnSubmittedAnswer(SubmitAnswerEventArgs args)
    {
        if (args.successful && args.currentTarget == this)
        {
            float successRate = CalculateSuccessRate(args.answer);
            Debug.Log("Received answer " + args.answer + " which gained a success rate of " + successRate);
            if (m_setter.Location < 0.01f || m_usedTriggerWords.Count > m_triggerWords.Count * 2)
            {
                FinishObject();
            }
        }
    }

    void FinishObject()
    {
        m_allWords.AddRange(m_usedAllWords);
        foreach (string regularWord in m_allWords)
        {
            HighlightWord(regularWord, m_nocolor);
        }
        m_allWords.Clear();
        m_usedAllWords.Clear();
        foreach (string triggerWord in m_triggerWords)
        {
            GlobalEvents.SendOnSubmitAnswerDone(new SubmitAnswerEventArgs { successful = true, answer = triggerWord, currentTarget = this, wasTriggerWord = false, coherenceIncrease = 1f });
            HighlightWord(triggerWord, m_targetWordColor);
            m_setter.Location = 0f;
            m_usedTriggerWords.Add(triggerWord);
        }
        foreach (string triggerWord in m_usedTriggerWords)
        {
            HighlightWord(triggerWord, m_targetWordColor);
        }
        m_triggerWords.Clear();
        GlobalEvents.SendOnObjectComplete(new SubmitAnswerEventArgs { currentTarget = this, targetKnot = m_targetKnot });
    }

    float CalculateSuccessRate(string submittedAnswer)
    {
        float returnValue = UnityEngine.Random.Range(0.05f, 0.1f);
        if (m_allWords.Contains(submittedAnswer))
        {
            returnValue = -UnityEngine.Random.Range(0.01f, 0.05f);
            m_allWords.RemoveAll((x) => x == submittedAnswer);
            m_usedAllWords.Add(submittedAnswer);
            if (m_triggerWords.Contains(submittedAnswer))
            {
                returnValue -= 0.1f;
                m_triggerWords.Remove(submittedAnswer);
                m_usedTriggerWords.Add(submittedAnswer);
                m_setter.Location += returnValue;
                GlobalEvents.SendOnSubmitAnswerDone(new SubmitAnswerEventArgs { successful = true, answer = submittedAnswer, currentTarget = this, wasTriggerWord = true, coherenceIncrease = returnValue });
                HighlightWord(submittedAnswer, m_targetWordColor);
            }
            else
            {
                m_setter.Location += returnValue;
                GlobalEvents.SendOnSubmitAnswerDone(new SubmitAnswerEventArgs { successful = true, answer = submittedAnswer, currentTarget = this, wasTriggerWord = false, coherenceIncrease = returnValue });
                HighlightWord(submittedAnswer, m_anyWordColor);
            }

        }
        else
        {
            if (ContainsWord(submittedAnswer))
            {
                returnValue = 0f;
            }
            m_setter.Location += returnValue;
            GlobalEvents.SendOnSubmitAnswerDone(new SubmitAnswerEventArgs { successful = false, answer = submittedAnswer, currentTarget = this, wasTriggerWord = false, coherenceIncrease = returnValue });
        }
        return returnValue;
    }

    public bool ContainsWord(string wordToFind)
    {

        bool containsWholeWord = Regex.IsMatch(
            m_text.text,
            $@"\b{Regex.Escape(wordToFind)}\b",
            RegexOptions.IgnoreCase
        );

        return containsWholeWord;
    }

    void HighlightWord(string word, Color color)
    {
        if (m_text == null || string.IsNullOrEmpty(word)) return;

        string hexColor = ColorUtility.ToHtmlStringRGB(color);

        // Regex with word boundaries, case-insensitive
        string pattern = $@"\b{Regex.Escape(word)}\b";
        string replacement = $"<color=#{hexColor}>$0</color>";

        m_text.text = Regex.Replace(
            m_text.text,
            pattern,
            replacement,
            RegexOptions.IgnoreCase
        );
    }


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
