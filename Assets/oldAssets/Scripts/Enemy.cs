using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 3.0f;
    public float damage = 25.0f;
    public Transform frontCheck;
    public LayerMask layerMask;


    private float facingRight = -1f;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        transform.Translate(new Vector3(facingRight, 0f, 0f) * speed * Time.deltaTime);

        if (Physics2D.OverlapPoint(frontCheck.position, layerMask))
        {
            facingRight *= -1f;
            transform.localScale = new Vector3(transform.localScale.y * -facingRight, transform.localScale.y, transform.localScale.z);
        }      
    }

    public void Die()
    {
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var item in colliders)
        {
            item.enabled = false;
        }

        GetComponent<Rigidbody2D>().AddForce(new Vector2(2f, 13f), ForceMode2D.Impulse);
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);

        Invoke("DisableObject", 2.0f);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerVariables>().Harm(damage);
        }
    }

    private void DisableObject()
    {
        Destroy(gameObject);
    }
}
