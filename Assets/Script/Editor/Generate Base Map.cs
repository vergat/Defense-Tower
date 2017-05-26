using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class GenerateBaseMap {

    static private GameObject prefab=null;

    [MenuItem("GenerateBaseMap/Generator")]
    public static void Generator()
    {
        GameObject rootMap;
        rootMap = GameObject.Find("rootMap");
        if (rootMap == null)
        {
            rootMap = new GameObject("rootMap");
        }
        int numChild = rootMap.transform.childCount;
        for (int i=0; i < numChild; ++i)
        {
            GameObject.DestroyImmediate(rootMap.transform.GetChild(0).gameObject);
        }
        prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Sprite/Landscape/landscape_empty.prefab");
        if (prefab != null)
        {
            int counter = 0;
            List<GameObject> nextChunk = new List<GameObject>();

            SpriteRenderer sprite = prefab.GetComponent<SpriteRenderer>();

            float width = (((sprite.sprite.texture.width-1.0f)/100.0f)/2.0f);
            float hight = (((sprite.sprite.texture.height - 33.0f) / 100.0f) / 2.0f);

            GameObject aux = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            aux.transform.SetParent(rootMap.transform);
            aux.name = aux.name + "_"+counter;
            nextChunk.Add(aux);
            foreach(GameObject a in nextChunk)
            {

            }
            for (int i = 0; i < nextChunk.Count; i++)
            {
                GameObject aux2 = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                aux2.transform.SetParent(rootMap.transform);
                Vector3 newPosition;
                newPosition.x = aux.transform.position.x + width;
                newPosition.y = aux.transform.position.y + hight;
                newPosition.z = aux.transform.position.y + hight;
                aux2.transform.position = newPosition;
                aux = aux2;
            }

        }

    }
}
