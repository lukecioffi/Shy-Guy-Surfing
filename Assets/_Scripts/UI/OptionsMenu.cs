using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
	public DataCarrier file;
	public CameraBorder borderController;
	public Animator curtain_anim;
	public SequencedMusicPlayer smp;
	
	public int activeMenu;
	public int ctr;
	
	public TextMeshPro[] texts;
	public Animator arrow;
	
	public Vector2[] resolutions;
	public int resCtr;
	
	[Header("Audio")]
	[SerializeField] private AudioSource audio;
	[SerializeField] private AudioClip sfx_menumove;
	[SerializeField] private AudioClip sfx_wrong;
	
	private bool axisDownX;
	private bool axisDownY;
	
	float init_vol;
	
    // Start is called before the first frame update
    void Start()
    {
        activeMenu = 0;
		resCtr = Array.IndexOf(resolutions, file.resolution);
		if(resolutions[resCtr].x % 416 == 0) file.has_border = false;
		else file.has_border = true;
		init_vol = smp.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
			{
				if(!axisDownY)
				{
					audio.PlayOneShot(sfx_menumove, 1.0f);
					if(Input.GetAxisRaw("Vertical") > 0) activeMenu--;
					else activeMenu++;
					activeMenu %= texts.Length;
					if(activeMenu < 0) activeMenu = 5;
					
					axisDownY = true;
				}
			}
		else axisDownY = false;
		
		// POSITION ARROW
		if(activeMenu != 6) arrow.transform.parent.position = (Vector2)texts[activeMenu % texts.Length].transform.position + new Vector2(-9, 0.25f);
		else arrow.transform.parent.position = (Vector2)texts[activeMenu % texts.Length].transform.position + new Vector2(-3.75f, 0f);
		
		// FULLSCREEN ON OR OFF
		if(activeMenu == 0)
		{
			if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f)
			{
				if(!axisDownX)
				{
					audio.PlayOneShot(sfx_menumove, 1.0f);
					
					Screen.fullScreen = !Screen.fullScreen;
					
					axisDownX = true;
				}
			}
			else axisDownX = false;
			
		}
		if(Screen.fullScreen) texts[0].SetText("ON");
		if(!Screen.fullScreen) texts[0].SetText("OFF");
		
		// SCREEN RESOLUTION
		if(activeMenu == 1)
		{
			if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f)
			{
				if(!axisDownX)
				{
					audio.PlayOneShot(sfx_menumove, 1.0f);
					if(Input.GetAxisRaw("Horizontal") > 0) resCtr++;
					else resCtr--;
					
					resCtr %= resolutions.Length;
					if(resCtr < 0) resCtr = resolutions.Length - 1;
					
					if(resolutions[resCtr].x % 416 == 0) file.has_border = false;
					else file.has_border = true;
					
					file.resolution = resolutions[resCtr];
					Screen.SetResolution((int)resolutions[resCtr].x, (int)resolutions[resCtr].y, Screen.fullScreen);
					axisDownX = true;
				}
			}
			else axisDownX = false;
		}
		texts[1].SetText((int)resolutions[resCtr].x + "X" + (int)resolutions[resCtr].y);
		
		// BORDER
		if(activeMenu == 2)
		{
			if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f)
			{
				if(!axisDownX)
				{
					audio.PlayOneShot(sfx_menumove, 1.0f);
					
					if(Input.GetAxisRaw("Horizontal") > 0) file.border_id++;
					else file.border_id--;
					
					borderController.SetBorder();
					axisDownX = true;
				}
			}
			else axisDownX = false;
		}
		// SAY BORDER NAME
		texts[2].SetText(borderController.borders[file.border_id % borderController.borders.Length].name);
		
		// MASTER VOLUME
		if(activeMenu == 3)
		{
			if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f)
			{
				if(!axisDownX)
				{
					audio.PlayOneShot(sfx_menumove, 1.0f);
					
					if(Input.GetAxisRaw("Horizontal") > 0) AudioListener.volume += 0.05f;
					else AudioListener.volume -= 0.05f;
					
					AudioListener.volume = Mathf.Round(AudioListener.volume / 0.05f) * 0.05f;
					
					AudioListener.volume = Mathf.Clamp(AudioListener.volume, 0, 1);
					
					borderController.SetBorder();
					axisDownX = true;
				}
			}
			else axisDownX = false;
		}
		// SAY MASTER VOLUME
		texts[3].SetText(AudioListener.volume * 100 + "%");
		
		// MUSIC VOLUME
		if(activeMenu == 4)
		{
			if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f)
			{
				if(!axisDownX)
				{
					audio.PlayOneShot(sfx_menumove, 1.0f);
					
					if(Input.GetAxisRaw("Horizontal") > 0) SequencedMusicPlayer.staticVol += 0.05f;
					else SequencedMusicPlayer.staticVol -= 0.05f;
					
					SequencedMusicPlayer.staticVol = Mathf.Clamp(SequencedMusicPlayer.staticVol, 0, 1);
					
					SequencedMusicPlayer.staticVol = Mathf.Round(SequencedMusicPlayer.staticVol / 0.05f) * 0.05f;
					smp.volume = init_vol * SequencedMusicPlayer.staticVol;
					
					borderController.SetBorder();
					axisDownX = true;
				}
			}
			else axisDownX = false;
		}
		// SAY MUSIC VOLUME
		texts[4].SetText(SequencedMusicPlayer.staticVol * 100 + "%");
		
		// RANDOMIZE E ON OR OFF
		if(activeMenu == 5)
		{
			if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0.5f)
			{
				if(!axisDownX)
				{
					audio.PlayOneShot(sfx_menumove, 1.0f);
					
					file.randomize_E_Mode = !file.randomize_E_Mode;
					
					axisDownX = true;
				}
			}
			else axisDownX = false;
			
		}
		if(file.randomize_E_Mode) texts[5].SetText("ON");
		if(!file.randomize_E_Mode) texts[5].SetText("OFF");
		
		if(activeMenu == 6)
		{
			if(Input.GetButtonDown("Jump"))
			{
				StartCoroutine(BackToMenu());
				this.enabled = false;
			}
		}
		
    }
	
	IEnumerator BackToMenu()
	{
		file.master_volume = AudioListener.volume;
		file.music_volume = SequencedMusicPlayer.staticVol;
		file.Save();
		
		audio.PlayOneShot(audio.clip, 0.3f);
		curtain_anim.Play("FallFast", 0);
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene("MainMenu");
	}
}
