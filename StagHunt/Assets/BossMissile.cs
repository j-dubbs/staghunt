using UnityEngine;
using System.Collections;

public class BossMissile : MonoBehaviour
{

	private Vector3 missileDirection;


	// Use this for initialization
	void Start ()
	{
		Destroy (gameObject, 10f);
	}

	public void SetMissileDirection (Vector3 target)
	{
		
		missileDirection = target;
		missileDirection *= 1 / 100f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate (missileDirection);
	}
}
