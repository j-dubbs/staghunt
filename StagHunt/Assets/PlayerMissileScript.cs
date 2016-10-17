using UnityEngine;
using System.Collections;

public class PlayerMissileScript : MonoBehaviour
{

	int YBound = 5;
	int XBound = 9;

	public int playerWhoFiredID;

	private Vector3 missileVelocity;
	private Vector2 missileForce;
	// Use this for initialization
	public Rigidbody2D missilerigidbody;

	void Awake ()
	{
		missilerigidbody = GetComponent<Rigidbody2D> ();
	}

	void Start ()
	{
		Destroy (gameObject, 2f);
		missileForce.y = 1f;
		missileVelocity.y = .1f;
		//rigidbody.AddForce (missileVelocity);

	}

	void FixedUpdate ()
	{

		//rigidbody.AddForce (missileVelocity);
	}



	public void SetVelocity (Vector3 newVelocity)
	{
		missileVelocity = newVelocity;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate (missileVelocity);

		if (transform.position.y < -YBound || transform.position.y > YBound) {
			Vector3 newTransform = transform.position.y < 0 ? new Vector3 (transform.position.x, YBound, 0)
				: new Vector3 (transform.position.x, -1 * YBound, 0);
			transform.position = newTransform;
		}

		if (transform.position.x < -XBound || transform.position.x > XBound) {
			Vector3 newTransform = transform.position.x < 0 ? new Vector3 (XBound, transform.position.y, 0)
				: new Vector3 (-1 * XBound, transform.position.y, 0);
			transform.position = newTransform;
		}
	}
}
