﻿using System.Collections;
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
			nav.SetDestination (player.position);
		}
	}