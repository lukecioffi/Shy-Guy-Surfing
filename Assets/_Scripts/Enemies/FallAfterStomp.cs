using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallAfterStomp : MonoBehaviour
{
    Rigidbody2D rb;
	Collider2D c;
	SidewaysGravity sg;
	
	// Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		c = GetComponent<Collider2D>();
		sg = GetComponent<SidewaysGravity>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void TakeDamage()
	{
		if(sg == null)
		{
			rb.bodyType = RigidbodyType2D.Dynamic;
			rb.gravityScale = 3.5f;
		}
		else
		{
			transform.SetParent(null);
			rb.bodyType = RigidbodyType2D.Dynamic;
			sg.enabled = true;
		}
		Invoke("DisableCollider", 0.04f);
	}
	
	void DisableCollider()
	{
		c.enabled = false;
	}
}
