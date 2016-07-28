using UnityEngine;
using System.Collections;

public class FreezeAfterCollision : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        GameObject audioGameObject = transform.Find("DropAudio").gameObject;
        AudioSource source = audioGameObject.GetComponent<AudioSource>();
        source.volume = collision.relativeVelocity.magnitude;
        source.Play();
        StartCoroutine(Freeze());
    }



    IEnumerator Freeze()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(GetComponent<Rigidbody>());
    }

}
