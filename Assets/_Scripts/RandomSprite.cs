using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour
{
    public RuntimeAnimatorController[] skins;
	Animator anim;
	
	// Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
		anim.runtimeAnimatorController = skins[Random.Range(0, skins.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
