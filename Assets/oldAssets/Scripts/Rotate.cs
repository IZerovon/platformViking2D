using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    public float speed = -1000.0f;
	
	void Update ()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * speed);
	}
}
