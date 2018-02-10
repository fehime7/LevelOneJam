using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour {

    public MonsterType mType;

    Transform player;
    UnityEngine.AI.NavMeshAgent nav;
	float timer = 0.0f;
	public float randomTimerDelay = 3.0f;
	public int maxX = 25;
	public int minX = -25;
	public int maxY = 25;
	public int minY = -25;

	void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }
    }

    void Update()
    {
		MonsterType playerType = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentType;

        switch (mType)
        {
            case MonsterType.Monster1:
				if (playerType == MonsterType.Monster3)
				{
					nav.SetDestination(player.position);
				}
				else if (playerType == MonsterType.Monster2)
				{
					Vector3 enemyDirection = 2 * gameObject.transform.position - player.position;
					nav.SetDestination(enemyDirection);
				}
				//if nor chasing neither escaping
				else {
					if (timer > randomTimerDelay)
					{
						nav.SetDestination(getRandomPosition());
						timer = 0;
					}
				}
                break;
            case MonsterType.Monster2:
                if (playerType == MonsterType.Monster1)
                {
                    nav.SetDestination(player.position);
                }
                else if (playerType == MonsterType.Monster3)
                {
                    Vector3 enemyDirection = 2 * gameObject.transform.position - player.position;
                    nav.SetDestination(enemyDirection);
                }
				//if nor chasing neither escaping
				else
				{
					if (timer > randomTimerDelay)
					{
						nav.SetDestination(getRandomPosition());
						timer = 0;
					}
				}
				break;
            case MonsterType.Monster3:
                if (playerType == MonsterType.Monster2)
                {
                    nav.SetDestination(player.position);
                }
                else if (playerType == MonsterType.Monster1)
                {
                    Vector3 enemyDirection = 2 * gameObject.transform.position-player.position;
                    nav.SetDestination(enemyDirection);
                }
				//if nor chasing neither escaping
				else
				{
					if (timer > randomTimerDelay)
					{
						nav.SetDestination(getRandomPosition());
						timer = 0;
					}
				}
				break;
        }
		timer += Time.deltaTime;
	}

	private Vector3 getRandomPosition() {
		float x;
		float y;
		float z;
		Vector3 pos;

		x = Random.Range(minX, maxX);
		y = 5;
		z = Random.Range(minY, maxY);
		pos = new Vector3(x, y, z);

		return pos;
	}
}
