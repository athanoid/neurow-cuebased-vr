﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// used

public class Scoring : MonoBehaviour
{
	public static Scoring SCORE;
	
	private static int waypoints = 0;
//	public Transform player;
//	Transform waypoint;
	
	private static float time;
	
	//public Text score;
//	public Text finalscore;
	public Text gameTime;

	public static string curr_score = "";
	public static float upTime = 0f;

	public static float duration;
	public static float initDuration;

	public static int count, crossCount, temp;
	public static int tempScore = 0;
	public static float avgScore = 0f;
	public static float avgAvg = 0f;
	public static float avgTotal = 0f;
	public static float rewardAvg = 0f;
	public static float fScore = 0f;

	public GameObject fireworks;
	public GameObject rewardtext;
	public GameObject scoreText;
	public GameObject finalScoreText;
	public GameObject rocket;
	public static bool fireDisplay = false;
	public static bool once = false;
	public Transform defTextPos;


// count = countR + countL;
// avgScore = count / (count+countW);
public static Scoring Instance;

	void Awake()
	{
		Instance = this;
		rewardtext = GameObject.Find("RewardText");
		fireworks = GameObject.Find("Fireworks");
		rewardtext.SetActive(false);
		fireworks.SetActive(false);
		rocket = GameObject.Find("Rocket2");
		scoreText = GameObject.Find("ScoreText");
		finalScoreText = GameObject.Find("FinalScore");

		//scoreText.SetActive(true);
		//Debug.Log ("First Move!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
		finalScoreText.transform.localPosition = new Vector3(0f,-150f,0f);


//		if(SCORE != null)
//			GameObject.Destroy(SCORE);
//		else
//			SCORE = this;
//		
//		DontDestroyOnLoad(this);
	}


	public static void updateDuration(string dur) {

		duration = float.Parse (dur);
		initDuration = duration;
	//	Debug.Log("time "+time+" : "+"upTime "+upTime);
	}

