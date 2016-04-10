using UnityEngine;
using System.Collections;

public class PlayerScores {

	private static float[] scores;

	public static void Initialize(int playercount){
		scores = new float[playercount];
	}

	public static void AddScore(int player, float score)
	{
	    scores[player] += score;
	}

    public static float GetScore(int player){
		return scores[player];
	}

	public static float GetMaxScore(){
		if(scores == null || scores.Length < 1) return 0;
		float max = 0;
		for(int i = 0; i < scores.Length; i ++){
			if(scores[i] > max){
				max = scores[i];
			}
		}
		return max;
	}
}
