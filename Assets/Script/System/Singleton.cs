using UnityEngine;

public class Singleton<T> where T : class,new()
{
	private static T _instance;


	public static T Instance
	{
		get
		{
			return _instance;
		}
	}

	public static bool Init(string name)
	{
		if (_instance == null)
		{
			_instance = new T();
			Debug.LogFormat("Singleton Init. {0}", name);
			return true;
		}
		return false;
	}
}