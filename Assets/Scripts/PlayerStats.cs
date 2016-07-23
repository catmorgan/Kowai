using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {
	public float lives;
	public float ammo;
	public float EYEPOWER;
	public bool dead = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
		if (this.transform.position.y < -7) {
			dead = true;
		}
		if (dead) {
			Destroy (this.gameObject);
		}
	}
	
	void OnGUI() {
	}
}
