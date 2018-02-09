using UnityEngine;

public class MonsterManager : MonoBehaviour
{
	//public PlayerHealth playerHealth;
	public GameObject enemy;
	public float spawnTime = 3f;
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

		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

		Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
	}
}

