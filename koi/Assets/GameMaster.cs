using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster me;
	public FollowMouse_3D player;
	public GameState[] gameStates;
	public GameState gameState;
	public int sfxChance;
	public int stateTransitionChance;

	public bool transitionGameState;

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
	void Update () {

		ModulateWeather();
		CheckWeather();

		if (transitionGameState) {

		}
	}

	void ModulateWeather() {

		int rand = Random.Range(0, sfxChance);

		if (rand == 1) {

			AudioManager.Instance.PlayRandomWeatherSFX();
			
		}
	}

	void CheckWeather() {

		int rand = Random.Range(0, stateTransitionChance);

		if (rand == 1) {

			transitionGameState = true;

		}
	}



}
