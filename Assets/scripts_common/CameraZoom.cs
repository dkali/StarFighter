using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CameraZoom : MonoBehaviour {
	public float camera_smooth;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("player");
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody pl_rb = player.GetComponent<Rigidbody> ();
		float moveHorizontal = pl_rb.velocity.x;
		float moveVertical = pl_rb.velocity.z;

		Camera cam = GetComponent<Camera> ();

		float delta = 0;
		if (moveHorizontal != 0 || moveVertical != 0)
			delta = camera_smooth;
		else
			delta = - camera_smooth;
		cam.orthographicSize = Mathf.Clamp (cam.orthographicSize + delta, 7, 10);
	}
}
