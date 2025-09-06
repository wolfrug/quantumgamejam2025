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
}

public static class GlobalEvents
{
    // Submit answer event
    public delegate void SubmitAnswerEvent(SubmitAnswerEventArgs eventArgs);
    public static SubmitAnswerEvent OnSubmittedAnswer;
    public static void SendOnSubmitAnswer(SubmitAnswerEventArgs args)
    {
        OnSubmittedAnswer?.Invoke(args);
    }

    // Game events
    public delegate void GameEvent(GameEventArgs eventArgs);
    public static GameEvent OnObjectivesComplete;
    public static void SendOnObjectivesComplete(GameEventArgs args)
    {
        OnObjectivesComplete?.Invoke(args);
    }

}
