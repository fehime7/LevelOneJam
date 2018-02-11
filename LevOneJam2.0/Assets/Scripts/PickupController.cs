using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    public MonsterType mType;
	public AudioClip pickUp;
	private AudioSource audio;

	void Start(){
		audio = gameObject.AddComponent<AudioSource> ();
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {

			Debug.Log("puickUp mask");
			col.gameObject.GetComponent<PlayerMovement>().Mutate(mType);
			//this.gameObject.tag = "PickedUp";

			audio.PlayOneShot(pickUp);
			//gameObject.AddComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Audio/Sounds/169375__yoh__whoosh-crystal-reverse-yoh"), 1.0f);
			Destroy(this.gameObject);
			//this.gameObject.SetActive(false);


        }
    }
}
