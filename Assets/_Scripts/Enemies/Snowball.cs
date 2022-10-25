using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : MonoBehaviour
{
	Rigidbody2D rb;
	public ParticleSystem snow_ps;
	
	float startingLocalX;
	public bool rolling;
	
	public Collider2D trigger_t;
	
	public AudioSource skid;
	
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		snow_ps.Stop();
		
		startingLocalX = transform.localPosition.x;
		rb.AddForce(new Vector2(0, 35000));
		trigger_t.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!rolling)
		{
			transform.localPosition = new Vector3(startingLocalX, transform.localPosition.y, transform.localPosition.z);
		}
		
		if(rolling)
		{
			if(transform.localScale.x < 1)
			{
				transform.localScale *= 1.008f;
			}
			
			if(transform.localScale.x >= 1)
			{
				Collider2D[] _c = GetComponentsInChildren<Collider2D>();
				foreach(Collider2D c in _c)
				{
					c.enabled = false;
				}
			}
		}
    }
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player" || collider.gameObject.tag == "Untagged")
		{
			if(snow_ps != null) snow_ps.Play();
			rb.AddForce(new Vector2(10000, 0));
			rolling = true;
			trigger_t.enabled = true;
			skid.Play();
		}
	}
	
	public void TakeDamage()
	{
		return;
	}
}
