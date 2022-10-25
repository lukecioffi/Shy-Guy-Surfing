using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalReznor : MonoBehaviour
{
	
	[SerializeField] GameObject fireballPrefab;
	[SerializeField] GameObject burnerPrefab;
	[SerializeField] Transform firePoint;
	[SerializeField] Animator anim;
	
	[SerializeField] PlayerMovement target;
	[SerializeField] EnemySurfer surf_controller;
	
	bool isSurfing;
	
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
		
        transform.rotation = Quaternion.Euler(0, 0, 0);
		if(isSurfing)
		{
			if(transform.position.x <= -14)
			{
				Vector3 warp_pos;
				warp_pos = new Vector3(13, -3.75f, 0);
				transform.parent.position = warp_pos;
			}
			if(transform.position.x >= 14)
			{
				Vector3 warp_pos;
				warp_pos = new Vector3(-13, -3.75f, 0);
				transform.parent.position = warp_pos;
			}
		}
    }
	
	void FixedUpdate()
	{
		if(isSurfing && target != null)
		{
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("RZ_Idle"))
			{
				if(target.transform.position.x - transform.position.x < 0)
				{
					transform.localScale = new Vector3(1, 1, 1);
				}
				else
				{
					transform.localScale = new Vector3(-1, 1, 1);
				}
			}
			surf_controller.rb.AddForce(new Vector2(-transform.localScale.x * 20f, 0));
		}
	}
	
	void FireOrNot()
	{
		float i = Random.Range(0.0f, 4.0f);
		if(!isSurfing && i < 0.75f)
		{
			anim.Play("RZ_Fire");
		}
		else if(isSurfing && i < 2f)
		{
			anim.Play("RZ_Fire2");
		}
	}
	
	void Fire()
	{
		
		if(!isSurfing)
		{
			var fb = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
			Rigidbody2D fb_r = fb.GetComponent<Rigidbody2D>();
			if(target != null) fb_r.velocity = new Vector2(-7 * transform.localScale.x, (target.transform.position.y - transform.position.y) / 3);
			else fb_r.velocity = new Vector2(-7 * transform.localScale.x, 0);
		}
		else
		{
			Quaternion angle = Quaternion.Euler(0, 0, 90 * transform.localScale.x);
			var fb = Instantiate(burnerPrefab, firePoint.position + (transform.right * -transform.localScale.x * 1.25f), angle);
			fb.transform.SetParent(transform);
		}
	}
	
	void Bump()
	{
		var plat = transform.parent;
		var plat_rb = plat.GetComponent<Rigidbody2D>();
		var plat_rt = plat.GetComponent<Rotator>();
		plat_rb.bodyType = RigidbodyType2D.Dynamic;
		plat_rt.enabled = false;
		plat.SetParent(null);
		plat_rb.AddForce(transform.up * 7000);
		isSurfing = true;
		surf_controller.audio.Play();
	}
}
