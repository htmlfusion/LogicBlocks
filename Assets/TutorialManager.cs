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
    private GameObject tutorialAsset;

    // Boolean markers to check instruction status
    private bool placedStartBlock = false;
    private bool placedEndBlock = false;
    private bool placedLogicBlock = false;
    private bool flippedSide = false;

	// Use this for initialization
	void Start () {
        tutorialAsset = GameObject.Find("tutorial_animation");
        audio = GetComponent<AudioSource>();

        StartCoroutine(StartTutorial());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator StartTutorial() {
        // Initial wait for tutorial to start
        yield return new WaitForSeconds(3);

        // Play beginning of tutorial and wait until it finishes
        audio.PlayOneShot(startTutorialClip);
        yield return new WaitForSeconds(startTutorialClip.length + 3);

        // Turn off tutorial asset
        // tutorialAsset.SetActive(false);

        // Play instructions
        audio.PlayOneShot(startBlockTutorialClip);
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
            
        }   
    }

    IEnumerator startEndBlockFinished()
    {
        // Play clip for finishing start/end block placing
        audio.PlayOneShot(startEndBlockFinishClip);
        yield return new WaitForSeconds(startEndBlockFinishClip.length + 1);

        // Play clip for logic block placing
        audio.PlayOneShot(logicBlockTutorialClip);
        yield return new WaitForSeconds(logicBlockTutorialClip.length);
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
