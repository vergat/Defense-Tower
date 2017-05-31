using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class GenerateBaseMap {

    static private GameObject prefab=null;
    static private float offsetWidth = 1.0f;
    static private float offsetHight = 33.0f;
    static private uint sizeMap = 7;

    [MenuItem("GenerateBaseMap/Generator")]
    public static void Generator()
    {
        GameObject rootMap;

        rootMap = GameObject.Find("rootMap");
        
        //Check if exist a root and delete the object inside it
        if (rootMap == null)
        {
            rootMap = new GameObject("rootMap");
        }
        int numChild = rootMap.transform.childCount;
        for (int index=0; index < numChild; ++index)
        {
            GameObject.DestroyImmediate(rootMap.transform.GetChild(0).gameObject);
        }

        prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Sprite/Landscape/landscape_empty.prefab");

        if (prefab != null)
        {
            int counter = 0;
            List<GameObject> chunks = new List<GameObject>();

            SpriteRenderer sprite = prefab.GetComponent<SpriteRenderer>();

            float width = (((sprite.sprite.texture.width - offsetWidth)/100.0f)/2.0f);
            float hight = (((sprite.sprite.texture.height - offsetHight)/100.0f)/2.0f);

            GameObject firstChunk = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
            firstChunk.transform.SetParent(rootMap.transform);
            firstChunk.name = firstChunk.name + "_"+counter;
            firstChunk.transform.position =new  Vector3(0, 0, 0);
            counter++;
            chunks.Add(firstChunk);
            for (int row=1; chunks.Count != 0;row++)
            {
                List<GameObject> tempListchunks = new List<GameObject>();
                for (int indexChunks = 0; indexChunks < chunks.Count; indexChunks++)
                {
                    Vector3 newPosition;
                    if ((indexChunks == 0) && (row<(sizeMap)))
                    {
                        GameObject firstChunkLeft = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                        firstChunkLeft.transform.SetParent(rootMap.transform);
                        newPosition.x = chunks[indexChunks].transform.position.x - width;
                        newPosition.y = chunks[indexChunks].transform.position.y + hight;
                        newPosition.z = chunks[indexChunks].transform.position.y + hight;
                        firstChunkLeft.transform.position = newPosition;
                        firstChunkLeft.name = firstChunkLeft.name + "_" + counter;
                        counter++;
                        tempListchunks.Add(firstChunkLeft);
                    }
                    if ((row<(sizeMap)) || (indexChunks<chunks.Count-1))
                    {
                        GameObject newChunk = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
                        newChunk.transform.SetParent(rootMap.transform);
                        newPosition.x = chunks[indexChunks].transform.position.x + width;
                        newPosition.y = chunks[indexChunks].transform.position.y + hight;
                        newPosition.z = chunks[indexChunks].transform.position.y + hight;
                        newChunk.transform.position = newPosition;
                        newChunk.name = newChunk.name + "_" + counter;
                        counter++;
                        tempListchunks.Add(newChunk);
                    }
                }
                chunks = tempListchunks;
            }
            Camera camera = GameObject.FindObjectOfType<Camera>();
            Vector3 cameraPos;
            cameraPos.x = 0;
            cameraPos.y = hight* (sizeMap-1);
            cameraPos.z = -1;
            camera.transform.position = cameraPos;
        }
        
    }
}
