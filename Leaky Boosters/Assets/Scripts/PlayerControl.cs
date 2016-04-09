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
		


	void OnCollisionEnter(Collision col)
	{
	}
}
