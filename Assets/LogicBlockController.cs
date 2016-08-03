using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LogicBlockController : MonoBehaviour {

	enum Behaviors {Trigger, Output};
    Dictionary<string, Behaviors> sideBehaviors = new Dictionary<string, Behaviors>();
    public enum BlockTypes { Start, End, Operator };
    public BlockTypes BlockType = BlockTypes.Operator;
    public float pullRadius = 4000;

    [Tooltip("Pull force of out block.")]
    public float pullForce = 5;

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

    public void FixedUpdate()
    {
        if (BlockType == BlockTypes.End)
        {
            foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius))
            {

                if (collider.gameObject.name.StartsWith("Ball"))
                {
                    // calculate direction from target to me
                    Vector3 forceDirection = transform.position - collider.transform.position;

                    // apply force on target towards me
                    collider.attachedRigidbody.AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
                }
            }
        }
    }


    void OnCollisionEnter(Collision collision) {
		// TODO Ensure that it isn't colliding with it's source panel;
		// ball.GetComponent<LogicBall> ().SourcePanel = this.gameObject;
        if (BlockType == BlockTypes.End)
        {
            Destroy(collision.gameObject);
            if (BallCount() == 1)
            {
                GameObject startBlock = GameObject.FindGameObjectWithTag("StartBlock");
                startBlock.GetComponent<LogicBlockController>().ShootBall("Front");
            }
        }

	}

    public int BallCount()
    {
       return GameObject.FindGameObjectsWithTag("Ball").Length;
    }

	public void SideClicked (GameObject side) {
		
		// If this side is a button, we'll shoot a ball from all outputs on the box
		if (!sideBehaviors.ContainsKey (side.name)) {
			return;
		}

		if (sideBehaviors [side.name] == Behaviors.Trigger) {
			foreach (KeyValuePair<string, Behaviors> pair in sideBehaviors) {
				if (pair.Value == Behaviors.Output) {
					// Shoot ball
					ShootBall (pair.Key);
				}
			}
		}

	}

	public void ShootBall(string sideName) {
        print("shooting ball");
        Transform side = transform.Find("Block_" + sideName);
        GameObject ball = (GameObject) Instantiate(Resources.Load("Ball")); ;
		MeshFilter plane = side.GetComponent<MeshFilter> ();
		plane.mesh.RecalculateNormals ();
		Rigidbody rigidbody = ball.GetComponent<Rigidbody> ();
		rigidbody.velocity = (side.localToWorldMatrix * plane.mesh.normals [0]) * 10;
		ball.transform.position = side.position + rigidbody.velocity;
		//ball.GetComponent<LogicBall> ().SourcePanel = this.gameObject;
//		ball.transform.Translate (ballRigidBody.velocity);
	}

}
