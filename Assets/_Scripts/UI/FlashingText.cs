using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingText : MonoBehaviour
{
	[SerializeField]
    public float age;
	[SerializeField]
	public float maxAge;
	[SerializeField]
	public MeshRenderer rend;
	[SerializeField]
	private int delay;
	
	// Start is called before the first frame update
    void Start()
    {
        age = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(age > 150)
		{
			if(Input.GetButtonDown("Jump") || Input.GetButtonDown("Submit"))
			{
				Destroy(gameObject);
			}
		}
    }
	
	void FixedUpdate()
	{
		age++;
		
		if(age > 150 && age % delay == 0) // Blink
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
		
		
		if(age >= maxAge && maxAge > 0)
		{
			Destroy(gameObject);
		}
	}
}
