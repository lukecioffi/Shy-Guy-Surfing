using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBlock : MonoBehaviour
{
	PlayerMovement player;
	Animator[] anims;
	
	int hp = 9;
	
	Animator block_anim;
	SpriteRenderer rend;
	AudioSource audio;
	[SerializeField] Sprite[] sprites;
	
	[SerializeField] GameObject shardsPrefab;
	
    // Start is called before the first frame update
    void Start()
    {
		player = transform.parent.GetComponent<PlayerMovement>();
		rend = GetComponent<SpriteRenderer>();
		anims = transform.parent.GetComponentsInChildren<Animator>();
		block_anim = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
		
		player.flightTimer = 0;
		player.flying = false;
        player.enabled = false;
		foreach(Animator a in anims)
		{
			if(a != block_anim && a != null)
				a.speed = 0;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
		{
			block_anim.Play("Shake", 0, 0f);
			audio.Play();
			hp--;
			
			if(hp <= 0)
			{
				Break();
			}
			else if(hp < 3)
			{
				rend.sprite = sprites[2];
			}
			else if(hp < 6)
			{
				rend.sprite = sprites[1];
			}
		}
		
		if(player == null)
		{
			Break();
		}
    }
	
	void Break()
	{
		player.enabled = true;
		foreach(Animator a in anims)
		{
			if(a != block_anim && a != null)
				a.speed = 1;
		}
		Instantiate(shardsPrefab, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
