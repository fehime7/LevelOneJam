using UnityEngine;

public class MonsterManager : MonoBehaviour
{
	//public PlayerHealth playerHealth;
	public GameObject enemy1;
	public GameObject enemy3;
	public GameObject enemy2;
	public float spawnTime = 5f;
	public Transform[] spawnPoints;
	public GameObject[] enemiesToRemove;

	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
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
		/*if(playerHealth.currentHealth <= 0f)
        {
            return;
        }*/

		//int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		Vector3 spawnPoint1 = GameObject.FindGameObjectWithTag("Spawning1").transform.position;
		Vector3 spawnPoint2 = GameObject.FindGameObjectWithTag("Spawning2").transform.position;
		Vector3 spawnPoint3 = GameObject.FindGameObjectWithTag("Spawning3").transform.position;
		Quaternion spawnPointRot1 = GameObject.FindGameObjectWithTag("Spawning1").transform.rotation;
		Quaternion spawnPointRot2 = GameObject.FindGameObjectWithTag("Spawning2").transform.rotation;
		Quaternion spawnPointRot3 = GameObject.FindGameObjectWithTag("Spawning3").transform.rotation;
		Instantiate (enemy1, spawnPoint1, spawnPointRot1);
		Instantiate (enemy2, spawnPoint2, spawnPointRot2);
		Instantiate (enemy3, spawnPoint3, spawnPointRot3);
	}
}

