using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement1 : MonoBehaviour {
		Transform player;
		UnityEngine.AI.NavMeshAgent nav;



		void Awake ()
		{
			if (GameObject.FindGameObjectWithTag("Player")){
				player = GameObject.FindGameObjectWithTag("Player").transform;
				nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
			}
		}


		void Update ()
		{
			Vector3 enemyDirection = -player.position;
			if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentType == MonsterType.Monster3) {
				nav.SetDestination(player.position);
			} else if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentType == MonsterType.Monster2) {
				nav.SetDestination (enemyDirection); // running away from player
			}
		}
	}
