using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {
    public AudioClip startTutorialSound;
    public AudioClip startBlockTutorial;
    public AudioClip endBlockTutorial;
    public AudioClip logicBlockTutorial;
    private new AudioSource audio;
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
        audio.PlayOneShot(startTutorialSound);



    }

    void PlacedStartBlock()
    {
        if(placedStartBlock == false)
        {
            /* Play End Block tutorial */
            audio.PlayOneShot(startBlockTutorial);


            placedStartBlock = true;
        }
    }

    void PlacedEndBlock()
    {
        if (placedEndBlock == false)
        {
            /* Play Logic Block tutorial */
            audio.PlayOneShot(logicBlockTutorial);

            Debug.Log("Placed the end block");
            placedEndBlock = true;
        }   
    }
}
