using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CameraZoom : MonoBehaviour {
	public float camera_smooth;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_IPHONE || UNITY_ANDROID
		//read from joystick
		float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
		float moveVertical = CrossPlatformInputManager.GetAxis("Vertical");
#else
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
#endif
		Camera cam = GetComponent<Camera> ();

		float delta = 0;
		if (moveHorizontal != 0 || moveVertical != 0)
			delta = camera_smooth;
		else
			delta = - camera_smooth;
		cam.orthographicSize = Mathf.Clamp (cam.orthographicSize + delta, 7, 10);
	}
}
