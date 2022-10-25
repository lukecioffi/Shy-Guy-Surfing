using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobratSpit : MonoBehaviour
{
    [SerializeField] SimplePatrolPath spp;
	[SerializeField] Animator anim;
	[SerializeField] Transform firePoint;
	[SerializeField] GameObject ballPrefab;
	
	[SerializeField] float fireSpeed;
	
	EnemyRailMovement rail;
	int cooldown = 0;
	
	// Start is called before the first frame update
    void Start()
    {
        rail = FindObjectOfType<EnemyRailMovement>();
		cooldown = 1000;
    }

    // Update is called once per frame
    void Update()
    {
		if(spp.ptr == 1 && cooldown == 0)
		{
			anim.Play("Spit", 0, 0f);
			Shoot();
			cooldown = 50;
		}
    }
	
	void FixedUpdate()
	{
		
		if(cooldown > 0) cooldown--;
	}
	
	void OnBecameVisible()
	{
		cooldown = 0;
	}
	
	void Shoot()
	{
		GameObject ball = Instantiate(ballPrefab, firePoint.position, firePoint.rotation);
		//ball.transform.SetParent(transform.parent);
		
		Rigidbody2D ball_rb = ball.GetComponent<Rigidbody2D>();
		
		if(rail != null)
			ball_rb.velocity = -transform.right * fireSpeed + new Vector3(-rail.speed, 0, 0);
		else ball_rb.velocity = -transform.right * fireSpeed;
	}
	
	void TakeDamage()
	{
		return;
	}
}
