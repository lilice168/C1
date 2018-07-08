using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// <Manager>
public class SceneTransferManager : MonoBehaviour
{
	public static SceneTransferManager m_Instance;

	void Start()
	{
		m_Instance = this;
		DontDestroyOnLoad(this);
	}

	public void LoadScene(string _Name)
	{
		SceneManager.LoadSceneAsync(_Name);
	}
}
