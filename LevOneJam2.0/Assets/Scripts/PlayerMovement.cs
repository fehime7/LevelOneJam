using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	Vector3 movement;
	Rigidbody playerRigidBody;

    private float speed = 10;
    public MonsterType currentType = MonsterType.None;
    private GameObject[] masks;
    private MonsterManager monManager;
    private float timer = 0;

    void Awake() {
		playerRigidBody = GetComponent <Rigidbody> ();
	}

    private void Start()
    {
        monManager = (MonsterManager)FindObjectOfType(typeof(MonsterManager));
        masks = new GameObject[System.Enum.GetValues(typeof(MonsterType)).Length-1];

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform masksTrans = gameObject.transform.GetChild(i).transform;

            if (masksTrans.name == "Models")
            {
                for (int j = 0; j < masksTrans.childCount; j++)
                {
                    GameObject modelObj = masksTrans.GetChild(j).gameObject;

                    masks[j] = modelObj;
                    modelObj.SetActive(false);
                }
            }
        }
    }

    void FixedUpdate () {
        ChangeSpeed();
        Move();
        Turning();

        timer += Time.deltaTime;
    }

	void Move () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        movement.Set(h, 0, v);
        
		movement = movement.normalized * speed * Time.deltaTime;  //normalized value between 0 and 1

        playerRigidBody.MovePosition (transform.position + movement);
    }

	void Turning () {
        if (movement != Vector3.zero)
        {
            Quaternion targetRot = Quaternion.LookRotation(movement);
            playerRigidBody.MoveRotation(targetRot);
        }
    }

    public void Mutate(MonsterType monsterType)
    {
        if (monsterType != currentType)
        {
            switch (currentType)
            {
                case MonsterType.Monster1:
                    masks[0].SetActive(false);
                    break;

                case MonsterType.Monster2:
                    masks[1].SetActive(false);
                    break;

                case MonsterType.Monster3:
                    masks[2].SetActive(false);
                    break;
            }

            switch (monsterType)
            {
                case MonsterType.Monster1:
                    speed = 5;
                    masks[0].SetActive(true);
                    break;

                case MonsterType.Monster2:
                    speed = 7;
                    masks[1].SetActive(true);
                    break;

                case MonsterType.Monster3:
                    speed = 9;
                    masks[2].SetActive(true);
                    break;
            }
        }

        currentType = monsterType;

        foreach (GameObject monster in monManager.enemies)
        {
            monster.GetComponent<MonsterMovement>().playerDetected = false;
        }
    }

    void ChangeSpeed()
    {
        //k = timer % 0.75f + 0.125f;
        //k = timer % 0.5f + 0.25f;
        //k = timer % 0.35f + 0.325f;
        //k = timer % 0.3f + 0.35f;
        //k = timer % 0.25f + 0.375f;
        //k = timer % 0.2f + 0.4f;
        float k = 0;
        float newSpeed = 0;

        switch(currentType)
        {
            case MonsterType.None:
                newSpeed = 8;
                break;
            case MonsterType.Monster1:
                k = timer % 0.75f + 0.125f;

                //Cubic InOut
                if ((k *= 2f) < 1f) newSpeed = 0.5f * k * k * k;
                else newSpeed = 0.5f * ((k -= 2f) * k * k + 2f);

                newSpeed *= 8;
                break;
            case MonsterType.Monster2:
                k = timer % 0.3f + 0.35f;

                //Cubic InOut
                if ((k *= 2f) < 1f) newSpeed = 0.5f * k * k * k;
                else newSpeed = 0.5f * ((k -= 2f) * k * k + 2f);

                newSpeed *= 9;
                break;
            case MonsterType.Monster3:
                newSpeed = 4.5f;
                break;
        }

        speed = newSpeed;
    }
}
