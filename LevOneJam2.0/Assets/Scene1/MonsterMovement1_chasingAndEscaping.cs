using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement1 : MonoBehaviour {
		Transform player;
		UnityEngine.AI.NavMeshAgent nav;
		bool following;


		void Awake ()
		{
			player = GameObject.FindGameObjectWithTag ("Player").transform;
			nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
			(player.getMonsterType() == MonsterType.Monster2) ? following = true : following = false;
		}


		void Update ()
		{
			nav.SetDestination (player.position);
		}
	}
