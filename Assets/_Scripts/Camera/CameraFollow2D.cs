using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField]
	GameObject player;
	
	[SerializeField]
	float speedOffset;
	
	[SerializeField]
	Vector2 posOffset;
	
	[SerializeField]
	float leftLimit;
	[SerializeField]
	float rightLimit;
	[SerializeField]
	float topLimit;
	[SerializeField]
	float bottomLimit;
	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
		
		transform.position = new Vector3
		(
			Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
			Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
			transform.position.z
		);
    }
	
	private void OnDrawGizmos()
	{
		//Draw a box around our camera boundary
		Gizmos.color = Color.red;
		Gizmos.DrawLine(new Vector2(leftLimit - 13, topLimit + 8), new Vector2(rightLimit + 13, topLimit + 8));
		Gizmos.DrawLine(new Vector2(rightLimit + 13, topLimit + 8), new Vector2(rightLimit + 13, bottomLimit - 6));
		Gizmos.DrawLine(new Vector2(leftLimit - 13, bottomLimit - 6), new Vector2(rightLimit + 13, bottomLimit - 6));
		Gizmos.DrawLine(new Vector2(leftLimit - 13, bottomLimit - 6), new Vector2(leftLimit - 13, topLimit + 8));
	}
}
