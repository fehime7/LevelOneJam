using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    enum Monster { Monster1, Monster2, Monster3 };

    private float speed = 5;
    private float time = 0;
    private Monster currentType = Monster.Monster1;
    private GameObject[] models;

    private void Start()
    {
        models = new GameObject[System.Enum.GetValues(typeof(Monster)).Length];

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform modelsTrans = gameObject.transform.GetChild(i).transform;

            if (modelsTrans.name == "Models")
            {
                for (int j = 0; j < modelsTrans.childCount; j++)
                {
                    GameObject modelObj = modelsTrans.GetChild(j).gameObject;

                    models[j] = modelObj;
                    modelObj.SetActive(false);
                }
            }
        }

        models[0].SetActive(true);
    }

    void Update () {
        time += Time.deltaTime;

        if (time < 5)
        {
            Mutate(Monster.Monster1);
        } else if (time >= 5 && time < 10)
        {
            Mutate(Monster.Monster2);
        } else if (time >= 10 && time < 15)
        {
            Mutate(Monster.Monster3);
        }


        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        
        transform.Translate(x, 0, z);
    }

    void Mutate(Monster monsterType)
    {
        if (monsterType != currentType)
        {
            switch (currentType)
            {
                case Monster.Monster1:
                    models[0].SetActive(false);
                    break;

                case Monster.Monster2:
                    models[1].SetActive(false);
                    break;

                case Monster.Monster3:
                    models[2].SetActive(false);
                    break;
            }

            switch (monsterType)
            {
                case Monster.Monster1:
                    speed = 1;
                    models[0].SetActive(true);
                    break;

                case Monster.Monster2:
                    speed = 5;
                    models[1].SetActive(true);
                    break;

                case Monster.Monster3:
                    speed = 10;
                    models[2].SetActive(true);
                    break;
            }
        }
    }
}
