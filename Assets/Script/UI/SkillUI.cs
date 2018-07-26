using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// <Controller>
public class SkillUI : MonoBehaviour 
{

	public const string SELECT_CARD = "SELECT_CARD";

	Button m_Battle;
    Button m_Exit;
	Image m_CardIcon;
	List<SkillContext> m_SkillContexts = new List<SkillContext>(4);
	public List<ElementCard> m_ElementCards = new List<ElementCard>(30);


	public int m_DeckIndex = 0; // 牌組編號
	public int m_CardIndex = 0; // 選擇卡片Index.

	#region Awake
	void Awake()
	{
		FindChild();
		InitEvent();

	}

	void FindChild()
	{
		Transform ts = transform;
		m_Battle = ts.Find("Battle").GetComponent<Button>();
		m_Battle.onClick.AddListener(ClickBattle);
        m_Exit = ts.Find("Title/Exit").GetComponent<Button>();
        m_Exit.onClick.AddListener(ClickExit);


		Transform content = ts.Find("Card View/Viewport/Content");
		Transform elementCard = ts.Find("Card View/Viewport/Content/ElementCard");
		elementCard.gameObject.AddComponent<ElementCard>();

		m_ElementCards.Add(elementCard.GetComponent<ElementCard>());
		for(int i = 1; i < VerDef.DeckCardMax; i++){
			Transform elementCardTs = Transform.Instantiate(elementCard);
			elementCardTs.SetParent(content);

			m_ElementCards.Add(elementCardTs.GetComponent<ElementCard>());
		}
        /*
		for(int i = 0; i < 4; i++){
			Transform skillContext = ts.Find(string.Format("CardContext/SkillContext{0}",i+1));
			SkillContext skillcontext = new SkillContext(skillContext);
			m_SkillContexts.Add(skillcontext);
		}
        */
	}


	#endregion

	void Start()
	{
		//SetDeck();
	}

	void InitEvent()
	{
		EventHandler.Add<int>(SELECT_CARD, SelectCard);
	}

	void OnDestroy()
	{
		EventHandler.Remove<int>(SELECT_CARD, SelectCard);

        UIManager.m_Instance.RemoveUI(UITypeName.SkillUI);

	}



	#region SetDeck
	void SetDeck()
	{
		PlayerCard ourPlayerCard = DeckManager.Instance.GetPlayerCard(PlayerType.Player1, m_DeckIndex);
		for(int i = 0; i < VerDef.DeckCardMax; i++){
			Card card = ourPlayerCard.GetCard(i);
			if(card == null){
				DebugManager.LogError("[Error]DeckUI SetDeck no Card Index: " + i);
			}

			m_ElementCards[i].m_DeckIndex = m_DeckIndex;
			m_ElementCards[i].m_Index = i;
			m_ElementCards[i].m_ID = card.GetID();
		}
	}
	#endregion


	#region Button Event

	void ClickBattle()
	{
		DebugManager.Log("Click Battle.");

        //UIManager.m_Instance.Load(UITypeName.Menu);
		// SelectCard End Done.
		//EventHandler.Invoke(BattleManager.BATTLE_RUN);
		//EventHandler.Invoke<int>(BattleScene.BATTLE_SLECET, m_CardIndex);
	}

    void ClickExit()
    {
        DebugManager.Log("Click Exit.");

        Destroy(gameObject);
        //UIManager.m_Instance.Load(UITypeName.Menu);
        // SelectCard End Done.
        //EventHandler.Invoke(BattleManager.BATTLE_RUN);
        //EventHandler.Invoke<int>(BattleScene.BATTLE_SLECET, m_CardIndex);
    }
	#endregion

	#region Select Event
	public void SelectCard(int _Index)
	{
		m_CardIndex = _Index;
		DebugManager.Log(string.Format("CardIndex:{0}",m_CardIndex));
	}
	#endregion

}
