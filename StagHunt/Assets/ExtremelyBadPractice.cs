using UnityEngine;
using System.Collections;

public class ExtremelyBadPractice : MonoBehaviour {

	float turnSpeed = 150.0f;
	public Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {


		/*
		if(Input.GetKey(KeyCode.UpArrow))
			transform.Translate(Vector3.up * 2.0f * Time.deltaTime);

		if(Input.GetKey(KeyCode.DownArrow))
			transform.Translate(Vector3.up * -1.0f * Time.deltaTime);

*/
		if(Input.GetKey(KeyCode.D))
			transform.Rotate (Vector3.forward, -1.0f * turnSpeed * Time.deltaTime);

		if(Input.GetKey(KeyCode.A))
			transform.Rotate (Vector3.forward, turnSpeed * Time.deltaTime);

	}


	void FixedUpdate() {
		if (Input.GetKey (KeyCode.W))
			rigidBody.AddForce (transform.up * 2.0f);

		/*
		if(Input.GetKey(KeyCode.DownArrow))
			rigidBody.AddForce (transform.up * -1.0f);
		*/


		if (Input.GetKey (KeyCode.S)) {
			if (rigidBody.drag < 5) {
				rigidBody.drag+=.5f;
			}
		} else {
			rigidBody.drag--;
		}
	}
}
