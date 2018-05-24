using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smooth = 5.0f;
    public float offset = 1.0f;
	
	void LateUpdate ()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y + offset, transform.position.z);

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);
        if (transform.position.y < 1.5f)
        {
            transform.position = new Vector3(transform.position.x, 1.5f, -10);
        }
    }
}
