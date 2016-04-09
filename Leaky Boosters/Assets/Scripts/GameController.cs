using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController Instance;
	private GameObject ballObj;
	private BallGrabber ballGrabInstance;


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
		Start ();
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
