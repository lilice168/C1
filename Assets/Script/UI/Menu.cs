using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// <Controller>
public class Menu : MonoBehaviour 
{
	Button m_Books;
	Button m_Story;
	Button m_Figure;

	void Awake()
	{
		Transform ts = transform;
		m_Books = ts.Find("Books").GetComponent<Button>();
		m_Books.onClick.AddListener(ClickBooks);
		m_Story = ts.Find("Story").GetComponent<Button>();
		m_Story.onClick.AddListener(ClickStory);
		m_Figure = ts.Find("Figure").GetComponent<Button>();
		m_Figure.onClick.AddListener(ClickFigure);
	}

	void Start () {
		
	}

	void OnDestroy()
	{
		UIManager.m_Instance.RemoveUI(UITypeName.Menu);
	}


    // 選擇技能.
	void ClickBooks()
	{
		DebugManager.Log("Click Books.");

        UIManager.m_Instance.Load(UITypeName.SkillUI);
	}
    // 選任務.
	void ClickStory()
	{
		DebugManager.Log("Click Story.");

        // TODO: 任務介面.

        // 暫用直接進任務關卡
        int missionIndex = 1;
        BattleManager.m_Instance.InitScene(missionIndex);

	}

	void ClickFigure()
	{
		DebugManager.Log("Click Figure.");
	}
}
