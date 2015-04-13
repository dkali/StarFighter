using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]
public class Boundary
{
	//constraints for the projectile
	public float xMin, xMax, zMin, zMax;
}

public class BrakingParams
{
	public float tParam;
	public float x_speed, y_speed;
	
	public BrakingParams(){
		tParam = 0;
	}

	//Vector3 speed - velocity vector of the player when the input was released
	public BrakingParams(Vector3 speed){
		tParam = 0;
		x_speed = speed.x;
		y_speed = speed.z;
	}
}


public class PlayerController : MonoBehaviour {
	public float speed;
	public float braking_speed;
	public float tilt;
	public Boundary boundary;

	private GameObject players_engine;
	private BrakingParams br_params;
	private Vector3 previous_frame_movement_vector;

	// Use this for initialization
	void Start () {
		players_engine = GameObject.Find ("player_engine_particle");
		br_params = new BrakingParams();
		previous_frame_movement_vector = Vector3.zero;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
#if UNITY_IPHONE || UNITY_ANDROID
		//read from joystick
		float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
		float moveVertical = CrossPlatformInputManager.GetAxis("Vertical");
#else
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
#endif
		Rigidbody rb = GetComponent<Rigidbody> ();

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		//print ("player movement: " + movement.ToString());

		if (movement == Vector3.zero) {
			if (previous_frame_movement_vector != Vector3.zero)
			{
				//input released at this frame
				players_engine.SetActive(false);
				print ("INPUT RELEASED");
				print ("player speed: " + rb.velocity.ToString());
				br_params = new BrakingParams(rb.velocity);
			}

			//slowly stop the movement
			if (br_params.tParam < 1) {
				br_params.tParam += braking_speed;
				//print ("tParam = " + br_params.tParam.ToString());
				float new_x = Mathf.Lerp(br_params.x_speed, 0, br_params.tParam);
				float new_z = Mathf.Lerp(br_params.y_speed, 0, br_params.tParam);
				rb.velocity = new Vector3(new_x, 0.0f, new_z);
			}

			previous_frame_movement_vector = movement;
			return;
		}

		//set ignition
		players_engine.SetActive(true);
		
		//move player
		rb.velocity = movement * speed;

		//rotate
		Transform transform = GetComponent<Transform> ();
		Quaternion turnQ = Quaternion.LookRotation(movement);
		Quaternion tiltQ = Quaternion.Euler(0, 0, rb.velocity.x * -tilt);

		Quaternion resultQ = turnQ; //turnQ * tiltQ;

		//Quaternion targetQ = Quaternion.Euler(0, moveHorizontal*10, rb.velocity.x * -tilt);
		int smooth = 10; // скорость поворота градусов в секунду
		transform.rotation = Quaternion.Slerp(transform.rotation, resultQ, Time.deltaTime * smooth);

		previous_frame_movement_vector = movement;
	}
}
