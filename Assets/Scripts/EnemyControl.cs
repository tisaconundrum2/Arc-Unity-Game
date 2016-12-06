using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {
    GameObject scoreUIText;
    public float speed;
    public GameObject Explosion; //Explosion Prefab

    // Use this for initialization
    void Start () {
        speed = 2f;
        scoreUIText = GameObject.FindGameObjectWithTag("ScoreTextTag");
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 position = transform.position;                                            // setup a Vector2 position, make it equal to our current transform.position
        position = new Vector2(position.x, position.y - speed * Time.deltaTime);          // the position will change in the y direction, going down over time
        transform.position = position;                                                    // transform.position is now the position
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));                // The minimum world point is at 0,0 or the lower left hand corner
        if (transform.position.y < min.y)                                                 // if our transform.position goes below the world point's y axis
        {                                                                                 //
            Destroy(gameObject);                                                          // then destroy the object enemyShip.
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if there is a collision with PlayerShipTag or PlayerBulletTag, explode!
        if ((collision.tag == "PlayerShipTag" || collision.tag == "PlayerBulletTag"))
        {
            PlayExplosion();
            //100 points for every killed enemy
            scoreUIText.GetComponent<GameScore>().Score += 100;
            Destroy(gameObject); //destroy enemy ship
        }
    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);
        explosion.transform.position = transform.position;
    }
}
