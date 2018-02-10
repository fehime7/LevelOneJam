﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    public MonsterType mType;

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);

        if (col.gameObject.name == "Player")
        {
            col.gameObject.GetComponent<PlayerMovement>().Mutate(mType);
            Destroy(this.gameObject);
        }
    }
}
