using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
	Player1,
	Player2,
}

public sealed class PlayerCard
{
	Card[] m_Card; // 卡牌

	public PlayerCard()
	{
		m_Card  = new Card[VerDef.DeckCardMax];
	}

	public void SetCard(int _Index, Card _Card){
		if(_Index < 0 || _Index >= VerDef.DeckCardMax)
			return;

		m_Card[_Index] = _Card;
	}

	public Card GetCard(int _Index){
		if(_Index < 0 || _Index >= VerDef.DeckCardMax)
			return null;

		return m_Card[_Index];
	}
}

// <Manager>
public class DeckManager : Singleton<DeckManager> 
{
	// 玩家牌庫.
	Dictionary<PlayerType, Dictionary<int, PlayerCard> > m_PayerCards = new Dictionary<PlayerType, Dictionary<int, PlayerCard> >();
	// 設定戰鬥中使用的牌庫Index.
	Dictionary<PlayerType, int> m_BattleIndexs = new Dictionary<PlayerType, int>();

	public void SetPlayerCard(PlayerType _PlayerType, int _Index, PlayerCard _PlayerCard)
	{
		if(m_PayerCards.ContainsKey(_PlayerType)){
			if(m_PayerCards[_PlayerType].ContainsKey(_Index)){
				m_PayerCards[_PlayerType][_Index] = _PlayerCard;
			}else{
				m_PayerCards[_PlayerType].Add(_Index, _PlayerCard);
			}
		}else{
			Dictionary<int, PlayerCard> cards = new Dictionary<int, PlayerCard>();
			cards.Add(_Index, _PlayerCard);
			m_PayerCards.Add(_PlayerType, cards);
		}
	}

	public PlayerCard GetPlayerCard(PlayerType _PlayerType, int _Index)
	{
		if(m_PayerCards.ContainsKey(_PlayerType)){
			if(m_PayerCards[_PlayerType].ContainsKey(_Index)){
				return m_PayerCards[_PlayerType][_Index];
			}
		}
		return null;
	}


	#region Battle Card
	public void SetBattleIndex(PlayerType _PlayerType, int _Index)
	{
		if(m_BattleIndexs.ContainsKey(_PlayerType)){
			m_BattleIndexs[_PlayerType] = _Index;
		}else{
			m_BattleIndexs.Add(_PlayerType, _Index);
		}
	}

	Card card = null; // 配合GetBattleCard做取得card資料動作.
	PlayerCard player_card = null;  // 配合GetBattleCard做取得playercard資料動作.
	public Card GetBattleCard(PlayerType _PlayerType, int _CardIndex)
	{
		int index = 0;
		if(m_BattleIndexs.TryGetValue(_PlayerType, out index)){
			if(m_PayerCards[_PlayerType].ContainsKey(index)){
				player_card = GetPlayerCard(_PlayerType, index);
				if(player_card != null){
					card = player_card.GetCard(_CardIndex);
					if(card != null){
						BioTable table = DBManager.Instance.GetBioTable();
						BioQuery data = table.GetQueryRowByID(card.m_ID);
						if(data != null){
							return new Card(data);
						}
					}
				}
			}
		}
		return null;
	}
	#endregion



	#region Test code
	public void TestSetCards()
	{
		PlayerCard playerCard = new PlayerCard(); 

		BioTable table = DBManager.Instance.GetBioTable();
		for(int index = 0; index <VerDef.DeckCardMax; index++){
			int bioID = index + 1;
			BioQuery data = table.GetQueryRowByID(bioID);
			if(data == null)
				continue;

			Card card = new Card(data);
			playerCard.SetCard(index, card);
		}

		// player1
		SetPlayerCard(PlayerType.Player1, 0, playerCard);
		// player2
		SetPlayerCard(PlayerType.Player2, 0, playerCard);
	}

	#endregion

}
