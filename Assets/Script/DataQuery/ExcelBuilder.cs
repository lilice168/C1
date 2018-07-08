using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Excel;
using System.Data;

public class ExcelBuilder : MonoBehaviour {

	void Start () 
	{		
		XLSX();

	}

	void XLSX()
	{
		FileStream stream = File.Open(Application.dataPath + "/Excel/生物表.xlsx", FileMode.Open, FileAccess.Read);
		IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

		DataSet result = excelReader.AsDataSet();

		int columns = result.Tables[0].Columns.Count;
		int rows = result.Tables[0].Rows.Count;

		for(int i = 0;  i< rows; i++)
		{
			for(int j =0; j < columns; j++)
			{
				string  nvalue  = result.Tables[0].Rows[i][j].ToString();
				Debug.Log(nvalue);
			}
		}	
	}
}
