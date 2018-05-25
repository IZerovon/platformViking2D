using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemy : MonoBehaviour {

	private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerVariables.playerScore += 100;
            GetComponentInParent<Enemy>().Die();
            gameObject.SetActive(false);
        }
    }
}
