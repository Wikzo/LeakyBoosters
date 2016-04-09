using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class PlayerJoinPrompt : MonoBehaviour {

	public int playerNum;
	public Text text;
	public Image background;

	public bool active = false;
	public bool ready = false;

	const string pressToJoin = "Press button to join!";
	const string needMorePlayers = "Need more players...";
	const string pressWhenReady = "Press button when ready!";
	const string waitingForPlayers = "Waiting for other players...";

	// Use this for initialization
	void Start () {
		Color bg = GameController.Instance.players[playerNum].playerColor;
		bg.a = 0.7f;
		background.color = bg;
	}
	
	// Update is called once per frame
	void Update () {
		InputDevice inputDevice = (InputManager.Devices.Count > playerNum) ? InputManager.Devices[playerNum] : null;
		if (inputDevice == null) return;
		if(inputDevice.Action1.WasPressed ){
			if(!active){
				active = true;
				if(IngameGUI.Instance.EnoughJoined()){
					text.text = pressWhenReady;
				} else {
					text.text = needMorePlayers;
				}
			} else {
				if(!ready){
					ready = true;
					if(IngameGUI.Instance.EnoughJoined()){
						text.text = waitingForPlayers;
						if(IngameGUI.Instance.AllReady()){
							IngameGUI.Instance.StartGame();
						}
					} else {
						text.text = needMorePlayers;
					}
				}
			}
		}
	}

	public void CheckPrompt(bool enoughJoined){ //dont go recursive yo

		if(!active){
			text.text = pressToJoin;
		} else {
			if(enoughJoined){
				text.text = ready ? waitingForPlayers : pressWhenReady;
			} else {
				ready = false;
				text.text = needMorePlayers;
			}
		}
	}

	public bool IsReady(){
		return !active || ready;
	}
}
