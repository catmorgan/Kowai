using UnityEngine;
using System.Collections;

public class ShootWeapon : MonoBehaviour {
	
	private PlayerMovement movement;
	//public float speed;
	public GameObject bulletPrefab;
	//public Animation shoot;
	
	//publ float deltaX;
	private Transform spawn;

	void Start () {
		movement = GetComponent<PlayerMovement>();
	}
	
	void Update () {
		
		//deltaX = speed * Time.deltaTime;
		//bullet.transform.Translate(new Vector3(deltaX,0,0));//
		if (Input.GetButtonDown("Shoot")) {
//			Vector3 pos = new Vector3(
//				transform.position.x + transform.localScale.y / 2 + 
//				movement.playerDirection,
//				transform.position.y,
			//shoot.Play();
//				0);
			Bang ();
			}
		//shoot.Stop();
			
//			float distance = 20;
//			Ray ray = new Ray(bullet.position,bullet.forward);
//			RaycastHit hit;
//			if (Physics.Raycast(ray,out hit, distance)) {
//				distance = hit.distance;
//			}
			
			//bullet.transform.Translate(Vector3.right * speed * Time.deltaTime);
		}
	
	void Bang() {
		
		if (movement.playerDirection == 1) {
			spawn = transform.Find ("SpawnRight");
		} else {
			spawn = transform.Find("SpawnLeft");
		}
		
		GameObject bullet = Instantiate(bulletPrefab,
			spawn.position,
			Quaternion.identity) as GameObject;
		
		PewPew p = bullet.GetComponent<PewPew>();
		p.dir = movement.playerDirection;
	}
}
