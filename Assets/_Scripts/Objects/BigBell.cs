using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBell : MonoBehaviour
{
    AudioSource audio;
	Animator anim;
	ParticleSystem ps;
	
	int timer = 0;
	
	// Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		ps = GetComponentInChildren<ParticleSystem>();
		ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void FixedUpdate()
	{
		if(timer > 0) timer--;
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player" && timer == 0)
		{
			Shine();
		}
	}
	
	void Shine()
	{
		anim.Play("Toll", 0, 0f);
		audio.Play();
		ps.Play();
		timer = 20;
		Invoke("Darken", 0.5f);
		
		PictureBoo[] all_boos = FindObjectsOfType<PictureBoo>();
		foreach(PictureBoo b in all_boos)
		{
			if (b.popped)
			{
				b.col.enabled = false;
				Destroy(b.gameObject, 0.5f);
			}
		}
	}
	
	void Darken()
	{
		var black = GameObject.FindWithTag("Darkness");
		Animator b_a = black.GetComponent<Animator>();
		b_a.Play("Darken", 0, 0f);
	}
}
