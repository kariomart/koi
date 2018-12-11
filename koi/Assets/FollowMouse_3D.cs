using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse_3D : MonoBehaviour {

    public float moveSpeed = 0.004f;
    Camera cam;
    Transform reticle;
    AudioSource waterSfx;
    public GameObject foodRipple;
    public bool goingUp;
    public bool goingDown;
    int timeToSurface;
    int goingToSurfaceTimer;

    // Use this for initialization
    void Start () {
        
        reticle = GameObject.Find("reticle").transform;
        Cursor.visible = false;
        cam = Camera.main;
        waterSfx = GetComponent<AudioSource>();
        
    }
  
  // Update is called once per frame
  void FixedUpdate () {

        //checkPosition();
        //waterSounds();
        spawnRipple();
        Vector3 mouseDir = (cam.ScreenToWorldPoint(Input.mousePosition) - transform.position);
		mouseDir.y = 0.51f;
        //reticle.position = Vector2.Lerp(transform.position, cam.ScreenToWorldPoint(Input.mousePosition), moveSpeed) + mouseDir * 2;
        //transform.position = Vector3.Lerp(transform.position, mouseDir, moveSpeed);
		transform.position = transform.position + mouseDir * moveSpeed;
        transform.position = new Vector3(transform.position.x, .51f, transform.position.z);

        //float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0f, 0f, rotation_z - 90f);

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

        if (goingUp) {
            AudioManager.Instance.openFilter();
            //goingToSurfaceTimer ++;

            // if (goingToSurfaceTimer > timeToSurface) {
            //     goingDown = true;
            // }
        }

    }

    void checkPosition() {

        if (Mathf.Abs(transform.position.x) > MapGenerator.me.tileSize * MapGenerator.me.xTiles / 2) {
            transform.position = Vector2.zero;
        }

        if (Mathf.Abs(transform.position.y) > MapGenerator.me.tileSize * MapGenerator.me.yTiles / 2) {
            transform.position = Vector2.zero;
        }
    }

    void waterSounds() {

        float dis = Vector2.Distance(transform.position, cam.transform.position); 
        float vol = AudioManager.RemapFloat(dis, 0, 4.5f, 0, 1f);
        waterSfx.volume = vol;

		if (transform.position.x < Camera.main.transform.position.x) {dis *= -1;}
		float pan = AudioManager.RemapFloat(dis, -5f, 5f, -1f, 1f);
        waterSfx.panStereo = pan;


    }


    void OnTriggerEnter(Collider coll) {

		if (coll.gameObject.layer == LayerMask.NameToLayer("Food")) {
            goingUp = true;
            //AudioManager.Instance.PlayFoodSound();
			Destroy(coll.gameObject, 2);
		}

	}

    void spawnRipple() {

        int rand = Random.Range(0, 100);
        if (rand == 1) {
            Instantiate(foodRipple, new Vector2(transform.position.x + Random.Range(-1, 1), transform.position.y + Random.Range(-1, 1)), Quaternion.identity);
        }

    }

}
