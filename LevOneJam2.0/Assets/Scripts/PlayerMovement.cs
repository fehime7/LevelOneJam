using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	Vector3 movement;
	Rigidbody playerRigidBody;

    private float speed = 10;
    public MonsterType currentType = MonsterType.None;
    private GameObject[] masks;

    void Awake() {
		playerRigidBody = GetComponent <Rigidbody> ();
	}

    private void Start()
    {
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
        Move();
        Turning();

        if (Input.GetKeyDown("space"))
            Ability();
    }

	void Move () {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        movement.Set (h, movement.y, v);
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
        Debug.Log(monsterType);

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
    }

    void Ability()
    {
        //Debug.Log("JUMP");

        //playerRigidBody.AddForce(new Vector3(1000, 1000, 1000), ForceMode.Impulse);
        //playerRigidBody.velocity += 100 * Vector3.up;

        //movement.y += 1;
    }
}
