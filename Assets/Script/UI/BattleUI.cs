using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// <Controller>
public class BattleUI : MonoBehaviour 
{

	Button m_Skill1;
    Button m_Skill2;
    Button m_Skill3;


	#region Awake
	void Awake()
	{
		FindChild();
		InitEvent();

	}

	void FindChild()
	{
		Transform ts = transform;
        m_Skill1 = ts.Find("Skill1").GetComponent<Button>();
        m_Skill1.onClick.AddListener(ClickSkill1);
        m_Skill2 = ts.Find("Skill2").GetComponent<Button>();
        m_Skill2.onClick.AddListener(ClickSkill2);
        m_Skill3 = ts.Find("Skill3").GetComponent<Button>();
        m_Skill3.onClick.AddListener(ClickSkill3);

	}


	#endregion

	void InitEvent()
	{
		
	}

	void OnDestroy()
	{
        UIManager.m_Instance.RemoveUI(UITypeName.BattleUI);
	}

	#region Button Event
    void ClickSkill1()
	{
        DebugManager.Log("Click ClickSkill1.");
        EventHandler.Invoke(PlayerController.SKILL1);
	}

    void ClickSkill2()
	{
        DebugManager.Log("Click ClickSkill2.");
        EventHandler.Invoke(PlayerController.SKILL2);
	}

    void ClickSkill3()
    {
        DebugManager.Log("Click ClickSkill3.");
        EventHandler.Invoke(PlayerController.SKILL3);
    }
	#endregion
}
