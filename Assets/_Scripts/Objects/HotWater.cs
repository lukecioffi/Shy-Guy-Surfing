using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotWater : MonoBehaviour
{
	PlayerMovement player;
	
	public float heatTimer = 0;
	
	public SpriteRenderer bar;
	public SpriteRenderer holder;
	public ParticleSystem ps;
	
	void OnEnable()
	{
		holder.enabled = true;
		bar.enabled = true;
		player = GetComponent<PlayerMovement>();
		heatTimer = 0;
	}
	
	void OnDisable()
	{
		holder.enabled = false;
		bar.enabled = false;
		heatTimer = 0;
	}
	
    void FixedUpdate()
	{
		if(player.controller.m_Grounded && heatTimer < 150)
		{
			if(!ps.isPlaying) ps.Play();
			heatTimer += 2;
		}
		else if(heatTimer > 0)
		{
			if(ps.isPlaying) ps.Stop();
			heatTimer -= 2;
		}
		else heatTimer = 0;
		
		//heatTimer = Mathf.Round(heatTimer / 0.02f) * 0.02f;
		
		if((heatTimer > 90f) && (heatTimer % 2 == 0))
		{
			bar.enabled = !bar.enabled;
			holder.enabled = !holder.enabled;
		}
		else
		{
			bar.enabled = true;
			holder.enabled = true;
		}
		
		bar.size = new Vector2(1, 0.375f + (heatTimer / 150));
		holder.color = new Color(1, 1 - (heatTimer / 150), 1 - (heatTimer / 150), 1);
		
		
		if(heatTimer >= 150)
		{
			if(GetComponentInChildren<FireShield>() == null) player.Die();
		}
	}
}
