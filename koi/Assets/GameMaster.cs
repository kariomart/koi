using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster me;
	public GameState gameState;
	public int sfxChance;

	// Use this for initialization
	void Start () {

		me = this;
		
	}
	
	// Update is called once per frame
	void Update () {

		ModulateWeather();
		
	}

	void ModulateWeather() {

		int rand = Random.Range(0, sfxChance);

		if (rand == 1) {
			
		}
	}



}
