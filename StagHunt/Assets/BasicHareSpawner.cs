using UnityEngine;
using System.Collections;

public class BasicHareSpawner : MonoBehaviour
{


	public HareScript hareToSpawn;
	float hareInterval = 5f;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (SpawnHare ());
	
	}

	//Coroutine for spawning hares
	IEnumerator SpawnHare ()
	{
		while (true) {

			float X = Random.Range (-9f, 9f);
			float Y;
			if (Mathf.RoundToInt (Random.value) == 0) {
				Y = 5f;
			} else
				Y = -5f;

			Vector3 spawnLocation = new Vector3 (X, Y, 0f);

			HareScript hareInstance = Instantiate (hareToSpawn, spawnLocation, Quaternion.identity) as HareScript;
			hareInstance.safeLootSpawn = true;

			yield return new WaitForSeconds (hareInterval);
		}




	}



	// Update is called once per frame
	void Update ()
	{
	
	}
}
