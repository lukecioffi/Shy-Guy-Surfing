using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RecordsMenu : MonoBehaviour
{
	int ptr;
	int ptr2;
	
	public DataCarrier file;
	
	public TextMeshPro[] records;
	public TextMeshPro[] buttons;
	public TextMeshPro[] buttons2;
	public Animator[] buttonAnims;
	
	Color _yellow;
	Color _white;
	
	AudioSource audio;
	[SerializeField] AudioClip selectSFX;
	[SerializeField] AudioClip breakSFX;
	Animator anim;
	
	bool axisDown;
	
    // Start is called before the first frame update
    void Start()
    {
        LoadData();
		
		audio = GetComponent<AudioSource>();
		anim = GetComponent<Animator>();
		_yellow = new Color(1, 1, 0, 1);
		_white = new Color(1, 1, 1, 1);
    }

	void Update()
	{
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Stay"))
		{
			if(Input.GetAxisRaw("Horizontal") != 0)
			{
				if(!axisDown)
				{
					audio.Play();
					ptr += (int)Input.GetAxisRaw("Horizontal");
					if(ptr < 0) ptr = 0;
					if(ptr > 2) ptr = 2;
					buttonAnims[ptr].Play("Bounce", 0, 0f);
					axisDown = true;
				}
			}
			else axisDown = false;
			
			if(Input.GetButtonDown("Submit"))
			{
				if(ptr == 0)
				{
					StartCoroutine(ReturnToMenu());
				}
				
				if(ptr == 1)
				{
					StartCoroutine(ReturnToMenu());
				}
				
				if(ptr == 2)
				{
					anim.Play("Results_FlipIn");
					ptr = 0;
				}
				
				audio.PlayOneShot(selectSFX);
			}
		}
		
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Results_Erase"))
		{
			if(Input.GetAxisRaw("Horizontal") != 0)
			{
				if(!axisDown)
				{
					audio.Play();
					ptr2 += (int)Input.GetAxisRaw("Horizontal");
					if(ptr2 < 0) ptr2 = 0;
					if(ptr2 > 1) ptr2 = 1;
				}
				axisDown = true;
			}
			else axisDown = false;
			
			if(Input.GetButtonDown("Submit"))
			{
				if(ptr2 == 1)
				{
					audio.PlayOneShot(breakSFX);
					file.Erase();
					LoadData();
				}
				else audio.PlayOneShot(selectSFX);
				anim.Play("Results_FlipOut");
			}
		}
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i = 0; i < buttons.Length; i++)
		{
			if (i == ptr)
			{
				buttons[i].color = _yellow;
			}
			else buttons[i].color = _white;
		}
		
		for(int i = 0; i < buttons2.Length; i++)
		{
			if (i == ptr2)
			{
				buttons2[i].color = _yellow;
			}
			else buttons2[i].color = _white;
		}
    }
	
	void LoadData()
	{
		records[0].SetText(file.highScore + "m"); // Longest Surf
		records[1].SetText(file.totalDistance + "m"); // Total Distance
		records[2].SetText(file.totalRuns.ToString()); // Total Rides
		if(file.totalRuns > 0) records[3].SetText(file.totalDistance / file.totalRuns + "m"); // Average Distance
		else records[3].SetText("0m");
		records[4].SetText(file.bossesDefeated.ToString()); // Bosses Defeated
		
		int total_costumes = 1;
		for(int i = 1; i < 13; i++)
		{
			if(file.costumeLocks[i]) total_costumes++;
		}
		
		records[5].SetText(total_costumes + "/" + "20"); // Costumes Collected
		
		
		// Print total play time
		records[6].SetText(
			TimeSpan.FromSeconds(file.totalPlayTime).Hours.ToString("00") + ":" + 
			TimeSpan.FromSeconds(file.totalPlayTime).Minutes.ToString("00") + ":" + 
			TimeSpan.FromSeconds(file.totalPlayTime).Seconds.ToString("00")
		);
		
	}
	
	IEnumerator ReturnToMenu()
	{
		anim.Play("Close");
		file.Save();
		yield return new WaitForSeconds(2.0f);
		LoadScene("MainMenu");
	}
	
	public void LoadScene(string sceneName)
    {
		SceneManager.LoadScene(sceneName);
    }
}
