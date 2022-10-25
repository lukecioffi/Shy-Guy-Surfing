using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingKoopa : MonoBehaviour
{
	public SimplePatrolPath path;
	public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void TakeDamage()
	{
		path.enabled = false;
		GetComponent<Collider2D>().enabled = false;
		rb.bodyType = RigidbodyType2D.Dynamic;
		rb.gravityScale = 3;
	}
}
