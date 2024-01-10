using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private readonly Dictionary<string, UI_Base> _uiDic = new Dictionary<string, UI_Base>();

    public T GetUIComponent<T>() where T : UI_Base
    {
        string key = typeof(T).Name;
        if (!_uiDic.ContainsKey(key))
        {
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/{key}");
            if (!prefab)
            {
                Debug.LogError($"UI Prefab 로드 실패 : {key}");
                return null;
            }
            GameObject obj = Instantiate(prefab);
            if(obj.TryGetComponent<T>(out T component))
            {
                Debug.LogError($"Get UI Component 실패 : {key}");
                return null; 
            }
            _uiDic.Add(key, obj.GetComponent<T>());

        }
        return _uiDic[key] as T; 
    }

    public bool TryGetUIComponent<T>(out T uiComponent) where T : UI_Base
    {
        string key = typeof(T).Name;
        if (!_uiDic.ContainsKey(key))
        {
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/UI/{key}");
            if (!prefab)
            {
                Debug.LogError($"UI Prefab 로드 실패 : {key}");
                uiComponent = null;
                return false;
            }
            GameObject obj = Instantiate(prefab);
            if (!obj.TryGetComponent<T>(out T component))
            {
                Debug.LogError($"Get UI Component 실패 : {key}");
                uiComponent = null;
                return false;
            }
            _uiDic.Add(key, component);
        }
        uiComponent = _uiDic[key] as T;
        return true;
    }

    public void RemoveUIComponent<T>() where T : UI_Base
    {
        string key = typeof(T).Name;
        if (_uiDic.ContainsKey(key))
        {
            _uiDic.Remove(key);
        }
    }

    public void RemoveAllUIComponent()
    {
        _uiDic.Clear();
    }
}
