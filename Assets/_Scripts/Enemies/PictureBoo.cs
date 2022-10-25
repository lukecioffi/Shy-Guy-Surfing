using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureBoo : MonoBehaviour
{
    private PlayerMovement player;
	[SerializeField] private Animator anim;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private AudioSource audio;
	[SerializeField] private ParticleSystem ps;
	[SerializeField] private ParticleSystem ps2;
	
	public bool popped = false;
	
	Vector2 force;
	public float chaseSpeed;
	int timer;
	public int chaseTime = 400;
	
	public RuntimeAnimatorController[] skins;
	public Collider2D col;
	
	GameObject black;
	
    // Start is called before the first frame update
    void Start()
    {
		col = GetComponent<Collider2D>();
        player = FindObjectOfType<PlayerMovement>();
		ps.Stop();
		ps2.Stop();
		
		black = GameObject.FindWithTag("Darkness");
    }

    // Update is called once per frame
    void Update()
    {
		if(player != null)
		{
			if(player.transform.position.x > transform.position.x - 3)
			{
				if(!popped)
				{
					Pop();
				}
			}
		}
		
		if(black == null)
		{
			Destroy(gameObject);
		}
    }
	
	void FixedUpdate()
    {
		//if(rb.velocity.y > 0)
			//rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 0.1f);
		
		if(popped && player != null)
		{
			// if(timer < chaseTime)
			// {
				force = player.transform.position - transform.position;
				
				if(player.transform.position.x > transform.position.x)
				{
					transform.localScale = new Vector3(1, 1, 1);
				}
				else transform.localScale = new Vector3(-1, 1, 1);
			// }
			
			rb.AddForce(force.normalized * chaseSpeed);
			timer++;
		}
    }
	
	void Pop()
	{
		transform.SetParent(null);
		rb.bodyType = RigidbodyType2D.Dynamic;
		rb.velocity = new Vector2(2f, 2.0f + Random.Range(-2.0f, 1.0f));
		
		audio.Play();
		ps.Play();
		ps2.Play();
		
		anim.runtimeAnimatorController = skins[Random.Range(0, skins.Length)];
		popped = true;
	}
}
