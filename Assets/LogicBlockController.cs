using UnityEngine;
using System.Collections.Generic;

public class LogicBlockController : MonoBehaviour {

    public enum Behaviors { Trigger, Output };
    public string[] sides = new string[6]{"Front", "Back", "Top", "Bottom", "Left", "Right"};
    Dictionary<string, Behaviors> sideBehaviors = new Dictionary<string, Behaviors>();
    public enum BlockTypes { Start, End, Operator };
    public BlockTypes BlockType = BlockTypes.Operator;
    public float pullRadius = 4000;
    public Renderer rend;

    [Tooltip("Pull force of out block.")]
    public float pullForce = 100;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
        if (this.tag == "StartBlock")
        {
            rend.material.color = Color.green;
            TogglePorts(false);
        }
        else if (this.tag == "EndBlock")
        {
            rend.material.color = Color.red;
            TogglePorts(false);
        }
        for (int i=0; i<sides.Length; i++)
        {
            SetSideBehavior(sides[i], Behaviors.Trigger);
        }
    }

    void TogglePorts(bool active)
    {
        foreach (Transform child in transform) 
        {
            if (child.gameObject.name.EndsWith("_Cylinder"))
            {
                child.gameObject.SetActive(active);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
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
        if (BlockType == BlockTypes.End)
        {
            Destroy(collision.gameObject);
        }
    }

    public int BallCount()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        print("Balls " + balls.Length);
        return balls.Length;
    }

	public bool SideClicked (string sideName) {

        bool fired = false;
		// If this side is a button, we'll shoot a ball from all outputs on the box
		if (!sideBehaviors.ContainsKey (sideName)) {
			return fired;
		}

		if (sideBehaviors [sideName] == Behaviors.Trigger) {
			foreach (KeyValuePair<string, Behaviors> pair in sideBehaviors) {
				if (pair.Value == Behaviors.Output) {
					// Shoot ball
                    // If any of the balls were successfully shot, then we set fired to true
					if(ShootBall (pair.Key))
                    {
                        fired = true;
                    }
                }
			}
		}
        return fired;
	}

    public void FlipSide(string sideName)
    {
        if (sideBehaviors[sideName] == Behaviors.Output)
        {
            SetSideBehavior(sideName, Behaviors.Trigger);
        }
        else
        {
            SetSideBehavior(sideName, Behaviors.Output);
        }

    }

    public void SetSideBehavior(string sideName, Behaviors behavior)
    {
        string materialName;
        if (behavior == Behaviors.Output)
        {
            materialName = "OutBallMaterial";
        }
        else
        {
            materialName = "InBallMaterial";
        }
        GameObject visualPort = transform.Find(sideName + "_Cylinder").gameObject;
        Material portMaterial = Resources.Load("Materials/" + materialName, typeof(Material)) as Material;
        visualPort.GetComponent<Renderer>().material = portMaterial;
        sideBehaviors[sideName] = behavior;
    }

    public float Width()
    {
        float cubeSize = (transform.localToWorldMatrix * GetComponent<BoxCollider>().bounds.max).x;
        return cubeSize;
    }

    public float sideClearence(string sideName)
    {
        Vector3 normal = sideNormal(sideName);
        Vector3 position = sidePosition(sideName);
        RaycastHit[] hits = Physics.RaycastAll(position, normal);
        if (hits.Length > 0)
        {
            float shortest = Mathf.Infinity;
            for (int i=0; i<hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if (hit.distance < shortest)
                {
                    shortest = hit.distance;
                }
            }
            return shortest;
        }
        else
        {
            return Mathf.Infinity;
        }
    
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
        return pos;
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
        float cubeSize = transform.localScale.x;
        Vector3 sideDir =  sideNormal(sideName);
        Vector3 offset = sideDir * cubeSize / 2;
        return transform.position + offset;
    }

	public bool ShootBall(string sideName)
    {
        float clearence = sideClearence(sideName);
        float width = Width();
        print(clearence);
        print(width);

        // If the face is too close to another object then don't shoot the ball
        if (clearence > width )
        {
            GameObject ball = (GameObject)Instantiate(Resources.Load("Ball"));
            Rigidbody rigidbody = ball.GetComponent<Rigidbody>();
            Vector3 pos = sidePosition(sideName);
            Vector3 normal = sideNormal(sideName);
            rigidbody.velocity = normal * 3;
            ball.transform.position = pos + normal * transform.localScale.x;
            return true;
        } else
        {
            return false;
        }
    }

}
