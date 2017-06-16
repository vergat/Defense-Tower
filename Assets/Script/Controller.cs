using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnRoadChange(RoadData roadDataPassed);

public class Controller : MonoBehaviour {

    [SerializeField]
    private WidgetRoadSelector uiPrefab = null;

    private WidgetRoadSelector uiInstance = null;

    public OnRoadChange onRoadChange = null;

   
    
	private void Awake ()
    {
        if (uiPrefab == null)
        {
            return;
        }
        uiInstance = Instantiate(uiPrefab);
        uiInstance.onRoadSelected += OnRoadSelectCallBack;
	}
	
    private void OnRoadSelectCallBack(RoadData roadDataPassed)
    {
        if (onRoadChange != null)
        {
            onRoadChange(roadDataPassed);
        }
    }

}
