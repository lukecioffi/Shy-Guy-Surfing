using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleShield : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip bounce_sfx;
	
	bool bounding;
	
	// Start is called before the first frame update
    void Start()
    {
        player = transform.parent.GetComponent<PlayerMovement>();
		Destroy(player.activeShield);
		player.activeShield = this.gameObject;
		rb = player.controller.m_Rigidbody2D;
		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire")
			&& !player.controller.m_OnWater && !player.controller.m_Grounded)
		{
			player.flightTimer = 0;
			BoundDown();
		}
		
		
		
		
		if(bounding && (player.controller.m_OnWater || player.controller.m_Grounded))
		{
			BoundUp();
		}
		
    }
	
	void FixedUpdate()
	{
		//player.flightTimer = 0;
		if(bounding)
		{
			rb.velocity = new Vector2(rb.velocity.x, -25 * transform.parent.localScale.y);
		}
		
	}
	
	void BoundDown()
	{
		rb.velocity = new Vector2(rb.velocity.x, -25 * transform.parent.localScale.y);
		anim.Play("BoundDown", 0, 0f);
		audio.PlayOneShot(bounce_sfx);
		bounding = true;
		return;
	}
	
	void BoundUp()
	{
		rb.velocity = new Vector2(rb.velocity.x, 20 * transform.parent.localScale.y);
		anim.Play("BoundUp", 0, 0f);
		audio.PlayOneShot(bounce_sfx);
		bounding = false;
	}
}
