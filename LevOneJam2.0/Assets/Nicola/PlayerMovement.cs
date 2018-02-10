using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

	Vector3 movement;
	Rigidbody playerRigidBody;
	int floorMask; //collision with floor
	float camRayLenght = 100f;  //??
   
    private float time = 0;
    private MonsterType currentType = MonsterType.Monster1;
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
    }

    void FixedUpdate () {
		float h = Input.GetAxisRaw ("Horizontal"); //-1, 0, 1 because it is raw
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
		Turning ();
	}

	void Move (float h, float v) {
		movement.Set (h, 0f, v);

		movement = movement.normalized * speed * Time.deltaTime;  //normalized value between 0 and 1

		playerRigidBody.MovePosition (transform.position + movement);
	}

	void Turning () {
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition); //array that we cast from the camera into the scene //ray raggio al centro della camera verso la scena 

		RaycastHit floorHit; //to get the information back from the RayCast

		if (Physics.Raycast (camRay, out floorHit, camRayLenght, floorMask)){
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse); //lookRotation '??
			playerRigidBody.MoveRotation(newRotation);
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
