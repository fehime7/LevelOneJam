using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour {

    public MonsterType mType;

    bool playerDetected = false;
    Transform player;
    UnityEngine.AI.NavMeshAgent nav;
    private GameObject[] reactions;

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
        MonsterType playerType = player.GetComponent<PlayerMovement>().currentType;
        float playerDistance = Vector3.Distance(gameObject.transform.position, player.position);

        if (mType != playerType)
        {
            if (playerDistance < 20)
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
            } else
            {
                playerDetected = false;
            }
        }
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
