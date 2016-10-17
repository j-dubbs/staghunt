using UnityEngine;
using System.Collections;

public class GoldenHareScript : MonoBehaviour
{

	bool Vulnerable = false;
	Vector3 targetDestination;
	BoxCollider2D thisCollider;

	//Golden hares death
	public delegate void GoldenHareKilled (int ID);

	public static event GoldenHareKilled deathOfAHare;


	// Use this for initialization
	void Start ()
	{
		thisCollider = GetComponent<BoxCollider2D> ();
		targetDestination = new Vector3 (-12f, 0f, 0f);
		StartCoroutine (goldenHareCoroutine ());
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	IEnumerator goldenHareCoroutine ()
	{
		thisCollider.enabled = false;
		Vector3 velocity = Vector3.zero;
		yield return new WaitForSeconds (2f);

		Vulnerable = true;
		thisCollider.enabled = true;
		while (transform.position != targetDestination) {
			transform.position = Vector3.SmoothDamp (transform.position, targetDestination, ref velocity, 7f);

			yield return null;
		}
		Destroy (gameObject);


	}


	void RewardsOfTheGoldenHare (int KillerID)
	{

	}



	void OnCollisionEnter2D (Collision2D coll)
	{
		if (Vulnerable) {

			PlayerMissileScript instance = coll.gameObject.GetComponent<PlayerMissileScript> ();
			Debug.Log ("Killed by" + instance.playerWhoFiredID);
			deathOfAHare (instance.playerWhoFiredID);
			Destroy (gameObject);
		}
	}
}
