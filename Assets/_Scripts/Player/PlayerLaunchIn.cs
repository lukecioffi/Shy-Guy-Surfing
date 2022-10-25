using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunchIn : MonoBehaviour
{
    [SerializeField]
	private CharacterController2D controller;
    [SerializeField]
	private BoxCollider2D cameraCol;
    [SerializeField]
	private BoxCollider2D killBound;
    [SerializeField]
	private Rigidbody2D rb;
    [SerializeField]
	private Vector2 launchVelocity;
    [SerializeField]
	private bool wholePlayer;
	
	
	
	
	// Start is called before the first frame update
    void Start()
    {
		rb.velocity = launchVelocity;
		
		if(wholePlayer)
		{
			cameraCol.enabled = false;
			killBound.enabled = false;
		}
    }

    // Update is called once per frame
    void Update()
    {
		if(wholePlayer)
		{
			if(cameraCol.enabled == false)
			{
				if(controller.m_Grounded)
				{
					cameraCol.enabled = true;
					killBound.enabled = true;
				}
			}
		}
    }
}
