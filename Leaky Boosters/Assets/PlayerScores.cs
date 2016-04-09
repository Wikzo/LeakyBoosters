using UnityEngine;
using System.Collections;

public class PlayerScores {

	private static int[] scores;

	public static void Initialize(int playercount){
		scores = new int[playercount];
	}

	public static void AddScore(int player, int score){
		scores[player] += score;
	}

	public static int GetScore(int player){
		return scores[player];
	}
}
