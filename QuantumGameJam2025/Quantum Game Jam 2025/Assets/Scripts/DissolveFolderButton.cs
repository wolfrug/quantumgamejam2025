using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DissolveFolderButton : FolderButton
{
    public DissolveSet m_dissolveSetter;
    public float m_startValue = 1;

    [Tooltip("How much the value changes per completed object")]
    public float m_valuePerCompleted = -0.2f;

    [Tooltip("Positive to increase dissolution (can make it impossible to complete) or negative to still have an effect")]
    public float m_valuePerFailed = -0.1f;
    public List<string> m_requiredCompletedObjects = new List<string> { };

    private bool m_visible = false;
    protected override void Start()
    {
        base.Start();
        m_dissolveSetter.Location = m_startValue;
        if (m_dissolveSetter.Location > 0f)
        {
            Unlock(false);
        }
        GlobalEvents.OnObjectComplete += GlobalEvents_OnObjectComplete;
        GlobalEvents.OnObjectComplete += GlobalEvents_OnObjectFailed;
    }
    protected override void OnDestroy()
    {
        GlobalEvents.OnObjectComplete -= GlobalEvents_OnObjectComplete;
        GlobalEvents.OnObjectComplete -= GlobalEvents_OnObjectFailed;
    }

    void GlobalEvents_OnObjectComplete(SubmitAnswerEventArgs args)
    {
        if (!m_visible)
        {
            if (m_requiredCompletedObjects.Contains(args.targetKnot))
            {
                m_dissolveSetter.Location += m_valuePerCompleted;
            }
            if (Mathf.Approximately(m_dissolveSetter.Location, 0f) || m_dissolveSetter.Location < 0f)
            {
                Unlock(true);
            }
        }
    }
    void GlobalEvents_OnObjectFailed(SubmitAnswerEventArgs args)
    {
        if (!m_visible)
        {
            if (m_requiredCompletedObjects.Contains(args.targetKnot))
            {
                m_dissolveSetter.Location += m_valuePerFailed;
            }
            if (Mathf.Approximately(m_dissolveSetter.Location, 0f) || m_dissolveSetter.Location < 0f)
            {
                Unlock(true);
            }
        }
    }

    public void Unlock(bool unlock)
    {
        m_visible = unlock;
        m_selfButton.interactable = unlock;
        if (!Locked && unlock)
        {
            GlobalEvents.SendOnUnlockedFolder(new DesktopUIEventsArgs { folderButton = this });
            m_dissolveSetter.Location = 0f;
        }
    }
}
