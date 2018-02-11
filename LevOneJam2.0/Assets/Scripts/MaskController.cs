using UnityEngine;

public class MaskController : MonoBehaviour
{
	//public PlayerHealth playerHealth;
	public GameObject[] mask;
	private float spawnTime = 4f;
	private float deSpawnTime = 8f;
	public Transform[] spawnPoints;
	public GameObject[] masksToRemove;
	private bool[] isSpawned = { false,false,false};
    private float timer = 0;

	void Start ()
	{
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
		InvokeRepeating ("DeSpawn", deSpawnTime, deSpawnTime);
	}

    private void Update()
    {
        timer += Time.deltaTime;
    }

    void Spawn ()
	{
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);

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
			}
        }

        isSpawned[0] = false;
        isSpawned[1] = false;
        isSpawned[2] = false;
    }
}


