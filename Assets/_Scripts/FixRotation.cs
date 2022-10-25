using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour
{
    Quaternion rotation;
	public bool fixToZero;
	
	void Awake()
	{
		rotation = transform.rotation;
		if(fixToZero) rotation = Quaternion.identity;
	}
	
	void LateUpdate()
	{
		transform.rotation = rotation;
	}
}
