﻿using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
	
	private static AudioManager instance = null;
	public FollowMouse player;
	
	//we need an SFX Prefab - these will be instantiated for the purpose of playing sounds
	[SerializeField] GameObject myPrefabSFX;
	
	//we also need two audio sources for game music and menu music
	[SerializeField] AudioSource gameMusicAudioSource;
	public int scaleNum;
	public AudioClip[][] scales = new AudioClip[4][]; //, menuMusicAudioSource;

	//this is where our game SFX are going to live
	
	//clips that play when we activate different objects
	[Header("Food Sounds")]
	public AudioClip[] mixolydianScale;
	public AudioClip[] pentatonicScaleC3;
	public AudioClip[] pentatonicScaleC2;
	public AudioClip[] dorianScale;
	
	
	//our audio mixer groups, which we are routing our sfx to
	[Header("Mixer Groups")] 
	public AudioMixerGroup abstractAmbience;
	public AudioMixer ambienceMaster;

	//Mixer snapshots let us crossfade easily between game states.
	//We can also add weights to multiple snapshots in order to blend them.
	// [Header("Mixer Snapshots")] 
	// public AudioMixerSnapshot menuMixerSnapshot;
	// public AudioMixerSnapshot gameMixerSnapshot;
	

	//========================================================================
	//Singleton Pattern


	public static AudioManager Instance {
		get { 
			return instance;
		}
	}

	void Awake () {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
		} else {
			instance = this;
		}

		DontDestroyOnLoad(this.gameObject);
		scales[0] = pentatonicScaleC3;
		scales[1] = mixolydianScale;
		scales[2] = dorianScale;
		scales[3] = pentatonicScaleC2;

		ambienceMaster = abstractAmbience.audioMixer;

		// if (SceneManager.GetActiveScene().name == "Menu") {
		// 	StartMenu();
		// }

		
	}
	//========================================================================

	
	//In tour, once we activate a site, it illuminates and plays a tone.
	//This is currently set up so that we randomly select a sound effect from the corresponding array,
	//Then route it to the appropriate group in the mixer
	public void PlayFoodSound() {
		//Plays a random site sound when we find a site (one of the large circles)
		AudioClip foodSound;
		//First find a clip randomly from the array
		AudioClip[] scale = scales[scaleNum];
		foodSound = scale[Random.Range(0, scale.Length)];
		float dis = Vector2.Distance(Camera.main.transform.position, player.transform.position);
		if (player.transform.position.x < Camera.main.transform.position.x) {dis *= 1;}
		dis = Mathf.Clamp(dis /= 5f, -1, 1);
//		Debug.Log(dis);
		
		//Then we play this clip - note that nothing is changing for panning and volume is set at 1.0
		PlaySFX(foodSound, 1.0f, dis, abstractAmbience);
	}
	
	//========================================================================
	
	
	//This is a general method to instantiate our SFX prefab with the settings that we want, then destroy it when it's
	//done playing
	public void PlaySFX (AudioClip g_SFX, float g_Volume, float g_Pan, AudioMixerGroup g_destGroup) {
		GameObject t_SFX = Instantiate (myPrefabSFX) as GameObject;
		t_SFX.name = "SFX_" + g_SFX.name;
		t_SFX.GetComponent<AudioSource> ().clip = g_SFX;
		t_SFX.GetComponent<AudioSource> ().volume = g_Volume;
		t_SFX.GetComponent<AudioSource> ().panStereo = g_Pan;
		t_SFX.GetComponent<AudioSource> ().outputAudioMixerGroup = g_destGroup;
		t_SFX.GetComponent<AudioSource> ().Play ();
		DestroyObject(t_SFX, g_SFX.length);
	}

	public void LowerSFXOctave() {

		float currentPitch;
		ambienceMaster.GetFloat("pitch", out currentPitch);
		float newPitch = currentPitch - Mathf.Pow(1.05946f, 14);
		ambienceMaster.SetFloat("pitch",  newPitch);

	}

	public void RaiseSFXOctave() {

		float currentPitch;
		ambienceMaster.GetFloat("pitch", out currentPitch);
		float newPitch = currentPitch + Mathf.Pow(1.05946f, 12);
		ambienceMaster.SetFloat("pitch",  newPitch);

	}


	// public void StartGame() {
	// 	if (gameMusicAudioSource != null && !gameMusicAudioSource.isPlaying) {
	// 		Debug.Log("Play Game Music");
	// 		gameMusicAudioSource.outputAudioMixerGroup = gameMusicGroup;
	// 		gameMusicAudioSource.Play();
	// 	}

	// 	if (gameMixerSnapshot != null) {
	// 		gameMixerSnapshot.TransitionTo(1.0f);
	// 	}
		
	// }

	
	
	//I've included a remap float function, which might be useful if you want to scale audio values based
	//on arbitrary gameplay ranges
	public static float RemapFloat (float inputValue, float inputLow, float inputHigh, float outputLow, float outputHigh) {
		return (inputValue - inputLow) / (inputHigh - inputLow) * (outputHigh - outputLow) + outputLow;
	}

	public void setScale(int index = 0) {

		scaleNum = index;

	}
	
}
