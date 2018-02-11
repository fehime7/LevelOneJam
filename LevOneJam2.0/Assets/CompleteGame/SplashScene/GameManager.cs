using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Sprite mySprite;
	private bool secondSprite; 

	// Use this for initialization
	void Start () {
		secondSprite = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (secondSprite == false) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				gameObject.GetComponent<Image> ().sprite = mySprite;
				secondSprite = true;
			}
		}
		else if (secondSprite == true) {
			if (Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.Space)) {
				SceneManager.LoadScene (1);
			}
		}
	}
}