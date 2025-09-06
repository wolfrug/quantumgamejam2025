using UnityEngine;
using InkEngine;
using System.Reflection.Emit;
using UnityEditor;
using UnityEngine.UI;
public class InkTextReader : InkText
{
    public string title_variable_name = "OBJECTIVE";
    public int title_variable_index = 2;

    public Image m_image;

    protected override void Awake()
    {
        base.Awake();
        GlobalEvents.OnObjectComplete += GlobalEvents_OnObjectComplete;
    }
    void OnDestroy()
    {
        GlobalEvents.OnObjectComplete -= GlobalEvents_OnObjectComplete;
    }

    void GlobalEvents_OnObjectComplete(SubmitAnswerEventArgs args)
    {
        if (args.targetKnot == m_targetKnot)
        {
            SetFinished();
        }
    }
    protected override void OnEnable()
    {
        InkDialogueLine finalLine = GetUnifiedDialogueLine();
        m_targetText.SetText(finalLine.GetVariable(title_variable_name).VariableArguments[title_variable_index]);
        if (finalLine.GetVariable(title_variable_name).VariableArguments.Count > 3)
        {
            m_image.enabled = true;
            m_image.sprite = m_storyData.m_defaultTextVariables.m_sprites.Find((x) => x.id == finalLine.GetVariable(title_variable_name).VariableArguments[3]).sprite;
        }
        string array = m_storyData.InkStory.variablesState[m_targetKnot + "_array"] as string;
        bool isFinished = int.Parse(InkArrays.GetStringByKey("completed", array)) > 0;
        if (isFinished)
        {
            SetFinished();
        }
    }
    public override void UpdateSelf()
    {
        InkDialogueLine finalLine = GetUnifiedDialogueLine();
        InvokeDialogueEvents(finalLine);
        m_targetText.SetText(finalLine.GetVariable(title_variable_name).VariableArguments[title_variable_index]);

    }

    void SetFinished()
    {
        m_targetText.color = Color.green;
    }

    InkDialogueLine GetUnifiedDialogueLine()
    {
        string finalString = "";
        InkDialogueLine finalLine = new InkDialogueLine();
        InkDialogueLine[] text = m_storyData.CreateStringArrayKnot(m_targetKnot, null, "stringTable");
        for (int i = 0; i < text.Length; i++)
        {
            InkDialogueLine line = text[i];
            finalString += line.displayText;
            if (i < text.Length)
            {
                finalString += m_lineBreak;
            }
            finalLine.inkVariables.AddRange(line.inkVariables);
            finalLine.inkTags.AddRange(line.inkTags);
        }
        finalLine.displayText = finalString;
        return finalLine;
    }
}
