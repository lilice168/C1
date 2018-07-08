using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <Manager>
public class UIManager : MonoBehaviour
{
	public static UIManager m_Instance;

	List<GameObject> m_UIs = new List<GameObject>();


	void Start()
	{
		m_Instance = this;
		DontDestroyOnLoad(this);
	}

	public bool Load(UITypeName name)
	{
		string fileName = UIName.GetTypeName(name);
		StartCoroutine(Loading(fileName));

		return true;
	}

	IEnumerator Loading(string _name)
	{
		yield return BundleManager.Instance.Load(BundleType.UI);

		while(!BundleManager.Instance.IsDownLoadDone(BundleType.UI)){
			yield return null;
		}

		GameObject go = BundleManager.Instance.LoadGameObject<GameObject>(BundleType.UI, _name);
		go.name = _name;
		if(!CheckUI(_name)){
			m_UIs.Add(go);
		}
		DebugManager.LogWarning(go.name + "is done.");
	}

	public bool CheckUI(string _Name)
	{
		for(int i = 0; i < m_UIs.Count; i++){
			if(m_UIs[i].name.Equals(_Name)){
				return true;
			}
		}
		return false;
	}

	public void RemoveUI(UITypeName _Name)
	{
		string fileName = UIName.GetTypeName(_Name);
		for(int i = 0; i < m_UIs.Count; i++){
			if(m_UIs[i].name.Equals(fileName)){
				m_UIs.RemoveAt(i);
				return;
			}
		}
	}

	#region 設定UI顯示
	public bool SetUIShow(UITypeName _Name ,bool _Show){

		string fileName = UIName.GetTypeName(_Name);
		for(int i = 0; i < m_UIs.Count; i++){
			if(m_UIs[i].name.Equals(fileName)){
				m_UIs[i].SetActive(_Show);
				return true;
			}
		}
		return false;
	}

	public bool IsUIShow(UITypeName _Name){

		string fileName = UIName.GetTypeName(_Name);
		for(int i = 0; i < m_UIs.Count; i++){
			if(m_UIs[i].name.Equals(fileName)){
				return m_UIs[i].activeSelf;
			}
		}
		return false;
	}
	#endregion
}
