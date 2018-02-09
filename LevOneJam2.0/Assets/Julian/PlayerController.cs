using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private float speed = 5;
    private float time = 0;
    private MonsterType currentType = MonsterType.Monster1;
    private GameObject[] models;

    private void Start()
    {
        models = new GameObject[System.Enum.GetValues(typeof(MonsterType)).Length];

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
            Mutate(MonsterType.Monster1);
        } else if (time >= 5 && time < 10)
        {
            Mutate(MonsterType.Monster2);
        } else if (time >= 10 && time < 15)
        {
            Mutate(MonsterType.Monster3);
        }


        float x = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        
        transform.Translate(x, 0, z);
    }

    void Mutate(MonsterType monsterType)
    {
        if (monsterType != currentType)
        {
            switch (currentType)
            {
                case MonsterType.Monster1:
                    models[0].SetActive(false);
                    break;

                case MonsterType.Monster2:
                    models[1].SetActive(false);
                    break;

                case MonsterType.Monster3:
                    models[2].SetActive(false);
                    break;
            }

            switch (monsterType)
            {
                case MonsterType.Monster1:
                    speed = 1;
                    models[0].SetActive(true);
                    break;

                case MonsterType.Monster2:
                    speed = 5;
                    models[1].SetActive(true);
                    break;

                case MonsterType.Monster3:
                    speed = 10;
                    models[2].SetActive(true);
                    break;
            }
        }

        currentType = monsterType;
    }
}
