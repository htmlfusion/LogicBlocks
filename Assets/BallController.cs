using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour
{

    public GameObject block;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        AudioSource hitSound = GetComponent<AudioSource>();
        hitSound.volume = hitSound.volume * collision.relativeVelocity.magnitude * 2;
        hitSound.Play();
    }
}
