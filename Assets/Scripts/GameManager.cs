using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner;
    public GameObject GameOver;
    public GameObject scoreUIText;
    public GameObject bgMove;

    public enum GameManagerState
    {
        Opening,
        GamePlay,
        GameOver,
    }
    GameManagerState GMState;

	// Use this for initialization
	void Start () {
        GMState = GameManagerState.Opening;
	}
    //Function to update the game manager state
    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                GameOver.SetActive(false);                                            // set the GameOver display to invisible
                playButton.SetActive(true);                                           // display the play button
                break;

            case GameManagerState.GamePlay:
                scoreUIText.GetComponent<GameScore>().Score = 0;                      //reset the score
                playButton.SetActive(false);                                          //hide the play button after it gets pressed
                playerShip.GetComponent<PlayerController>().Init();                   //make playership visible
                enemySpawner.GetComponent<EnemySpawn>().ScheduleEnemySpawner();       //Start enemy spawner
                break;

            case GameManagerState.GameOver:
                enemySpawner.GetComponent<EnemySpawn>().UnscheduleEnemySpawner();     //Stop enemy spawner
                bgMove.GetComponent<bgMove>().resetOffset();                          //Reset the offset for the quad mesh
                GameOver.SetActive(true);                                             //Display game over
                enemySpawner.GetComponent<EnemySpawn>().maxSpawnRateInSeconds = 2;    //Reset the Enemy Spawn Rate
                Invoke("ChangeToOpeningState", 2f);                                   //When gameover change back to open state
                break;
        }
    }

    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    //call this function when the play button is pressed
    public void StartGamePlay()
    {
        GMState = GameManagerState.GamePlay;
        UpdateGameManagerState();
    }

    //function changes manager state to opening state
    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }
}
