using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack1 : MonoBehaviour {

		public float timeBetweenAttacks = 0.5f;
		public int attackDamage = 10;


		Animator anim;
		GameObject player;
		PlayerHealth playerHealth;
		//EnemyHealth enemyHealth;
		bool playerInRange;
		float timer;


		void Awake ()
		{
			player = GameObject.FindGameObjectWithTag ("Player");
		if (player)
		{
			playerHealth = player.GetComponent<PlayerHealth>();
		}
		else {
		}
			//enemyHealth = GetComponent<EnemyHealth>();
			//anim = GetComponent <Animator> ();
		}




	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject == player)
		{
			Debug.Log("collision detected");

			MonsterType playerType = other.gameObject.GetComponent<PlayerMovement>().currentType;
			MonsterType myType = MonsterType.None;
			myType = gameObject.GetComponent<MonsterMovement>().mType;

			Debug.Log(playerType + "   " + myType);

			//when the monster should die
			if (myType != playerType)
			{
				if (((myType == MonsterType.Monster1) && (playerType == MonsterType.Monster2)) || ((myType == MonsterType.Monster3) && (playerType == MonsterType.Monster1)) || ((myType == MonsterType.Monster2) && (playerType == MonsterType.Monster3)))
				{
					gameObject.GetComponent<FadeAlpha>().kill();
					Debug.Log("monster should die");
				}
				//when the player should die
				else /*if (((myType == MonsterType.Monster2) && (playerType == MonsterType.Monster1)) || ((myType == MonsterType.Monster1) && (playerType == MonsterType.Monster3)) || ((myType == MonsterType.Monster3) && (playerType == MonsterType.Monster2)))*/
				{
					//later check with timer if in range to decrease the life
					playerInRange = true;
					Debug.Log("player should die");
				}
			}

		}
	}


	void OnCollisionExit(Collision other)
	{
		if (other.gameObject == player) {
			playerInRange = false;
		}
	}


	void Update ()
	{
		timer += Time.deltaTime;

		if(timer >= timeBetweenAttacks && playerInRange/* && enemyHealth.currentHealth > 0*/)
		{
			Attack ();
		}

		if(playerHealth && playerHealth.currentHealth <= 0)
		{
			player.GetComponent<FadeAlpha>().kill();
			//anim.SetTrigger ("PlayerDead");
		}
	}


		void Attack ()
		{
			timer = 0f;
			Debug.Log("Attack");
			//player.GetComponent<FadeAlpha>().kill();
			if (playerHealth && playerHealth.currentHealth > 0)
			{
				playerHealth.TakeDamage (attackDamage);
			}
		}
	}

