using UnityEngine;

public class GameManager : GameEventHandler
{
    public static GameManager Instance { get; private set; }

    public bool isDebugLog = false;

    void Awake()
    {
        Instance = this;
    }

    public override void OnDestroy()
    {
        UnregisterGameEvent();
        base.OnDestroy();

        Instance = null;
    }

    public override void HandleGameEvent(GameEvent ge)
    {
        
    }
}