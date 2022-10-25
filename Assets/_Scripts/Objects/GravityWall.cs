using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWall : MonoBehaviour
{
	public int coef;
    void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.transform.tag == "Player")
		{
			collider.GetComponent<PlayerMovement>().default_grav = 4 * coef;
			collider.transform.localScale = new Vector2(1, coef);
		}
	}
}
