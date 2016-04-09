using UnityEngine;
using System.Collections;
using DG.Tweening;


public class SunGUI : MonoBehaviour {

	private static SunGUI _instance;
	public static SunGUI instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = FindObjectOfType<SunGUI>();
			}
			return _instance;
		}
	}

	public Vector2[] starPositions;

	public Light sunSource;
	public GameObject starPrefab;
	public Canvas starCanvas;
	private GameObject[] stars = new GameObject[0]; //Player 0, 1, 2, 3

	private float initialAngle;
	private float targetAngle = 18;

	public float timeLimit = 120;
	public float time;
	bool gameOver = false;
	int countdown = 9001;

	void Awake()
	{
		if(_instance != null) Destroy(_instance);
		_instance = this;
	}

	// Use this for initialization
	void Start () {
		gameOver = false;
		initialAngle = sunSource.transform.eulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		float gameProgress =  time / timeLimit;
		Vector3 newEulerAngles = sunSource.transform.eulerAngles;
		newEulerAngles.x = Mathf.Lerp(initialAngle, targetAngle, gameProgress);
		sunSource.transform.eulerAngles = newEulerAngles;
		float maxScore = PlayerScores.GetMaxScore();
		for(int i = 0; i < stars.Length; i++){
			float newProximity = maxScore > 0 ? (PlayerScores.GetScore(i) / maxScore) * gameProgress : 0;
			stars[i].GetComponent<StarPosition>().UpdateProximity(newProximity);
		}
		if(gameProgress >= 1 && !gameOver){
			gameOver = true;
			IngameGUI.Instance.ShowFinalScreen();
		}
		int secondsLeft = Mathf.FloorToInt(timeLimit - time);
		if(secondsLeft > 0 && secondsLeft < 11 && countdown > secondsLeft ){
			countdown = secondsLeft;
			IngameGUI.Instance.CountdownNumber(countdown);
		}
	}

	public void InstantiateStars(int playercount){
		if(stars.Length > 0){
			for(int i = 0; i < stars.Length; i++){
				Destroy(stars[i]);
			}
		}
		stars = new GameObject[playercount];
		for(int i = 0; i < playercount; i++){
			GameObject newStar = Instantiate(starPrefab);
			newStar.transform.SetParent(starCanvas.transform);
			newStar.GetComponent<RectTransform>().anchoredPosition = starPositions[i];
			newStar.transform.localScale = Vector3.one;

			stars[i] = newStar;
		}
	}
}
