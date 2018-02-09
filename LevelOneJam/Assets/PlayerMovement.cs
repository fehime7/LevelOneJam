using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6f;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidBody;
	int floorMask; //collision with floor
	float camRayLenght = 100f;  //??

	void Awake() {
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent <Animator> ();
		playerRigidBody = GetComponent <Rigidbody> ();
	}

	void FixedUpdate () {
		float h = Input.GetAxisRaw ("Horizontal"); //-1, 0, 1 because it is raw
		float v = Input.GetAxisRaw ("Vertical");

		Move (h, v);
		Turning ();
		Animating (h, v);
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

	void Animating (float h, float v) {
		bool walking = h != 0f || v != 0f; //did we press horizontal or vertical axes? in case we are moving!
		anim.SetBool ("IsWalking", walking);
	}

}
