using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour {

    [SerializeField]
    private Controller control = null;

    private void OnEnable()
    {
        control.onRoadChange += OnRoadChangeCallback;
    }

    private void OnDisable()
    {
        control.onRoadChange -= OnRoadChangeCallback;
    }

    private void OnRoadChangeCallback(RoadData roadDataAsset)
    {

    }
}
