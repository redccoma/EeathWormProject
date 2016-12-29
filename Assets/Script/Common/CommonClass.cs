#region StateInfo
public interface IStateInfo
{
    eGameState beforeState { get; }
    eGameState currentState { get; }
}
[System.Serializable]
public class StateInfo : IStateInfo
{
    public eGameState beforeState { get; set; }
    public eGameState currentState { get; set; }

    public StateInfo()
    {
        Init();
    }

    public void Init()
    {
        beforeState = eGameState.Init;
        currentState = eGameState.Init;
    }
}
#endregion