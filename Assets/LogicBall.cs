using UnityEngine;
using System.Collections;

public class LogicBall : MonoBehaviour {

	private GameObject sourcePanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject SourcePanel {
		get {
			return sourcePanel;
		}
		set {
			sourcePanel = value;
		}
	}
}
