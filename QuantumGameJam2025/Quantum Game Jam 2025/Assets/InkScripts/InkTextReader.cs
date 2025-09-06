using UnityEngine;
using InkEngine;
public class InkTextReader : InkText
{
    public string title_variable_name = "OBJECTIVE";
    public int title_variable_index = 2;

    protected override void OnEnable()
    {
        InkDialogueLine finalLine = GetUnifiedDialogueLine();
        m_targetText.SetText(finalLine.GetVariable(title_variable_name).VariableArguments[title_variable_index]);
    }
    public override void UpdateSelf()
    {
        InkDialogueLine finalLine = GetUnifiedDialogueLine();
        InvokeDialogueEvents(finalLine);
        m_targetText.SetText(finalLine.GetVariable(title_variable_name).VariableArguments[title_variable_index]);
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
