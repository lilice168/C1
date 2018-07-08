using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <Controller>
// 遊戲初始化
public class Main : MonoBehaviour {

	IEnumerator Start () 
	{
		DontDestroyOnLoad(this);
		InitSingletonSystem();
        yield return BundleManager.Instance.InitBundle();
		InitMonoSingletonSystem();
		InitData();
        yield return InitGame();
	}

    void OnDestroy()
    {
        BundleManager.Instance.UnLoadAllBundle();
    }

	// 初始化 Singleton
	void InitSingletonSystem()
	{
		DebugManager.Init("DebugManager");
		DebugManager.LogWarning("Main Init Start");

		BundleManager.Init("BundleManager");
		DBManager.Init("DBManager");
		DeckManager.Init("DeckManager");

		DebugManager.LogWarning("Main Init Finish");
	}

	// 初始化MonoBehaviour Singleton.
	void InitMonoSingletonSystem()
	{
		DebugManager.LogWarning("InitMonoSingletonSystem Start");

		// UIManager.
		BundleManager.Instance.LoadGameObject<GameObject>(BundleType.Prefab, "UIManager");

		// SceneManager.
		BundleManager.Instance.LoadGameObject<GameObject>(BundleType.Prefab, "SceneManager");

		// SpriteManager.
		BundleManager.Instance.LoadGameObject<GameObject>(BundleType.Prefab, "SpriteManager");

        // SpriteManager.
        BundleManager.Instance.LoadGameObject<GameObject>(BundleType.Prefab, "BattleManager");

		DebugManager.LogWarning("InitMonoSingletonSystem Finish");
	}

	// 初始化遊戲資料
	void InitData()
	{
		DebugManager.LogWarning("InitData Start");

		DBManager.Instance.LoadAllDB();
		//yield return DBManager.Instance.Test(); // 測試ＣＯＤＥ

		DebugManager.LogWarning("InitData Finish");
	}

	// 初始化遊戲
    IEnumerator InitGame()
	{
		// UIManager Load
        while(!UIManager.m_Instance)
            yield return null;
        
		UIManager.m_Instance.Load(UITypeName.Menu);

		//DeckManager.Instance.TestSetCards(); // 測試ＣＯＤＥ
		//StartCoroutine(BundleManager.Instance.Test());  // 測試ＣＯＤＥ
	}
}
