using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    public MonsterType mType;

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);

        if (col.gameObject.name == "Player")
        {
			Debug.Log("puickUp mask");
			col.gameObject.GetComponent<PlayerMovement>().Mutate(mType);
			//this.gameObject.tag = "PickedUp";

			//Destroy(this.gameObject);
			this.gameObject.SetActive(false);
        }
    }
}
