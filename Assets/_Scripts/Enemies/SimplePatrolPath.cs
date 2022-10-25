using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePatrolPath : MonoBehaviour
{
    public Transform[] patrolPoints;
	public int startPtr;
	public int endPtr = -1;
	public int ptr;
	
	public float moveSpeed;
	
	public bool smooth;
	public bool backAndForth;
	private bool goBack;
	
	private float distToNext;
	private float distFromLast;
	private float speedMod;
	
	public bool randomized = true;
	
	public bool flipOnBack = false;
	public SpriteRenderer body;
	
	
	// Start is called before the first frame update
    void Start()
    {
		if(randomized) ptr = Random.Range(0, patrolPoints.Length);
		transform.position = patrolPoints[ptr].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(flipOnBack && body != null)
		{
			body.flipX = goBack || (ptr == 0);
		}
    }
	
	void FixedUpdate()
	{
		// Make enemy move towards next point
		distToNext = Vector2.Distance(transform.position, patrolPoints[ptr].position);
		if(ptr == 0)
			distFromLast = Vector2.Distance(transform.position, patrolPoints[patrolPoints.Length - 1].position);
		else distFromLast = Vector2.Distance(transform.position, patrolPoints[ptr - 1].position);
		
		if(smooth == true) // Move smoothly
		{
			if(distToNext > distFromLast) // Speed up
			{
				speedMod = distFromLast;
			}
			else // Slow down
			{
				speedMod = distToNext;
			}
			if(speedMod > 1) // Set Maximum speed at dist = 1
			{
				speedMod = 1;
			}
			transform.position = Vector2.MoveTowards(transform.position, patrolPoints[ptr].position, moveSpeed * speedMod + 0.01f);
		}
		else // Move rigidly
		{
			transform.position = Vector2.MoveTowards(transform.position, patrolPoints[ptr].position, moveSpeed);
		}
		
		
		// Changing targeted point
		
		if(transform.position == patrolPoints[ptr].position)
		{
			if(ptr != endPtr)
			{
				if(goBack) ptr--; // Count ptr down if goBack is true.
				else ptr++; // Count up otherwise.
			}
		}
		if(ptr >= patrolPoints.Length) // Check if targeted point is the last.
		{
			if(backAndForth)
			{
				goBack = true; // Start going back if backAndForth is true.
				ptr = patrolPoints.Length - 1;
			}
			else ptr = 0; // Reset if backAndForth is false.
			
		}
		
		if(ptr == 0) // Reset goBack at point 0.
		{
			goBack = false;
		}
	}
	
	private void OnDrawGizmos()
	{
		//Draw lines between each point
		Gizmos.color = Color.yellow;
		for(int i = 0; i < patrolPoints.Length - 1; i++)
		{
			Gizmos.DrawLine(patrolPoints[i].position, patrolPoints[i + 1].position);
		}
	}
}
