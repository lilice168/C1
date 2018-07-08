using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static BattleManager m_Instance;


    public GameObject m_StartPoint;
    public GameObject m_MonsterPoint;

    // Use this for initialization
    void Start()
    {
        m_Instance = this;
        DontDestroyOnLoad(this);
    }


    #region 初始化場景.
    public void InitScene(int _MissionIndex) { 

        SceneTransferManager.m_Instance.LoadScene("Node");
        StartCoroutine("InitData");
    }

    IEnumerator InitData()
    {
        while(SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Node"))
            yield return null;

        // Prefab.
        LoadPrefab();
        // UI.
        LoadUI();
        // 開場動畫.
        BornPoint();
        // 開場動畫.
        SceneAnimation();
    }

    void LoadPrefab()
    {
        BundleManager.Instance.LoadGameObject<GameObject>(BundleType.Prefab, "DeathGod");
        BundleManager.Instance.LoadGameObject<GameObject>(BundleType.Prefab, "M001");
    }

    void LoadUI()
    {
        // UI.
        UIManager.m_Instance.Load(UITypeName.BattleUI);
    }

    void BornPoint()
    {
        m_StartPoint = GameObject.Find("StartPoint");
        m_MonsterPoint = GameObject.Find("MonsterPoint");
    }

    void SceneAnimation()
    {
        
    }
    #endregion
}
