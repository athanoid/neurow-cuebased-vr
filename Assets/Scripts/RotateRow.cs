using UnityEngine;
using System.Collections;

// used
// rotates hands (rows boat)

public class RotateRow : MonoBehaviour {

	public static  float speed = 300f;

	public float angle, angle1;
	public float xDiff, yDiff;

	float angleOffset = 0f;
	Quaternion initRotationL, initRotationR;

	public Transform targetHand;

	// Use this for initialization
	void Start () {

		angleOffset = 90.0f;
		initRotationL = GameObject.FindGameObjectWithTag ("LeftRow").transform.localRotation;
		initRotationR = GameObject.FindGameObjectWithTag ("RightRow").transform.localRotation;

	}
	
	// Update is called once per frame
	void Update () {
	
		if (GameObject.Find ("rigidHand") != null) {
			targetHand = GameObject.Find ("rigidHand").transform;
		} else {
			targetHand = targetHand;
		}

		if (MoveBoat.training) {

			// Row Left 
			if ((Input.GetKey (KeyCode.LeftArrow) || (MoveBoat.left && MoveBoat.hidearrow)) && this.gameObject.name == "Lpivot") {
				GameObject.FindGameObjectWithTag ("LeftRow").transform.Rotate (Vector3.right * speed * Time.deltaTime);
				// Oculus left haptic feedback
				if(Settings.haptic){
					//OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
					StartCoroutine(Haptics (1, 1, 4.0f, false, true));
                    //if (Application.isEditor)
                    //    Debug.Log ("VIBRATE left training!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
				}
			}
			//if (MoveBoat.left == false && this.gameObject.name != "Lpivot") {
			//	Debug.Log ("STOP VIBRATING LEFT TRAINING!!!!!!!!");
			//	OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
			//}

			// Row Right 
			if ((Input.GetKey (KeyCode.RightArrow) ||(MoveBoat.right && MoveBoat.hidearrow)) && this.gameObject.name == "Rpivot") {
				GameObject.FindGameObjectWithTag ("RightRow").transform.Rotate (Vector3.right * speed * Time.deltaTime);
				// Oculus right haptic feedback
				if(Settings.haptic){
					//OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
					StartCoroutine(Haptics (1, 1, 4.0f, true, false));
                   //if (Application.isEditor)
                   //     Debug.Log ("VIBRATE right!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
				}
			}

		} else { // for ONLINE

			// Row Left 
			if ((Input.GetKey (KeyCode.LeftArrow) ||(MoveBoat.left && MoveBoat.hidearrow && MoveBoat.ldaSignal()>=0) ) && this.gameObject.name == "Lpivot") {
				GameObject.FindGameObjectWithTag ("LeftRow").transform.Rotate (Vector3.right * speed * Time.deltaTime);
				// Oculus left haptic feedback
				if(Settings.haptic){
					OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.LTouch);
					Debug.Log ("VIBRATE left online!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
				}
			}
			if ((MoveBoat.left && MoveBoat.ldaSignal() < 0) || MoveBoat.cross == false) {
				Debug.Log ("STOP VIBRATING LEFT ONLINE!!!!!!!!");
				OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
			}

			// Row Right 
			if ((Input.GetKey (KeyCode.RightArrow) || (MoveBoat.right && MoveBoat.hidearrow && MoveBoat.ldaSignal()<=0) ) && this.gameObject.name == "Rpivot") {
				GameObject.FindGameObjectWithTag ("RightRow").transform.Rotate (Vector3.right * speed * Time.deltaTime);
				// Oculus right haptic feedback
				if(Settings.haptic){
					OVRInput.SetControllerVibration(1, 1, OVRInput.Controller.RTouch);
					Debug.Log ("VIBRATE right!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
				}
			}
			if ((MoveBoat.right && MoveBoat.ldaSignal() > 0) || MoveBoat.cross == false) {
				Debug.Log ("STOP VIBRATING RIGHT ONLINE!!!!!!!!");
				OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
			}
		}
	}

	IEnumerator Haptics(float frequency, float amplitude, float duration, bool rightHand, bool leftHand) {
		if (rightHand)
			OVRInput.SetControllerVibration (frequency, amplitude, OVRInput.Controller.RTouch);
		if (leftHand)
			OVRInput.SetControllerVibration (frequency, amplitude, OVRInput.Controller.LTouch);

		yield return new WaitForSeconds(duration);

		if (rightHand)
			OVRInput.SetControllerVibration (0, 0, OVRInput.Controller.RTouch);
		if (leftHand)
			OVRInput.SetControllerVibration (0, 0, OVRInput.Controller.LTouch);
	}

}
