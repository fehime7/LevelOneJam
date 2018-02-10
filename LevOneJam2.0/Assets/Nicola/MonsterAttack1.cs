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
			playerHealth = player.GetComponent <PlayerHealth> ();
			//enemyHealth = GetComponent<EnemyHealth>();
			//anim = GetComponent <Animator> ();
		}




	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject == player)
		{
			Debug.Log("player in range");


			MonsterType playerType = other.gameObject.GetComponent<PlayerMovement>().currentType;
			MonsterType myType = MonsterType.Monster1;
			if (gameObject.name == "Enemy1(Clone)")
			{
				myType = MonsterType.Monster1;
			}
			else if (gameObject.name == "Enemy2(Clone)")
			{
				myType = MonsterType.Monster2;
			}
			else if (gameObject.name == "Enemy3(Clone)")
			{
				myType = MonsterType.Monster3;
			}
			//when the monster should die
			if (((myType == MonsterType.Monster1) && (playerType == MonsterType.Monster2)) || ((myType == MonsterType.Monster3) && (playerType == MonsterType.Monster1)) || ((myType == MonsterType.Monster2) && (playerType == MonsterType.Monster3)))
			{
				gameObject.GetComponent<FadeAlpha>().kill();
				Debug.Log("monster should die");
				//UPDATE SCORE MANAGER
			}
			//when the player should die
			else if (((myType == MonsterType.Monster2) && (playerType == MonsterType.Monster1)) || ((myType == MonsterType.Monster1) && (playerType == MonsterType.Monster3)) || ((myType == MonsterType.Monster3) && (playerType == MonsterType.Monster2)))
			{
				//later check with timer if in range to decrease the life
				playerInRange = true;
				Debug.Log("player should die");
			}

		}
	}

	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject == player)
		{
			Debug.Log("player in range");
			

			MonsterType playerType = other.GetComponent<PlayerMovement>().currentType;
			MonsterType myType = MonsterType.Monster1;
			if (gameObject.name == "Enemy1")
			{
				myType = MonsterType.Monster1;
			}
			else if (gameObject.name == "Enemy2")
			{
				myType = MonsterType.Monster2;
			}
			else if (gameObject.name == "Enemy3")
			{
				myType = MonsterType.Monster3;
			}
			//when the monster should die
			if (((myType == MonsterType.Monster1) && (playerType == MonsterType.Monster2)) || ((myType == MonsterType.Monster3) && (playerType == MonsterType.Monster1)) || ((myType == MonsterType.Monster2) && (playerType == MonsterType.Monster3)))
			{
				gameObject.GetComponent<FadeAlpha>().kill();
				Debug.Log("monster should die");
			}
			//when the player should die
			else if (((myType == MonsterType.Monster2) && (playerType == MonsterType.Monster1)) || ((myType == MonsterType.Monster1) && (playerType == MonsterType.Monster3)) || ((myType == MonsterType.Monster3) && (playerType == MonsterType.Monster2)))
			{
				//later check with timer if in range to decrease the life
				playerInRange = true;
				Debug.Log("player should die");
			}
			
		}
	}


		void OnTriggerExit (Collider other)
		{
			if(other.gameObject == player)
			{
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

			if(playerHealth.currentHealth <= 0)
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
			if(playerHealth.currentHealth > 0)
			{
				playerHealth.TakeDamage (attackDamage);
			}
		}
	}

