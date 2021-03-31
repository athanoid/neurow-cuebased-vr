using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used

public class FlashArrow : MonoBehaviour {
	public static int stimB;
	public static int fps_countL = 0; 
	public static int fps_countR = 0;
	public static float cycleDuration;
	public GameObject arrowL, arrowR;

	private float updateCount = 0;
    private float fixedUpdateCount = 0;
    private float updateUpdateCountPerSecond;
    private float updateFixedUpdateCountPerSecond;


	
	void Start () {	
		arrowL = GameObject.Find ("arrowFLR");
		arrowR = GameObject.Find ("arrowFRR");
		if(Settings.grayF){
			arrowL = GameObject.Find("arrowFLG");
			arrowR = GameObject.Find("arrowFRG");
		}

		//Debug.Log("Flash Arrow!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

		// Uncommenting this will cause framerate to drop to *10* frames per second.
        // This will mean that FixedUpdate is called more often than Update.
        //Application.targetFrameRate = 10;
        Application.targetFrameRate = 60;
        StartCoroutine(Loop());
	}
	
	// Increase the number of calls to Update.
    void FixedUpdate()
    {
        fixedUpdateCount += 1;
    }
	
	void Update () {	
		updateCount += 1;

		//StopAllCoroutines();
		//Debug.Log("stopping all coroutines----------");
		if (Settings.flash) {
			if (MoveBoat.left) {
				// make arrow flash 30 Hz
				//Debug.Log ("inside FlashArrow - move boat left");

				//StartCoroutine (blinkLeft (30.0f));
				StartCoroutine (blinkLeft (12.0f));
				//Debug.Log("blink call counter total: "+fps_countL);
			}

			if (MoveBoat.right) {
				// make arrow flash 40 Hz
				//Debug.Log ("inside FlashArrow - move boat right");
				//StartCoroutine (blinkRight (40.0f));
				StartCoroutine (blinkRight (18.0f)); 		
			}
		}
	}

	IEnumerator blinkLeft(float frequency) {
		cycleDuration = 1.0f / frequency;
		//Debug.Log("flashing left");
//		while(MoveBoat.left)
		for(int i=0; i<12; i++)
		{
			yield return new WaitForSeconds(cycleDuration);
			arrowL.SetActive(false);
			//Debug.Log("left turned off");
			yield return new WaitForSeconds(cycleDuration);
			//Debug.Log("waited for frequency");
			arrowL.SetActive(true);
			//Debug.Log("left turned on");
//			yield return new WaitForSeconds(cycleDuration);

			fps_countL += 1;
		}

//		Debug.Log("Left Counter: "+fps_countL);
	}

	IEnumerator blinkRight(float frequency) {
		cycleDuration = 1.0f / frequency;
		//Debug.Log("flashing right");
		for(int j=0; j<18; j++)
		{
			yield return new WaitForSeconds(cycleDuration);
			arrowR.SetActive(false);
			//Debug.Log("right turned off");
			yield return new WaitForSeconds(cycleDuration);
			arrowR.SetActive(true);
			//Debug.Log("right turned on");

			fps_countR += 1;
		}

//		Debug.Log("Right Counter: "+fps_countR);
	}

	// Show the number of calls to both messages.
    //void OnGUI()
    //{
    //    GUIStyle fontSize = new GUIStyle(GUI.skin.GetStyle("label"));
    //    fontSize.fontSize = 24;
    //    GUI.Label(new Rect(100, 100, 200, 50), "Update: " + updateUpdateCountPerSecond.ToString(), fontSize);
    //    GUI.Label(new Rect(100, 150, 200, 50), "FixedUpdate: " + updateFixedUpdateCountPerSecond.ToString(), fontSize);
   	//	  Debug.Log("updateUpdateCountPerSecond: " +updateUpdateCountPerSecond);
    //    Debug.Log("updateFixedUpdateCountPerSecond: " +updateFixedUpdateCountPerSecond);
    //    Debug.Log("screen current resolution: " +Screen.currentResolution.refreshRate);
        // Update is at 60 fps
        // FixedUpdate is at 50 fps
    //}

    // Update both CountsPerSecond values every second.
    IEnumerator Loop()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            updateUpdateCountPerSecond = updateCount;
            updateFixedUpdateCountPerSecond = fixedUpdateCount;


            updateCount = 0;
            fixedUpdateCount = 0;
        }
    }

}
