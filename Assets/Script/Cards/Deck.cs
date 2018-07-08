using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 牌庫資料<DATA>
[System.Serializable]
public class Deck
{
	public int m_DeckHp; // 牌庫生命
	public Card[] m_Card; // 卡牌


	public Deck()
	{
		m_DeckHp = 7;
		m_Card  = new Card[VerDef.DeckCardMax];
	}


	public void SetDeckCard(int _Index, Card _Card)
	{
		if(_Index < 0 || _Index >= VerDef.DeckCardMax)
			return;

		m_Card[_Index] = _Card;
	}
}
