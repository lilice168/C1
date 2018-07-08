using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// <Manager>
using UnityEngine.UI;


public class SpriteManager : MonoBehaviour
{
	public static SpriteManager m_Instance;

	Dictionary<string, Sprite> m_SpriteCache = new Dictionary<string, Sprite>();
	Dictionary<Sprite, List<GameObject> > m_ImageCache = new Dictionary<Sprite, List<GameObject> >();

	void Start()
	{
		m_Instance = this;
		DontDestroyOnLoad(this);
	}

	public void LoadIcon(Image _Image, string _Name, string _SubName = "")
	{
		StartCoroutine(Loading(_Image, _Name, _SubName));
	}

	IEnumerator Loading(Image _Image, string _Name, string _SubName)
	{
		yield return BundleManager.Instance.Load(BundleType.Sprite);

		while(!BundleManager.Instance.IsDownLoadDone(BundleType.Sprite)){
			yield return null;
		}
		 
		string allName = _Name + _SubName;
		Sprite sprite = null;
		if(m_SpriteCache.TryGetValue( allName, out sprite)){
			_Image.sprite = sprite;

			m_ImageCache[sprite].Add(_Image.gameObject);

		}else{
			sprite = BundleManager.Instance.LoadSprite(_Name, _SubName);

			if(sprite != null){
				_Image.sprite = sprite;

				if(m_ImageCache.ContainsKey(sprite)){
					m_ImageCache[sprite].Add(_Image.gameObject);
				}else{
					List<GameObject> images = new List<GameObject>();
					images.Add(_Image.gameObject);
					m_ImageCache.Add(sprite, images);
				}

				m_SpriteCache.Add(allName, sprite);
			}
		}
	}


}
