﻿using UnityEngine;

public class MaskController : MonoBehaviour
{
	//public PlayerHealth playerHealth;
	public GameObject[] mask;
	private float spawnTime = 5f;
	private float deSpawnTime = 8f;
	public Transform[] spawnPoints;
	public GameObject[] masksToRemove;
	int maskIndex;

	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		InvokeRepeating ("DeSpawn", deSpawnTime, deSpawnTime);
		maskIndex = Random.Range (0, mask.Length);

	}

	/*
	private void FixedUpdate()
	{
	//	if (GameUI.isDead || GameUI.isWin){
			CancelInvoke();
			enemiesToRemove = GameObject.FindGameObjectsWithTag("Enemy");
			foreach (GameObject enemy in enemiesToRemove)
			{
				enemy.SetActive(false);
			}
	//	}
	}*/


	void Spawn ()
	{

		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		maskIndex = Random.Range (0, mask.Length);


		Instantiate (mask[maskIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}

	void DeSpawn  () 
	{

		masksToRemove = GameObject.FindGameObjectsWithTag ("PickUp");

		/*
		 foreach (GameObject mask[maskInde] in masksToRemove)
		{
			//mask.SetActive(false);
			Destroy (mask[maskIndex]);
		}
		*/

		for(int i=0 ; i<masksToRemove.Length; i++){
			if (masksToRemove[i])
			{
				Destroy(masksToRemove[i]);
			}
		}
	}
}

