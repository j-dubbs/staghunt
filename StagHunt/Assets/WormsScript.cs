using UnityEngine;
using System.Collections;

public class WormsScript : MonoBehaviour
{


	public Vector3 Target;
	SpriteRenderer wormRender;

	Vector3 direction;

	void Awake ()
	{
		
		wormRender = GetComponent <SpriteRenderer> ();
	}

	public void SetTarget (Vector3 target)
	{
		Target = target;

	}

	// Use this for initialization
	void Start ()
	{
		Destroy (gameObject, 7f);
		StartCoroutine (AttackCoroutine ());


	}

	IEnumerator AttackCoroutine ()
	{
		direction = Target - transform.position;
		//Initalize and wait
		wormRender.color = Color.red;

		yield return new WaitForSeconds (2f);

		wormRender.color = Color.white;
		while (true) {
			transform.Translate (direction * Time.deltaTime);
			yield return null;
		}

	}


	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "PlayerMissile") {
			Destroy (gameObject);
		}
	}

	// Update is called once per frame
	void Update ()
	{
		
	
	}
		

}
