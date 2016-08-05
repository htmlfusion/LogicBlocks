using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LogicBlockController : MonoBehaviour {

    enum Behaviors { Trigger, Output };
    public string[] sides = new string[6]{"Front", "Back", "Top", "Bottom", "Left", "Right"};
    Dictionary<string, Behaviors> sideBehaviors = new Dictionary<string, Behaviors>();
    public enum BlockTypes { Start, End, Operator };
    public BlockTypes BlockType = BlockTypes.Operator;
    public float pullRadius = 4000;

    [Tooltip("Pull force of out block.")]
    public float pullForce = 100;

    // Use this for initialization
    void Start () {
        for (int i=0; i<sides.Length; i++)
        {
            sideBehaviors.Add(sides[i], Behaviors.Output);
        }
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

        print("collision");
        Vector3 direction = collision.transform.position - transform.position;
        if (Vector3.Dot(transform.forward, direction) > 0)
        {
            print("Back");
        }
        if (Vector3.Dot(transform.forward, direction) < 0)
        {
            print("Front");
        }
        if (Vector3.Dot(transform.forward, direction) == 0)
        {
            print("Side");
        }

        if (BlockType == BlockTypes.End)
        {
            Destroy(collision.gameObject);
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

    public void FlipSide(string sideName)
    {
        GameObject visualPort = transform.Find(sideName + "_Cylinder").gameObject;
        string materialName;
        if (sideBehaviors[sideName] == Behaviors.Output)
        {
            sideBehaviors[sideName] = Behaviors.Trigger;
            materialName = "InBallMaterial";
        }
        else
        {
            sideBehaviors[sideName] = Behaviors.Output;
            materialName = "OutBallMaterial";
        }

        Material portMaterial = Resources.Load("Materials/"+materialName, typeof(Material)) as Material;
        visualPort.GetComponent<Renderer>().material = portMaterial;
    }

    public Vector3 sideNormal(string sideName)
    {
        float cubeSize = GetComponent<MeshRenderer>().bounds.max.x;
        Vector3 pos;
        switch (sideName)
        {
            case "Bottom":
                pos =  -transform.up;
                break;
            case "Top":
                pos = transform.up;
                break;
            case "Left":
                pos = - transform.right;
                break;
            case "Right":
                pos = transform.right;
                break;
            case "Back":
                pos = - transform.forward;
                break;
            case "Front":
                pos = transform.forward;
                break;
            default:
                pos = transform.forward;
                break;
        }
        return (transform.localToWorldMatrix * pos).normalized;
    }

    public string NormalToSide(Vector3 hitNormal)
    {
        for (int i=0; i<sides.Length; i++)
        {
            Vector3 side = sideNormal(sides[i]);
            if (hitNormal == side)
            {
                return sides[i];
            }
        }
        return null;
    }

    public Vector3 sidePosition(string sideName)
    {
        float cubeSize = GetComponent<MeshRenderer>().bounds.max.x;
        return transform.position + sideNormal(sideName) * cubeSize / 2;
    }

	public void ShootBall(string sideName)
    {
        GameObject ball = (GameObject) Instantiate(Resources.Load("Ball")); ;
		Rigidbody rigidbody = ball.GetComponent<Rigidbody> ();
        Vector3 pos = sidePosition(sideName);
        Vector3 normal = sideNormal(sideName);
        rigidbody.velocity = normal;
		ball.transform.position = pos + rigidbody.velocity;
	}

}
