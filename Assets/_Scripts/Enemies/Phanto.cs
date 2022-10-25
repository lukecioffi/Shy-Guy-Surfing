using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phanto : MonoBehaviour
{
    private PlayerMovement player;
	[SerializeField] private Animator anim;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private AudioSource audio;
	[SerializeField] private ParticleSystem ps;
	
	public bool popped = false;
	
	Vector2 force;
	public float chaseSpeed;
	int timer;
	public int chaseTime = 400;
	
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
		ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
		if(player != null)
		{
			if(player.transform.position.x > transform.position.x + 2)
			{
				if(!popped)
				{
					Pop();
				}
			}
		}
    }
	
	void FixedUpdate()
    {
		//if(rb.velocity.y > 0)
			//rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 0.1f);
		
		if(popped && player != null)
		{
			if(timer < chaseTime)
			{
				force = player.transform.position - transform.position;
			}
			
			rb.AddForce(force.normalized * chaseSpeed);
			timer++;
		}
    }
	
	void Pop()
	{
		transform.SetParent(null);
		rb.bodyType = RigidbodyType2D.Dynamic;
		rb.velocity = new Vector2(-5f, 9.0f + Random.Range(0.0f, 2.0f));
		
		anim.Play("Rage", 0, 0f);
		audio.Play();
		ps.Play();
		
		popped = true;
	}
	
	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
