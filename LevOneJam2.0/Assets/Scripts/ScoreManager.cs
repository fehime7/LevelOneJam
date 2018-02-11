using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public int score;

	Text text;


	void Awake ()
	{
		text = GetComponent <Text> ();
		score = 0;
	}


	void Update ()
	{
		text.text = "      " + score;
	}
}
