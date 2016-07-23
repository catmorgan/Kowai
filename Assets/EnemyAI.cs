using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	
	private GameObject player;
	public GameObject enemy;
	public int num;
	
	private int one = 1;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.FindGameObjectWithTag("Player");
		
		if (Vector3.Distance(player.transform.position, this.transform.position) < 10) {
			while (one > 0 ) {
			Instantiate(enemy, this.transform.position, Quaternion.identity);
				one--;
				//this.transform.Translate(2,0,0);
				//num--;
			//}
			}
		}
	}
}
