using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissovleController : MonoBehaviour {

    public Material m_Material;

    float m_ShowTime;
    float m_Time;

	void Awake()
	{
        m_ShowTime = 1.2f;
	}

	public void ShowOn()
    {
        if(m_Material == null)
            return;

        m_Time = 0.0f;
        m_Material.SetFloat("_DissolveValue", 1.0f);
        StartCoroutine("ShowBegin");
    }

    IEnumerator ShowBegin()
    {
        if (m_Material == null)
            yield break;
        
        while(m_Time < m_ShowTime){
            float value = Mathf.Lerp(1.0f, 0.0f, m_Time);
            m_Material.SetFloat("_DissolveValue", value);
            m_Time += Time.deltaTime;
            yield return null;
        }
    }

    public void ShowOff()
    {
        if (m_Material == null)
            return;

        m_Time = 0.0f;
        m_Material.SetFloat("_DissolveValue", 0.0f);
        StartCoroutine("ShowEnd");
    }

    IEnumerator ShowEnd()
    {
        if (m_Material == null)
            yield break;
        
        while (m_Time < m_ShowTime)
        {
            float value = Mathf.Lerp(0.0f, 1.0f, m_Time);
            m_Material.SetFloat("_DissolveValue", value);
            m_Time += Time.deltaTime;
            yield return null;
        }
    }
}
