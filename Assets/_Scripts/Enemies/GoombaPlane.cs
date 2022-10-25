using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaPlane : MonoBehaviour
{
    PlayerMovement player;
	Animator anim;
	AudioSource audio;
	Rigidbody2D rb;
	bool following;
	
	[SerializeField] private GameObject bulletPrefab;
	
	private int age, age2;
	public int firerate;
	public ParticleSystem ps;
	
	// Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();
		audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		
		if(player != null)
		{
		
			if(!following)
			{
				if(player.transform.position.x > transform.position.x)
				{
					audio.Play();
					transform.SetParent(null);
					anim.Play("Follow", 0, 0f);
					following = true;
				}
			}
		}
		
    }
	
	void FixedUpdate()
	{
		if(player != null)
		{
			if(following)
			{
				transform.position = new Vector2
				(
					Mathf.Lerp(transform.position.x, player.transform.position.x - (age2 * 0.01f + 6), Time.deltaTime),
					Mathf.Lerp(transform.position.y, player.transform.position.y, Time.deltaTime)
				);
			}
		}
		
		if(age >= firerate)
		{
			Invoke("Fire", 0.0f);
			Invoke("Fire", 0.15f);
			age = 0;
		}
		
		if(following)
		{
			age++;
			age2++;
			
			if(age2 > 500)
				TakeDamage();
		}
	}
	
	void Fire()
	{
		GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
		Rigidbody2D bullet_rb = bullet.GetComponent<Rigidbody2D>();
		bullet_rb.velocity = transform.right * 10;
	}

	void OnBecameInvisible()
	{
		age = -1000;
		rb.bodyType = RigidbodyType2D.Dynamic;
		rb.gravityScale = 3.5f;
	}
	
	void TakeDamage()
	{
		age = -1000;
		age2 = 0;
		ps.Play();
		following = false;
		Destroy(gameObject, 1);
	}
}
