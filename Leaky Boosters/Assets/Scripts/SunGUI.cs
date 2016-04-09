using UnityEngine;
using System.Collections;
using DG.Tweening;


public class SunGUI : MonoBehaviour {

	public Light sunSource;
	public GameObject starPrefab;
	private GameObject[] stars; //Player 0, 1, 2, 3

	private float initialAngle;
	private float targetAngle = 18;

	public float timeLimit = 120;
	public float time;

	// Use this for initialization
	void Start () {
		initialAngle = sunSource.transform.eulerAngles.x;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		Vector3 newEulerAngles = sunSource.transform.eulerAngles;
		newEulerAngles.x = Mathf.Lerp(initialAngle, targetAngle, time / timeLimit);
		sunSource.transform.eulerAngles = newEulerAngles;
	}

	void InstantiateStars(){

	}
}
