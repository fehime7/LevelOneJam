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
		nav.SetDestination (player.position);
	}
}
