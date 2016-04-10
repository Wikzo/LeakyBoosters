using UnityEngine;
using System.Collections;
using DG.Tweening;

public class IntroBehaviour : MonoBehaviour {

	public Transform ball;

	// Use this for initialization
	void Start () {
		Camera.main.transform.DOLocalMoveZ(-10f, 8, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
		Camera.main.transform.DOLocalMoveY(1.39f, 5, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
		ball.DOLocalMoveY(4.6f, 10, false).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
	}
	
	// Update is called once per frame
	void Update () {
		ball.eulerAngles += Vector3.up * Time.deltaTime * 6;
	}
}
