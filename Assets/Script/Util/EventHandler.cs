using System;
using System.Collections.Generic;

public class EventHandler {

	public delegate void Message();
	public delegate void Message<T>(T param);

	static readonly Dictionary<string, Delegate> m_Msgs = new Dictionary<string, Delegate>(); 
	static readonly Dictionary<string, Delegate> m_TMsgs = new Dictionary<string, Delegate>(); 

	public static void Add(string key, Message msg) 
	{
		Delegate val;
		if(m_Msgs.TryGetValue(key, out val)){
			m_Msgs[key] = (Message)val + msg;	
		}
		else{
			m_Msgs.Add(key, msg);	
		}
	}

	public static void Add<T>(string key, Message<T> msg) 
	{
		Delegate val;
		if(m_TMsgs.TryGetValue(key, out val)) {
			m_TMsgs[key] = (Message<T>)val + msg;
		} else {
			m_TMsgs.Add(key, msg);
		}
	}

	public static bool Invoke(string key) {
		Delegate val;
		if(m_Msgs.TryGetValue(key, out val)) {
			((Message)val)();
			return true;
		}
	
		return false;
	}

	public static bool Invoke<T>(string key, T param) {

		Delegate val;
		if(m_TMsgs.TryGetValue(key, out val)) {
			((Message<T>)val)(param);
			return true;
		}
		return false;
	}
		
	public static void Remove(string key, Message msg) {
		Delegate val;
		if(m_Msgs.TryGetValue(key, out val)) {
			val = (Message)val - msg;
			if(val != null) {
				m_Msgs[key] = val;
			} else {
				m_Msgs.Remove(key);
			}
		}
	}

	public static void Remove<T>(string key, Message<T> msg) 
	{
		Delegate val;
		if(m_TMsgs.TryGetValue(key, out val)) {
			val = (Message<T>)val - msg;
			if(val != null) {
				m_TMsgs[key] = val;
			} else {
				m_TMsgs.Remove(key);
			}
		}
	}
}
