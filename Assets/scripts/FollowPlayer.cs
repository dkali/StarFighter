using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GameObject player = GameObject.Find ("player");
		Transform transform = GetComponent<Transform> ();

		transform.position = new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z);
	}
}
