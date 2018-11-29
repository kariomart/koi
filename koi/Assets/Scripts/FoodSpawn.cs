using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawn : MonoBehaviour {

	public GameObject food;
	public int numOfFood;
	public int foodRange;

	// Use this for initialization
	void Start () {

		SpawnFood ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SpawnFood() {

		GameObject foodParent = new GameObject("Foods");

		for (int i = 0; i < numOfFood; i++) {

			GameObject f = Instantiate (food, new Vector3 (Random.Range (-foodRange, foodRange), Random.Range (-foodRange, foodRange), 0), Quaternion.identity);
			f.transform.parent = foodParent.transform;

		}


	}
}
