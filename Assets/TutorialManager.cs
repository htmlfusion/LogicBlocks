using UnityEngine;
using System.Collections;


namespace HoloToolkit.Unity
{
    public class TutorialManager : Singleton<TutorialManager>
    {
        // Audio clips
        public AudioClip startBlockTutorialClip;
        public AudioClip endBlockTutorialClip;
        public AudioClip startEndBlockFinishClip;
        public AudioClip logicBlockTutorialClip;
        public AudioClip flipSideTutorialClip;
        public AudioClip finishedTutorialClip;

        // Audio source and GameObjects
        private AudioSource audio;
        private GameObject tutorialAsset;
        private GameObject startMenu;
        private GameObject tutorialMenu;
        private GameObject[] logicBlocks;

        // Spatial map items
        private GameObject SpatialMap;
        private Material Transparent;
        private Material Occlusion;

        // Boolean markers to check instruction status
        private bool placedStartBlock = false;
        private bool placedEndBlock = false;
        private bool placedLogicBlock = false;
        private bool flippedSide = false;
        public bool menuOn = true;

        // Use this for initialization
        void Start()
        {
            startMenu = GameObject.Find("StartMenu");
            tutorialMenu = GameObject.Find("TutorialMenu");
            tutorialAsset = GameObject.Find("tutorial_animation");
            audio = GetComponent<AudioSource>();

            // Get materials we'll be switching for spatial mesh
            Transparent = Resources.Load("Materials/Transparent", typeof(Material)) as Material;
            Occlusion = SpatialMappingManager.Instance.SurfaceMaterial;
            
            MainMenu();
        }

        public void StartTutorial()
        {
            // Reset tutorial statuses
            placedStartBlock = false;
            placedEndBlock = false;
            placedLogicBlock = false;
            flippedSide = false;
            menuOn = false;

            startMenu.SetActive(false);
            tutorialMenu.SetActive(false);
            tutorialAsset.SetActive(false);

            StartCoroutine(PlaybackTutorial());
        }

        public void StartGame()
        {
            startMenu.SetActive(false);
            tutorialMenu.SetActive(false);
            menuOn = false;
            tutorialAsset.SetActive(false);

            // Set spatial mesh back to occlusion material
            SpatialMappingManager.Instance.SetSurfaceMaterial(Occlusion);
        }

        public void MainMenu()
        {
            startMenu.SetActive(true);
            tutorialMenu.SetActive(true);
            tutorialAsset.SetActive(true);
            menuOn = true;

            // Set transparent spatial mesh
            SpatialMappingManager.Instance.SetSurfaceMaterial(Transparent);

            // Delete created blocks, start and end blocks
            logicBlocks = GameObject.FindGameObjectsWithTag("LogicBlock");
            for (var i = 0; i < logicBlocks.Length; i++)
            {
                Destroy(logicBlocks[i]);
            }
            Destroy(GameObject.FindGameObjectWithTag("StartBlock"));
            Destroy(GameObject.FindGameObjectWithTag("EndBlock"));
        }

        IEnumerator PlaybackTutorial()
        {
            // Initial wait for tutorial to start. Might not need this initial IEnumerator, it's only 1 second
            yield return new WaitForSeconds(1);

            // Set spatial map back to occlusion material
            SpatialMappingManager.Instance.SetSurfaceMaterial(Occlusion);
            
            // Turn off tutorial asset and play instructions
            tutorialAsset.SetActive(false);
            audio.PlayOneShot(startBlockTutorialClip);
        }

        void PlacedStartBlock()
        {
            if (placedStartBlock == false)
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

}
