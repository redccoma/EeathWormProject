/// <summary>
/// enum 이름 규칙
/// enum이름 혹은 enum이름_read클래스이름
/// </summary>
public enum eGameEventType
{
    INVALIDTYPE,    
    CHANGEGAMESTATE_IStateInfo,

    INPUT_UP,
    INPUT_DOWN,
    INPUT_LEFT,
    INPUT_RIGHT,
}

public enum eGameState
{
    Init,
    Play,
    End
}