using System.Collections;
using System.Collections.Generic;
using System.Linq;

using InkEngine;

using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DungeonComponentEvent : UnityEvent<InkDungeonComponent> { }

/// <summary>
/// This is a temporary component that interfaces with Ink to activate/deactivate things in dungeon mode.
/// It works by reading the Ink state and then running a simple Unity Action.
/// It is only intended to run it once at dungeon start, not dynamically during the dungeon (we have triggers for that)
/// </summary>
public class InkDungeonComponent : MonoBehaviour
{
    public InkStoryData m_storyData;
    [Tooltip("This is the list variable we are looking at.")]
    public string m_inkListVariable;
    [Tooltip("Required if we want to -set- a list variable")]
    public string m_originListVariable;
    [Tooltip("I.e. any of these, any of them, or none of them")]
    public ArgumentRequirements m_requirements;
    [Tooltip("And these are the possible states that activate it (or dont')")]
    public List<string> m_watchStates = new List<string> { };

    [Tooltip("If set to false, will only update self once on start")]
    public bool m_updateSelfOnEnable = false;

    public DungeonComponentEvent m_onSuccess;
    public DungeonComponentEvent m_onFailure;


    private void Start()
    {
        if (m_storyData == null)
        {
            m_storyData = Resources.LoadAll<InkStoryData>("InkStoryData")[0];
        }
        if (m_storyData.IsLoaded())
        {
            UpdateSelf();
        }
    }


    private void OnEnable()
    {
        if (m_updateSelfOnEnable)
        {
            if (m_storyData!=null && m_storyData.IsLoaded())
            {
                Invoke(nameof(UpdateSelf), 0.1f);
            }
        }
    }


    public void UpdateSelf()
    {
        var newList = m_storyData.InkStory.variablesState[m_inkListVariable] as Ink.Runtime.InkList;
        List<string> keyList = new List<string> { };
        if (newList != null && newList.Count > 0)
        {
            foreach (var item in newList)
            {
                keyList.Add(item.Key.itemName);
            }
            //Debug.Log("<color=red>Found the following entries in the list " + m_inkListVariable + "(" + string.Join("\n", keyList + ")</color>"));
            if (ArgumentMatch(keyList))
            {
                m_onSuccess.Invoke(this);
            }
            else
            {
                m_onFailure.Invoke(this);
            }
        }
        else
        {
            m_onFailure.Invoke(this);
        }
    }

    public void AddToList(string newEntry)
    {
        Ink.Runtime.InkList newList = new Ink.Runtime.InkList(m_originListVariable, m_storyData.InkStory);
        Ink.Runtime.InkList oldList = m_storyData.InkStory.variablesState[m_inkListVariable] as Ink.Runtime.InkList;
        foreach (var entry in oldList)
        {
            newList.AddItem(entry.Key);
        }
        newList.AddItem(newEntry);
        m_storyData.InkStory.variablesState[m_inkListVariable] = newList;
    }
    public void AddFirstEntryToSelf()
    {
        if (m_watchStates.FirstOrDefault()!=null)
        {
            AddToList(m_watchStates.FirstOrDefault());
        }
    }

    bool ArgumentMatch(List<string> arguments)
    {
        switch (m_requirements)
        {
            case ArgumentRequirements.ANY_OF:
                {
                    if (m_watchStates.FindAll((x) => arguments.Contains(x)).Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            case ArgumentRequirements.EXACTLY:
                {
                    if (m_watchStates.FindAll((x) => arguments.Contains(x)).Count == m_watchStates.Count)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            case ArgumentRequirements.NONE_OF:
                {
                    if (m_watchStates.FindAll((x) => arguments.Contains(x)).Count > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            case ArgumentRequirements.ALL: // this one isn't really useful
                {
                    return true;
                }
            default:
                {
                    return false;
                }
        }
    }
}