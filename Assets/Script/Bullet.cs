using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    private float speed = 10.0f;
 
    private GameObject target;
    public GameObject Target
    {
        get { return target; }
        set { target = value; }
    }

    private Vector3 startPosition;
    public Vector3 StartPosition
    {
        get { return startPosition; }
        set { startPosition = value; }
    }

    private Vector3 targetPosition;
    public Vector3 TargetPosition
    {
        get { return targetPosition; }
        set { targetPosition = value; }
    }

    private float distance;
    private float startTime;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        TargetPosition = Target.transform.position;
        distance = Vector3.Distance(startPosition, targetPosition);
	}
	
	// Update is called once per frame
	void Update () {
        if (target != null)
        {
            targetPosition = target.transform.position;
            distance = Vector3.Distance(startPosition, targetPosition);
        }
        Quaternion rotation =Quaternion.FromToRotation(Vector3.up, gameObject.transform.position - targetPosition);
        float timeInterval = Time.time - startTime;
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);
        gameObject.transform.rotation = rotation;
        if (gameObject.transform.position.Equals(targetPosition))
        {
            Destroy(gameObject);
        }
    }
}
