using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCoinManager : MonoBehaviour {
    [SerializeField]
    private Sprite halfHeart;
    [SerializeField]
    private Sprite emptyHeart;
    [SerializeField]
    private GameObject life;

    private int hitPoint=10;
	// Use this for initialization
	void Awake () {
      // Component[] gife= life.GetComponentsInChildren();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
