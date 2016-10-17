using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerControllerBasic : MonoBehaviour
{

	float velocityMax = 10.0f;

	float turnSpeed = 100.0f;
	public Rigidbody2D myRigidBody;
	public Transform shipTransform;
	string verticalAxis;
	string horizontalAxis;


	// Use this for initialization
	void Start ()
	{
		myRigidBody = GetComponent<Rigidbody2D> ();
		myRigidBody.interpolation = RigidbodyInterpolation2D.Extrapolate;
		myRigidBody.gravityScale = 0;
	}

	public void SetTransform (Transform transform)
	{
		shipTransform = transform;
	}

	//Defines the axis this player will be using
	public void SetAxisSet (string axis)
	{
		verticalAxis = axis + "Vertical";
		horizontalAxis = axis + "Horizontal";
	}




	// Update is called once per frame
	void Update ()
	{
	}


	public void FixedUpdate ()
	{
		//Rotation handled horizontally
		float horizontalAxisValue = Input.GetAxis (horizontalAxis);
		myRigidBody.MoveRotation (myRigidBody.rotation + turnSpeed * Time.fixedDeltaTime * -1.0f * horizontalAxisValue);


		//Forces handled veritcally
		float verticalAxisValue = Input.GetAxis (verticalAxis);

		//Positive add force, negative cap drag
		if (verticalAxisValue >= 0) {
			myRigidBody.drag--;
			if (myRigidBody.velocity.magnitude < velocityMax) {
				myRigidBody.AddForce (transform.up * verticalAxisValue);
			}
		} else if (verticalAxisValue < 0) {
			
			myRigidBody.drag = myRigidBody.drag < 5 ? myRigidBody.drag += .5f : myRigidBody.drag += 0f;

		} else
			myRigidBody.drag--;
			
	
	}

}
