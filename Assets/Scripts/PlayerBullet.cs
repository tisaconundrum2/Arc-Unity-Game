using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

    public float speed;
    // Use this for initialization
	void Start () {
        speed = 8f;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 position = transform.position;                                              // get the Bullet's current position
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);            // compute the bullet's new position
        transform.position = position;                                                      // update the bullet's position
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));                  // this is our minimum world point, it is in the lower left hand corner
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));                  // this is our maximum world point, it is in the upper right hand corner
        if ((transform.position.x < min.x) || (transform.position.x > max.x) ||             // if the bullet object goes outside any of the world points, specified above
            (transform.position.y < min.y) || (transform.position.y > max.y))               //
        {                                                                                   //
            Destroy(gameObject);                                                            // destroy the Bullet object
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detect collision of Enemy Ship with player Bullet or enemy bullet
        if (collision.tag == "EnemyShipTag" || collision.tag == "EnemyBulletTag")
        {
            Destroy(gameObject); // destroy the bullet
        }
    }
}
