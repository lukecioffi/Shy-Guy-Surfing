using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewaysGravity : MonoBehaviour
{
	public Rigidbody2D rb;
	public float intensity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void FixedUpdate()
	{
		rb.AddForce(-Vector2.right * 9.8f * intensity);
	}
}
