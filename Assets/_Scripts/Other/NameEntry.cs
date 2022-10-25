using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NameEntry : MonoBehaviour
{
	public DataCarrier file;
	public TextMeshPro name_T;
	
	bool complete = false;
	
	public SpriteRenderer typingBar;
	
	bool axisDownX;
	int ctr = 1;
	public AudioSource audio;
	public AudioClip sfx_message;
	public Animator anim;
	public Animator[] buttonAnims;
	public TextMeshPro[] buttons;
	
	public SpriteRenderer blackmatte;
	
	private string nameInput = "";
	private string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
	float t = 0;
	
    // Start is called before the first frame update
    void Start()
    {
		anim.Play("Open", 0, 0f);
		complete = false;
		StartCoroutine(NamePlayer());
		
		t = 0;
    }

    // Update is called once per frame
    void Update()
	{	
		if(typingBar.enabled)
		{
			typingBar.transform.localPosition = new Vector3(-3.1875f + (nameInput.Length * 0.5f), 0.125f, 0.5f);
			
			if(Input.anyKeyDown)
			{
				char keyPressed = ' ';
				
				if(Input.inputString.Length > 0)
					if(Alphabet.IndexOf(Input.inputString[0]) != -1) keyPressed = Input.inputString[0];
				
				if(keyPressed != ' ' && nameInput.Length < 14)
				{
					nameInput += keyPressed;
				}
				
				if(Input.GetKeyDown(KeyCode.Backspace))
				{
					if(nameInput.Length > 1)
						nameInput = nameInput.Substring(0, nameInput.Length - 1);
					else
						nameInput = "";
				}
				
				name_T.SetText(nameInput);
			}
		}
		else
		{
			if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) >= 0.5f)
			{
				if(!axisDownX)
				{
					if(ctr == 0) ctr = 1;
					else ctr = 0;
					audio.Play();
					buttonAnims[ctr].Play("Bounce", 0, 0f);
				}
				axisDownX = true;
			}
			else
			{
				axisDownX = false;
			}
		}
	}
	
	void FixedUpdate()
	{
		for(int i = 0; i < buttons.Length; i++)
		{
			if (i == ctr)
			{
				buttons[i].color = Color.yellow;
			}
			else buttons[i].color = Color.white;
		}
		
		if(complete)
		{
			blackmatte.color = Color.Lerp(new Color(0, 0, 0, 0.5f), new Color(0, 0, 0, 0f), t);
			t += 0.02f;
		}
		else
		{
			blackmatte.color = Color.Lerp(new Color(0, 0, 0, 0f), new Color(0, 0, 0, 0.5f), t);
			t += 0.02f;
		}
	}
	
	public void Retry()
	{
		StopAllCoroutines();
		StartCoroutine(NamePlayer());
	}
	
	IEnumerator NamePlayer()
	{
		buttonAnims[0].transform.parent.gameObject.SetActive(false);
		buttonAnims[1].transform.parent.gameObject.SetActive(false);
		
		audio.PlayOneShot(sfx_message);
		typingBar.enabled = true;
		nameInput = "";
		name_T.SetText(nameInput);
		
		yield return new WaitUntil(() => (name_T.text.Length > 0 && Input.GetKeyDown(KeyCode.Return)) || !typingBar.enabled);
		
		typingBar.enabled = false;
		
		ctr = 1;
		
		buttonAnims[0].transform.parent.gameObject.SetActive(true);
		buttonAnims[1].transform.parent.gameObject.SetActive(true);
		
		audio.Play();
		buttonAnims[1].Play("Bounce", 0, 0f);
		
		yield return new WaitForSeconds(0.125f);
		yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
		
		if(ctr == 0)
		{
			Retry();
		}
		
		if(ctr == 1)
		{
			file.playerName = nameInput;
			audio.PlayOneShot(sfx_message);
			anim.Play("Close", 0, 0f);
			t = 0;
			complete = true;
			
			file.Save();
			yield return new WaitForSeconds(1.0f);
			SceneManager.LoadScene("MainMenu");
		}
	}
}


