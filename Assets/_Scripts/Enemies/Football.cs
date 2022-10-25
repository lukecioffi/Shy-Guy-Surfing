using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Football : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
	[SerializeField] Vector2 bounceForce;
	[SerializeField] float minBounce;
	[SerializeField] float maxBounce;
	
	// Start is called before the first frame update
    void Awake()
    {
		bounceForce = new Vector2(bounceForce.x, Random.Range(minBounce, maxBounce));
    }
	
	void Start()
	{
		rb.AddForce(bounceForce);
		bounceForce = new Vector2(0, bounceForce.y);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.layer == LayerMask.NameToLayer("Water"))
		{
			rb.velocity = new Vector2(rb.velocity.x, 0);
			rb.AddForce(bounceForce);
		}
	}
	
	void TakeDamage()
	{
		rb.velocity = new Vector2(rb.velocity.x, 0);
		rb.AddForce(bounceForce * -0.5f);
	}
}
