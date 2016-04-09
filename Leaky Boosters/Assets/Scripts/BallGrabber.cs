using UnityEngine;
using System.Collections;

public class BallGrabber : MonoBehaviour {


	GameObject curOwner;
	GameObject[] players;

	int maxFollowers = 100;
	int curFollowers = 100;

    [SerializeField] private Vector3 offset;
	bool isCaught = false;
	[SerializeField]
	private float forceMultiplier; 

	Rigidbody myBody;
	SphereCollider myCol;

    public GameObject ShockWavePrefab;

    private SpawnZones spawnZone;

    private Renderer renderer;

	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("Player");

		print (players [0]);
		myBody = GetComponent<Rigidbody> ();
		myCol = GetComponent<SphereCollider> ();

	    spawnZone = FindObjectOfType<SpawnZones>();

	    renderer = GetComponent<Renderer>();

        offset = new Vector3(0,transform.localScale.y / 2,0);



	}
	
	// Update is called once per frame
	void Update ()
	{

	    if (transform.position.y < spawnZone.transform.position.y + offset.y)
	    {
            BallReset();
            transform.position = spawnZone.GetRandomBallSpawnZone();
        }

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
		transform.position = curOwner.transform.position + offset * 2.6f;
		PlayerScores.AddScore(curOwner.GetComponent<PlayerMovement>().playerNum, Mathf.FloorToInt(Time.deltaTime * 1000));
	}

	void SetCurOwner(int playerIndex)
	{
		curFollowers = maxFollowers;

		if(curOwner != null)
			curOwner.GetComponent<PlayerControl> ().SetHasBall (false);
		
		curOwner = GetPlayer(playerIndex);
		curOwner.GetComponent<PlayerControl> ().SetHasBall (true);

		ActivateForceField(curOwner.GetComponent<Rigidbody>());
		Camera.main.GetComponent<CameraControl> ().ShakeScreen (Random.Range(0.2f, 0.4f));
	}

    public float ForceFieldPower = 10f;
    public float ForceFieldRadius = 5;
	private void ActivateForceField(Rigidbody playerBody)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, ForceFieldRadius);
        foreach (Collider hit in colliders)
        {
            if (!hit.CompareTag("Player"))
                continue;

            Rigidbody rb = hit.GetComponent<Rigidbody>();

			//print (playerBody);

			if (rb != null && rb != playerBody)
            {
                Instantiate(ShockWavePrefab, transform.position, Quaternion.identity);
                rb.AddExplosionForce(ForceFieldPower, transform.position, ForceFieldRadius, 2);
            }
        }
    }

	void OnCollisionEnter(Collision other)
	{
		if (!isCaught && other.transform.CompareTag("Player"))
		{
			isCaught = true;
			myBody.isKinematic = true;
			myCol.enabled = false;
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
		myBody.isKinematic = false;

		// Get a pseudorandom direction above a certain threshold. Ssssssh gamejam code.. 
		int rnd = Random.Range (0, 2);
		if (rnd == 0)
		{
			myBody.AddForce ( new Vector3(Random.Range(0.6f, 1f), 1, Random.Range(0.6f, 1f)) * forceMultiplier * 100f);
		} else
		{
			myBody.AddForce ( new Vector3(Random.Range(-0.6f, -1f), 1, Random.Range(-0.6f, -1f)) * forceMultiplier * 100f);
		}

		//myCol.enabled = true;
		StartCoroutine (Cooldown ());
		curOwner = null;
		isCaught = false; 
	}

    private void BallReset()
    {
        myCol.enabled = true;
        curOwner = null;
        isCaught = false;
        myBody.isKinematic = false;

        StartCoroutine(BlinkRespawn(6));

    }

    IEnumerator Cooldown()
	{
		yield return new WaitForSeconds (0.4f);
		myCol.enabled = true;
	}

    IEnumerator BlinkRespawn(int count)
    {
        renderer.enabled = !renderer.enabled;
        yield return new WaitForSeconds(0.2f);

        if (count - 1 > 0)
            StartCoroutine(BlinkRespawn(count - 1));
        else
            renderer.enabled = true;
    }
}
