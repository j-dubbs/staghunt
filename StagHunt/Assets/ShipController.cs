using UnityEngine;
using System.Collections;


//This Controller should contain comopnents for movement and firing
public class ShipController : MonoBehaviour
{


	int YBound = 5;
	int XBound = 9;

	private PlayerControllerBasic myMovementController;
	private ActionController myActionController;

	public PlayerMissileScript projectilePrefab;

	// Use this for initialization
	void Awake ()
	{
		myMovementController = gameObject.AddComponent<PlayerControllerBasic> () as PlayerControllerBasic;
		myActionController = gameObject.AddComponent<ActionController> () as ActionController;

		//myMovementController.SetAxisSet ("Arrow");
		myActionController.missileToSpawn = projectilePrefab;
		myMovementController.SetTransform (transform);
		gameObject.layer = 9;
		DontDestroyOnLoad (gameObject);
	}

	//Set it for action too
	public void SetMovementAxis (string axisSet)
	{
		myMovementController.SetAxisSet (axisSet);
		myActionController.SetAxis (axisSet);
	}

	public void SetID (int number)
	{
		myActionController.SetID (number);
	}


	void Start ()
	{
		//Subscribe to the end
		GameManager.TimerEnd += FuelCheck;
	}

	// Update is called once per frame
	void Update ()
	{

		//Camera wrap around effect
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


	void FixedUpdate ()
	{
		
	}


	void FuelCheck ()
	{
		Debug.Log ("Fuel check called");
		if (myActionController.GetFuel () < 100) {
			GameManager.TimerEnd -= FuelCheck;
			Destroy (gameObject);
		}
	}







}
