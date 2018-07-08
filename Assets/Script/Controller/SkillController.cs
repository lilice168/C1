using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SkillData
{
    public Transform m_Attacker;
    public Transform m_Target;
    public int m_AttackerFace; // 1 :right, -1 : left
    public int m_Damage;
   
  
    // 技能方向
    public Vector3 GetMove()
    {
        return new Vector3(m_AttackerFace * 0.5f, 0.0f, 0.0f);
    }

}


public class SkillController : MonoBehaviour 
{
    SkillData m_SKillData;

    public void Init(Transform attacker, Transform target = null)
    {
        // 初始化技能資料.
        m_SKillData = new SkillData();
        m_SKillData.m_Attacker = attacker;
        m_SKillData.m_Target = target;
        m_SKillData.m_Damage = 10;

        // 計算技能面向.
        float rorateY = attacker.eulerAngles.y;
        m_SKillData.m_AttackerFace = (rorateY == 0) ? 1 : -1;

        //設定技能位置.
        InitSkillPostion();
    }

    void InitSkillPostion()
    {
        // 設定起始位置.
        if (m_SKillData.m_AttackerFace == -1)
        { // 向左轉.
            transform.rotation = Quaternion.Euler(0f, -180f, 0f);
        }
        else
        { // 向右轉.
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        transform.position = m_SKillData.m_Attacker.position;
    }

	void FixedUpdate()
	{
        transform.position += m_SKillData.GetMove();
	}



    #region 碰撞區塊
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == VerDef.Tag_Monster)
        {
            DebugManager.Log("Collider Monster");
            Destroy(gameObject);

        }
    }
    #endregion
}
