using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

// used

public class MoveBoat : MonoBehaviour {

	public static float boatspeed = 5f;//default = 5
	public static float turnspeed = 10f;
	public static float stoppingAngle = 45f;

	// scoring
	public static int countR = 0;
	public static int countL = 0;
	public static int countW = 0;

	public Transform compass;
	public GameObject EndofSessionPanel;

	public GameObject crossui, leftarrow, rightarrow;
	public GameObject RewardText;
	public static bool case800, case786, cross, left, right, hidearrow = false;
	public static bool training;
	public GameObject trainingPanel;
	public string sceneName;
	//Toggle BackgroundHap;


	// Use this for initialization
	void Start () {

		Cursor.visible = true;
		//settings
		EndofSessionPanel.SetActive(false);
		Settings.reverseHands = true;
		boatspeed = 15f;

		crossui = GameObject.Find ("CrossVR");
		leftarrow = GameObject.Find ("LeftArrowVR");
		rightarrow = GameObject.Find ("RightArrowVR");
		//BackgroundHap = GetComponent<Toggle>();
		crossui.SetActive(false);
		leftarrow.SetActive(false);
		rightarrow.SetActive(false);

		// HAS TO BE COMMENTED OUT FOR TRAINING ....-------------------------------------------
//		RewardText = GameObject.Find("RewardText");
//		RewardText.SetActive(false);
		// ------------------------------------------------------------------------------------


		
		Debug.Log ("about to call scenemanager!!!!!!!!!!!!!!!!!!");
		// check scene name to enable training
		if (SceneManager.GetActiveScene().name == "boat_online") {
			Debug.Log ("boat is online!!!!!!!!!!!!!!!!!!");
			training = false;
		} 
		else {
			training = true;
			Debug.Log ("boat is training!!!!!!!!!!!!!!!!!!");
		}

//		Debug.Log ("scene: " + SceneManager.GetActiveScene().name);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey("escape"))
		{
			Debug.Log ("Quit!");
			Application.Quit();
		}

		//Debug.Log (Assets.LSL4Unity.Scripts.Examples.ExampleFloatInlet.signal);

		// if simple ERD is selected, then only FWD, else FWD is automatic and L/R is through LDA input
		//if(Input.GetKey(KeyCode.UpArrow))

		//-------------------------------------------------------------------------------
		// COULD BE WHY BOAT MOVES IN BEGINNING OF GAME???????
		transform.Translate(Vector3.forward * boatspeed * Time.deltaTime);
		//-------------------------------------------------------------------------------

		//if((compass.localEulerAngles.y >= (360 - stoppingAngle) && compass.localEulerAngles.y <= 360) || (compass.localEulerAngles.y >= 0 && compass.localEulerAngles.y <= (0 + stoppingAngle)))
		//	transform.Translate(Vector3.forward * boatspeed * Time.deltaTime);

		// not sure what 1010 is
		//if (Receivemarkers.markerint == 1010) 
			//EndofSessionPanel.SetActive(true); //pop window
			//Debug.Log ("End of Session!");


		//Todo
		// on 32770 experiment stop
		// quit app
		if (Receivemarkers.markerint == 32770) //32770 experiment stop
			Debug.Log ("32770 experiment stop");
			EndofSessionPanel.SetActive(true); //pop window
		


		if (training) {
			//Debug.Log("inside MoveBoat training");

			getStim ();
			
			if (Input.GetKey (KeyCode.LeftArrow) || (left  && hidearrow)) {
				left = true;
				//			right = false;

				// Oculus left haptic feedback
				//if(Settings.haptic){
				//	OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
				//	Debug.Log ("using training left!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
				//}

				if (!Settings.reverseHands)
					transform.Rotate (Vector3.up * turnspeed * Time.deltaTime, Space.World);
				else
					transform.Rotate (Vector3.down * turnspeed * Time.deltaTime, Space.World);
				//	transform.Translate(Vector3.forward * boatspeed * Time.deltaTime);
			}
			if (Input.GetKey (KeyCode.RightArrow) || (right && hidearrow)) {
				//			left = false;
				right = true;

				// Oculus right haptic feedback
				//if(Settings.haptic)
				//	OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);

				if (!Settings.reverseHands)
					transform.Rotate (Vector3.down * turnspeed * Time.deltaTime, Space.World);
				else
					transform.Rotate (Vector3.up * turnspeed * Time.deltaTime, Space.World);
				//	transform.Translate(Vector3.forward * boatspeed * Time.deltaTime);
			}

			if (Input.GetKeyUp (KeyCode.LeftArrow))
				left = false;

			if (Input.GetKeyUp (KeyCode.RightArrow))
				right = false;

		} 

		else { // if Online
			Debug.Log("inside MoveBoat online"); 

			getStimOnline ();

//			if (left && hidearrow == true)
//				left = true;
//			else left = false;

			if (Input.GetKey (KeyCode.LeftArrow) ||(left && hidearrow)&& ldaSignal()>=0) {
				//left = true;
				Debug.Log("inside left arrow");
				countL += 1;	// scoring - left rows
				//			right = false;

				// Oculus left haptic feedback
				//if(Settings.haptic){
				//	OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
				//	Debug.Log ("VIBRATE left!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
				//}

				if(!Settings.reverseHands)
					transform.Rotate (Vector3.up * turnspeed * Time.deltaTime, Space.World);
				else
					transform.Rotate (Vector3.down * turnspeed * Time.deltaTime, Space.World);
				//	transform.Translate(Vector3.forward * boatspeed * Time.deltaTime);
			}
			else if (Input.GetKey (KeyCode.RightArrow) ||(right && hidearrow) && ldaSignal()<=0) {
				//			left = false;
				right = true;
				Debug.Log("inside right arrow");
				countR += 1;	// scoring - right rows

				// Oculus right haptic feedback
				//if(Settings.haptic){
				//	OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
				//	Debug.Log ("VIBRATE right!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
				//}

				if(!Settings.reverseHands)
					transform.Rotate (Vector3.down * turnspeed * Time.deltaTime, Space.World);
				else
					transform.Rotate (Vector3.up * turnspeed * Time.deltaTime, Space.World);
				//	transform.Translate(Vector3.forward * boatspeed * Time.deltaTime);
			}

			else {
				if(hidearrow)
					countW += 1;
			}

			if(Input.GetKeyUp(KeyCode.LeftArrow)){
				left = false;
			}

			if(Input.GetKeyUp(KeyCode.RightArrow)){
				right = false;
			}

		}

	}

