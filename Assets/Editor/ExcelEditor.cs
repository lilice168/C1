using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using Excel;
using System.Data;
using System.Collections.Generic;

public class ExcelEditor : EditorWindow 
{
	List<string> m_AllExcels = new List<string>();
	Dictionary<int, bool> m_OutputFile = new Dictionary<int, bool>();
	Dictionary<int, string> m_OutputName = new Dictionary<int, string>();

	[MenuItem("Tools/ExcelEditor")]
	static void show()
	{
		GetWindow<ExcelEditor>().Init();
		GetWindow<ExcelEditor>().Show();
	}

	// load excel.
	void Init()
	{
		string[] allfiles = Directory.GetFiles(Application.dataPath, "*.xlsx", SearchOption.AllDirectories);
		m_AllExcels.AddRange(allfiles);

		for(int i = 0; i < m_AllExcels.Count; i++){

			string excelName = FindFileName(m_AllExcels[i]) ;
			if(string.Equals(excelName, string.Empty))
				continue;

			m_OutputName.Add(i, excelName);
		}
	}

	private string FindFileName(string _ExcelName)
	{
		string fileName = string.Empty;
		string[] splits = _ExcelName.Split('/');
		for(int i = 0; i < splits.Length; i++){

			string[] names = splits[i].Split('~');
			if(names.Length == 2)
				continue;

			names = splits[i].Split('.');
			if(names.Length == 2){
				fileName = names[0];
				break;
			}
		}

		return fileName;
	}


	// show
	void OnGUI()
	{
		if (GUILayout.Button("輸出檔案", GUILayout.Width(210)))
		{
			LoadData();
		}
		EditorGUILayout.BeginHorizontal();

		if (GUILayout.Button("全選", GUILayout.Width(100)))
		{
			AllOutPut();
		}

		if (GUILayout.Button("不全選", GUILayout.Width(100)))
		{
			CancelAllOutPut();
		}

		EditorGUILayout.EndHorizontal();
		GUILayout.Space(10);
		// 設定列表
		Dictionary<int, string>.Enumerator it = m_OutputName.GetEnumerator();
		while(it.MoveNext()){
			int index = it.Current.Key;
			string fileName = it.Current.Value;

			if(m_OutputFile.ContainsKey(index)){
				m_OutputFile[index] = EditorGUILayout.Toggle(fileName, m_OutputFile[index]);
			}else{
				bool isOutput = true;
				isOutput = EditorGUILayout.Toggle(fileName, isOutput);
				m_OutputFile.Add(index, isOutput);
			}
		}

	}

	void AllOutPut()
	{
		List<int> indexs = new List<int>( m_OutputFile.Keys);
		for(int i = 0 ; i < indexs.Count; i++){
			int index = indexs[i];
			m_OutputFile[index] = true;
		}
	}

	void CancelAllOutPut()
	{
		List<int> indexs = new List<int>( m_OutputFile.Keys);
		for(int i = 0 ; i < indexs.Count; i++){
			int index = indexs[i];
			m_OutputFile[index] = false;
		}
	}

	void LoadData()
	{
		int fileIndex = 0;
		Dictionary<int, bool>.Enumerator it = m_OutputFile.GetEnumerator();
		while(it.MoveNext()){
			fileIndex = fileIndex + 1;
			float filePorcess = (float)fileIndex / (float)m_OutputFile.Count;

			int index = it.Current.Key;
			bool isOutput = it.Current.Value;

			if(!isOutput)
				continue;

			if(!m_OutputName.ContainsKey(index))
				continue;

			string fileName = m_OutputName[index];
			string path = m_AllExcels[index];


			// 讀取 Excel檔案
			FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read);

			// 創建讀取 Excel檔
			IExcelDataReader excelRead = ExcelReaderFactory.CreateOpenXmlReader(stream);

			// 將讀取到 Excel檔暫存至內存
			DataSet result = excelRead.AsDataSet();

			// 獲得 Excel檔的行與列的數目
			int columns = result.Tables[0].Columns.Count;
			int rows = result.Tables[0].Rows.Count;

			List< List<string> > allRowData = new List< List<string> >(); 

			// 將資料讀取出來
			for (int i = 1; i < rows; i++)
			{
				List<string> data = new List<string>();
				for (int j = 0; j < columns; j++)
				{
					string rowData = result.Tables[0].Rows[i][j].ToString();
					data.Add(rowData);
				}
				allRowData.Add(data);
			}

			Output(fileName, allRowData);
			EditorUtility.DisplayProgressBar("進度", "ScriptableObject輸出中 " + (int)filePorcess * 100 +"%", filePorcess);

			// 讀取完後一定要關閉
			excelRead.Close();
		}
		EditorUtility.ClearProgressBar();
		EditorUtility.DisplayDialog("警告", "ScriptableObject建立完成", "確定");

	}

	void Output(string _FileName, List< List<string> > _Data)
	{
		if(_FileName.Equals("生物表")){
			BioTable d = ScriptableObject.CreateInstance<BioTable>();
			d.InitQueryTable(_Data);
			// 建立ScriptableObject，其檔案為data[0].asset
			AssetDatabase.CreateAsset(d, @"Assets" + "/Excel/" + typeof(BioTable) +".asset");
		}
	}


	void  OnInspectorUpdate()
	{
		Repaint();
	}

}
