using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator anim;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip blast_sfx;
    [SerializeField] GameObject puffPrefab;
	
	bool blasted;
	public float blastSpeed;
	
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
			if(!blasted && anim.GetCurrentAnimatorStateInfo(0).IsName("Fire"))
			{
				Blast();
			}
		}
		
		if(blasted && (player.controller.m_OnWater || player.controller.m_Grounded))
		{
			blasted = false;
		}
    }
	
	void FixedUpdate()
	{
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
		{
			rb.velocity = new Vector2(blastSpeed, rb.velocity.y);
		}
	}
	
	void Blast()
	{
		anim.Play("Dash", 0, 0f);
		audio.PlayOneShot(blast_sfx, 2.0f);
		blasted = true;
		return;
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
		{
			if(collider.gameObject.tag == "Stompable")
			{
				audio.PlayOneShot(player.kickSFX, 4.0f);
				Instantiate(puffPrefab, collider.transform.position, transform.rotation);
				
				collider.gameObject.BroadcastMessage("TakeDamage");
				//Destroy(collider.gameObject);
			}
		}
	}
}
