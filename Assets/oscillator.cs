using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oscillator : MonoBehaviour {

    [SerializeField] float period = 4f;
    [Range(0, 1)][SerializeField] float movementFactor = 0f;

    Vector3 startPosition;
    [SerializeField] Vector3 movementVector;

	// Use this for initialization
	void Start () {
        startPosition = GetComponent<Transform>().position;	
        print(startPosition);
	}
	
	// Update is called once per frame
	void Update () {

        if (period <= Mathf.Epsilon) { return; };

        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;

        movementFactor = Mathf.Sin(cycles * tau) / 2f + 0.5f;

        Vector3 offset = movementVector * movementFactor;
        transform.position = startPosition + offset;	
	}
}
