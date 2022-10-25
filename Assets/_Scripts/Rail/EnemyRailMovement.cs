using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRailMovement : MonoBehaviour
{
    public float speed;
    private float baseSpeed;
	public Rigidbody2D rb;
	public Transform distMeasure;
	
	public bool gameOn = false;
	
	public SequencedMusicPlayer smp;
	
	public PlayerMovement player;
	
    public float deltaSpeed;
	
	void Awake()
	{
		DestroyOnEnd.rail = transform;
	}
	
	// Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed * -1;
		baseSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void FixedUpdate()
	{
		//speed += deltaSpeed;
		rb.velocity = transform.right * speed * -1;
		
		if(player != null)
		{
			speed = baseSpeed + (-distMeasure.position.x * deltaSpeed);
			player.drag = speed * 0.05f;
		}
		
		if(player == null)
		{
			if(gameOn)
			{
				if(speed > 0)
				{
					if(smp != null) smp.volume -= 0.01f;
					
					speed -= 0.1f;
				}
				if(speed <= 0)
				{
					speed = 0;
				}
			}
		}
	}
	
	public void SetSpeed(float newSpeed)
	{
		baseSpeed = newSpeed;
		speed = newSpeed;
	}
}
