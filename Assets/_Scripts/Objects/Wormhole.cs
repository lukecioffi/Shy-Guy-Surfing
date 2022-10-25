using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
	int cooldown = 0;
	public Wormhole exit;
	public bool reverseGrav;
	AudioSource audio;
	
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(cooldown > 0) cooldown--;
		else cooldown = 0;
    }
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.transform.tag == "Player")
		{
			if(cooldown == 0) TryWarp(collider.transform);
		}
	}
	
	void TryWarp(Transform entity)
	{
		if(exit == null) return;
		entity.position = exit.transform.position;
		if(reverseGrav)
		{
			entity.transform.localScale = new Vector2(entity.transform.localScale.x, -entity.transform.localScale.y);
			entity.GetComponent<PlayerMovement>().default_grav *= -1;
		}
		exit.AcceptWarp();
		audio.Play();
	}
	
	public void AcceptWarp()
	{
		
		cooldown = 20;
	}
}
