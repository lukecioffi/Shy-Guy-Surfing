using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeSpeed : MonoBehaviour
{
	ParticleSystem ps;
	EnemyRailMovement rail;
	
	public bool rainbow;
	float hue;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
		rail = FindObjectOfType<EnemyRailMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        var main = ps.main;
		if(transform.parent.parent.localScale.y == 1) main.startRotation = 0;
		if(transform.parent.parent.localScale.y == -1) main.startRotation = 3.14159f;
    }
	
	void FixedUpdate()
	{
		if(rail != null)
		{
			var main = ps.main;
			main.startSpeed = rail.speed;
			if(transform.parent.parent.localScale.y == -1)
			{
				main.startRotation = 180.0f * Mathf.Deg2Rad;
			}
			
			if(rainbow)
			{
				main.startColor = Color.HSVToRGB(hue, 0.5f, 1.0f);
				
				hue += 0.01f;
				
				if(hue >= 1.0f)
				{
					hue = 0;
				}
			}
		}
	}
}
