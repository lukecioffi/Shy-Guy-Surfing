using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooligan : MonoBehaviour
{
	Rigidbody2D rb;
	bool visible = false;
	
	public float startSpeed;
	
	float startingLocalX;
	float startingLocalY;
	
	public GameObject fallingPrefab;
	public ParticleSystem wake_ps;
	
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		startingLocalX = transform.localPosition.x;
		startingLocalY = transform.localPosition.y;
		
		wake_ps.Stop();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(visible)
			rb.velocity = new Vector2(-startSpeed * transform.localScale.x, rb.velocity.y);
		else
		{
			transform.localPosition = new Vector3(startingLocalX, startingLocalY, transform.localPosition.z);
		}
    }
	
	void TakeDamage()
	{
		var _f = Instantiate(fallingPrefab, transform.position, transform.rotation);
		_f.GetComponent<Rigidbody2D>().velocity = rb.velocity;
		Destroy(gameObject, 0.04f);
	}
	
	void OnBecameVisible()
	{
		visible = true;
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.layer == LayerMask.NameToLayer("Water"))
			wake_ps.Play();
	}
	
	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.gameObject.layer == LayerMask.NameToLayer("Water"))
			wake_ps.Stop();
	}
}
