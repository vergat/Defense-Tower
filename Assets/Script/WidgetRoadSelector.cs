using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnRoadSelect(RoadData roadDataPassed);

public class WidgetRoadSelector : MonoBehaviour {

    [SerializeField]
    RoadSelectorData roadDataAsset;

    [SerializeField]
    Image mainImageRoad;

    [SerializeField]
    Image leftImageRoad;

    [SerializeField]
    Image rightImageRoad;

    [SerializeField]
    Text roadCost;

    List<RoadData> roadData;

    int currentRoad = 0;

    public OnRoadSelect onRoadSelected = null;

    void Start () {
        this.GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
        roadData = roadDataAsset.ListRoadData;
        mainImageRoad.sprite = roadData[currentRoad].Road;
        roadCost.text = roadData[currentRoad].Costo.ToString();
        mainImageRoad.enabled=true;
        rightImageRoad.sprite = roadData[(currentRoad + 1)%roadData.Capacity].Road;
        rightImageRoad.enabled = true;
        leftImageRoad.sprite = roadData[((roadData.Capacity + currentRoad) - 1) % roadData.Capacity].Road;
        leftImageRoad.enabled = true;
    }
	
    public void LeftClickEvent()
    {

        currentRoad = ((roadData.Capacity+currentRoad)-1)% roadData.Capacity;
        mainImageRoad.sprite = roadData[currentRoad % roadData.Capacity].Road;
        roadCost.text = roadData[currentRoad % roadData.Capacity].Costo.ToString();
        rightImageRoad.sprite = roadData[(currentRoad + 1) % roadData.Capacity].Road;
        leftImageRoad.sprite = roadData[((roadData.Capacity + currentRoad) - 1) % roadData.Capacity].Road;
    }

    public void RightClickEvent()
    {
        currentRoad = ((roadData.Capacity + currentRoad) + 1) % roadData.Capacity;
        mainImageRoad.sprite = roadData[currentRoad % roadData.Capacity].Road;
        roadCost.text = roadData[currentRoad % roadData.Capacity].Costo.ToString();
        leftImageRoad.sprite = roadData[((roadData.Capacity + currentRoad) - 1) % roadData.Capacity].Road;
        rightImageRoad.sprite = roadData[(currentRoad + 1) % roadData.Capacity].Road;
    }

    public void Onclick()
    {
        Debug.Log(currentRoad.ToString());
        onRoadSelected(roadData[currentRoad]);
    }


}
