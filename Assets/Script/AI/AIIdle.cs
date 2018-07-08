using UnityEngine;

public class AIIdle : AI 
{

    float m_MoveTime;

    public AIIdle(AIIndex index)
    {
        m_AIState = AIState.EIdle;
        m_AIIndex = index;
        m_MoveTime = 0;
    }

    #region 做動作
    public override void Do()
    {
        if (m_AIIndex == AIIndex.EAction1) {
            Action1();
        }
    
    }

    void Action1()
    {
        MoveData moveData = new MoveData();

        if (m_MoveTime < 5) {
            moveData.m_Move = new Vector3(-0.1f, 0.0f, 0.0f);
            moveData.m_ToRight = false;
        }else if(m_MoveTime >= 5 && m_MoveTime < 10){
            moveData.m_Move = new Vector3(0.1f, 0.0f, 0.0f);
            moveData.m_ToRight = true;
        }else{
            m_MoveTime = 0;
            return;
        }


    
        m_MoveTime += Time.deltaTime;
        EventHandler.Invoke<MoveData>(MonsterController.MOVE, moveData);
    }
    #endregion

    public override void Update()
    {
        Do();
    }

}
