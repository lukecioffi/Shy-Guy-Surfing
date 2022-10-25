using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReznorRack : MonoBehaviour
{
    public Rotator[] spinners;
    public RivalReznor[] rhinos;
	public AudioSource audio;
	public Rigidbody2D rb;
	
	bool destroyed;
	
	// Start is called before the first frame update
    void Start()
    {
        spinners = GetComponentsInChildren<Rotator>();
        rhinos = GetComponentsInChildren<RivalReznor>();
		audio = GetComponent<AudioSource>();
		rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rhinos = GetComponentsInChildren<RivalReznor>();
		foreach(Rotator s in spinners)
		{
			s.zSpeed  = s.startingZSpeed * (1 + (2 - (rhinos.Length / 2)));
		}
		
		if(FindObjectOfType<RivalReznor>() == null)
		{
			if(!destroyed)
			{
				audio.Play();
				rb.bodyType = RigidbodyType2D.Dynamic;
				Animator a = transform.parent.GetComponent<Animator>();
				PlayerPrefs.SetInt("BossesDefeated", PlayerPrefs.GetInt("BossesDefeated") + 1); 
				a.SetBool("Destroyed", true);
				destroyed = true;
			}
		}
    }
}
