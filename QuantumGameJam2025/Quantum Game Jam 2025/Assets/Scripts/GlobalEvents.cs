using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class GameEventArgs
{
    public bool wonGame;
}

[System.Serializable]
public class SubmitAnswerEventArgs
{
    public bool successful;
    public string answer;
    public DissolveObject currentTarget;
    public bool wasTriggerWord;
    public float coherenceIncrease;
    public string targetKnot;
}

[System.Serializable]
public class DesktopUIEventsArgs
{
    public FolderButton folderButton;
}

public static class GlobalEvents
{
    // Submit answer event
    public delegate void SubmitAnswerEvent(SubmitAnswerEventArgs eventArgs);
    public static SubmitAnswerEvent OnSubmittedAnswer;
    public static SubmitAnswerEvent OnSubmittedAnswerDone;
    public static SubmitAnswerEvent OnObjectComplete;
    public static SubmitAnswerEvent OnObjectFailed;
    public static void SendOnSubmitAnswer(SubmitAnswerEventArgs args)
    {
        OnSubmittedAnswer?.Invoke(args);
    }
    public static void SendOnSubmitAnswerDone(SubmitAnswerEventArgs args)
    {
        OnSubmittedAnswerDone?.Invoke(args);
    }
    public static void SendOnObjectComplete(SubmitAnswerEventArgs args)
    {
        OnObjectComplete?.Invoke(args);
    }
    public static void SendOnObjectFailed(SubmitAnswerEventArgs args)
    {
        OnObjectFailed?.Invoke(args);
    }

    // Desktop UI events
    public delegate void DesktopUIEvent(DesktopUIEventsArgs eventArgs);
    public static DesktopUIEvent OnClickedFolder;
    public static void SendOnClickedFolder(DesktopUIEventsArgs args)
    {
        OnClickedFolder?.Invoke(args);
    }
    // Game events
    public delegate void GameEvent(GameEventArgs eventArgs);
    public static GameEvent OnObjectivesComplete;
    public static void SendOnObjectivesComplete(GameEventArgs args)
    {
        OnObjectivesComplete?.Invoke(args);
    }

}
