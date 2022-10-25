using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIcicle : MonoBehaviour
{
    Rigidbody2D rb;
	PlayerMovement player;
	AudioSource audio;
	public GameObject shardsPrefab;
	public float fallSpeed = 2f;
	
	float startingLocalX;
	
	// Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		audio = GetComponent<AudioSource>();
		player = FindObjectOfType<PlayerMovement>();
		
		startingLocalX = transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null && rb.gravityScale == 0)
		{
			if(player.transform.position.x >= transform.position.x)
			{
				Fall();
			}
		}
    }
	
	void FixedUpdate()
	{
		transform.localPosition = new Vector3(startingLocalX, transform.localPosition.y, transform.localPosition.z);
	}
	
	void Fall()
	{
		audio.Play();
		rb.gravityScale = fallSpeed;
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player" || collider.gameObject.tag == "Untagged")
		{
			Instantiate(shardsPrefab, transform.position, transform.rotation);
			Destroy(gameObject);
		}
	}
}
