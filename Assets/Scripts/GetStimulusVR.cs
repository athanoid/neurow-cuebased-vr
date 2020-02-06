using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStimulusVR : MonoBehaviour {

	public GameObject cross, leftarrow, rightarrow;

	// Use this for initialization
	void Start () {

		cross.SetActive(false);
		leftarrow.SetActive(false);
		rightarrow.SetActive(false);
	}

	// Update is called once per frame
	void Update () {


		if (Input.GetKey("escape"))
		{
			Debug.Log ("Quit!");
			Application.Quit();
		}


		getStim ();

	}


	void getStim()
	{
		int stim = Receivemarkers.markerint;

		switch (stim)
		{
		case 800: //hide cross
			cross.SetActive(false);
			leftarrow.SetActive(false);
			rightarrow.SetActive(false);
			break;
		case 786: // show cross
			cross.SetActive(true);
			leftarrow.SetActive(false);
			rightarrow.SetActive(false);
			break;
		case 770: // right arrow
			cross.SetActive(true);
			leftarrow.SetActive(false);
			rightarrow.SetActive(true);
			break;
		case 769: // left arrow
			cross.SetActive(true);
			leftarrow.SetActive(true);
			rightarrow.SetActive(false);
			break;
		case 781: // hide arrow
			cross.SetActive(true);
			leftarrow.SetActive(false);
			rightarrow.SetActive(false);
			break;
			//		default:
			//			cross.enabled = false;
			//			leftarrow.enabled = false;
			//			rightarrow.enabled = false;
			//			break;
		}
	}


}
