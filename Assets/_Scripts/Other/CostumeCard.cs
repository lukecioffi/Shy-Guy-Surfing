using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeCard : MonoBehaviour
{
	public DataCarrier file;
	
	public Sprite[] card_icons;
	
	int ptr;
	
	bool got_all;
	
	[SerializeField] Animator anim;
	[SerializeField] Collider2D col;
	[SerializeField] Rotator rot;
	[SerializeField] AudioSource audio;
	[SerializeField] SpriteRenderer rend;
	
	[SerializeField] NotificationBox notif;
	
	void Awake()
	{	
		if(Random.Range(0.0f, 1.0f) > 0.1f)
		{
			Destroy(gameObject);
		}
		
		got_all = true;
		
		for(int i = 1; i < card_icons.Length; i++)
		{	
			if(!file.costumeLocks[i]) got_all = false;
		}
		
		if(got_all) Destroy(gameObject);
	}
	
	// Start is called before the first frame update
    void Start()
    {	
		ptr = Random.Range(1, card_icons.Length);
		
		// Randomize again if that costume is already collected
		while(file.costumeLocks[ptr] && !got_all)
		{
			ptr = Random.Range(1, card_icons.Length);
		}
		
		rend.sprite = card_icons[ptr];
		
		notif = FindObjectOfType<NotificationBox>(); // Find NotificationBox
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player")
		{
			Collect();
			col.enabled = false;
		}
	}
	
	void Collect()
	{
		anim.Play("Fly", 0, 0f);
		audio.Play();
		rot.ySpeed *= 8;
		
		file.costumeLocks[ptr] = true;
		file.Save();
		
		if(notif != null) notif.SendNotif("NEW COSTUME COLLECTED!");
	}
}
