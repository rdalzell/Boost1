using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    Rigidbody rigitbody;
    AudioSource thrust_audio;

	// Use this for initialization
	void Start () {
        rigitbody = GetComponent<Rigidbody>();
        thrust_audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Ok");
                break;
            default:
                print("Dead");
                break;
        }
    }

    private void Thrust()
    {
        float thrust_factor = mainThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rigitbody.AddRelativeForce(Vector3.up * thrust_factor);
            if (!thrust_audio.isPlaying)
            {
                thrust_audio.Play();
            }
        }
        else
        {
            if (thrust_audio.isPlaying)
            {
                thrust_audio.Stop();
            }
        }
    }


    private void Rotate()
    {
        float thrust_factor = rcsThrust * Time.deltaTime;

        rigitbody.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * thrust_factor);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * thrust_factor);
        }

        rigitbody.freezeRotation = false;
    }
}
