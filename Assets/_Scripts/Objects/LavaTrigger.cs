using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerStay2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player")
		{
			if(collider.gameObject.GetComponentInChildren<FireShield>() == null)
			{
				var player = collider.gameObject.GetComponent<PlayerMovement>();
				Destroy(player.activeShield);
				player.activeShield = null;
				player.Die();
			}
		}
	}
}
