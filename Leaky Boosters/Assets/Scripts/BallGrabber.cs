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
		PlayerScores.AddScore(curOwner.GetComponent<PlayerMovement>().playerNum, Mathf.FloorToInt(Time.time));
	}

	void SetCurOwner(int playerIndex)
	{
		curFollowers = maxFollowers;

		if(curOwner != null)
			curOwner.GetComponent<PlayerControl> ().SetHasBall (false);
		
		curOwner = GetPlayer(playerIndex);
		curOwner.GetComponent<PlayerControl> ().SetHasBall (true);
	}

	void OnCollisionEnter(Collision other)
	{
		if (!isCaught && other.transform.CompareTag("Player"))
		{
			isCaught = true;
			GetComponent<Rigidbody>().isKinematic = true;
			GetComponent<SphereCollider>().enabled = false;
			SetCurOwner(other.transform.GetComponent<PlayerMovement> ().playerNum);

		}
	}

	GameObject GetPlayer(int playerIndex)
	{
		for (int i = 0; i < players.Length; i++)
		{
			if (players[i].GetComponent<PlayerMovement>().playerNum == playerIndex)
			{
				return players [i];
			}
		}

		return null;
	}


	public void LoseFollowers()
	{
	//	curFollowers -= 20;

	//	if (curFollowers <= 0)
		//{
			GetComponent<Rigidbody>().isKinematic = false;
			GetComponent<SphereCollider>().enabled = false;
			float rnd = Random.Range (0f, 1f);
			GetComponent<Rigidbody> ().AddForce ( Vector3.up * 100f);
			StartCoroutine (Cooldown ());
		//	print ("I lost the ball");
			isCaught = false; 
	//	}
	}

	IEnumerator Cooldown()
	{
		yield return new WaitForSeconds (1f);
		GetComponent<SphereCollider>().enabled = true;

	}
}
