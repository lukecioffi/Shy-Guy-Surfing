using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTunnel : MonoBehaviour
{
    public Vector2 thrust;
	
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerStay2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player")
		{
			if(collider.GetComponent<Rigidbody2D>().velocity.y < 0)
			{
				collider.GetComponent<Rigidbody2D>().AddForce(thrust * 1.5f);
			}
			else if(collider.GetComponent<Rigidbody2D>().velocity.y > 15)
			{
				collider.GetComponent<Rigidbody2D>().AddForce(-thrust);
			}
			else collider.GetComponent<Rigidbody2D>().AddForce(thrust);
		}
	}
}
