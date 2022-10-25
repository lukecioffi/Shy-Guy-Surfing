using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    private static HighScore instance = null;

    public static HighScore Instance {
             get { return instance; }
    }
	
	public int high_score;
	
	// Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
            return;
        } 
        instance = this;
		DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
		{
			if(PlayerPrefs.GetInt("HighScore") <= high_score)
			{
				PlayerPrefs.SetInt("HighScore", high_score);
			}
		}
    }
}
