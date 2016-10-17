using UnityEngine;
using System.Collections;

public class HareScript : MonoBehaviour
{

	int YBound = 5;
	int XBound = 9;
	public LootableObjectScript LootPrefab;

	private Vector3 randomDirection;
	private float maxSpeed = .0125f;
	private float minSpeed = .0125f;


	public bool safeLootSpawn = false;

	// Use this for initialization
	void Start ()
	{
		randomDirection.x = Random.Range (-maxSpeed, maxSpeed) + minSpeed;
		randomDirection.y = Random.Range (-maxSpeed, maxSpeed) + minSpeed;

		//StartCoroutine (movementCoroutine ());
	}


	IEnumerator movementCoroutine ()
	{
		while (true) {
			transform.Translate (randomDirection);

			yield return null;
		}

	}

	void SetSafeSpawn (bool spawn)
	{
		safeLootSpawn = spawn;
	}

	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate (randomDirection);


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


	void OnCollisionEnter2D (Collision2D coll)
	{
		LootableObjectScript lootInstance = Instantiate (LootPrefab, transform.position, Quaternion.identity) as LootableObjectScript;
		lootInstance.safeSpawn = safeLootSpawn;
		Destroy (gameObject);
	}



}
