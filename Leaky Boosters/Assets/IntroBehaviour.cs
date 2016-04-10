using UnityEngine;
using System.Collections;
using DG.Tweening;
using InControl;
using UnityEngine.SceneManagement;

public class IntroBehaviour : MonoBehaviour {

	public Transform ball;
	public Transform pressToPlayText;
	public GameObject howtoPlay;

	// Use this for initialization
	void Start () {
		Camera.main.transform.DOLocalMoveZ(-10f, 8, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
		Camera.main.transform.DOLocalMoveY(1.39f, 5, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
		ball.DOLocalMoveY(4.6f, 10, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
		pressToPlayText.DOScale(Vector3.one * 0.95f, 1).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
	}
	
	// Update is called once per frame
	void Update () {
		ball.eulerAngles += Vector3.up * Time.deltaTime * 6;

		for(int i = 0; i < InputManager.Devices.Count; i++){
			if(InputManager.Devices[i].Action1.WasPressed){
				if(howtoPlay.activeInHierarchy){
					SceneManager.LoadScene(1);
				} else {
					howtoPlay.SetActive(true);
					howtoPlay.transform.DOLocalMoveY(0, 0.5f);
				}
			}
		}
	}
}
