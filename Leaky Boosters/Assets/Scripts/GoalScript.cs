using UnityEngine;
using System.Collections;

public class GoalScript : MonoBehaviour {

	Renderer ren;
	Color normalCol = new Color(0.5f, 0.5f, 0.5f);
	Color goalCol = new Color(0, 0, 1);
	// Use this for initialization
	void Start () {
		ren = GetComponent<Renderer> ();
		ren.material.color = normalCol;

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Ball") ) 
		{
			print ("you scored");
			ren.material.color = goalCol;
			StartCoroutine (resetColor ());
		}
	}


	IEnumerator resetColor()
	{
		yield return new WaitForSeconds (2f);
		ren.material.color = normalCol;
	}
}
