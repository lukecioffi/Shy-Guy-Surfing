using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
	float _t = 0f;
	
	public int seconds;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

 
	void Update()
	{
		_t += Time.deltaTime;
	
		if (_t >= 1f)
		{
			seconds++;
			_t = 0f;
		}
	}
	
	void FixedUpdate()
	{
		
	}
	
	
}
