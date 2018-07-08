using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum BundleType
{
	UI,
	DB,
	Prefab,
	Sprite,
}
// <Manager>
public class BundleManager : Singleton<BundleManager>
{
    List<BundleType> m_AssetBundleLoading = new List<BundleType>();
    Dictionary<BundleType, AssetBundle> m_AssetBundleMap = new Dictionary<BundleType, AssetBundle>();

    string m_Path = "file://" + Application.streamingAssetsPath;


    public IEnumerator InitBundle()
    {
        yield return Load(BundleType.UI);
        while (!IsDownLoadDone(BundleType.UI))
            yield return null;

        yield return Load(BundleType.Prefab);
        while (!IsDownLoadDone(BundleType.Prefab))
            yield return null;

        yield return Load(BundleType.DB);
        while (!IsDownLoadDone(BundleType.DB))
            yield return null;

        yield return Load(BundleType.Prefab);
        while (!IsDownLoadDone(BundleType.Prefab))
            yield return null;

        yield return Load(BundleType.Sprite);
        while (!IsDownLoadDone(BundleType.Sprite))
            yield return null;

    }

    public void UnLoadAllBundle()
    {
        BundleManager.Instance.UnLoadBundle(BundleType.UI);
        BundleManager.Instance.UnLoadBundle(BundleType.DB);
        BundleManager.Instance.UnLoadBundle(BundleType.Prefab);
        BundleManager.Instance.UnLoadBundle(BundleType.Sprite);
    }


	// 讀取需要的Bundle
	public IEnumerator Load(BundleType _type)
	{
		string downLoadPath;
		switch(_type)
		{
		case BundleType.UI:
			downLoadPath = "ui";
			break;
		case BundleType.DB:
			downLoadPath = "db";
			break;
		case BundleType.Prefab:
			downLoadPath = "prefab";
			break;
		case BundleType.Sprite:
			downLoadPath = "sprite";
			break;
		default:
			yield break;
		}
			
		string url = m_Path +"/"+ downLoadPath;
		yield return LoadBundle(_type, url);
	}

	IEnumerator LoadBundle(BundleType _type, string _url)
	{
		if(m_AssetBundleLoading.Contains(_type)){
			yield return null;
		}else{
			m_AssetBundleLoading.Add(_type);
			if(m_AssetBundleMap.ContainsKey(_type)){
				DebugManager.LogWarning(string.Format("DownLoad Type{0} is done.", _type));
			}else{
				DebugManager.LogWarning("DownLoad Path:" + _url);

				UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(_url);
				yield return request.Send();

				AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
				if(ab == null){
					DebugManager.LogError("DownLoad AssetBundle NULL:" + _url);
					yield break;
				}

				m_AssetBundleMap.Add(_type, ab);
			}
		}
	}

	// 判斷Bundle是否下載完
	public bool IsDownLoadDone(BundleType _type)
	{
		if(m_AssetBundleMap.ContainsKey(_type)){
			return true;
		}
		return false;
	}

	#region 從Bundle拿取資料
	public T LoadGameObject<T>(BundleType _type, string _name) where T : Object
	{
		T go = m_AssetBundleMap[_type].LoadAsset<T>(_name);
		if(m_AssetBundleMap.ContainsKey(_type)){
			if(typeof(T) == typeof(GameObject)){
				return GameObject.Instantiate(go);
			}else if(typeof(T) == typeof(Texture)){
				return Texture.Instantiate(go);
			}else{
				return Object.Instantiate(go);
			}
		}
		return null;
	}

	public Sprite LoadSprite(string _Name, string _SubName)
	{
		Sprite[] spriteSheet = m_AssetBundleMap[BundleType.Sprite].LoadAssetWithSubAssets<Sprite>(_Name);

		for(int i = 0; i < spriteSheet.Length; i++){
			if(spriteSheet[i].name.Equals(_Name + _SubName)){

				return Sprite.Instantiate(spriteSheet[i]);
			}
		}

		return null;
	}

	#endregion


	// 釋放Bundle
	public bool UnLoadBundle(BundleType _type)
	{
		if(m_AssetBundleMap.ContainsKey(_type)){
			m_AssetBundleMap[_type].Unload(true);
			m_AssetBundleMap.Remove(_type);

			m_AssetBundleLoading.Remove(_type);
			DebugManager.LogWarning("UnLoad Bunlde." + _type);
			return true;
		}
		return false;
	}


	#region Test code
	// 測試ＣＯＤＥ
	public IEnumerator Test()
	{
		string path = "file://" + Application.streamingAssetsPath;
		string fileName = "ui";
		string url = path +"/"+ fileName;

		UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url);
		yield return request.Send();

		AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
		GameObject go = ab.LoadAsset<GameObject>("Menu");
		GameObject.Instantiate(go);
		ab.Unload( true);

	}
	#endregion

}
