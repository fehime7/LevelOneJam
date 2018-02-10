using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement1 : MonoBehaviour {
		Transform player;
		UnityEngine.AI.NavMeshAgent nav;



		void Awake ()
		{
			player = GameObject.FindGameObjectWithTag ("Player").transform;
			nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
		}


		void Update ()
		{

			Vector3 enemyDirection = -player.position;
			nav.SetDestination (player.position);
			//nav.SetDestination (enemyDirection); // running away from player
		}
	}
