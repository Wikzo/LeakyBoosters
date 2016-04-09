using UnityEngine;
using System.Collections;

public class StarPosition : MonoBehaviour {

	[Range(0,1)]
	public float proximity = 0;
	private Vector2 initPos;
	private static Vector2 targetPos = new Vector2(0, 9);

	// Use this for initialization
	void Start () {
		initPos = GetComponent<RectTransform>().anchoredPosition;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(initPos, targetPos, proximity);
	}

	public void UpdateProximity(float newProxitmity){
		proximity = newProxitmity;
	}
}
