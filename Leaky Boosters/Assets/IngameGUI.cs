using UnityEngine;
using System.Collections;

public class IngameGUI : MonoBehaviour {


	public static IngameGUI Instance;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != null)
			Destroy (this.gameObject);

		DontDestroyOnLoad (this);
	}

	public RectTransform playerPanelContainer;
	public PlayerPanel playerPanelPrefab;


	public void ShowFinalScreen(){
		while(playerPanelContainer.childCount > 0){
			Destroy(playerPanelContainer.GetChild(0).gameObject);
		}
		GetComponent<Canvas>().enabled = true;
		for(int i = 0; i < GameController.playerCount; i++){
			PlayerPanel newPanel = Instantiate(playerPanelPrefab) as PlayerPanel;
			newPanel.Initialize(Mathf.FloorToInt(PlayerScores.GetScore(i)), Color.green);
			newPanel.transform.SetParent(playerPanelContainer);
			newPanel.transform.localScale = Vector3.one;

		}
	}
}
