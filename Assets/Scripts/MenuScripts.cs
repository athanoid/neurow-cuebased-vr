using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScripts : MonoBehaviour {

	public Button graz;
	public Button ntrainm;
	public Button ntrainf;

	public Button nonlinem;
	public Button nonlinef;

	// Use this for initialization
	void Start () {

		Button grazbtn = graz.GetComponent<Button>();
		grazbtn.onClick.AddListener(delegate() { GoToLevel("graz_training"); });

		Button ntrainmbtn = ntrainm.GetComponent<Button>();
		ntrainmbtn.onClick.AddListener(delegate() { GoToLevel("boat_training"); });

		Button ntrainfbtn = ntrainf.GetComponent<Button>();
		ntrainfbtn.onClick.AddListener(delegate() { GoToLevel("boat_training_female"); });

		Button nonlinembtn = nonlinem.GetComponent<Button>();
		ntrainmbtn.onClick.AddListener(delegate() { GoToLevel("boat_online"); });

		Button nonlinefbtn = nonlinef.GetComponent<Button>();
		ntrainfbtn.onClick.AddListener(delegate() { GoToLevel("boat_online_female"); });

	}
	
	// Update is called once per frame
	void Update () {


		
	}

	void GoToLevel(string lvl)
	{
		// Load the level
		Application.LoadLevel(lvl);
	}
		



}
