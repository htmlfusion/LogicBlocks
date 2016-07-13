using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LogicBlockController : MonoBehaviour {

	enum Behaviors {Trigger, Output};

	Dictionary<string, Behaviors> sideBehaviors = new Dictionary<string, Behaviors>();

	// Use this for initialization
	void Start () {
		sideBehaviors.Add ("Front", Behaviors.Output);
		sideBehaviors.Add ("Top", Behaviors.Trigger);
		sideBehaviors.Add ("Bottom", Behaviors.Output);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			GameObject _this = gameObject;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.transform == _this.transform) {
					SideClicked (hit.collider.gameObject);
				}
			}
		}
	}

	void OnCollisionEnter(Collision collision) {
		print ("Collision");
		// TODO Ensure that it isn't colliding with it's source panel;
		// ball.GetComponent<LogicBall> ().SourcePanel = this.gameObject;

	}

	public void SideClicked (GameObject side) {
		
		// If this side is a button, we'll shoot a ball from all outputs on the box
		print(side.name);
		if (!sideBehaviors.ContainsKey (side.name)) {
			return;
		}

		if (sideBehaviors [side.name] == Behaviors.Trigger) {
			foreach (KeyValuePair<string, Behaviors> pair in sideBehaviors) {
				if (pair.Value == Behaviors.Output) {
					// Shoot ball
					Transform outputSide = transform.Find(pair.Key);
					ShootBall (outputSide);
				}
			}
		}

	}

	private void ShootBall(Transform side) {
		GameObject ball = (GameObject)Instantiate(Resources.Load("LogicSphere")); ;
		ball.transform.localScale = new Vector3 (4, 4, 4);
		MeshFilter plane = side.GetComponent<MeshFilter> ();
		plane.mesh.RecalculateNormals ();
		Rigidbody rigidbody = ball.GetComponent<Rigidbody> ();
		rigidbody.velocity = (side.localToWorldMatrix * plane.mesh.normals [0]) * 10;
		ball.transform.position = side.position;
		ball.GetComponent<LogicBall> ().SourcePanel = this.gameObject;
//		ball.transform.Translate (ballRigidBody.velocity);
	}
}
