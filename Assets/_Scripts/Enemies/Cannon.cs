using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject cannonballPrefab;
	public float fireSpeed;
	public int firerate;
	
	protected EnemyRailMovement rail;
	public Animator anim;
	
	int timer;
	
	// Start is called before the first frame update
    void Start()
    {
        rail = FindObjectOfType<EnemyRailMovement>();
		timer = -2000;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
		if(timer >= firerate)
		{
			Fire();
			timer = 0;
		}
    }
	
	public virtual void Fire()
	{
		GameObject ball = Instantiate(cannonballPrefab, transform.position, transform.rotation);
		//ball.transform.SetParent(transform.parent);
		
		Rigidbody2D ball_rb = ball.GetComponent<Rigidbody2D>();
		
		if(rail != null)
			ball_rb.velocity = transform.up * fireSpeed + new Vector3(-rail.speed, 0, 0);
		else ball_rb.velocity = transform.up * fireSpeed;
		if(anim != null) anim.Play("Shoot", 0, 0f);
	}
	
	void OnBecameVisible()
	{
		timer = Random.Range(0, firerate);
	}
	
	void OnBecameInvisible()
	{
		timer = -2000;
	}
}
