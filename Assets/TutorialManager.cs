using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {
    // Audio clips
    public AudioClip startTutorialClip;
    public AudioClip startBlockTutorialClip;
    public AudioClip endBlockTutorialClip;
    public AudioClip startEndBlockFinishClip;
    public AudioClip logicBlockTutorialClip;
    public AudioClip flipSideTutorialClip;
    public AudioClip finishedTutorialClip;

    private new AudioSource audio;

    // Boolean markers to check instruction status
    private bool placedStartBlock = false;
    private bool placedEndBlock = false;
    private bool placedLogicBlock = false;
    private bool flippedSide = false;

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
        // Initial wait for tutorial to start
        yield return new WaitForSeconds(2);

        // Play beginning of tutorial
        audio.PlayOneShot(startTutorialClip);
        yield return new WaitForSeconds(startTutorialClip.length + 2);

        // Play second part of tutorial
        audio.PlayOneShot(startBlockTutorialClip);
        yield return new WaitForSeconds(startBlockTutorialClip.length + 2);

        // Play third part of tutorial
        //Invoke("startBlockTutorial", startTutorialSound.length);
        //yield return new WaitForSeconds(startBlockTutorialClip.length + 3);
    }

    void PlacedStartBlock()
    {
        if(placedStartBlock == false)
        {
            placedStartBlock = true;

            /* Play End Block tutorial */
            audio.PlayOneShot(endBlockTutorialClip);
        }
    }

    void PlacedEndBlock()
    {
        if (placedEndBlock == false)
        {
            placedEndBlock = true;

            /* Play Logic Block tutorial */
            StartCoroutine(startEndBlockFinished());
            
            //audio.PlayOneShot(logicBlockTutorialClip);
            
        }   
    }

    IEnumerator startEndBlockFinished()
    {
        audio.PlayOneShot(logicBlockTutorialClip);
        yield return new WaitForSeconds(logicBlockTutorialClip.length);
        audio.PlayOneShot(startEndBlockFinishClip);
        yield return new WaitForSeconds(startEndBlockFinishClip.length);
    }

    void PlacedLogicBlock()
    {
        if (placedLogicBlock == false)
        {
            placedLogicBlock = true;

            /* Play Flip side tutorial */
            audio.PlayOneShot(flipSideTutorialClip);

        }
    }

    void FlippedSide()
    {
        if (flippedSide == false)
        {
            flippedSide = true;

            /* Play finished tutorial clip */
            audio.PlayOneShot(finishedTutorialClip);

        }
    }
}
