using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalMetaKnight : MonoBehaviour
{
    EnemySurfer surf_controller;
	
	[SerializeField] Animator anim;
	[SerializeField] Transform firePoint;
	
	public GameObject swordBeamPrefab;
	
	public PlayerMovement target;
	
	public int timer = 0;
	public int timer2 = 0;
	public int slashRate = 400;
	public int burstRate = 1200;
	
	public float surfSpeed = 1.5f;
	
	// Start is called before the first frame update
    void Start()
    {
        surf_controller = GetComponentInChildren<EnemySurfer>();
		target = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
		if(target != null && anim.GetCurrentAnimatorStateInfo(0).IsName("MK_Idle")) // Checks if there is a target and MK is not using triple slash
		{
			surf_controller.rb.drag = 0;
			// Turn Meta Knight to face player
			if(target.transform.position.x - transform.position.x < 0)
			{
				transform.localScale = new Vector3(1, 1, 1);
			}
			else
			{
				transform.localScale = new Vector3(-1, 1, 1);
			}
			
			// Make MK use Triple Slash if he is on the edge of the screen or if timer is met
			if(transform.position.x < -9.5 || transform.position.x > 9.5)
			{
				anim.Play("MK_TripleSlash");
			}
			
			if(timer >= slashRate)
			{
				anim.Play("MK_TripleSlash");
				timer = 0;
			}
			
			if(timer2 >= burstRate)
			{
				anim.Play("MK_Burst");
				surf_controller.rb.velocity = new Vector2(0, 0);
				timer2 = 0;
				timer = -slashRate;
			}
			
			
		}
		
		if(target != null && anim.GetCurrentAnimatorStateInfo(0).IsName("MK_Burst"))
		{
			surf_controller.rb.mass = 1000;
		}
		else surf_controller.rb.mass = 1;
    }
	
	void FixedUpdate()
	{
		if(target != null)
		{
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("MK_Idle") || anim.GetCurrentAnimatorStateInfo(0).IsName("MK_Guard"))
				surf_controller.rb.AddForce(new Vector2(-transform.localScale.x * surfSpeed, 0));
		}
		
		timer++;
		timer2++;
	}
	
	void FireSwordBeam()
	{
		GameObject beam = Instantiate(swordBeamPrefab, firePoint.position, firePoint.rotation);
		Rigidbody2D beam_rb = beam.GetComponent<Rigidbody2D>();
		beam_rb.velocity = new Vector2(-14 * transform.localScale.x, 0);
		surf_controller.rb.velocity = new Vector2(0, 0);
	}
	
	void FireDoubleSwordBeam()
	{
		GameObject beam = Instantiate(swordBeamPrefab, transform.position, firePoint.rotation);
		Rigidbody2D beam_rb = beam.GetComponent<Rigidbody2D>();
		beam_rb.velocity = new Vector2(-14 * transform.localScale.x, 0);
		beam = Instantiate(swordBeamPrefab, firePoint.position, firePoint.rotation);
		beam_rb = beam.GetComponent<Rigidbody2D>();
		beam_rb.velocity = new Vector2(14 * transform.localScale.x, 0);
		surf_controller.rb.velocity = new Vector2(0, 0);
	}
	
	void FireRandomSwordBeam()
	{
		GameObject beam = Instantiate(swordBeamPrefab, transform.position + transform.up * 5.5f, firePoint.rotation);
		Rigidbody2D beam_rb = beam.GetComponent<Rigidbody2D>();
		beam_rb.velocity = new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(-8.0f, 8.0f));
	}
	
	void Thrust()
	{
		surf_controller.rb.velocity = new Vector2(-transform.localScale.x * 14, 0);
		surf_controller.rb.drag = 4;
	}
	
	void GuardOrNot()
	{
		int r = Random.Range(0, 3);
		
		if(r == 0)
		{
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("MK_Idle"))
			{
				//anim.Play("MK_Guard");
			}
		}
	}
}
