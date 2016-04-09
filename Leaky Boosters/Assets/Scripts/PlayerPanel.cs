using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour {

	public Image fill;
	public Image playerColor;
	public Text scoreText;
	int targetScore;
	float fillSpeed;

	public void Initialize(int score, Color player){
		playerColor.color = player;
		targetScore = score;
		fill.fillAmount = 0;
		FillPanel();
	}

	public void FillPanel(){
		StartCoroutine(FillCR());
	}

	IEnumerator FillCR(){
		float shownScore = 0;
		float maxScore = PlayerScores.GetMaxScore();
		float targetFill = targetScore / maxScore;
		while(shownScore < targetScore){
			yield return null;
			fill.fillAmount += targetFill * Time.deltaTime;
			shownScore += targetScore * Time.deltaTime;
			scoreText.text = Mathf.FloorToInt(shownScore).ToString("000000");
		}
		fill.fillAmount = targetFill;
		scoreText.text = targetScore.ToString("000000");
	}

}
