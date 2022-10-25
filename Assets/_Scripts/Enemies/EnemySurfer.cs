using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySurfer : MonoBehaviour
{
	public bool isBoss;
	
    int i_frames = 0;
	[SerializeField] int max_i_frames = 75;
	
	int hp = 0;
	[SerializeField] int max_hp = 6;
	
	public SpriteRenderer rend;
	
	public Color baseColor;
	public Color hurtColor;
	
	public AudioSource audio;
	public AudioClip[] sounds;
	
	Collider2D[] cols;
	
	[SerializeField] private LineRenderer hpLine;
	
	[SerializeField] private ParticleSystem ps;
	[SerializeField] private GameObject splashPref;
	[SerializeField] private GameObject fallingPrefab;

	public Rigidbody2D rb;
	
	[SerializeField] private Transform r_GroundCheck;
	[SerializeField] private LayerMask r_WhatIsWater;
	
	[SerializeField] private string defeated_message;
	
	public bool r_OnWater;
	
	[SerializeField] NotificationBox notif;
	
	// Start is called before the first frame update
    void Start()
    {
		cols = GetComponentsInChildren<Collider2D>();
		hp = max_hp;
		
		notif = FindObjectOfType<NotificationBox>(); // Find NotificationBox
    }

    // Update is called once per frame
    void Update()
    {
		// Toggle splash when on water
		r_OnWater = false;
		
		Collider2D[] colliders = Physics2D.OverlapCircleAll(r_GroundCheck.position, 0.35f, r_WhatIsWater);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				r_OnWater = true;
			}
		}
		
		if(r_OnWater)
		{
			if(!ps.isPlaying)
			{
				Instantiate(splashPref, transform.position, transform.rotation);
				ps.Play();
			}
		}
		else
		{
			if(ps.isPlaying)
				ps.Stop();
		}
        
		// Update HP line
		if(hpLine != null)
		{
			hpLine.SetPosition(1, new Vector3(((float)hp / (float)max_hp) * 2 - 1, 0, 0));
			hpLine.transform.localScale = new Vector3(transform.lossyScale.x, 1, 1);
		}
    }
	
	void FixedUpdate()
	{
		if(i_frames > 0)
		{
			foreach(Collider2D c in cols) // Disable triggers
			{
				if(c.isTrigger)
					c.enabled = false;
			}
			
			if(i_frames % 2 == 1)
			{
				rend.color = hurtColor;
			}
			else rend.color = baseColor;
			
			i_frames--;
		}
		else
		{
			rend.color = baseColor;
			i_frames = 0;
			foreach(Collider2D c in cols)
			{
				c.enabled = true;
			}
		}
	}
	
	void TakeDamage()
	{
		if(i_frames == 0)
		{
			//audio.Play();
			hp--;
			if(hp == 0)
			{
				Die();
			}
			i_frames = max_i_frames;
		}
	}
	
	void Die()
	{
		if(!isBoss)
		{
			if(fallingPrefab != null) Instantiate(fallingPrefab, transform.position, transform.rotation);
			Destroy(gameObject, 0.04f);
		}
		else if(isBoss)
		{
			if(notif != null) notif.SendNotif(defeated_message);
			Instantiate(fallingPrefab, transform.position, transform.rotation);
			PlayerPrefs.SetInt("BossesDefeated", PlayerPrefs.GetInt("BossesDefeated") + 1); 
			Destroy(transform.parent.gameObject, 0.04f);
		}
	}
	
	void ActivateEvent(string call_event)
	{
		transform.parent.SendMessage(call_event);
	}
	
	void PlaySound(int num)
	{
		audio.PlayOneShot(sounds[num]);
	}
	
	void Hazardize()
	{
		gameObject.tag = "Instakill";
	}
	
	void Dehazardize()
	{
		gameObject.tag = "Stompable";
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(FindObjectOfType<PlayerMovement>() != null)
		{
			if(collider.gameObject.tag == "Bomb")
			{
				collider.gameObject.BroadcastMessage("Explode");
				Die();
			}
		}
	}
}
