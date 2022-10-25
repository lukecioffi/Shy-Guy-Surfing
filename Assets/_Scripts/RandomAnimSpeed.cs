using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimSpeed : MonoBehaviour
{
	public float min;
	public float max;
	Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
		anim.speed = Random.Range(min, max);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
