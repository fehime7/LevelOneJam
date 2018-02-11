using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour {

    public MonsterType mType;

    public bool playerDetected = false;
    private bool posReaction = true;
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
            MonsterType charType = MonsterType.None;

            if (closestChar.GetComponent<PlayerMovement>())
            {
                charType = closestChar.GetComponent<PlayerMovement>().currentType;
            } else if (closestChar.GetComponent<MonsterMovement>())
            {
                charType = closestChar.GetComponent<MonsterMovement>().mType;
            }

			float charDistance = Vector3.Distance(gameObject.transform.position, closestChar.position);

			if (mType != charType && charDistance < detectDistance)
			{
                if (charType == MonsterType.None)
                {
                    Chase();
                } else
                {
                    switch (mType)
                    {
                        case MonsterType.Monster1:
                            if (charType == MonsterType.Monster3)
                            {
                                Chase();
                            }
                            else if (charType == MonsterType.Monster2)
                            {
                                Avoid();
                            }
                            break;
                        case MonsterType.Monster2:
                            if (charType == MonsterType.Monster1)
                            {
                                Chase();
                            }
                            else if (charType == MonsterType.Monster3)
                            {
                                Avoid();
                            }
                            break;
                        case MonsterType.Monster3:
                            if (charType == MonsterType.Monster2)
                            {
                                Chase();
                            }
                            else if (charType == MonsterType.Monster1)
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
        ChangeSpeed();
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
        
        if (closestChar == player && !playerDetected)
        {
            StartCoroutine(React(true));
            playerDetected = true;
        }
    }

    void Avoid()
    {
        Vector3 enemyDirection = 2 * gameObject.transform.position - closestChar.position;
        nav.SetDestination(enemyDirection);

        if (closestChar == player && !playerDetected)
        {
            StartCoroutine(React(false));
            playerDetected = true;
        }
    }

    IEnumerator React(bool positive)
    {
        if (positive)
        {
            posReaction = true;
            reactions[0].SetActive(true);
			reactions[1].SetActive(false);
			yield return new WaitForSeconds(0.5f);
			reactions[0].SetActive(false);
			yield return new WaitForSeconds(0.5f);

			if (playerDetected && posReaction)
			{
				StartCoroutine(React(true));
			}
        }
        else
        {
            posReaction = false;
            reactions[1].SetActive(true);
            reactions[0].SetActive(false);
			yield return new WaitForSeconds(0.5f);
			reactions[1].SetActive(false);

			yield return new WaitForSeconds(0.5f);

			if (playerDetected && !posReaction)
			{
				StartCoroutine(React(false));
			}
        }
    }

    void UpdateClosestChar()
    {
		if (player)
		{
			float minDistance = Vector3.Distance(transform.position, player.position);
			closestChar = player;
            MonsterType playerType = player.gameObject.GetComponent<PlayerMovement>().currentType;

			foreach (GameObject enemy in monManager.enemies)
			{
				float dist = Vector3.Distance(transform.position, enemy.transform.position);

				if ((playerType != mType && dist < (minDistance - detectDistance)) || (playerType == mType && dist < minDistance))
				{
					minDistance = dist;
					closestChar = enemy.transform;
				}
			}
		}
    }

    void ChangeSpeed()
    {
        float k = 0;
        float newSpeed = 0;

        switch (mType)
        {
            case MonsterType.Monster1:
                //k = timer % 0.75f + 0.125f;
                k = timer % 0.9f + 0.05f;

                //Cubic InOut
                if ((k *= 2f) < 1f) newSpeed = 0.5f * k * k * k;
                else newSpeed = 0.5f * ((k -= 2f) * k * k + 2f);

                newSpeed *= 4;
                break;
            case MonsterType.Monster2:
                //k = timer % 0.3f + 0.35f;
                k = timer % 0.4f + 0.3f;

                //Cubic InOut
                if ((k *= 2f) < 1f) newSpeed = 0.5f * k * k * k;
                else newSpeed = 0.5f * ((k -= 2f) * k * k + 2f);

                newSpeed *= 4;
                break;
            case MonsterType.Monster3:
                newSpeed = 3.5f;
                break;
        }

        if (nav) nav.speed = newSpeed;
    }
}
