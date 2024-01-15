using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    private Dictionary<string, GameObject> prefabs = new Dictionary<string, GameObject>();

    public Sprite LoadSprite(string name)
    {
        if(!sprites.ContainsKey(name))
        {
            Sprite sprite = Resources.Load<Sprite>($"Sprites/{name}");
            if(!sprite )
            {
                Debug.LogError($"Sprite 로드 실패 : {name}"); 
                return null;
            }
            sprites.Add(name, sprite);
        }
        return sprites[name]; 
    }
       
    public GameObject Instantiate(string name, Transform parent = null)
    {
        if(!prefabs.ContainsKey(name))
        {
            GameObject go = Resources.Load<GameObject>($"Prefabs/{name}");
            if(!go)
            {
                Debug.LogError($"Prefab 로드 실패 : {name}"); 
                return null;
            }
            prefabs.Add(name, go);
        }

        GameObject prefab = prefabs[name]; 

        return Instantiate(prefab, prefab.transform.position, prefab.transform.rotation, parent);
    }
}
