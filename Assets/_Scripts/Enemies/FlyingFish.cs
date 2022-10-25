using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingFish : MonoBehaviour
{
	private Rigidbody2D rb;
	
	private float startingHeight;
	private float startingLocalX;
	
	public float jumpPower;
	public float gravity;
	
	public int bouncePtr = 0;
	public int maxBounce = 4;
    // Start is called before the first frame update
    void Start()
    {
		startingHeight = transform.position.y;
		startingLocalX = transform.localPosition.x;
        rb = GetComponent<Rigidbody2D>();
		bouncePtr = Random.Range(0, maxBounce);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= startingHeight)
		{
			if(rb.velocity.y < 0)
			{
				if(bouncePtr < maxBounce)
				{
					rb.velocity = new Vector2(rb.velocity.x, 13);
					bouncePtr++;
				}
				else
				{
					rb.velocity = new Vector2(rb.velocity.x, jumpPower);
					bouncePtr = 0;
				}
			}
		}
    }
	
	void FixedUpdate()
	{
		transform.localPosition = new Vector3(startingLocalX, transform.localPosition.y, transform.localPosition.z);
		rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - gravity);
		if(rb.velocity.y < 0)
		{
			transform.localScale = new Vector3(1, -1, 1);
		}
		else
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
	}
}
