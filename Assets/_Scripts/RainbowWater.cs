using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RainbowWater : MonoBehaviour
{
	Tilemap _t;
	
	float hue;
	public float saturation;
    // Start is called before the first frame update
    void Start()
    {
        _t = GetComponent<Tilemap>();
    }

	void FixedUpdate()
	{
		_t.color = Color.HSVToRGB(hue, saturation, 1.0f);
		
		hue += 0.01f;
			
		if(hue >= 1.0f)
		{
			hue = 0;
		}
	}
}
