using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
	//public PlayerHealth playerHealth;
	public GameObject enemy1;
	public GameObject enemy2;
    public GameObject enemy3;
    public float spawnTime = 5f;
	public Transform[] spawnPoints;
    public List<GameObject> enemies;

	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Spawn ()
	{
		/*if(playerHealth.currentHealth <= 0f)
        {
            return;
        }*/

		//int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		enemies.Add(Instantiate (enemy1, spawnPoints[0].transform.position, spawnPoints[0].transform.rotation));
        enemies.Add(Instantiate(enemy2, spawnPoints[1].transform.position, spawnPoints[1].transform.rotation));
        enemies.Add(Instantiate(enemy3, spawnPoints[2].transform.position, spawnPoints[2].transform.rotation));
	}
}

