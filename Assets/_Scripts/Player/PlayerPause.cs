using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPause : MonoBehaviour
{
    private bool paused = false;
	
	[SerializeField]
	private PlayerMovement player;
	
	[SerializeField]
	private ReadyTitle readySign;
	[SerializeField]
	private MeshRenderer pauseSign;
	private SequencedMusicPlayer smp;
	
	private float default_vol;
	
	[SerializeField]
	private AudioSource audio;
	
	public DataCarrier file;
	
	// Start is called before the first frame update
    void Start()
    {
		player = FindObjectOfType<PlayerMovement>();
		readySign = FindObjectOfType<ReadyTitle>();
        smp = FindObjectOfType<SequencedMusicPlayer>();
		default_vol = smp.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause") && readySign == null && player != null)
		{
			Pause();
		}
		
		if(paused)
		{
			if(Input.GetButton("Jump") && Input.GetButton("Fire"))
			{
				smp.volume = default_vol;
				Time.timeScale = 1;
				file.Save();
				SceneManager.LoadScene("MainMenu");
			}
		}
    }
	
	void Pause()
	{
		audio.Play();
		if(!paused)
		{
			pauseSign.enabled = true;
			paused = true;
			smp.volume = default_vol * 0.0001f;
			Time.timeScale = 0;
			return;
		}
		
		if(paused)
		{
			pauseSign.enabled = false;
			paused = false;
			smp.volume = default_vol;
			Time.timeScale = 1;
			return;
		}
	}
	
}
