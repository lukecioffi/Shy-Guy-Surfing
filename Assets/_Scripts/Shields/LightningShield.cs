using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningShield : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip bounce_sfx;
    [SerializeField] ParticleSystem spark_ps;
	
	bool bounced;
	
	// Start is called before the first frame update
    void Start()
    {
        player = transform.parent.GetComponent<PlayerMovement>();
		Destroy(player.activeShield);
		player.activeShield = this.gameObject;
		rb = player.controller.m_Rigidbody2D;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire"))
		{
			if (!bounced)
			{
				if(player.flying)
				{
					player.flightTimer = 0;
					player.flying = false;
				}
				Bounce();
			}
		}
		
		
		
		
		if(bounced && (player.controller.m_OnWater || player.controller.m_Grounded))
		{
			bounced = false;
		}
		
    }
	
	void FixedUpdate()
	{
		
	}
	
	void Bounce()
	{
		rb.velocity = new Vector2(rb.velocity.x, 14.5f * transform.parent.localScale.y);
		audio.PlayOneShot(bounce_sfx, 2.0f);
		spark_ps.Play();
		bounced = true;
		return;
	}
}
