using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Ink.Runtime;
using UnityEngine;

namespace InkEngine
{

    public class InkStoryStateManager : MonoBehaviour
    {
        public InkStoryData m_storyObject;
        public bool m_startOnInit = true;
        public SimpleInkWriter m_mainWriter;

        public string m_startKnot = "start";

        public string m_inkListVariable;
        [Tooltip("Required if we want to -set- a list variable")]
        public string m_originListVariable;

        void Awake()
        {
            if (m_startOnInit)
            {
                InitStory();
            }
            GlobalEvents.OnObjectComplete += GlobalEvent_OnObjectComplete;
            GlobalEvents.OnObjectFailed += GlobalEvent_OnObjectFailed;
        }
        void OnDestroy()
        {
            GlobalEvents.OnObjectComplete -= GlobalEvent_OnObjectComplete;
            GlobalEvents.OnObjectFailed -= GlobalEvent_OnObjectFailed;
        }

        void Start()
        {
            if (m_startKnot != "")
            {
                m_mainWriter.PlayKnot(m_startKnot);
            }
        }

        void GlobalEvent_OnObjectComplete(SubmitAnswerEventArgs args)
        {
            m_mainWriter.PlayKnot(args.targetKnot + ".win");
        }
        void GlobalEvent_OnObjectFailed(SubmitAnswerEventArgs args)
        {
            m_mainWriter.PlayKnot(args.targetKnot + ".lose");
        }
        public void AddToList(string newEntry)
        {
            Ink.Runtime.InkList newList = new Ink.Runtime.InkList(m_originListVariable, m_storyObject.InkStory);
            Ink.Runtime.InkList oldList = m_storyObject.InkStory.variablesState[m_inkListVariable] as Ink.Runtime.InkList;
            foreach (var entry in oldList)
            {
                newList.AddItem(entry.Key);
            }
            newList.AddItem(newEntry);
            m_storyObject.InkStory.variablesState[m_inkListVariable] = newList;
        }
        public void SaveStory()
        {
            m_storyObject.SaveStory();
        }
        public void InitStory()
        { // Init -or- load
            m_storyObject.InitStory();
        }
        public bool SavedStory
        {
            get
            {
                return m_storyObject.SavedStory;
            }
        }
        public bool IsLoaded()
        {
            return m_storyObject.IsLoaded();
        }

    }
}