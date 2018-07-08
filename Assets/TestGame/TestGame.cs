using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 用來做測試用.
public class TestGame : MonoBehaviour {

	public static TestGame m_Instance;
	public bool m_IsDone;

	// Use this for initialization
	IEnumerator Start () {
		m_Instance = this;

		DebugManager.LogWarning("Test Init Start");
		InitSingletonSystem();
		yield return InitMonoSingletonSystem();
		yield return Test();

		m_IsDone = true;
		DebugManager.LogWarning("Test Init Finish");

	}
	
	void InitSingletonSystem()
	{
		DebugManager.Init("DebugManager");

		BundleManager.Init("BundleManager");
		DBManager.Init("DBManager");
		DeckManager.Init("DeckManager");


	}

	IEnumerator InitMonoSingletonSystem()
	{
		yield return BundleManager.Instance.Load(BundleType.Prefab);
		while(!BundleManager.Instance.IsDownLoadDone(BundleType.Prefab)){
			yield return null;
		}

		// UIManager.
		BundleManager.Instance.LoadGameObject<GameObject>(BundleType.Prefab, "UIManager");

		// SceneManager.
		BundleManager.Instance.LoadGameObject<GameObject>(BundleType.Prefab, "SceneManager");

		// SpriteManager.
		BundleManager.Instance.LoadGameObject<GameObject>(BundleType.Prefab, "SpriteManager");

	}

	IEnumerator Test()
	{
		yield return InitData();
		DeckManager.Instance.TestSetCards(); // 測試ＣＯＤＥ
	}

	IEnumerator InitData()
	{
		DebugManager.LogWarning("InitData Start");
		yield return BundleManager.Instance.Load(BundleType.DB);
		while(!BundleManager.Instance.IsDownLoadDone(BundleType.DB)){
			DebugManager.LogWarning("Loading Data");
			yield return null;
		}
		DBManager.Instance.LoadAllDB();

		DebugManager.LogWarning("InitData Finish");
	}
}
