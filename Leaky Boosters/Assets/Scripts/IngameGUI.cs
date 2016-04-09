using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class IngameGUI : MonoBehaviour {


	public static IngameGUI Instance;
	public Text countdown;

	void Awake()
	{
		if (Instance != null)
			Destroy (Instance.gameObject);
		if (Instance == null)
			Instance = this;
		countdown.CrossFadeAlpha(0,0,true);
	}

	public RectTransform playerPanelContainer;
	public PlayerPanel playerPanelPrefab;
	public Transform startGamePanel;
	public PlayerJoinPrompt[] playerJoinPrompts;
	public GameObject endGameScreen;

	public void CountdownNumber(int secondsLeft){
		countdown.CrossFadeAlpha(0.5f,0,true);
		countdown.CrossFadeAlpha(0, 0.5f, true);
		countdown.text = secondsLeft.ToString();
		countdown.transform.DOShakeRotation(0.2f, Vector3.forward * (10 - secondsLeft) * 9, (10 - secondsLeft) * 5, 90);
	}

	public void ShowFinalScreen(){
		while(playerPanelContainer.childCount > 0){
			Destroy(playerPanelContainer.GetChild(0).gameObject);
		}
		endGameScreen.SetActive(true);
		for(int i = 0; i < GameController.playerCount; i++){
			PlayerPanel newPanel = Instantiate(playerPanelPrefab) as PlayerPanel;
			newPanel.Initialize(Mathf.FloorToInt(PlayerScores.GetScore(i)), GameController.Instance.activePlayers[i].playerColor);
			newPanel.transform.SetParent(playerPanelContainer);
			newPanel.transform.localScale = Vector3.one;

		}
	}

	public void StartGame(){
		StartCoroutine(StartGameCountDown());
	}

	IEnumerator StartGameCountDown(){
		countdown.CrossFadeAlpha(1,0,true);
		float secondsToStart = 3.9f;
		countdown.text = Mathf.FloorToInt(secondsToStart).ToString();
		startGamePanel.DOLocalMoveY(-640, 0.5f, true);
		GameController.Instance.InstantiateGame(ActivePlayers());
		while(secondsToStart > 0){
			yield return null;
			secondsToStart -= Time.deltaTime;
			countdown.text = Mathf.FloorToInt(secondsToStart).ToString();
		}
		countdown.text = "GO!";
		countdown.CrossFadeAlpha(0,2f,true);
		SunGUI.gameOver = false;


	}

	public bool EnoughJoined(){
		int ready = 0;
		for(int i = 0; i < playerJoinPrompts.Length; i++){
			if(playerJoinPrompts[i].active){
				ready++;
			}
		}
		bool enoughReady = ready > 1;
		for(int i = 0; i < playerJoinPrompts.Length; i++){
			playerJoinPrompts[i].CheckPrompt(enoughReady);
		}
		return enoughReady;
	}

	public bool AllReady(){
		for(int i = 0; i < playerJoinPrompts.Length; i++){
			if(playerJoinPrompts[i].active){
				if(!playerJoinPrompts[i].IsReady()){
					return false;
				}
			}
		}
		return true;
	}

	int[] ActivePlayers(){
		int activeCount = 0;
		ArrayList listOfNumbers = new ArrayList();
		for(int i = 0; i < playerJoinPrompts.Length; i++){
			if(playerJoinPrompts[i].ready){
				activeCount++;
				listOfNumbers.Add(playerJoinPrompts[i].playerNum);
			}
		}
		int[] activePlayers = new int[activeCount];
		for(int i = 0; i < listOfNumbers.Count; i++){
			activePlayers[i] = (int)listOfNumbers[i];
		}
		return activePlayers;
	}
}
