using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformInputs : MonoBehaviour {

    public float speed = 10.0f;
    public float jumpHeight = 14.0f;
    public Transform groundcheck;

    private bool grounded;
    private float horizontalDirection;
    private Rigidbody2D rgdb2d;
    private Animator anim;

    void Start()
    {
        rgdb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalDirection = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizontalDirection, 0, 0) * speed * Time.deltaTime, Camera.main.transform);

        grounded = Physics2D.OverlapPoint(groundcheck.position);

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            rgdb2d.velocity += new Vector2(rgdb2d.velocity.x, jumpHeight);
        }

        if (horizontalDirection > 0)
        {
            Flip(1);
        } else if (horizontalDirection < 0)
        {
            Flip(-1);
        }

        anim.SetFloat("Speed", Mathf.Abs(horizontalDirection));
    }

    private void Flip(int facingRight)
    {
        Vector3 myScale = transform.localScale;
        myScale.x = facingRight;
        transform.localScale = myScale;
    }
}
