using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rocket : MonoBehaviour {

    Rigidbody rigitbody;

	// Use this for initialization
	void Start () {
        rigitbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        ProcessKey();
	}

    private void ProcessKey()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            rigitbody.AddRelativeForce(Vector3.up);    
        }

        if (Input.GetKey(KeyCode.A))
        {
            print("Port Thrust");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            print("Starboard Thrust");
        }    
                
    }
}
