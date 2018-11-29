using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour {

    public float moveSpeed = 0.004f;

    // Use this for initialization
    void Start () {

    }
  
  // Update is called once per frame
  void FixedUpdate () {

        transform.position = Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), moveSpeed);
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z - 90f);

        if (Input.GetKeyDown(KeyCode.Space)) {
            int rand  = Random.Range(0, AudioManager.Instance.scales.Length);

            while (rand == AudioManager.Instance.scaleNum) {
				rand = Random.Range(0, AudioManager.Instance.scales.Length);
			}
            AudioManager.Instance.scaleNum = rand;
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            //AudioManager.Instance.scaleNum = AudioManager.Instance.scaleNum % AudioManager.Instance.scales.Length;

        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)) {
            AudioManager.Instance.LowerSFXOctave();
            
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            AudioManager.Instance.RaiseSFXOctave();
            
        }
    }
	
}
