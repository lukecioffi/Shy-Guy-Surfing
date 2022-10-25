using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayHandler : MonoBehaviour
{
	public Transform boxHolder;
	public Vector2 targetPos;
	
	public AudioSource audio;
	public AudioClip sfx_message;
	public Animator anim;
	
	public SpriteRenderer arrowLeft;
	public SpriteRenderer arrowRight;
	
	public int maxPages;
	
	public int page = 0;
	bool turning = false;
	float t = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        page = 0;
		anim.Play("Open", 0, 0f);
		audio.PlayOneShot(sfx_message);
    }

    // Update is called once per frame
    void Update()
    {
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Stay"))
		{
			if(!turning)
			{
				if(Input.GetAxisRaw("Horizontal") > 0.5f)
				{
					if(page < maxPages - 1)
					{
						page++;
						StartCoroutine(TurnPage());
					}
				}
				if(Input.GetAxisRaw("Horizontal") < -0.5f)
				{
					if(page > 0)
					{
						page--;
						StartCoroutine(TurnPage());
					}
				}
			}
			
			if(page == maxPages - 1)
			{
				if(Input.GetButtonDown("Jump"))
				{
					StartCoroutine(StartGame());
				}
			}
			if(Input.GetButtonDown("Pause"))
			{
				StartCoroutine(Quit());
			}
		}
		
		if(turning) // DISABLE ARROWS WHILE TURNING PAGE
		{
			if(arrowLeft.enabled) arrowLeft.enabled = false;
			if(arrowRight.enabled) arrowRight.enabled = false;
		}
		else // ENABLE ARROWS CONDITIONALLY
		{
			if(page == 0)
			{
				if(arrowLeft.enabled) arrowLeft.enabled = false;
			}
			else if(!arrowLeft.enabled) arrowLeft.enabled = true;
			
			if(page == maxPages - 1)
			{
				if(arrowRight.enabled) arrowRight.enabled = false;
			}
			else if(!arrowRight.enabled) arrowRight.enabled = true;
		}
    }
	
	void FixedUpdate()
	{
		if(turning)
		{
			boxHolder.localPosition = Vector2.Lerp(boxHolder.localPosition, targetPos, 0.2f);
		}
		else
		{
			
		}
	}
	
	IEnumerator TurnPage()
	{
		audio.Play();
		targetPos = new Vector2(-26 * page, 0);
		turning = true;
		yield return new WaitUntil(() => Vector2.Distance(boxHolder.localPosition, targetPos) < 0.0625f);
		turning = false;
		boxHolder.localPosition = targetPos;
	}
	
	IEnumerator StartGame()
	{
		audio.PlayOneShot(sfx_message);
		anim.Play("Close", 0, 0f);
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene("PreJourney");
	}
	
	IEnumerator Quit()
	{
		audio.PlayOneShot(sfx_message);
		anim.Play("Close", 0, 0f);
		yield return new WaitForSeconds(1.0f);
		SceneManager.LoadScene("MainMenu");
	}
	
}
