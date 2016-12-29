using UnityEngine;
using System.Collections;

public class StateManager : Singleton<StateManager>
{
    #region 변수
    private StateInfo mainInfo = new StateInfo();
    #endregion

    #region 프로퍼티
    #endregion
        
    #region GameEventHandler 상속
    public override void OnDestroy()
    {
        base.OnDestroy();

        mainInfo = null;
    }

    public override void HandleGameEvent(GameEvent ge)
    {
        switch (ge.event_type)
        { 
            default:
                break;
        }
    }
    #endregion

    public void SetState(object sender, eGameState state)
    {
        Debug.Log(string.Format("sender is {0}. excuted {1}", sender.GetType().Name, "SetState"));

        mainInfo.beforeState = mainInfo.currentState;
        mainInfo.currentState = state;

        GameUtil.SendGameEvent_Class<IStateInfo>(eGameEventType.CHANGEGAMESTATE_IStateInfo, (IStateInfo)mainInfo);
    }
}
