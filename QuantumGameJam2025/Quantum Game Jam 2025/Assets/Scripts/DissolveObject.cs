using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

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

    void Awake()
    {
        GlobalEvents.OnSubmittedAnswer += GlobalEvents_OnSubmittedAnswer;
    }
    void OnDestroy()
    {
        GlobalEvents.OnSubmittedAnswer -= GlobalEvents_OnSubmittedAnswer;
    }

    public void Init(string text, List<string> triggerWords, string targetKnot, float currentLocation, List<string> usedTriggerWords, List<string> usedAllWords)
    {
        m_text.SetText(text);
        m_setter.SetLocation(currentLocation);
        m_triggerWords = triggerWords;
        m_usedTriggerWords = usedTriggerWords;
        m_usedAllWords = usedAllWords;
        m_targetKnot = targetKnot;
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
            HighlightWord(regularWord, Color.green);
        }
        // So it overrides the green with blue ;)
        foreach (string triggerWord in m_usedTriggerWords)
        {
            HighlightWord(triggerWord, Color.blue);
        }

    }

    void GlobalEvents_OnSubmittedAnswer(SubmitAnswerEventArgs args)
    {
        if (args.successful && args.currentTarget == this)
        {
            float successRate = CalculateSuccessRate(args.answer);

            Debug.Log("Received answer " + args.answer + " which gained a success rate of " + successRate);
        }
    }

    float CalculateSuccessRate(string submittedAnswer)
    {
        float returnValue = Random.Range(0.05f, 0.1f);
        if (m_allWords.Contains(submittedAnswer))
        {
            returnValue = -Random.Range(0.01f, 0.05f);
            m_allWords.RemoveAll((x) => x == submittedAnswer);
            m_usedAllWords.Add(submittedAnswer);
            if (m_triggerWords.Contains(submittedAnswer))
            {
                returnValue -= 0.1f;
                m_triggerWords.Remove(submittedAnswer);
                m_usedTriggerWords.Add(submittedAnswer);
                m_setter.Location += returnValue;
                GlobalEvents.SendOnSubmitAnswerDone(new SubmitAnswerEventArgs { successful = true, answer = submittedAnswer, currentTarget = this, wasTriggerWord = true, coherenceIncrease = returnValue });
                HighlightWord(submittedAnswer, Color.blue);
            }
            else
            {
                m_setter.Location += returnValue;
                GlobalEvents.SendOnSubmitAnswerDone(new SubmitAnswerEventArgs { successful = true, answer = submittedAnswer, currentTarget = this, wasTriggerWord = false, coherenceIncrease = returnValue });
                HighlightWord(submittedAnswer, Color.green);
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
