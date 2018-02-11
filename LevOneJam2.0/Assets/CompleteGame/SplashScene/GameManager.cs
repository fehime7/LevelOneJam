using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public Image myImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
			myImage.sprite = Resources.Load<Sprite>("SpriteName");
		if(Input.GetKeyDown(KeyCode.Return))
			SceneManager.LoadScene(1);
	}
}