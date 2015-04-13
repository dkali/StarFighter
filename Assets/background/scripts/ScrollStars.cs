using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class ScrollStars : MonoBehaviour {
	public float scrollSpeed;
	private Renderer rend;
	private Vector2 savedOffset, initialOffset;
	private GameObject player;
	private Rigidbody pl_rb;
	private PlayerController pl_controller;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		initialOffset = rend.sharedMaterial.GetTextureOffset ("_MainTex");
		savedOffset = rend.sharedMaterial.GetTextureOffset ("_MainTex");
		player = GameObject.Find ("player");
		pl_rb = player.GetComponent<Rigidbody> ();
		pl_controller = player.GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		float moveHorizontal = pl_rb.velocity.x / pl_controller.speed;
		float moveVertical = pl_rb.velocity.z / pl_controller.speed;

		float dx = 0, dy = 0;
		//assuming that moveHorizontal=1 is a 100% of scrollSpeed
		dx = scrollSpeed * Mathf.Abs (moveHorizontal);
		if (moveHorizontal < 0)
			dx *= -1;

		dy = scrollSpeed * Mathf.Abs (moveVertical);
		if (moveVertical > 0)
			dy *= -1;

		savedOffset = rend.sharedMaterial.GetTextureOffset ("_MainTex");
		//swapped, since object is rotated relative to origin
		float new_offset_x = limit_to_1 (savedOffset.x + dy);
		float new_offset_y = limit_to_1 (savedOffset.y + dx);

		Vector2 offset = new Vector2 (new_offset_x, new_offset_y);

		rend.sharedMaterial.SetTextureOffset ("_MainTex", offset);

		//TODO: rework this script to make the scroll speed dependent from the player's speed
		// that automatically fix the issue with scrolling while braking
	}

	private float limit_to_1(float var)
	{
		if ( var > 0)
			return var -1;
		else
			return var +1;
	}

	void OnDisable(){
		rend.sharedMaterial.SetTextureOffset ("_MainTex", initialOffset);
	}

}
