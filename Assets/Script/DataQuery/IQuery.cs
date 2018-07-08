using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;


public static class QueryDataTool
{
	/// <summary>
	/// string to uint
	/// </summary>
	public static bool ToInt(string str, out uint value, bool igonreStringNullError = true)
	{
		str = str.Trim();
		if (string.IsNullOrEmpty(str))
		{
			value = 0;
			return igonreStringNullError;
		}
		bool convertSucess = uint.TryParse(str, out value);
		if (convertSucess == false)
			return false;

		return true;
	}
	/// <summary>
	/// string to int
	/// </summary>
	public static bool ToInt(string str, out int value, bool igonreStringNullError=true) 
	{
		str = str.Trim();

		if (string.IsNullOrEmpty (str)) {
			value = 0;
			return igonreStringNullError;
		}
		bool convertSucess = int.TryParse (str, out value);
		if (!convertSucess)
			return false;

		return true;
	}

	/// <summary>
	/// string to float
	/// </summary>
	public static bool ToFloat(string str,out float value, bool igonreStringNullError = true)
	{
		str = str.Trim();
		if (string.IsNullOrEmpty (str)) {
			value = 0;
			return igonreStringNullError;
		}
		bool convertSucess = float.TryParse (str, out value);
		if (convertSucess == false)
			return false;

		return true;
	}
}



public abstract class IQueryDatas
{
	public abstract int GetID();

	public bool FillQueryRow<T> (List<string> _QueryRowStr) where T:IQueryDatas
	{
		FieldInfo[] memberInfos = typeof(T).GetFields();        //取得所有欄位.
		int index = 0;

		//將 class's member 按順序對應到 table's columon 轉換儲存起來.
		foreach(FieldInfo memberInfo in memberInfos){

			/*
			//特別處理的欄位
			if (IsSpecialProcessRow(index))                     //複合欄位.
			{
				ProcessSpecialRowData(_queryRowStr, ref index);
				continue;
			}
			*/
			if (memberInfo.FieldType.IsEnum) {             //Enum.
				int value = 0;
				if (QueryDataTool.ToInt(_QueryRowStr[index], out value, false) == false)
				{
					//ExcelParserDebugTool.LogError(" 欄位為空白或字串錯誤(Enum)");
				}
				memberInfo.SetValue(this, value);
			}
			else if (memberInfo.FieldType == typeof(int)) {     //整數.
				int value = 0;
				if (QueryDataTool.ToInt(_QueryRowStr[index], out value) == false)
				{
					//ExcelParserDebugTool.LogError(" 欄位字串錯誤(int)");
				}
				memberInfo.SetValue(this, value);
			}
			else if(memberInfo.FieldType == typeof(uint)) {     //整數.
				uint value = 0;
				if (QueryDataTool.ToInt(_QueryRowStr[index], out value) == false)
				{
					//ExcelParserDebugTool.LogError(" 欄位字串錯誤(int)");
				}
				memberInfo.SetValue(this, value);
			}
			else if (memberInfo.FieldType == typeof(float)) {   //浮點數.
				float value = 0;
				if (QueryDataTool.ToFloat(_QueryRowStr[index], out value) == false)
				{
					//ExcelParserDebugTool.LogError(" 欄位字串錯誤(float)");
				}
				memberInfo.SetValue(this, value);
			}
			else if (memberInfo.FieldType == typeof(string)) {  //字串.
				memberInfo.SetValue(this, _QueryRowStr[index]);
			}
			else if (index >= _QueryRowStr.Count) {                
				break;
			}
			index++;
		}
		return true;
	}   
}

public class IQuery<T> : ScriptableObject where T:IQueryDatas
{
	[SerializeField] List<T> m_TableData = new List<T>();
	[NonSerialized]  protected Dictionary<int, T> m_DictTable = new Dictionary<int, T>();


	public void InitTable(){

		for(int index = 0 ; index < m_TableData.Count; index++){
			T data = m_TableData[index];
			int id = data.GetID();
			if(m_DictTable.ContainsKey(id) == false){
				m_DictTable.Add(id, data);
			}
		}
	}

	public T GetQueryRowByID(int _ID){

		if(m_DictTable.ContainsKey(_ID)){
			return m_DictTable[_ID];
		}
		return null;
	}

	#if UNITY_EDITOR
	public void InitQueryTable(List< List<string> > _RawStrSheetTable) {
		m_TableData.Clear();
		for(int i = 0; i < _RawStrSheetTable.Count; i++){
			List<string> rowDatas = _RawStrSheetTable[i];
			if(rowDatas == null){
				continue;
			}

			T queryDataRow = (T)Activator.CreateInstance(typeof(T));
			queryDataRow.FillQueryRow<T>(rowDatas);
			AddQueryDataToTable (queryDataRow);
		}
	}

	void AddQueryDataToTable(T _QueryData){
		m_TableData.Add (_QueryData);		
	}
	#endif

}