	// training
	void getStim()
	{
		int stim = Receivemarkers.markerint;

		switch (stim)
		{
		case 800: //hide cross
			OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
			OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
			crossui.SetActive(false);
			leftarrow.SetActive(false);
			rightarrow.SetActive(false);
			cross= false; 
			left = false;
			right = false;
			hidearrow = false;
			break;
		case 786: // show cross
			crossui.SetActive(true);
			leftarrow.SetActive(false);
			rightarrow.SetActive(false);
			cross = true;
			left = false;
			right = false;
			hidearrow = false;
			break;
		case 770: // right arrow
			crossui.SetActive(true);
			leftarrow.SetActive(false);
			rightarrow.SetActive(true);
			//if(Settings.haptic){
			//	OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
			//	Debug.Log ("VIBRATE right!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			//}
			cross= true;
			left = false;
			right = true;
			hidearrow = false;
			break;
		case 769: // left arrow
			crossui.SetActive(true);
			leftarrow.SetActive(true);
			rightarrow.SetActive(false);
			//if(Settings.haptic){
			//	OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
			//	Debug.Log ("VIBRATE left!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			//}
			cross= true; 
			left = true;
			right = false;
			hidearrow = false;
			break;
		case 781: // hide arrow
			crossui.SetActive(true);
			leftarrow.SetActive(false);
			rightarrow.SetActive(false);
			//OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
			//OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
			cross = true;
			hidearrow = true;
			//left= false;
			//right= false;
			break;
		}
	}

	public static float ldaSignal(){
		return Assets.LSL4Unity.Scripts.Examples.ExampleFloatInlet.signal;
	}

	// online
	void getStimOnline()
	{
		int stim = Receivemarkers.markerint;

		switch (stim)
		{
		case 800: //hide cross
			crossui.SetActive(false);
			leftarrow.SetActive(false);
			rightarrow.SetActive(false);
			cross= false;
			left = false;
			right = false;
			hidearrow = false;
			case800 = true;
			case786 = false;
			OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
			OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
			break;
		case 786: // show cross
			crossui.SetActive(true);
			leftarrow.SetActive(false);
			rightarrow.SetActive(false);
			cross = true;
			left = false;
			right = false;
			hidearrow = false;
			case800 = false;
			case786 = true;
			break;
		case 770: // right arrow
			crossui.SetActive(true);
			leftarrow.SetActive(false);
			rightarrow.SetActive(true);
			//if(Settings.haptic)
			//	OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
			cross= true;
			left = false;
			right = true;
			hidearrow = false;
			case800 = false;
			case786 = false;
			break;
		case 769: // left arrow
			crossui.SetActive(true);
			leftarrow.SetActive(true);
			rightarrow.SetActive(false);
			//if(Settings.haptic){
			//	OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
			//	Debug.Log ("using online left!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			//}
			cross= true;
			left = true;
			right = false;
			hidearrow = false;
			case800 = false;
			case786 = false;
			break;
		case 781: // hide arrow
			crossui.SetActive(true);
			leftarrow.SetActive(false);
			rightarrow.SetActive(false);
			//OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
			//OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
			cross= true;
			hidearrow = true;
			case800 = false;
			case786 = false;
			break;
			//		default:
			//			cross.enabled = false;
			//			leftarrow.enabled = false;
			//			rightarrow.enabled = false;
			//			break;
		}
	}

}
