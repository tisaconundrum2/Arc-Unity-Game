using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour {

    public GameObject EnemyBullet; //bullet prefab
    public float fireSpeed = 1f;
    private float changeTime = 0;
    public float waitTime = 1f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time > changeTime)
        {
            Invoke("FireEnemyBullet", waitTime);
            changeTime = Time.time + fireSpeed;
        }

    }

    //fire bullets

    void FireEnemyBullet()
    {
        GameObject playership = GameObject.Find("PlayerShip");                                      // Find the playership on the field
        if (playership != null)                                                                     // as long as the player is there, fire at them
        {                                                                                           //
            GameObject bullet = (GameObject)Instantiate(EnemyBullet);                               // instantiate a new Bullet object
            bullet.transform.position = transform.position;                                         // get the current position for the bullet
            Vector2 direction = playership.transform.position - bullet.transform.position;          // find where the playership is relative to the bullet
            bullet.GetComponent<EnemyBullet>().setDirection(direction);                             // shoot the bullet in that direction
        }
    }
}
