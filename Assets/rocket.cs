using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    [SerializeField] AudioClip thrustSound = null;
    [SerializeField] AudioClip newLevelSound = null;
    [SerializeField] AudioClip deathSound = null;

    [SerializeField] ParticleSystem thrustParticle;
    [SerializeField] ParticleSystem successParticle = null;
    [SerializeField] ParticleSystem deathParticle = null;

    enum State { alive, dead, transcending };

    State state;

    Rigidbody rigitbody;
    AudioSource thrust_audio;

	// Use this for initialization
	void Start () 
    {
        state = State.alive;
        rigitbody = GetComponent<Rigidbody>();
        thrust_audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (state == State.alive)
        {
            Thrust();
            Rotate();
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.transcending;
                thrust_audio.PlayOneShot(newLevelSound);
                successParticle.Play();
                Invoke("LoadNextLevel", 1f);
                break;
            default:
                state = State.dead;
                thrust_audio.Stop();
                deathParticle.Play();
                thrust_audio.PlayOneShot(deathSound);
                Invoke("RestartLevel", 1f);
                break;
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void Thrust()
    {
        float thrust_factor = mainThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rigitbody.AddRelativeForce(Vector3.up * thrust_factor);
            if (!thrust_audio.isPlaying)
            {
                thrust_audio.PlayOneShot(thrustSound);
            }
            thrustParticle.Play();
        }
        else
        {
            if (thrust_audio.isPlaying)
            {
                thrust_audio.Stop();
                thrustParticle.Stop();
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
