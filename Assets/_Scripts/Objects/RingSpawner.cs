using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingSpawner : MonoBehaviour
{
    EnemyRailMovement rail;
	
	public GameObject ringPrefab;
	
	public int timer, firerate, tenChance;
	
	// Start is called before the first frame update
    void Start()
    {
        rail = FindObjectOfType<EnemyRailMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void FixedUpdate()
	{
		if(timer >= firerate)
		{
			timer = 0;
			if(Random.Range(0, 11) < tenChance) return;
			GameObject new_ring = Instantiate(ringPrefab, 
				transform.position, 
				transform.rotation);
			new_ring.transform.SetParent(rail.transform);
		}
		
		timer++;
	}
}
