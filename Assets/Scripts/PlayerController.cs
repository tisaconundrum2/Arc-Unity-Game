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
    private Vector2 mousePosition;
    private Touch fingerPosition;
    private Vector2 touchPosition;
    private float rateOfFirePointer = 0;
    public float moveSpeed = 0.05f;
    public float rateOfFire = 0.2f;
    public GameObject Explosion; //Explosion Prefab

    //Reference to the lives UI text
    public Text LivesUIText;
    const int MaxLives = 9; // maximum number of lives... 9 is better than the 3 that was originally recommended
    int lives; // current number of lives that are available to us

    public void Init()
    {
        lives = MaxLives;
        LivesUIText.text = lives.ToString();
        transform.position = Vector2.zero;
        gameObject.SetActive(true);
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Space) || Input.touchCount > 0)
        {
            FireBullet();
        }
  
        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
        }


        if(Input.touchCount > 0)
        { 
            fingerPosition = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(new Vector2(fingerPosition.position.x, fingerPosition.position.y + 500));
            transform.position = Vector2.Lerp(transform.position, touchPosition, moveSpeed);
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detect collision with enemy ship, or with enemy bullet
        /*if ((collision.tag == "EnemyShipTag") || (collision.tag == "EnemyBulletTag")){*/
        if (collision.tag == "EnemyShipTag" || collision.tag == "EnemyBulletTag") {
            PlayExplosion();
            lives--;
            LivesUIText.text = lives.ToString();
            if (lives <= 0)
            {
                // change the game manager state here
                GameManager.GetComponent<GameManager>().SetGameManagerState(global::GameManager.GameManagerState.GameOver);
                //hide the player ship when they die
                gameObject.SetActive(false);
            }
        }

    }

    void FireBullet()
    {
        //play laser sound

        if (Time.time > rateOfFirePointer)
        {
            rateOfFirePointer = Time.time + rateOfFire;          //instantiate the first bullet
            GameObject bullet = (GameObject)Instantiate(PlayerBullet);
            GetComponent<AudioSource>().Play();
            bullet.transform.position = PlayerBulletPos.transform.position;//set the bullet init position

            rateOfFirePointer = Time.time + rateOfFire;          //instantiate the first bullet
            GameObject bullet1 = (GameObject)Instantiate(PlayerBullet);
            GetComponent<AudioSource>().Play();
            bullet1.transform.position = PlayerBulletPos1.transform.position;

            rateOfFirePointer = Time.time + rateOfFire;          //instantiate the first bullet
            GameObject bullet2 = (GameObject)Instantiate(PlayerBullet);
            GetComponent<AudioSource>().Play();
            bullet2.transform.position = PlayerBulletPos2.transform.position;
        }
    }
    
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);
        explosion.transform.position = transform.position;
    }
}

