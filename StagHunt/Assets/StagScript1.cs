using UnityEngine;
using System.Collections;

public class StagScript1 : MonoBehaviour
{

	//Potential travel points
	public Vector3[] potentialPoints;

	//Copy of currently executing movemetn
	private IEnumerator executingMovementCoroutine;

	private int Health = 12;

	public BossMissile missileToSpawn;

	void Awake ()
	{
		potentialPoints = new Vector3[5];
		potentialPoints [1] = new Vector3 (7, 3, 0);
		potentialPoints [2] = new Vector3 (7, -3, 0);
		potentialPoints [3] = new Vector3 (-7, 3, 0);
		potentialPoints [4] = new Vector3 (-7, -3, 0);
	}


	// Use this for initialization
	void Start ()
	{
		potentialPoints [0] = new Vector3 (transform.position.x, transform.position.y, 0);
		StartCoroutine (stagAiCoroutine ());
		StartCoroutine (attackCoroutine ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}


	IEnumerator stagAiCoroutine ()
	{
		int prevRandom = 1;

		while (Health > 0) {

			int randomInt = Mathf.RoundToInt (Random.Range (1.0f, 4.0f));
			//4 is current range 1 -4, use this modulus to avoid repeats
			//Make sure to assign prevRandom after this check lol
			if (randomInt == prevRandom) {
				randomInt = (randomInt + prevRandom) % 4;
			}

			//Start the movement
			executingMovementCoroutine = movementCoroutine (potentialPoints [randomInt]);
			StartCoroutine (executingMovementCoroutine);



			yield return new WaitForSeconds (2f);

			//Stop the movement
			StopCoroutine (executingMovementCoroutine);

			//Attack
			BossMissile missileInstance1 = Instantiate (missileToSpawn, transform.position, Quaternion.identity) as BossMissile;
			missileInstance1.SetMissileDirection (new Vector3 (1f, 1f, 0f));

			BossMissile missileInstance2 = Instantiate (missileToSpawn, transform.position, Quaternion.identity) as BossMissile;
			missileInstance2.SetMissileDirection (new Vector3 (1f, -1f, 0f));

			BossMissile missileInstance3 = Instantiate (missileToSpawn, transform.position, Quaternion.identity) as BossMissile;
			missileInstance3.SetMissileDirection (new Vector3 (-1f, 1f, 0f));

			BossMissile missileInstance4 = Instantiate (missileToSpawn, transform.position, Quaternion.identity) as BossMissile;
			missileInstance4.SetMissileDirection (new Vector3 (-1f, -1f, 0f));

			prevRandom = randomInt;
		}

		Destroy (gameObject);

	}


	//Coroutine responsible for executing movements
	IEnumerator movementCoroutine (Vector3 targetPosition)
	{
		Vector3 velocity = Vector3.zero;
		//bool reachedDestination = false;

		while (transform.position != targetPosition) {
			transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, 1f);

			//transform.Translate (movementVector);
			//yield return new WaitForSeconds (.001f);
			yield return null;
		}
		//yield return new WaitForSeconds (2f);
	}


	IEnumerator attackCoroutine ()
	{
		while (true) {

			yield return new WaitForSeconds (3f);
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "PlayerMissile") {
			Destroy (coll.gameObject);
			Health -= 1;
		}

	}

}
