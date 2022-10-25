using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatBehavior : MonoBehaviour
{
	private PlayerMovement player;
	private EnemyRailMovement rail;
	[SerializeField] private Animator anim;
	[SerializeField] private AudioSource audio;
	[SerializeField] private Rigidbody2D rb;
	
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        rail = FindObjectOfType<EnemyRailMovement>();
    }

    // Update is called once per frame
    void Update()
    {
		if(player != null)
		{
			if(player.transform.position.x > transform.position.x - 4)
			{
				if(anim.GetBool("Hanging") == true)
				{
					Drop();
				}
			}
		}
    }
	
	void FixedUpdate()
    {
		if(rb.velocity.y < 0)
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + 0.2f);
    }
	
	void Drop()
	{
		transform.SetParent(null);
		
		if(rail != null) rb.velocity = new Vector2(-rail.speed - Random.Range(0.0f, 4.0f), -8 + Random.Range(-2.0f, 1.0f));
		else rb.velocity = new Vector2(Random.Range(0.0f, 4.0f), -8 + Random.Range(-2.0f, 1.0f));
		
		audio.Play();
		anim.SetBool("Hanging", false);
	}
}
