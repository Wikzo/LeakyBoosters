using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController Instance;

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != null)
			Destroy (this.gameObject);

		DontDestroyOnLoad (this);
	}
		
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R))
		{
			RestartLevel();
		}
	}


	public void RestartLevel()
	{
		SceneManager.LoadScene (0);
	}
}
