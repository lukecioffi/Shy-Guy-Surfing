using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFellaSingleton : MonoBehaviour
{
    public static FallingFellaSingleton instance;
	
	// Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
        }
 
        instance = this;
    }
}
