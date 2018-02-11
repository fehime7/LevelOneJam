using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour {

    public MonsterType mType;

    bool playerDetected = false;
    Transform closestChar;
    Transform player;
    UnityEngine.AI.NavMeshAgent nav;
    private GameObject[] reactions;
    private MonsterManager monManager;
    private float detectDistance = 20;

	float timer = 0.0f;
	public float randomTimerDelay = 3.0f;
	public int maxX = 25;
	public int minX = -25;
	public int maxY = 25;
	public int minY = -25;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            closestChar = player;
            nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        reactions = new GameObject[2];

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform reactionsTrans = gameObject.transform.GetChild(i).transform;

            if (reactionsTrans.name == "Reactions")
            {
                for (int j = 0; j < reactionsTrans.childCount; j++)
                {
                    GameObject modelObj = reactionsTrans.GetChild(j).gameObject;

                    reactions[j] = modelObj;
					modelObj.SetActive(false);
                }
            }
        }

        monManager = (MonsterManager)FindObjectOfType(typeof(MonsterManager));
        InvokeRepeating("UpdateClosestChar", 0, 1.0f);
    }

    void Update()
    {
        this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 100000, 0), ForceMode.Impulse);

        if (closestChar)
		{
            MonsterType playerType = MonsterType.None;

            if (closestChar.GetComponent<PlayerMovement>())
            {
                playerType = closestChar.GetComponent<PlayerMovement>().currentType;
            } else if (closestChar.GetComponent<MonsterMovement>())
            {
                playerType = closestChar.GetComponent<MonsterMovement>().mType;
            }

			float playerDistance = Vector3.Distance(gameObject.transform.position, closestChar.position);

			if (mType != playerType && playerDistance < detectDistance)
			{
                if (playerType == MonsterType.None)
                {
                    Chase();
                } else
                {
                    switch (mType)
                    {
                        case MonsterType.Monster1:
                            if (playerType == MonsterType.Monster3)
                            {
                                Chase();
                            }
                            else if (playerType == MonsterType.Monster2)
                            {
                                Avoid();
                            }
                            break;
                        case MonsterType.Monster2:
                            if (playerType == MonsterType.Monster1)
                            {
                                Chase();
                            }
                            else if (playerType == MonsterType.Monster3)
                            {
                                Avoid();
                            }
                            break;
                        case MonsterType.Monster3:
                            if (playerType == MonsterType.Monster2)
                            {
                                Chase();
                            }
                            else if (playerType == MonsterType.Monster1)
                            {
                                Avoid();
                            }
                            break;
                    }
                }
			}
			else
			{
				playerDetected = false;
                reactions[0].SetActive(false);
                reactions[1].SetActive(false);

				if (timer > randomTimerDelay)
				{
					nav.SetDestination(getRandomPosition());
					timer = 0;
				}
			}
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

    void Chase()
    {
        nav.SetDestination(closestChar.position);
        
        if (!playerDetected)
        {
            StartCoroutine(React(true));
            playerDetected = true;
        }
    }

    void Avoid()
    {
        Vector3 enemyDirection = 2 * gameObject.transform.position - closestChar.position;
        nav.SetDestination(enemyDirection);

        if (!playerDetected)
        {
            StartCoroutine(React(false));
            playerDetected = true;
        }
    }

    IEnumerator React(bool positive)
    {
        if (positive)
        {
            reactions[0].SetActive(true);
			reactions[1].SetActive(false);
			yield return new WaitForSeconds(0.5f);
			reactions[0].SetActive(false);
			yield return new WaitForSeconds(0.5f);

			if (playerDetected && (reactions[0] == true))
			{
				StartCoroutine(React(true));
			}
        }
        else
        {
			
            reactions[1].SetActive(true);

            reactions[0].SetActive(false);
			yield return new WaitForSeconds(0.5f);
			reactions[1].SetActive(false);

			yield return new WaitForSeconds(0.5f);

			if (playerDetected && (reactions[1]==true))
			{
				StartCoroutine(React(false));
			}
        }

        //yield return new WaitForSeconds(2);

        //if (positive) reactions[0].SetActive(false);
        //else reactions[1].SetActive(false);
    }

    void UpdateClosestChar()
    {
		if (player)
		{
			float minDistance = Vector3.Distance(transform.position, player.position);
			closestChar = player;
			foreach (GameObject enemy in monManager.enemies)
			{
				float dist = Vector3.Distance(transform.position, enemy.transform.position);

				if (dist < (minDistance - detectDistance))
				{
					minDistance = dist;
					closestChar = enemy.transform;
				}
			}
		}
    }
}
