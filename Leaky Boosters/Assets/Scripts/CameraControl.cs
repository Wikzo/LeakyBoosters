using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CameraControl : MonoBehaviour {


	[SerializeField]
	float dampTime; 

	GameObject[] playerObjs;

	Vector3 startPos;



	// Use this for initialization
	void Start () {
		playerObjs = GameObject.FindGameObjectsWithTag ("Player");
		startPos = transform.position;

	
	}
	
/*	// Update is called once per frame
	void LateUpdate () {
	
		if (Input.GetKey(KeyCode.Space))
		{
			ShakeScreen (Random.Range(0.1f, 0.3f));
		} else 
		{
			transform.position = startPos;
		}
	}


	public void ShakeScreen(float shakeTime)
	{
		transform.DOShakePosition (shakeTime, 2f, 10, Random.Range (10, 80));
	}

	Vector3 distance = Vector3.zero;
	public void DynamicCamera()
	{
		distance = Vector3.zero;

		for (int i = 0; i < playerObjs.Length; i++)
		{
			if (i != playerObjs.Length - 1)
				distance += playerObjs [i + 1] - playerObjs [i];	
			else 
				distance += playerObjs [0] - playerObjs [i];	
		}


	}*/

}
