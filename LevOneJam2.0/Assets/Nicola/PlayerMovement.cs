using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	Vector3 movement;
	Rigidbody playerRigidBody;
	int floorMask; //collision with floor
	float camRayLenght = 100f;  //??

    private float speed = 3;
    public MonsterType currentType = MonsterType.Monster1;
    private GameObject[] models;

    void Awake() {
		floorMask = LayerMask.GetMask ("Floor");
		playerRigidBody = GetComponent <Rigidbody> ();
	}

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
        Mutate(MonsterType.Monster1);
    }

    void FixedUpdate () {
		float h = Input.GetAxisRaw ("Horizontal"); //-1, 0, 1 because it is raw
		float v = Input.GetAxisRaw ("Vertical");

		Move(h, v);
		Turning();

        if (Input.GetKeyDown("space"))
            Ability();
    }

	void Move (float h, float v) {
		movement.Set (h, movement.y, v);

		movement = movement.normalized * speed * Time.deltaTime;  //normalized value between 0 and 1

		playerRigidBody.MovePosition (transform.position + movement);
	}

	void Turning () {
        Quaternion targetRot = Quaternion.LookRotation(movement);
        //gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * speed);

        if (targetRot != new Quaternion(0, 0, 0, 0))
        {
            gameObject.transform.rotation = targetRot;
        }
    }

    public void Mutate(MonsterType monsterType)
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
                    speed = 3;
                    models[0].SetActive(true);
                    break;

                case MonsterType.Monster2:
                    speed = 5;
                    models[1].SetActive(true);
                    break;

                case MonsterType.Monster3:
                    speed = 7;
                    models[2].SetActive(true);
                    break;
            }
        }

        currentType = monsterType;
    }

    void Ability()
    {
        //Debug.Log("JUMP");

        //playerRigidBody.AddForce(new Vector3(1000, 1000, 1000), ForceMode.Impulse);
        //playerRigidBody.velocity += 100 * Vector3.up;

        //movement.y += 1;
    }
}
