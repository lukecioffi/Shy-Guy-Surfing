using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackOFireSpawner : MonoBehaviour
{
	[SerializeField]
	private int timer;
	[SerializeField]
	private int fireRate;
	
	public float min_pos = -10.0f;
	public float max_pos = 11.0f;
	
	[SerializeField]
	private bool vertical;
	
	[SerializeField]
	private GameObject jackOPrefab;
	
	[SerializeField]
	private Vector2 initVel;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= fireRate)
		{
			Drop();
			timer = 0;
		}
    }
	
	void FixedUpdate()
	{
		timer++;
	}
	
	void Drop()
	{
		Vector3 firePoint;
		if(!vertical)
		{
			firePoint = new Vector3
				(transform.position.x + Random.Range(min_pos, max_pos) + 0.5f, transform.position.y, transform.position.z);
		}
		else
		{
			firePoint = new Vector3
				(transform.position.x, transform.position.y + Random.Range(min_pos, max_pos) + 0.5f, transform.position.z);
		}
		var proj = Instantiate(jackOPrefab, firePoint, transform.rotation);
		Rigidbody2D proj_rb = proj.GetComponent<Rigidbody2D>();
		proj_rb.velocity = initVel;
	}
}
