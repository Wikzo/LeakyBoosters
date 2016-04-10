using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class EndGameBehaviour : MonoBehaviour {

	float startTime;

	Text pressRestartText;

	void Start(){
		startTime = Time.time;
		pressRestartText = transform.GetChild (1).GetComponent<Text> ();
		StartCoroutine (PressToContinue ());
	}

	// Update is called once per frame
	void Update () {
		if(Time.time - 3 < startTime) return;
		for(int i = 0; i < InputManager.Devices.Count; i++){
			if(InputManager.Devices[i].Action1.WasPressed){
				StopCoroutine (PressToContinue ());
				GameController.Instance.RestartLevel();
			}
		}
	}

	IEnumerator PressToContinue()
	{
		yield return new WaitForSeconds (3f);
		pressRestartText.gameObject.SetActive (true);
		pressRestartText.color = new Color(0,0,0,0);

		float t = 0f;

		while(t < 1f)
		{
			t += Time.deltaTime;
			pressRestartText.color = new Color(0,0,0,t);
			yield return null;
		}

		while(true)
		{
			t = 1f;

			while(t > 0f)
			{
				t -= Time.deltaTime;
				pressRestartText.color = new Color(0,0,0,t);
				yield return null;
			}

			t = 0f;

			while(t < 1f)
			{
				t += Time.deltaTime;
				pressRestartText.color = new Color(0,0,0,t);
				yield return null;
			}

		}
	}
}
