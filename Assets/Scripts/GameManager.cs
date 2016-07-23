using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	
	public GameObject player;
	private GameCamera cam;
	private Vector3 pos;
	
	// set up camera and spawn player
	void Start () {
		cam = GetComponent<GameCamera>();
		pos = cam.transform.position;
		Spawn ();
	}
	
	void Update() {
		if (GameObject.FindGameObjectsWithTag("Player").Length == 0) {
			cam.transform.position = pos;
			Spawn ();
		}
	}
	
	//spawn the player and have camera follow it
	private void Spawn() {
		cam.followTarget((Instantiate(player, new Vector3(-1,3,0), Quaternion.identity) as GameObject).transform);
	}
}
