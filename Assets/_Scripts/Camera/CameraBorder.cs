using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class CameraBorder : MonoBehaviour
{
	public DataCarrier file;
	
	public PixelPerfectCamera ppc;
	public SpriteRenderer rend;
	
	public Sprite[] borders;
	
    // Start is called before the first frame update
    void Start()
    {
        SetBorder();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		SetBorder();
    }
	
	public void SetBorder()
	{
		if(!file.has_border)
		{
			ppc.refResolutionX = 416;
			ppc.refResolutionY = 256;
			return;
		}
		ppc.refResolutionX = 480;
		ppc.refResolutionY = 270;
		file.border_id %= borders.Length;
		if(file.border_id < 0) file.border_id = borders.Length - 1;
		rend.sprite = borders[file.border_id % borders.Length];
	}
}
