public abstract class AI 
{

    public enum AIState
    {
        ENone = -1,
        EIdle = 0,
        EAttack,
    }

    public enum AIIndex
    {
        EAction1 = 0,
        EAction2,
    }


    public AIState m_AIState;
    public AIIndex m_AIIndex;

    public abstract void Do();
    public abstract void Update();

}
