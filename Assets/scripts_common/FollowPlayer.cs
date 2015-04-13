using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Transform transform = GetComponent<Transform> ();
		transform.position = new Vector3 (player.transform.position.x, transform.position.y, player.transform.position.z);
	}
}
