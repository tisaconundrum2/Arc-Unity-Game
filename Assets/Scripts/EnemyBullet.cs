using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    float speed; //bullet speed
    Vector2 _direction; //direction of bullet
    bool isReady; //Is the bullet's direction set?

    private void Awake()
    {
        speed = 5f;
        isReady = false;
    }


    // Use this for initialization

    public void setDirection(Vector2 direction)
    {
        //set the direction of bullet normalized
        _direction = direction.normalized;
        isReady = true;
    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (isReady)
        {
            Vector2 pos = transform.position;                                                     // setup a Vector2 pos, which is our current transform.position
            pos += _direction * speed * Time.deltaTime;                                           // have the pos move in _direction multiplied by speed over Time
            transform.position = pos;                                                             // have transform.position be our new current position
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));                    // this is our minimum world point, it is in the lower left hand corner
            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));                    // this is our maximum world point, it is in the upper right hand corner
            if ((transform.position.x < min.x) || (transform.position.x > max.x) ||               // if the bullet object goes outside any of the world points, specified above
                (transform.position.y < min.y) || (transform.position.y > max.y))                 //
            {                                                                                     //
                Destroy(gameObject);                                                              // destroy the Bullet object
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerShipTag" || collision.tag == "PlayerBulletTag")               // if the bullet hits the player or a the player's bullet
        {                                                                                         //
            Destroy(gameObject);                                                                  //Destroy the Bullet object
        }
    }
}
