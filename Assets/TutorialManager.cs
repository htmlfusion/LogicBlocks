using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {
    private AudioClip Blip;
    private AudioClip Drop;
    private AudioSource audio;
    private bool placedStartBlock = false;
    private bool placedEndBlock = false;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        Invoke("StartTutorial", 5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void StartTutorial() {
        Debug.Log("second llog");
        audio.PlayOneShot(Drop);
    }

    void PlacedStartBlock()
    {
        
        if(placedStartBlock == false)
        {
          
            placedStartBlock = true;
        }
    }

    void PlacedEndBlock()
    {
        if(placedEndBlock == false)
        {
            Debug.Log("Placed the end block");
            placedEndBlock = true;
        }   
    }
}
