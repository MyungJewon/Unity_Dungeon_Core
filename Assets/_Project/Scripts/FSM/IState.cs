namespace  DungeonCore.FSM
{
    public interface IState
    {
        void Enter();
        void Tick();
        void Exit();
    }
}