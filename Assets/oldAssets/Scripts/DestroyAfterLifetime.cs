using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterLifetime : MonoBehaviour {

    public float lifetime;

	void Start ()
    {
        Invoke("DestroyObject", lifetime);
	}
	
	private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
