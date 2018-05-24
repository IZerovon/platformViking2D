using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour {

    private float startSpeed;
    private bool active = true;

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && active)
        {
            active = false;
            startSpeed = other.GetComponent<PlatformInputs>().speed;
            other.GetComponent<PlatformInputs>().speed *= 1.5f;
            yield return new WaitForSeconds(3f);
            other.GetComponent<PlatformInputs>().speed = startSpeed;
            active = true;
        }
    }
}
