using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameObject GameManager;
    public GameObject PlayerBullet;
    public GameObject PlayerBulletPos;
    public GameObject PlayerBulletPos1;
    public GameObject PlayerBulletPos2;
    public GameObject enemySpawn;
    private Vector2 mousePosition;
    private Touch fingerPosition;
    private Vector2 touchPosition;
    private float rateOfFirePointer = 0;
    public float moveSpeed = 0.05f;
    public float rateOfFire = 0.2f;
    public GameObject Explosion; //Explosion Prefab

    public Text LivesUIText; //Reference to the lives UI text
    const int MaxLives = 9;  // maximum number of lives... 9 is better than the 3 that was originally recommended
    int currentLives;        // current number of lives that are available to us

    public void Init(){
        currentLives = MaxLives;
        LivesUIText.text = currentLives.ToString();
        transform.position = Vector2.zero;
        gameObject.SetActive(true);
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Space) || Input.touchCount > 0){
            FireBullet();
        }
  
        if (Input.GetMouseButton(0)){
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }


        if(Input.touchCount > 0){ 
            fingerPosition = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(new Vector2(fingerPosition.position.x, fingerPosition.position.y + 500));
            transform.position = Vector2.Lerp(transform.position, touchPosition, moveSpeed);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "EnemyShipTag" || collision.tag == "EnemyBulletTag") {                                             // Detect collision with enemy ship, or with enemy bullet
            PlayExplosion();                                                                                                    // display some fireworks when we collide
            currentLives--;                                                                                                     // decrement our life
            LivesUIText.text = currentLives.ToString();                                                                         // update the Lives UI
            //enemySpawn.GetComponent<EnemySpawn>().maxSpawnRateInSeconds += 0.1f;                                                // increase time it takes to spawn a new enemy

            if (currentLives <= 0)                                                                                              // if we are at or fall below 0 lives
            {                                                                                                                   //
                GameManager.GetComponent<GameManager>().SetGameManagerState(global::GameManager.GameManagerState.GameOver);     // change the game manager state here
                gameObject.SetActive(false);                                                                                    // hide the player ship when they die
            }
        }

    }

    void FireBullet(){
        if (Time.time > rateOfFirePointer){
            rateOfFirePointer = Time.time + rateOfFire;                         // instantiate the first bullet
            GameObject bullet = (GameObject)Instantiate(PlayerBullet);          // Create a new bullet in the world
            GetComponent<AudioSource>().Play();                                 // When the bullet is fired, have it play a sound
            bullet.transform.position = PlayerBulletPos.transform.position;     // move the bullet across the world view

            rateOfFirePointer = Time.time + rateOfFire;
            GameObject bullet1 = (GameObject)Instantiate(PlayerBullet);
            GetComponent<AudioSource>().Play();
            bullet1.transform.position = PlayerBulletPos1.transform.position;

            rateOfFirePointer = Time.time + rateOfFire;
            GameObject bullet2 = (GameObject)Instantiate(PlayerBullet);
            GetComponent<AudioSource>().Play();
            bullet2.transform.position = PlayerBulletPos2.transform.position;
        }
    }
    
    void PlayExplosion(){
        GameObject explosion = (GameObject)Instantiate(Explosion);
        explosion.transform.position = transform.position;
    }
}