	void Update()
	{
		Debug.Log("inside scoring update");
		if(scoreText != null)
		{
			//Debug.Log("inside score!=null");
			duration -= Time.deltaTime;
			if(duration < 0)
			{

				Settings.Timeout = true;
				duration = initDuration;
			//	Debug.Log("durationIN "+duration);
			}
			else{
				Settings.Timeout = false;
			}

			//Debug.Log("duration "+duration);


			gameTime.text = duration.ToString("F0") + " s";

// NEW SCORING ----------------------------------------------------------------------
			count = MoveBoat.countR + MoveBoat.countL;	//# of moves
//			Debug.Log("countR "+MoveBoat.countR);
//			Debug.Log("countL "+MoveBoat.countL);
//			Debug.Log("countW "+MoveBoat.countW);

			//if(count > 0){
	//			Debug.Log("inside count>0");
//				curr_score = ""+waypoints;
				curr_score = ""+fScore;
//				score.text = curr_score;
//				finalscore.text = ""+fScore;
				scoreText.GetComponent<Text>().text = ""+fScore;
//				Debug.Log("count: "+count);
//				avgScore = (float)count / (count + MoveBoat.countW);
//				Debug.Log("averageScore "+avgScore);
			//}

			//---- how to know if b/w blocks vs. b/w cues in block?------------------
			if(!MoveBoat.training){ //show reward
				
				print("MoveBoat.training: "+MoveBoat.training);

				// update final score
				//if(temp == 0 && MoveBoat.case786){	//786 = show cross
					//Debug.Log("case786");
			//		fScore += tempScore;
	//				Debug.Log("finalScore: "+scoreText.GetComponent<Text>().text);
				//}

				// only show points
				if(MoveBoat.cross && count >= 0f){ 
					temp += 1;	// counting #x cross disappears
//					avgScore = (float)count / (count + MoveBoat.countW);
				}
// CROSS COUNT
				if(temp > 0 && MoveBoat.case800){
					//Debug.Log("entering cross count");
					temp = 0;
					crossCount += 1;	// counting # cues for block
					avgScore = (float)count / (count + MoveBoat.countW);
					avgTotal += avgScore;
					once = true;
					
	//				Debug.Log("crossCount from cross count: "+crossCount);
	//				Debug.Log("avgScore: "+avgScore);
					
				}

				if(crossCount >= 1 && MoveBoat.case800){
					//Debug.Log("inside reward scoring");
//					Debug.Log("average score: "+avgScore);
// INSIDE REWARD
					if(crossCount == 4){
						avgAvg = avgTotal / 4.0f;
	//					Debug.Log("block average: "+avgAvg);
						rewardAvg = avgAvg;
					}
					else{
						rewardAvg = avgScore;
					}

					// REWARDS
					if(rewardAvg >= 0.0f && rewardAvg <= 0.55f){
	//					Debug.Log("averageScore<65 "+rewardAvg);
						rewardtext.GetComponent<Text>().text = "+1";
						tempScore = 1;
	//					Debug.Log("tempScore: "+tempScore);
					}
					else if(rewardAvg > 0.55f && rewardAvg <= 0.85f){
	//					Debug.Log("averageScore>=65 "+rewardAvg);
						rewardtext.GetComponent<Text>().text = "+5";
						tempScore = 5;
	//					Debug.Log("tempScore: "+tempScore);
					}
					// if(rewardAvg > 0.85){
					else { // if(rewardAvg >= 85f)
	//					Debug.Log("averageScore>=85 "+rewardAvg);
						rewardtext.GetComponent<Text>().text = "+10";
						tempScore = 10;
	//					Debug.Log("tempScore: "+tempScore);
					} 
				//}
// SHOW REWARD		
					//Debug.Log("show before reward---------------------------------");
					rewardtext.SetActive(true);
					//Debug.Log("show reward---------------------------------");
					if(once == true){
						fScore += tempScore;
						once = false;
					}
					//scoreText.SetActive(true);
					finalScoreText.transform.localPosition = new Vector3(0f,0f,0f);
					//Debug.Log("crossCount: "+crossCount);

					if(crossCount == 4){
						Debug.Log("FIREWORKS!!!!!!!!!!!!!!");
						fireDisplay = true;
						fireworks.SetActive(true);
						//if (fireworks.isActive == false )
						//	rewardtext.SetActive(false);

						// move reward text to fixed cross position
						//MoveReward.move = true;
// BREAK
						if(rocket.GetComponent<ParticleSystem>().isStopped){
							rewardtext.SetActive(false);
							Debug.Log("inside stopping particle system & hiding reward");
						}
					}
				}
				else if(crossCount == 0 && MoveBoat.case800){
					// ***** add false point feedback here *****
				}

				if(crossCount >= 1 && MoveBoat.cross){	// hide reward(s)
// HIDE				Debug.Log("MoveBoat.cross "+MoveBoat.cross);
	//				Debug.Log("inside hide reward");
					rewardtext.SetActive(false);
					//scoreText.SetActive(false);
					finalScoreText.transform.localPosition = new Vector3(0f,-150f,0f);
// MOVE POINTS
//					if(fireDisplay == true){
//						rewardtext.transform.position = new Vector3(rewardtext.transform.position.x, rewardtext.transform.position.y + 290.48f, rewardtext.transform.position.z);
						// moving reward text back to original position
//					}
					fireDisplay = false;
					fireworks.SetActive(false);


// RESET			// reset count values
	//				Debug.Log("reseting counts");
//					MoveBoat.countR = 0;
//					MoveBoat.countL = 0;
//					MoveBoat.countW = 0;
					if (crossCount == 4){
	//					Debug.Log("reseting crossCount!!!!!!!!!!!!!!!!!");
						//fScore += tempScore;
						crossCount = 0;
						avgTotal = 0;
						avgAvg = 0;
					}
	//				Debug.Log("average score after reset: "+avgScore);
//					avgScore = 0;
//					temp = 0;
				}


				// points & percentages
//				if(Settings.percentage){ 
//					if(avgScore > 0.0f && avgScore < 0.65f){
//						//Settings.Instance.rewardtext.GetComponent<Text>().text = "+1 "+avgScore+"%";
//					}
//					else if(avgScore >= 0.65f && avgScore < 0.85f){
//						//Settings.Instance.rewardtext.GetComponent<Text>().text = "+5 "+avgScore+"%";
//					}
//					else{ // if(avgScore >= 85f)
//						//Settings.Instance.rewardtext.GetComponent<Text>().text = "+10 "+avgScore+"%";
//					} 
//				}

			} 


			
//------------------------------------------------------------------------------------------
		}
	}
	
//	public static void Add()
//	{
//		waypoints++;
//		//	print(waypoints);
//	}
}