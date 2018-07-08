using System.Diagnostics;
using UnityEngine;

public class DebugManager : Singleton<DebugManager> 
{
	public static void Log(object message)
	{
	#if LOG
		UnityEngine.Debug.Log(message);
	#endif
	}

	public static void LogWarning(object message)
	{
	#if LOG
		UnityEngine.Debug.LogWarning(message);
	#endif
	}

	public static void LogError(object message)
	{
	#if LOG
		UnityEngine.Debug.LogError(message);
	#endif
	}
}