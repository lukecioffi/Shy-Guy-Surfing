using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuHandler : MonoBehaviour
{
	public DataCarrier file;
	[SerializeField] private Animator s_g_anim;
	[SerializeField] private Animator title_anim;
	[SerializeField] private Animator curtain_anim;
	[SerializeField] private FlashingText pressSpace;
	[SerializeField] private AudioSource audio;
	
	[SerializeField] private string nextScene;
	[SerializeField] private TextMeshPro h_score_text;
	[SerializeField] private TextMeshPro name_text;
	
	[SerializeField] private CostumeController costumeController;
	
	[SerializeField]
	private int ptr, menuPtr, activeMenu;
	
	[SerializeField] private Transform selectBox;
	[SerializeField] private TextMeshPro button_TMP;
	[SerializeField] private string[] buttonTexts;
	[SerializeField] private Animator buttonAnim;
	[SerializeField] private Animator arrowAnim1;
	[SerializeField] private Animator arrowAnim2;
	[SerializeField] private Animator arrowAnim3;
	[SerializeField] private AudioClip sfx_menumove;
	[SerializeField] private AudioClip sfx_wrong;
	
	[SerializeField] private bool demo;
	
	private bool locked_in;
	
	private bool axisDownX;
	private bool axisDownY;
	
	// Start is called before the first frame update
    void Awake()
    {
		file.Load();
		
		h_score_text.SetText(file.highScore + "m");
		name_text.SetText(file.playerName);
		
		if(file.playerName == "")
		{
			LoadScene("EnterName");
		}
		
		AudioListener.volume = file.master_volume;
		SequencedMusicPlayer.staticVol = file.music_volume;
    }

    // Update is called once per frame
    void Update()
    {
        // Change file.costumeID
		
		if(pressSpace == null && !locked_in) // Detect if real menu is open
		{
			if(selectBox != null) selectBox.transform.localPosition = new Vector3(0.25f, -1.75f, 0);
			audio.volume = 1f;
			
			if(Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0.5f)
			{
				if(!axisDownY)
				{
					if(activeMenu == 1 && costumeController.locked)
					{
						audio.PlayOneShot(sfx_wrong);
					}
					else
					{
						audio.PlayOneShot(sfx_menumove, 1.0f);
						activeMenu++;
						if(activeMenu > 1) activeMenu = 0;
						if(activeMenu < 0) activeMenu = 1;
					}
					axisDownY = true;
				}
			}
			else axisDownY = false;
			
			// Change menu element
			
			if(activeMenu == 0)
			{
				if(Input.GetAxisRaw("Horizontal") != 0)
				{
					if(!axisDownX)
					{
						audio.PlayOneShot(sfx_menumove, 1.0f);
						menuPtr += (int)Input.GetAxisRaw("Horizontal");
						
						if(menuPtr < 0) menuPtr = buttonTexts.Length - 1;
						if(menuPtr > buttonTexts.Length - 1) menuPtr = 0;
						
						buttonAnim.Play("Bounce", 0, 0f);
						
						if((int)Input.GetAxisRaw("Horizontal") == -1) arrowAnim1.Play("Hit", 0, 0f);
						else if((int)Input.GetAxisRaw("Horizontal") == 1) arrowAnim2.Play("Hit", 0, 0f);
						
						axisDownX = true;
					}
				}
				else axisDownX = false;
				
				if(Input.GetButtonDown("Submit"))
				{
					if(menuPtr == 0) // Journey Mode // Play Demo
					{
						if(demo)
						{
							nextScene = "GameScene";
							file.mode = GameMode.ENDURANCE;
							StartCoroutine(StartGame());
							return;
						}
						else
						{
							if(file.journeyHigh == 0) nextScene = "GameScene";
							else nextScene = "PreJourney";
							
							file.mode = GameMode.JOURNEY;
							StartCoroutine(StartGame());
							return;
						}
						
					}
					
					else if(menuPtr == 1) // Endurance Mode // Options Demo
					{
						if(demo)
						{
							StartCoroutine(StartOptions());
						}
						else
						{
							nextScene = "GameScene";
							file.mode = GameMode.ENDURANCE;
							StartCoroutine(StartGame());
						}
					}
					
					else if(menuPtr == 2) // How to Play // Quit Demo
					{
						if(demo)
						{
							StartCoroutine(Quit());
						}
						else
						{
							nextScene = "HowToPlay";
							StartCoroutine(StartGame());
						}
					}
					
					else if(menuPtr == 3)
					{
						StartCoroutine(StartOptions());
					}
					
					else if(menuPtr == 4)
					{
						StartCoroutine(StartRecords());
					}
					
					else if(menuPtr == 5)
					{
						StartCoroutine(StartLeaderboard());
					}
					
					else if(menuPtr == 6)
					{
						StartCoroutine(Quit());
					}
					locked_in = true;
				}
				
				if(arrowAnim1 != null) arrowAnim1.transform.parent.localPosition = new Vector2(-4.75f, 0f);
				if(arrowAnim2 != null) arrowAnim2.transform.parent.localPosition = new Vector2(4.75f, 0f);
				if(arrowAnim3 != null) arrowAnim3.transform.parent.rotation = Quaternion.Euler(0, 0, -90);
			}
			
			// Change costume
			
			if(activeMenu == 1)
			{
				if(Input.GetAxisRaw("Horizontal") != 0)
				{
					if(!axisDownX)
					{
						file.costumeID += (int)Input.GetAxisRaw("Horizontal");
						
						if(file.costumeID < 0) file.costumeID = 0;
						if(file.costumeID >= costumeController.costumes.Length) file.costumeID = costumeController.costumes.Length - 1;
						
						
						
						costumeController.SetCostumeID(file.costumeID);
						audio.PlayOneShot(sfx_menumove, 1.0f);
						
						if((int)Input.GetAxisRaw("Horizontal") == -1) arrowAnim1.Play("Hit", 0, 0f);
						else if((int)Input.GetAxisRaw("Horizontal") == 1) arrowAnim2.Play("Hit", 0, 0f);
						
						axisDownX = true;
					}
				}
				else axisDownX = false;
				
				arrowAnim1.transform.parent.position = new Vector2(-1.5f, -3.75f);
				arrowAnim2.transform.parent.position = new Vector2(1.5f, -3.75f);
				arrowAnim3.transform.parent.rotation = Quaternion.Euler(0, 0, 90);
			}
			
			
		}
		else if(Input.GetButtonDown("Submit") && pressSpace.age > 150 && !locked_in)
		{
			audio.PlayOneShot(audio.clip, 0.3f);
		}
		
		button_TMP.SetText(buttonTexts[menuPtr]);
    }
	
	IEnumerator StartGame()
	{
		audio.PlayOneShot(audio.clip, 0.3f);
		s_g_anim.Play("Exit", 0);
		title_anim.Play("Exit", 0);
		yield return new WaitForSeconds(1.5f);
		curtain_anim.Play("FallFast", 0);
		yield return new WaitForSeconds(0.5f);
		LoadScene(nextScene);
	}
	
	IEnumerator StartRecords()
	{
		nextScene = "Records";
		audio.PlayOneShot(audio.clip, 0.3f);
		curtain_anim.Play("FallFast", 0);
		yield return new WaitForSeconds(1.0f);
		LoadScene(nextScene);
	}
	
	IEnumerator StartOptions()
	{
		nextScene = "Options";
		audio.PlayOneShot(audio.clip, 0.3f);
		curtain_anim.Play("FallFast", 0);
		yield return new WaitForSeconds(1.0f);
		LoadScene(nextScene);
	}
	
	IEnumerator StartLeaderboard()
	{
		nextScene = "Leaderboard";
		audio.PlayOneShot(audio.clip, 0.3f);
		curtain_anim.Play("FallFast", 0);
		yield return new WaitForSeconds(1.0f);
		LoadScene(nextScene);
	}
	
	IEnumerator Quit()
	{
		audio.PlayOneShot(audio.clip, 0.3f);
		curtain_anim.Play("FallFast", 0);
		yield return new WaitForSeconds(1.0f);
		Application.Quit();
	}
	
	public void LoadScene(string sceneName)
    {
		SceneManager.LoadScene(sceneName);
    }
}
