using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour {

    public MonsterType mType;

    bool playerDetected = false;
    Transform player;
    UnityEngine.AI.NavMeshAgent nav;
    private GameObject[] reactions;

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

    void Start()
    {
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
    }

    void Update()
    {
		if (player)
		{
			MonsterType playerType = player.GetComponent<PlayerMovement>().currentType;
			float playerDistance = Vector3.Distance(gameObject.transform.position, player.position);

			if (mType != playerType && playerDistance < 20)
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
			else
			{
				playerDetected = false;

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
        nav.SetDestination(player.position);
        
        if (!playerDetected)
        {
            StartCoroutine(React(true));
            playerDetected = true;
        }
    }

    void Avoid()
    {
        Vector3 enemyDirection = 2 * gameObject.transform.position - player.position;
        nav.SetDestination(enemyDirection);

        if (!playerDetected)
        {
            StartCoroutine(React(false));
            playerDetected = true;
        }
    }

    IEnumerator React(bool positive)
    {
        if (positive) reactions[0].SetActive(true);
        else reactions[1].SetActive(true);

        yield return new WaitForSeconds(1);

        if (positive) reactions[0].SetActive(false);
        else reactions[1].SetActive(false);
    }
}
