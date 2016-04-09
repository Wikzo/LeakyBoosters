using UnityEngine;
using System.Collections;

public class BallGrabber : MonoBehaviour {


	GameObject curOwner;
	GameObject[] players;

	int maxFollowers = 100;
	int curFollowers = 100;

	[SerializeField]
	Vector3 offset = new Vector3 (0f, 2f, 0f);
	bool isCaught = false;

	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");

	}
	
	// Update is called once per frame
	void Update () {
	
		if (isCaught)
		{
			StayOnPlayer ();
		}

		if (Input.GetKeyDown(KeyCode.T))
		{
			print (curFollowers);
			LoseFollowers ();
		}

	}

	void StayOnPlayer()
	{
		transform.position = curOwner.transform.position + offset;
	}

	void SetCurOwner(int playerIndex)
	{
		curFollowers = maxFollowers;
		curOwner = GetPlayer(playerIndex);
	}

	void OnCollisionEnter(Collision other)
	{
		if (!isCaught && other.transform.CompareTag("Player"))
		{
			isCaught = true;
			SetCurOwner(other.transform.GetComponent<PlayerControl> ().GetPlayerIndex());
		}
	}

	GameObject GetPlayer(int playerIndex)
	{
		for (int i = 0; i < players.Length; i++)
		{
			if (players[i].GetComponent<PlayerControl>().GetPlayerIndex() == playerIndex)
			{
				return players [i];
			}
		}

		return null;
	}


	void LoseFollowers()
	{
		curFollowers -= 20;

		if (curFollowers <= 0)
		{
			isCaught = false; 
		}
	}
}
