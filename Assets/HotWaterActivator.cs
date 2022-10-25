using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotWaterActivator : MonoBehaviour
{
	public GameObject sign;
	
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Activate());
    }
	
	IEnumerator Activate()
	{
		transform.position = Vector2.zero;
		
		int count = 0;
		
		while(count <= 25)
		{
			sign.SetActive(true);
			yield return new WaitForSeconds(0.05f);
			sign.SetActive(false);
			yield return new WaitForSeconds(0.05f);
			count++;
		}
		
		foreach(HotWater h in FindObjectsOfType<HotWater>())
		{
			h.enabled = true;
		}
		
		sign.SetActive(false);
	}
}
