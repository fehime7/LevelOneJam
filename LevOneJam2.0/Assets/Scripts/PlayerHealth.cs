using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
	private int startingHealth = 50;
	public int currentHealth;
	public Slider healthSlider;
	//public Image damageImage;
	//public AudioClip deathClip;
	//public float flashSpeed = 5f;
	//public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	GameObject image;

	//Animator anim;
	//AudioSource playerAudio;
	PlayerMovement playerMovement;
	//PlayerShooting playerShooting;
	bool isDead;
	bool damaged;

	public Sprite mySprite;

	void Awake()
	{
		//anim = GetComponent<Animator>();
		//playerAudio = GetComponent<AudioSource>();
		playerMovement = GetComponent<PlayerMovement>();
		//playerShooting = GetComponentInChildren <PlayerShooting> ();
		currentHealth = startingHealth;
	}

	void Start(){
		image = GameObject.FindGameObjectWithTag("GameOver");
		image.SetActive (false);
	}


	void Update()
	{
		if (damaged)
		{
			//damageImage.color = flashColour;
		}
		else
		{
			//damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}
		damaged = false;
		Debug.Log(currentHealth + "currentHealth");
		healthSlider.value = currentHealth;
	}


	public void TakeDamage(int amount)
	{
		damaged = true;

		currentHealth -= amount;
		//playerAudio.Play();
		if (currentHealth <= 0 && !isDead)
		{
			Death();
		}
	}


	void Death()
	{
		gameObject.AddComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Audio/Sounds/203976__thatbennyguy__female-hurt"), 1.0f);

		isDead = true;

		//playerShooting.DisableEffects ();

		//anim.SetTrigger("Die");

		//playerAudio.clip = deathClip;
		//playerAudio.Play();

		playerMovement.enabled = false;
		//playerShooting.enabled = false;


		image.SetActive (true);
		Instantiate(Resources.Load("EnemyDeath"), gameObject.transform.position, Quaternion.Euler(Vector3.zero), null);

	}


	public void RestartLevel()
	{
		image.SetActive (false);
		SceneManager.LoadScene(1);
	}
}
