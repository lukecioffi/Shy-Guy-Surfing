using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
	PlayerMovement player;
	
    // Start is called before the first frame update
    void Start()
    {
		player = FindObjectOfType<PlayerMovement>();
    }
	
	void Update()
	{
		if(player != null)
			transform.up = (player.transform.position - transform.position).normalized;
	}
}
