using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 6f;
    public Rigidbody2D ballRb;
    private AudioSource bounceSound;
    // Start is called before the first frame update
    void Start()
    {
        ballRb.AddForce(Vector2.left*speed,ForceMode2D.Impulse);
        bounceSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Paddle"))
        {
            bounceSound.Play();
            ballRb.AddForce((ballRb.position - collision.gameObject.GetComponent<Rigidbody2D>().position), ForceMode2D.Impulse);
        } 
    }
}
