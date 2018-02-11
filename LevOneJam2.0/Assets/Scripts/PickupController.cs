using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    public MonsterType mType;
	public AudioClip pickUp;
	private AudioSource audioS;

	void Start(){
		audioS = GameObject.Find("Canvas").GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            audioS.PlayOneShot(pickUp);
           
			col.gameObject.GetComponent<PlayerMovement>().Mutate(mType);
			Destroy(this.gameObject);
        }
    }
}
