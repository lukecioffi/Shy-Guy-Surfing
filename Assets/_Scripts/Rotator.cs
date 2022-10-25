using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	public bool randomized = false;
	
	public float xSpeed;
	public float ySpeed;
	public float zSpeed;
	
	public float startingZSpeed = 1;
	
	// Start is called before the first frame update
    void Start()
    {
		if(randomized) transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
		
        startingZSpeed = zSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(xSpeed, ySpeed, zSpeed);
    }
}
