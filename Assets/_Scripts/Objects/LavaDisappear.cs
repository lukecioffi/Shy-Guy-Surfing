using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaDisappear : MonoBehaviour
{
	SceneChanger scene_c;
	Animator anim;
	
    // Start is called before the first frame update
    void Start()
    {
        scene_c = FindObjectOfType<SceneChanger>();
		anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetBool("Sinking") == false && (scene_c.scoreReader.d_score > scene_c.areaNum * scene_c.levelLength - 75))
		{
			anim.SetBool("Sinking", true);
		}
    }
}
