using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 10;
    public int damage;
    public GameObject target;
    public Vector3 startPosition;
    public Vector3 targetPosition;

    private float distance;
    private float startTime;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        distance = Vector3.Distance(startPosition, targetPosition);
	}
	
	// Update is called once per frame
	void Update () {
        
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);
        if (gameObject.transform.position.Equals(targetPosition))
        {
            Destroy(gameObject);
        }
    }
}
