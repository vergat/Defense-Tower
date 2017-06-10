using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSelectorData : ScriptableObject {

    // [SerializeField]
    // RoadData[] listRoadData;
   [SerializeField]
   List<RoadData> listRoadData;
    public List<RoadData> ListRoadData
    {
        get { return listRoadData; }
    }
    
}
[System.Serializable]
public class RoadData
{
    [SerializeField]
    private int costo;
    [SerializeField]
    Sprite road;

    public int Costo
    {
        get { return costo; }
    }

    public Sprite Road
    {
        get { return road; }
    }
}