using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float startpos;
	public float maxDistance;
	public float parallax;
	
	public float offset;
	public int loops;
	
	public EnemyRailMovement rail;
	
	// Start is called before the first frame update
    void Start()
    {
		if(rail == null)
			rail = FindObjectOfType<EnemyRailMovement>();
        startpos = transform.position.x;
		loops = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = (rail.transform.position.x * parallax);
		
		transform.position = new Vector3(startpos + dist + ((maxDistance + offset) * loops), transform.position.y, transform.position.z);
		
		if(transform.position.x <= startpos - maxDistance)
			loops = -(int)dist / (int)maxDistance;
		
    }
}
