using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LakituBehavior : MonoBehaviour
{
    private PlayerMovement player;
	[SerializeField]
    private GameObject eggPrefab;
	
	private int age;
	public int firerate;
	
	// Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(age >= firerate)
		{
			ThrowEgg();
			age = 0;
		}
    }
	
	void FixedUpdate()
	{
		if(player != null)
		{
			transform.position = new Vector2
			(
				Mathf.Lerp(transform.position.x, player.transform.position.x, Time.deltaTime),
				transform.position.y
			);
		}
		
		age++;
	}
	
	void ThrowEgg()
	{
		GameObject egg = Instantiate(eggPrefab, transform.position, transform.rotation);
		Rigidbody2D eggRB = egg.GetComponent<Rigidbody2D>();
		eggRB.AddForce(new Vector2(Random.Range(-200.0f, 200.0f), 400));
	}
}
