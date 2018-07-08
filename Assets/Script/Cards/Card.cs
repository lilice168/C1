using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 卡片屬性<DATA>
[System.Serializable]
public class Card  
{
	
	public int m_ID;
	public int m_Hp;
	public int m_Atk;
	public bool m_IsActive;


	public Card()
	{
		m_ID = 0;
		m_Hp = 0;
		m_Atk = 0;
		m_IsActive = true;
	}

	public Card(BioQuery _Data)
	{
		Clear();
		if(_Data == null)
			return;

		m_ID = _Data.m_ID;
		m_Hp = _Data.m_Hp;
		m_Atk = _Data.m_Atk;
		m_IsActive = true;
	}

	void Clear()
	{
		m_ID = 0;
		m_Hp = 0;
		m_Atk = 0;
		m_IsActive = true;
	}


	public int GetID(){return m_ID;}
}
