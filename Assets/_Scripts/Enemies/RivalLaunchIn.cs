using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalLaunchIn : MonoBehaviour
{
    [SerializeField]
	private Collider2D col;
    [SerializeField]
	private Rigidbody2D rb;
    [SerializeField]
	private Vector2 launchVelocity;
	
	
	// Start is called before the first frame update
    void Start()
    {
		rb.velocity = launchVelocity;
		
		col.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
		if(col.enabled == false)
		{
			if(rb.velocity.y < 0)
			{
				col.enabled = true;
			}
		}
    }
}
