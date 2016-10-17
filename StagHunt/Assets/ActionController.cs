using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{

	string fireButton;

	private int shipID;
	private int missiles = 3;
	private int Fuel = 0;
	private Text myStats;


	public PlayerMissileScript missileToSpawn;

	private PlayerMissileScript missileInstance;

	public void SetAxis (string axisName)
	{
		fireButton = axisName + "Fire";
	}

	public void SetID (int number)
	{
		shipID = number;
	}


	void Awake ()
	{
		
	}

	// Use this for initialization
	void Start ()
	{
		if (shipID == 1) {
			myStats = GameObject.Find ("Player1Stats").GetComponent<Text> ();
		}
		if (shipID == 2) {
			myStats = GameObject.Find ("Player2Stats").GetComponent<Text> ();
		}
		if (shipID == 3) {
			myStats = GameObject.Find ("Player3Stats").GetComponent<Text> ();
		}
		if (shipID == 4) {
			myStats = GameObject.Find ("Player4Stats").GetComponent<Text> ();
		}

		TextUpdate (false);
		GoldenHareScript.deathOfAHare += ReactToGolden;

	}

	void TextUpdate (bool hasDied)
	{
		if (!hasDied) {
			myStats.text = "Player" + shipID + "Fuel:" + Fuel.ToString () + " Missiles:" + missiles.ToString ();
		} else {
			myStats.text = "PLAYER" + shipID + "DEAD WITH Fuel:" + Fuel.ToString () + " Missiles:" + missiles.ToString ();
		}
		
	}

	// Update is called once per frame
	void Update ()
	{
		TextUpdate (false);

		if (Input.GetButtonDown (fireButton) && (missiles > 0)) {

			missileInstance = Instantiate (missileToSpawn, transform.position, transform.rotation) as PlayerMissileScript;
			missileInstance.playerWhoFiredID = shipID;
			missiles--;
		}
			

	}


	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Worm" || coll.gameObject.tag == "BossMissile") {
			TextUpdate (true);
			Destroy (gameObject);
		}

	}

	public void ReceiveBasicResource (string resourceType, int resourceAmount)
	{
	
		if (resourceType == "fuel") {
			Fuel += resourceAmount;
		}

		if (resourceType == "missiles") {
			missiles += resourceAmount;
		}

	}

	public int GetFuel ()
	{
		return Fuel;
	}

	public void ReactToGolden (int killerID)
	{
		if (killerID == shipID) {
			Fuel = 100;
			missiles += 10;
		} else {
			int fuelCanistersToLose = Mathf.RoundToInt (Random.Range (1f, 4f));
			int FuelToLose = fuelCanistersToLose * 25;

			if (Fuel > FuelToLose) {
				Fuel -= FuelToLose;
			} else {
				Fuel = 0;
			}

		}

	}


}
