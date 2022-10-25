using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashSFX : MonoBehaviour
{
	[SerializeField]
	private GameObject splashPref;
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.layer == LayerMask.NameToLayer("Water"))
		{
			//Instantiate(splashPref, transform.position, Quaternion.Euler(0, 0, 0));
			Instantiate(splashPref, transform);
		}
	}
	
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.layer == LayerMask.NameToLayer("Water"))
		{
			Instantiate(splashPref, transform.position, Quaternion.Euler(0, 0, 0));
		}
	}
}
