using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement2 : MonoBehaviour {
	Transform player;
	UnityEngine.AI.NavMeshAgent nav;

	void Awake ()
	{
		player = GameObject.FindGameObjectWithTag ("Normal").transform;
		nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
	}


	void Update ()
	{
		Vector3 enemyDirection = -player.position;
		if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentType == MonsterType.Monster1) {
			nav.SetDestination(player.position);
		} else if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().currentType == MonsterType.Monster3) {
			nav.SetDestination (enemyDirection); // running away from player
		}
	}
}
