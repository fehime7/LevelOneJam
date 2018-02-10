using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour {

    public MonsterType mType;

    Transform player;
    UnityEngine.AI.NavMeshAgent nav;

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
        switch(mType)
        {
            case MonsterType.Monster1:
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentType == MonsterType.Monster3)
                {
                    nav.SetDestination(player.position);
                }
                else if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentType == MonsterType.Monster2)
                {
                    Vector3 enemyDirection = gameObject.transform.position - player.position;
                    nav.SetDestination(enemyDirection); // running away from player
                }
                break;
            case MonsterType.Monster2:
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentType == MonsterType.Monster1)
                {
                    nav.SetDestination(player.position);
                }
                else if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentType == MonsterType.Monster3)
                {
                    Vector3 enemyDirection = gameObject.transform.position - player.position;
                    nav.SetDestination(enemyDirection); // running away from player
                }
                break;
            case MonsterType.Monster3:
                if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentType == MonsterType.Monster2)
                {
                    nav.SetDestination(player.position);
                }
                else if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentType == MonsterType.Monster1)
                {
                    Vector3 enemyDirection = gameObject.transform.position-player.position;
                    nav.SetDestination(enemyDirection); // running away from player
                }
                break;
        }
    }
}
