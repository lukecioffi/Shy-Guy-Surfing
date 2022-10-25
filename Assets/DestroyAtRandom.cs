using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAtRandom : MonoBehaviour
{
	public int chance;
    // Start is called before the first frame update
    void Awake()
    {
        if(Random.Range(0, 100) <= chance)
		{
			Destroy(gameObject);
		}
    }
}
