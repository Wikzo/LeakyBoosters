using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController Instance;
	private GameObject ballObj;
	private BallGrabber ballGrabInstance;
	public static int playerCount;
	public PlayerMovement[] players;
	public PlayerMovement[] activePlayers;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != null)
			Destroy (this.gameObject);

		DontDestroyOnLoad (this);
	}
		
	void Start()
	{
		if (ballObj == null)
			ballObj = GameObject.FindGameObjectWithTag ("Ball");

		ballGrabInstance = ballObj.GetComponent<BallGrabber> ();
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

		if (ballObj == null)
		{
			print ("Missing ball");
			return;
		}
		if(Input.GetKeyDown(KeyCode.R))
		{
			RestartLevel();
		}


	}


	public void RestartLevel()
	{
		SceneManager.LoadScene (0);
	}

	public GameObject GetBall()
	{
		return ballObj;
	}

	public BallGrabber GetBallGrabber()
	{
		return ballGrabInstance;
	}
}
