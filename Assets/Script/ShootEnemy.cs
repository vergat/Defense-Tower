using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : MonoBehaviour {
    private List<GameObject> enemyInRange;
    [SerializeField]
    private GameObject bullet=null;
    [SerializeField]
    private float fireRate = 1.0f;
    private float lastShoot;
	// Use this for initialization
	void Start () {
        enemyInRange = new List<GameObject>();
        lastShoot = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (enemyInRange.Count > 0)
        {
            if (Time.time - lastShoot > fireRate)
            {
                Shoot(enemyInRange[0]);
                lastShoot = Time.time;
            }
            

        }
	}
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.gameObject.tag.Equals("Enemy"))
        {
            enemyInRange.Add(enemy.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D enemy)
    {
        if (enemy.gameObject.tag.Equals("Enemy"))
        {
            enemyInRange.Remove(enemy.gameObject);
        }
    }

    void Shoot(GameObject target)
    {
        Vector3 startPosition = gameObject.transform.position;
        startPosition.y = startPosition.y+ .2f;
        GameObject newBullet = (GameObject)Instantiate(bullet);
        newBullet.transform.position = startPosition;
        Bullet bulletComp = newBullet.GetComponent<Bullet>();
        bulletComp.Target = target;
        bulletComp.StartPosition = startPosition;
    }
}
