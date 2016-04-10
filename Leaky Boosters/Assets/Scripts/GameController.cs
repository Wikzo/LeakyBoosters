using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController Instance;
	public BallGrabber ballGrabInstance;
	public static int playerCount;
	public PlayerMovement[] players;
	public PlayerMovement[] activePlayers;

	void Awake()
	{
		if (Instance != null){
			Destroy (Instance.gameObject);
		}
		
		Instance = this;

		DontDestroyOnLoad (this);
	}
		

	public void InstantiateGame(int[] playerNums){
		playerCount = playerNums.Length;
		activePlayers = new PlayerMovement[playerCount];
		for(int i = 0; i < playerCount; i++){
			activePlayers[i] = players[playerNums[i]];
			activePlayers[i].gameObject.SetActive(true);
		}
		PlayerScores.Initialize(playerCount);
		SunGUI.instance.InstantiateStars(playerCount);
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.R))
		{
			RestartLevel();
		}

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();


	}


	public void RestartLevel()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public BallGrabber GetBallGrabber()
	{
		return ballGrabInstance;
	}
}
