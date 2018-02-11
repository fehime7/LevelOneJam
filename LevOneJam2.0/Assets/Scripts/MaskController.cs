using UnityEngine;

public class MaskController : MonoBehaviour
{
	//public PlayerHealth playerHealth;
	public GameObject[] mask;
	private float spawnTime = 2f;
	private float deSpawnTime = 10f;
	public Transform[] spawnPoints;
	public GameObject[] masksToRemove;
	private bool[] isSpawned = { false,false,false};

	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		InvokeRepeating ("DeSpawn", deSpawnTime, deSpawnTime);
	}

	void Spawn ()
	{
		int spawnPointIndex = Random.Range (0, spawnPoints.Length-1);

		if (!isSpawned[spawnPointIndex])
		{
			Instantiate(mask[spawnPointIndex], spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
			isSpawned[spawnPointIndex] = true;
		}
	}

	void DeSpawn  () 
	{
		masksToRemove = GameObject.FindGameObjectsWithTag ("PickUp");

		for(int i=0 ; i<masksToRemove.Length; i++){
			if (masksToRemove[i])
			{
				Destroy(masksToRemove[i]);
				isSpawned[i] = false;
			}
		} 
	}
}


