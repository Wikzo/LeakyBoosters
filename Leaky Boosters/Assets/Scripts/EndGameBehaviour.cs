using UnityEngine;
using System.Collections;
using InControl;

public class EndGameBehaviour : MonoBehaviour {

	float startTime;

	void Start(){
		startTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
		if(Time.time - 3 < startTime) return;
		for(int i = 0; i < InputManager.Devices.Count; i++){
			if(InputManager.Devices[i].Action1.WasPressed){
				GameController.Instance.RestartLevel();
			}
		}
	}
}
