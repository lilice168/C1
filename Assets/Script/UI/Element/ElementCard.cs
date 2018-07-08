using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementCard : MonoBehaviour {

	Image m_Icon;
	Toggle m_Toggle;

	public int m_DeckIndex;// DeckIndex
	public int m_Index;// Index
	public int m_ID; // 生物表ID


	#region Awake
	void Awake()
	{
		FindChild();
	}

	void FindChild()
	{
		Transform ts = transform;

		m_Icon = ts.GetComponent<Image>();

		m_Toggle = ts.GetComponent<Toggle>();
		m_Toggle.onValueChanged.AddListener(Select);
	}

	#endregion

	void Start()
	{
		SetData();
	}


	void SetData()
	{
		SpriteManager.m_Instance.LoadIcon(m_Icon, "cardbg", "_0");
	}


	#region Event
	void Select(bool _IsSelect)
	{
		if(_IsSelect){
			EventHandler.Invoke<int>(DeckUI.SELECT_CARD, m_Index);
		}
	}
	#endregion
}
