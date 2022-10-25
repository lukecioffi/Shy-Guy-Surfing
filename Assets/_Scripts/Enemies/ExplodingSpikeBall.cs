using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingSpikeBall : MonoBehaviour
{
	public GameObject explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player")
		{
			Instantiate(explosionPrefab, transform.position, transform.rotation);
			Destroy(transform.parent.gameObject);
		}
	}
	
	void Explode()
	{
		Instantiate(explosionPrefab, transform.position, transform.rotation);
		Destroy(transform.parent.gameObject);
	}
}
