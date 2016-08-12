using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {
    public AudioClip startTutorialSound;
    public AudioClip startBlockTutorialSound;
    public AudioClip endBlockTutorialSound;
    public AudioClip logicBlockTutorialSound;
    private new AudioSource audio;
    private bool placedStartBlock = false;
    private bool placedEndBlock = false;

	// Use this for initialization
	void Start () {
       audio = GetComponent<AudioSource>();
       //Invoke("StartTutorial", 5);

        StartCoroutine(StartTutorial());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator StartTutorial() {
        yield return new WaitForSeconds(5);
        audio.PlayOneShot(startTutorialSound);
        yield return new WaitForSeconds(startTutorialSound.length + 5);
        audio.PlayOneShot(startBlockTutorialSound);
        yield return new WaitForSeconds(startBlockTutorialSound.length + 5);
        //Invoke("startBlockTutorial", startTutorialSound.length);
    }

   /* void startBlockTutorial()
    {
        audio.PlayOneShot(startBlockTutorialSound);
    }*/

    void PlacedStartBlock()
    {
        if(placedStartBlock == false)
        {
            /* Play End Block tutorial */
            audio.PlayOneShot(endBlockTutorialSound);


            placedStartBlock = true;
        }
    }

    void PlacedEndBlock()
    {
        if (placedEndBlock == false)
        {
            /* Play Logic Block tutorial */
            audio.PlayOneShot(logicBlockTutorialSound);

            Debug.Log("Placed the end block");
            placedEndBlock = true;
        }   
    }
}
