using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {
	
	public LayerMask collisionMask;
	public LayerMask obstacleMask;
	[HideInInspector]
	public bool grounded;
	[HideInInspector]
	public bool stopped;
	
	//box collider
	new private BoxCollider collider = new BoxCollider();
	private Vector3 size;
	private Vector3 center;
	private Vector3 originalSize;
	private Vector3 originalCenter;
	private float colliderScale;
	//distance between player and ground
	private float space = .005f;
	private PlayerStats playerStats;
	
	private float numOfCollisionRaysX = 3;
	private float numOfCollisionRaysY = 10;
	
	Ray ray;
	RaycastHit hit;
	
	public void Start() {
		//get player's collider
		collider = GetComponent<BoxCollider>();
		playerStats = GetComponent<PlayerStats>();
		colliderScale = transform.localScale.x;
		originalSize = collider.size;
		originalCenter = collider.center;
		SetCollider(originalSize, originalCenter);
	}

	public void move(Vector2 moveAmount) {
		//x and y to be changed
		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		Vector2 position = transform.position;
		
		//above and below collisions
		grounded = false;
		for (int i = 0; i < numOfCollisionRaysX; i++) {
			float dir = Mathf.Sign(deltaY);
			//left, center, and right position for raycast
			float x = (position.x + center.x - size.x /2) + size.x/(numOfCollisionRaysX - 1) * i;
			//bottom of collider
			float y = position.y + center.y + size.y /2 * dir;
			ray = new Ray(new Vector2(x,y), new Vector2(0, dir));
			Debug.DrawRay(ray.origin, ray.direction);
			//if the ray hits a collide-able object above or below
			if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaY) + space, collisionMask)){
				//distance between player and hit object
				float dist = Vector3.Distance(ray.origin, hit.point);
				
				//stops player at object
				if (dist > space) {
					deltaY = dist  * dir - space * dir;
				} else {
					deltaY = 0;
				}

				grounded = true;
				break;
			}
		}
		
		stopped = false;
		//left and right collisions
		for (int i = 0; i < numOfCollisionRaysY; i++) {
			float dir = Mathf.Sign(deltaX);
			//left, center, and right position
			float x = position.x + center.x + size.x /2 * dir;
			//side of collider
			float y = position.y + center.y - size.y /2 + size.y/(numOfCollisionRaysY-1) * i;
			ray = new Ray(new Vector2(x,y), new Vector2(dir,0));
			Debug.DrawRay(ray.origin, ray.direction);
			if (Physics.Raycast(ray, out hit, Mathf.Abs(deltaX) + space, collisionMask)){
				//distance between player and ground
				float dist = Vector3.Distance(ray.origin, hit.point);
				
				//stops player at ground
				if (dist > space) {
					deltaX = dist  * dir - space * dir;
				} else {
					deltaX = 0;
				}
				stopped = true;
				break;
			}
		}
		
		//collision of player's falling direction
		if (!grounded && !stopped) {
			Vector3 playersDir = new Vector3(deltaX, deltaY);
			Vector3 origin = new Vector3(position.x + center.x + size.x /2 * Mathf.Sign(deltaX), 
				position.y + center.y + size.y /2 * Mathf.Sign(deltaY));
			ray = new Ray(origin, playersDir.normalized);
		
			if(Physics.Raycast(ray, Mathf.Sqrt(deltaX * deltaX + deltaY * deltaY), collisionMask)) {
				grounded = true;
				deltaY = 0;
			}
			
		}
		
		//return the final translation after collision detection
		Vector2 finalMovement = new Vector2(deltaX, deltaY);
		transform.Translate(finalMovement);
		
	}
	
	public void SetCollider (Vector3 s, Vector3 c) {
		collider.size = s;
		collider.center = c;
		
		size = s * colliderScale;
		center = c * colliderScale;
	}
	
	void OnTriggerEnter(Collider other) {
		Debug.Log("why ");
		if (other.gameObject.CompareTag("Enemy") || 
			other.gameObject.CompareTag("Obstacle")) {
			playerStats.dead = true;
		}
	}

}
