using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAge : MonoBehaviour
{
    private int age;
	public int maxAge;
	
	public SpriteRenderer rend;
	
	public bool alwaysFlicker;
	
	// Start is called before the first frame update
    void Start()
    {
        age = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(age >= maxAge)
		{
			Destroy(gameObject);
		}
    }
	
	void FixedUpdate()
	{
		age++;
		
		if((maxAge >= 50 && maxAge - age <= 25) || alwaysFlicker)
		{
			if(age % 2 == 0) // Flicker
				rend.enabled = false;
			else 
				rend.enabled = true;
		}
	}
}
