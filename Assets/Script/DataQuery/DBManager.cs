using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : Singleton<DBManager> 
{
	enum ETableType{
		TableMin = 0,
		BioTable = TableMin,
		TableMax
	}

	Dictionary<ETableType, Object> m_DBMap = new Dictionary<ETableType, Object>();
	
	public void LoadAllDB()
	{

		for(ETableType index = ETableType.TableMin; index < ETableType.TableMax; index++){

			Object obj = null;
			if(index == ETableType.BioTable){
				obj = BundleManager.Instance.LoadGameObject<Object>(BundleType.DB, "BioTable");
				BioTable bioTable = obj as BioTable;
				bioTable.InitTable();
			}

			if(obj== null)
				continue;

			DebugManager.LogWarning(string.Format("DB {0} is Done.", obj.name));

			m_DBMap.Add(index, obj);
		}

		DebugManager.LogWarning("Load DB Count is " + m_DBMap.Count);
	}


	#region 讀DB
	public BioTable GetBioTable()
	{
		if(m_DBMap.ContainsKey(ETableType.BioTable)){
			return m_DBMap[ETableType.BioTable] as BioTable;
		}
		return null;
	}
	#endregion

	#region Test Code
	// 測試ＣＯＤＥ
	public IEnumerator Test()
	{
		LoadAllDB();

		BioTable table = GetBioTable();
		BioQuery data = table.GetQueryRowByID(1);

		DebugManager.LogWarning("DB Test :" + data.m_Name);

		yield return null;
	}

	#endregion
}