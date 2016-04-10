using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CameraControl : MonoBehaviour {


	//[SerializeField]
	//float dampTime; 

    public List<AudioClip> ScreenShakeSounds;
    private AudioSource audioSource;
	PlayerMovement[] playerObjs;
	//float[] xValues;
	//float[] zValues;
	//float maxX = 3f;
	//float maxZ = -16f;
	//float minX = -3f;
	//float minZ = -26f; 

	//Vector3 maxPosition, minPosition = Vector3.zero;

	//Vector3 offset; 

	Vector3 startPos;

	/*[SerializeField]
	private GameObject centerTmp;
	[SerializeField]
	private GameObject centerTmp1;*/

	private Transform shakeTransform;
	private int isShaking = 0;


	// Use this for initialization
	void Start () {
		//playerObjs = GameObject.FindGameObjectsWithTag ("Player");
/*		xValues = new float[playerObjs.Length];
		zValues = new float[playerObjs.Length];


		for (int i = 0; i < playerObjs.Length; i++)
		{
			xValues[i] = (playerObjs [i].transform.position.x);
			zValues[i] =  (playerObjs [i].transform.position.z);
		}*/

		startPos = transform.position;
		shakeTransform = this.transform;

	    audioSource = GetComponent<AudioSource>();



	}
	
	// Update is called once per frame
	void LateUpdate () {
	
/*		if (Input.GetKey(KeyCode.Space))
		{
			ShakeScreen (Random.Range(0.1f, 0.3f));
		} else 
		{
			transform.position = startPos;
		}*/

		DynamicCamera ();
	}


	public void ShakeScreen(float shakeTime)
	{
		isShaking = 1;
		shakeTransform.DOShakePosition (shakeTime, 2f, 20, Random.Range (10, 80));
        //transform.position.DOShakePosition (shakeTime, 2f, 10, Random.Range (10, 80));

        audioSource.PlayOneShot(ScreenShakeSounds[Random.Range(0, ScreenShakeSounds.Count)]);
        StartCoroutine (Cooldown (shakeTime));
	}
		
	IEnumerator Cooldown(float shakeTime)
	{
		yield return new WaitForSeconds (0.2f);
		isShaking = 0;
	}
	//Vector3 distance = Vector3.zero;

	public void DynamicCamera()
	{
		//distance = Vector3.zero;

/*		for (int i = 0; i < playerObjs.Length; i++)
		{
			if (i != playerObjs.Length - 1)
				distance += playerObjs [i + 1] - playerObjs [i];	
			else 
				distance += playerObjs [0] - playerObjs [i];	
		}*/

/*		for (int i = 0; i < playerObjs.Length; i++)
		{
			xValues[i] = (playerObjs [i].transform.position.x);
			zValues[i] =  (playerObjs [i].transform.position.z);
		}*/

		//centerPos = CalculateCenter ();

		transform.position = Vector3.Lerp( transform.position, CalculateCenter() + (isShaking * shakeTransform.position) + startPos, Time.deltaTime);
		//transform.LookAt (CalculateCenter ());
		//centerPos = CalculateCenter ();
	}

//	public Vector3 centerPos;
	Vector3 CalculateCenter()
	{
/*		maxX = Mathf.Max (xValues[0], xValues[1]);
		maxX = Mathf.Max (maxX, xValues[2]);
		maxX = Mathf.Max (maxX, xValues[3]);
		minX = Mathf.Min (xValues[0], xValues[1]);
		minX = Mathf.Min (minX, xValues[2]);
		minX = Mathf.Min (minX, xValues[3]);


		maxZ = Mathf.Max (zValues[0], zValues[1]);
		maxZ = Mathf.Max (maxZ, zValues[2]);
		maxZ = Mathf.Max (maxZ, zValues[3]);
		minZ = Mathf.Min (zValues[0], zValues[1]);
		minZ = Mathf.Min (minZ, zValues[2]);
		minZ = Mathf.Min (minZ, zValues[3]);

		maxPosition = new Vector3 (maxX, 0, maxZ);
		minPosition = new Vector3 (minX, 0, minZ);*/


		//centerTmp.transform.position = maxPosition;
		//centerTmp1.transform.position = minPosition;


/*		for (int i = 0; i < xValues.Length; i++)
		{
			if (i != xValues.Length - 1)
			{
				maxX = Mathf.Max (xValues[]);
				
			}
			
		}*/

		Vector3 result = Vector3.zero;
		playerObjs = GameController.Instance.activePlayers;
		for (int i = 0; i < playerObjs.Length; i++)
		{
			result += playerObjs [i].transform.position; 
		}

		result = result / 4; //(minPosition + maxPosition) * 0.5f; // + startPos;
		result.z = 0f;

		//result = new Vector3 ();
	//	result.x = Mathf.Max(result.x, minX );
	//	result.x = Mathf.Min (result.x, maxX);
	//	result.x = Mathf.Clamp (result.x, minX + startPos.x, maxX + startPos.x);
		//result.z = Mathf.Clamp (result.z, minZ + startPos.z, maxZ + startPos.z);
	//	result.z = startPos.z; 
	//	result.z = Mathf.Max(result.z, minZ + startPos.z);
	//	result.z = Mathf.Min (result.z, maxZ + startPos.z);
	//	result.y = startPos.y;

		//transform.forward = Vector3.Cross (result, Vector3.forward);

		return result;
	}

}
