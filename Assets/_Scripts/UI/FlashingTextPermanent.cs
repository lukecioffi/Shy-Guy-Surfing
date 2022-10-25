using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingTextPermanent : MonoBehaviour
{
	[SerializeField]
    public float age;
	[SerializeField]
    public int delay;
	[SerializeField]
	public MeshRenderer rend;
	
	// Start is called before the first frame update
    void Start()
    {
        age = 0;
    }
	
	void FixedUpdate()
	{
		age++;
		
		if(age % delay == 0) // Blink
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
	}
}
