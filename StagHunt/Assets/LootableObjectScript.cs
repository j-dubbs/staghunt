using UnityEngine;
using System.Collections;

public class LootableObjectScript : MonoBehaviour
{
	//Either missile or fuel
	string type;
	int amount;

	// Use this for initialization
	public Sprite fuelSprite;
	public Sprite ammoSprite;
	public bool safeSpawn = false;

	public WormsScript spawnAWorm;

	void Awake ()
	{
		int binaryDecider = Mathf.RoundToInt (Random.value);

		if (binaryDecider == 0) {
			type = "fuel";
			amount = 25;
			GetComponent<SpriteRenderer> ().sprite = fuelSprite; 
		} else {
			type = "missiles";
			amount = 2;
			GetComponent<SpriteRenderer> ().sprite = ammoSprite; 
		}


	}


	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		coll.gameObject.GetComponent<ActionController> ().ReceiveBasicResource (type, amount);

		Looted ();
	}

	void Looted ()
	{

		if (!safeSpawn) {
			
			float xSpawn = Random.Range (-9f, 9f);
			float ySpawn = 5f;
			if (Random.value < .5f) {
				ySpawn = -5f;
			}
			Vector3 spawnLoc = new Vector3 (xSpawn, ySpawn, 0f);

			WormsScript wormInstance = Instantiate (spawnAWorm, spawnLoc, Quaternion.identity) as WormsScript;
			wormInstance.SetTarget (transform.position);
		}
		Destroy (gameObject);
	}


}
