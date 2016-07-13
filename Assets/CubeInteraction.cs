using UnityEngine;
using System.Collections;

public class CubeInteraction : MonoBehaviour {

	public Rigidbody projectile;
	public Transform shotPos;
	public float shotForce = 1000f;
	public float moveSpeed = 10f;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				print (hit.triangleIndex);
				if (hit.triangleIndex == 2 || hit.triangleIndex == 3) {
					print ("You hit the top!");
				}
			}
		}
	}
		
}
