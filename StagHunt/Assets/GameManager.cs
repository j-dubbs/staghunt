using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

	public float TimeRemaining = 90.0f;
	public int TimeRemainingInt = 90;
	private int finalTime = 0;


	private Text TimeText;

	//Event and delegate
	public delegate void OnTimerEnd ();

	public static event OnTimerEnd TimerEnd;

	//Hares spawning
	public HareScript hareToSpawn;
	public GoldenHareScript theGoldenHare;

	//Prefab for instantiation
	public ShipController shipPrefab;
	//Keep some sprites in here
	public Sprite UltraMarine;
	public Sprite PinkShip;
	public Sprite Player3;
	public Sprite Player4;

	private Vector3 Player1Start, Player2Start, Player3Start, Player4Start;

	private SpriteRenderer testRender;

	//Setup Start locations
	void Awake ()
	{
		Player1Start = new Vector3 (-6, 0, 0);
		Player2Start = new Vector3 (-2, 0, 0);
		Player3Start = new Vector3 (2, 0, 0);
		Player4Start = new Vector3 (6, 0, 0);


	}

	void Start ()
	{
		ShipController ship1Instance, ship2Instance, ship3Instance, ship4Instance;

		//Create and assign instances



		ship1Instance = Instantiate (shipPrefab, Player1Start, Quaternion.identity) as ShipController;
		ship1Instance.SetMovementAxis ("Wasd");
		ship1Instance.GetComponent<SpriteRenderer> ().sprite = UltraMarine;
		ship1Instance.GetComponent<SpriteRenderer> ().sortingLayerName = "Foreground";
		ship1Instance.SetID (1);

		ship2Instance = Instantiate (shipPrefab, Player2Start, Quaternion.identity) as ShipController;
		ship2Instance.SetMovementAxis ("Arrow");
		ship2Instance.GetComponent<SpriteRenderer> ().sprite = PinkShip;
		ship2Instance.GetComponent<SpriteRenderer> ().sortingLayerName = "Foreground";
		ship2Instance.SetID (2);

		ship3Instance = Instantiate (shipPrefab, Player3Start, Quaternion.identity) as ShipController;
		ship3Instance.SetMovementAxis ("Gamepad1");
		ship3Instance.GetComponent<SpriteRenderer> ().sprite = Player3;
		ship3Instance.GetComponent<SpriteRenderer> ().sortingLayerName = "Foreground";
		ship3Instance.SetID (3);

		ship4Instance = Instantiate (shipPrefab, Player4Start, Quaternion.identity) as ShipController;
		ship4Instance.SetMovementAxis ("Gamepad2");
		ship4Instance.GetComponent<SpriteRenderer> ().sprite = Player4;
		ship4Instance.GetComponent<SpriteRenderer> ().sortingLayerName = "Foreground";
		ship4Instance.SetID (4);

		//Set time text
		TimeText = GameObject.Find ("TimeText").GetComponent<Text> ();

		StartCoroutine (spawningHares ());

	}
	
	// Update is called once per frame
	void Update ()
	{
		/*
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("Lets spawn this");
			SceneManager.LoadScene ("Boss");
		}
		*/

		//Timer End Event Called
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			Debug.Log (TimeRemaining);

		}

		TimeRemaining -= Time.deltaTime;
		TimeRemainingInt = (int)Mathf.Round (TimeRemaining);
		TimeText.text = TimeRemainingInt.ToString ();

		//Call when ready
		if (TimeRemainingInt < finalTime) {
			WrapUp ();
		}

	}


	//Call Timer End and then transition scene
	void WrapUp ()
	{
		TimerEnd ();
		SceneManager.LoadScene ("Boss");
	}


	IEnumerator spawningHares ()
	{
		while (TimeRemaining > 85) {
			float xSpawn = Random.Range (-9f, 9f);
			float ySpawn = 5f;
			if (Random.value < .5f) {
				ySpawn = -5f;
			}

			Vector3 harePos = new Vector3 (xSpawn, ySpawn, 0f);


			Instantiate (hareToSpawn, harePos, Quaternion.identity);

			yield return new WaitForSeconds (6f);
		}

		Instantiate (theGoldenHare);
		yield return new WaitForSeconds (8f);


		while (true) {
			float xSpawn = Random.Range (-9f, 9f);
			float ySpawn = 5f;
			if (Random.value < .5f) {
				ySpawn = -5f;
			}

			Vector3 harePos = new Vector3 (xSpawn, ySpawn, 0f);


			Instantiate (hareToSpawn, harePos, Quaternion.identity);

			yield return new WaitForSeconds (6f);
		}

	}




}
