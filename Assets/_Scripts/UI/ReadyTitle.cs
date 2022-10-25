using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyTitle : MonoBehaviour
{
    public float age;
	public float maxAge;
	public SpriteRenderer rend;
	
	// Start is called before the first frame update
    void Start()
    {
        age = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void FixedUpdate()
	{
		age++;
		
		if(age % 5 == 0) // Blink
		{
			if(rend.enabled == true)
			{
				rend.enabled = false;
			}
			else
			{
				rend.enabled = true;
			}
		}
		
		
		if(age >= maxAge)
		{
			Destroy(gameObject);
		}
	}
}
