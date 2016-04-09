using UnityEngine;
using System.Collections;
using InControl;

public class EndGameBehaviour : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < InputManager.Devices.Count; i++){
			if(InputManager.Devices[i].Action1.WasPressed){
				GameController.Instance.RestartLevel();
			}
		}
	}
}
