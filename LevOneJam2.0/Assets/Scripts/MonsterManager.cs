using UnityEngine;

public class MonsterManager : MonoBehaviour
{
	//public PlayerHealth playerHealth;
	public GameObject enemy1;
	public GameObject enemy2;
    public GameObject enemy3;
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
		Instantiate (enemy1, spawnPoints[0].transform.position, spawnPoints[0].transform.rotation);
		Instantiate (enemy2, spawnPoints[1].transform.position, spawnPoints[1].transform.rotation);
		Instantiate (enemy3, spawnPoints[2].transform.position, spawnPoints[2].transform.rotation);
	}
}

