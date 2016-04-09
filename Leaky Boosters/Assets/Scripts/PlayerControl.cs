using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	[SerializeField]
	private int playerIndex; 

	private bool hasBall = false;


	public int GetPlayerIndex()
	{
		return playerIndex;
	}
		

	public void SetHasBall(bool aVal)
	{
		hasBall = aVal;
	}

	void OnCollisionEnter(Collision col)
	{
		if (hasBall && col.transform.CompareTag("Player"))
		{
			GameController.Instance.GetBall().GetComponent<BallGrabber>().LoseFollowers ();
			hasBall = false;
			//print ("Got hit");
		}
	}
}
