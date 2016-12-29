using UnityEngine;
using System.Collections;

public class Test : GameEventHandler
{
    void Awake()
    {
        RegisterGameEvent(eGameEventType.CHANGEGAMESTATE_IStateInfo);
    }

    void Start()
    {
        StateManager.Instance.SetState(this, eGameState.Play);
    }

    public override void OnDestroy()
    {
        UnregisterGameEvent();
        base.OnDestroy();
    }

    public override void HandleGameEvent(GameEvent ge)
    {
        switch(ge.event_type)
        {
            case eGameEventType.CHANGEGAMESTATE_IStateInfo:
                IStateInfo stateInfo = (IStateInfo)ge.ReadClass();
                break;
        }
    }
}
