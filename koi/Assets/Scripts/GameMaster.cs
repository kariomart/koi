using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster me;
	public FollowMouse_3D player;
	public GameState[] gameStates;
	public GameState gameState;
	public int sfxChance;

	public int rainChance;
	public int rainStopChance;
	public bool raining;

	public GameObject rainRipple;

	// Use this for initialization
	void Start () {

		me = this;
		GameObject g = GameObject.Find("GameStates");
		gameStates = new GameState[g.transform.childCount];

		for (int i = 0; i < gameStates.Length; i ++) {
			gameStates[i] = g.transform.GetChild(i).GetComponent<GameState>();
		} 
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		int rand = Random.Range(0, sfxChance);
		if (rand == 10) {
			rand = Random.Range(0, 101);
			if (rand > 50) {
				AudioManager.Instance.PlayRandomAboveWaterSFX();
			} else {
				AudioManager.Instance.PlayRandomUnderwaterSFX();
			}
		}

		rand = Random.Range(0, rainChance);
		if (rand == 5 && !raining) {
			AudioManager.Instance.playRain();
			AudioManager.Instance.scaleNum = 2;
			raining = true;
		}



		if (raining) {

			rand = Random.Range(0, 100);
			if (rand == 1) {
				Instantiate(rainRipple, player.transform.position, Quaternion.identity);
			}

			rand = Random.Range(0, rainStopChance);
			if (rand == 1) {
				raining = false;
				AudioManager.Instance.StartCoroutine("FadeOutRain");
				AudioManager.Instance.scaleNum = 0;
			}
		
		}	
		
	}

}
