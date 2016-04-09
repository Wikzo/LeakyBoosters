using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class IngameGUI : MonoBehaviour {


	public static IngameGUI Instance;
	public Text countdown;

	void Awake()
	{
		countdown.CrossFadeAlpha(0,0,true);
		if (Instance == null)
			Instance = this;
		else if (Instance != null)
			Destroy (this.gameObject);
	}

	public RectTransform playerPanelContainer;
	public PlayerPanel playerPanelPrefab;

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
		playerPanelContainer.gameObject.SetActive(true);
		for(int i = 0; i < GameController.playerCount; i++){
			PlayerPanel newPanel = Instantiate(playerPanelPrefab) as PlayerPanel;
			newPanel.Initialize(Mathf.FloorToInt(PlayerScores.GetScore(i)), GameController.Instance.players[i].playerColor);
			newPanel.transform.SetParent(playerPanelContainer);
			newPanel.transform.localScale = Vector3.one;

		}
	}
}
