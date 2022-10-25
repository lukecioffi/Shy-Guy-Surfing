using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnd : MonoBehaviour
{
    public ParticleSystem[] ps;
	
	public static Transform rail;
	
	// Start is called before the first frame update
    void Start()
    {
		transform.SetParent(rail);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(ParticleSystem p in ps)
		{
			if(p.isPlaying == false)
			{
				Destroy(gameObject);
			}
		}
    }
}
