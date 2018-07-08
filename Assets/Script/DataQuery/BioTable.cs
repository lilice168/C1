using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BioQuery : IQueryDatas
{
	public int m_ID;
	public string m_Name;
	public int m_Hp;
	public int m_Atk;


	public override int GetID ()
	{
		return m_ID;
	}
}
	
public class BioTable : IQuery<BioQuery>
{

}