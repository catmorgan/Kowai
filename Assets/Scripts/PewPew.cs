using UnityEngine;
using System.Collections;

public class PewPew : MonoBehaviour {
	public float speed;
	public float dir;
	
	//private RaycastHit hit;
	//private Ray ray;
	private string lastButton = "";
	
	void Update () {
		Debug.Log(lastButton);
		if (dir == 1) {
		this.transform.Translate(Vector3.right * speed * Time.deltaTime);
			lastButton = "right";
		} else {
		this.transform.Translate(Vector3.left * speed * Time.deltaTime);
			lastButton = "left";
		}
		
//		ray = new Ray(new Vector2(this.transform.position.x,this.transform.position.y), new Vector2(0, dir));
//		if (Physics.Raycast(ray, out hit, 0.1f, enemyMask)){
//		}
		
		if (!this.renderer.isVisible) {
			Destroy (this.gameObject);
		}
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Enemy")) {
			Destroy (other.gameObject);
		}
	}
}
