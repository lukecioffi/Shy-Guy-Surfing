using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowman : MonoBehaviour
{
    Animator anim;
	AudioSource audio;
	public GameObject snowballPrefab;
	
	bool popped = false;
	
	// Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!popped)
		{
			if(transform.position.x <= 5)
			{
				anim.Play("Pop", 0, 0f);
				popped = true;
			}
		}
    }
	
	void Pop()
	{
		GameObject ball = Instantiate(snowballPrefab, transform.position, transform.rotation);
		ball.transform.SetParent(transform.parent);
		
	}
}
