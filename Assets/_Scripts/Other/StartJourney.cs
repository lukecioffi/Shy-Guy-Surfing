using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartJourney : MonoBehaviour
{
	public DataCarrier file;
    int thousandth_dist;
	int max_score;
	
	public Animator anim;
	public TextMeshPro dist_t;
	
	public GameObject[] areas;
	
	[SerializeField] private Animator buttonAnim;
	[SerializeField] private Animator arrowAnim1;
	[SerializeField] private Animator arrowAnim2;
	[SerializeField] private AudioClip sfx_menumove;
	[SerializeField] private AudioClip sfx_select;
	[SerializeField] private AudioClip sfx_wrong;
	[SerializeField] private AudioSource audio;
	[SerializeField] private TextMeshPro area_t;
	
	
	bool axisDown;
	
	// Start is called before the first frame update
    void Start()
    {
        ChangeArea();
		max_score = file.journeyHigh / 1000;
		
		// if(max_score == 0) // Start game automatically if highscore is 0
		// {
			// LoadScene("GameScene");
		// }
		
		if(max_score > 9) max_score = 9;
    }

    // Update is called once per frame
    void Update()
    {
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Stay"))
		{
			if(Input.GetAxisRaw("Horizontal") != 0)
			{
				if(!axisDown)
				{
					audio.PlayOneShot(sfx_menumove, 1.0f);
			
					thousandth_dist += (int)Input.GetAxisRaw("Horizontal");
					
					if(thousandth_dist < 0) thousandth_dist = 0;
					if(thousandth_dist > max_score) thousandth_dist = max_score;
					
					buttonAnim.Play("Bounce", 0, 0f);
							
					if((int)Input.GetAxisRaw("Horizontal") == -1) arrowAnim1.Play("Hit", 0, 0f);
					else if((int)Input.GetAxisRaw("Horizontal") == 1) arrowAnim2.Play("Hit", 0, 0f);
					
					ChangeArea();
					axisDown = true;
				}
			}
			else axisDown = false;
			
			if(Input.GetButtonDown("Submit"))
			{
				audio.PlayOneShot(sfx_select, 1.0f);
				StartCoroutine(StartGame());
			}
			
			if(Input.GetButtonDown("Pause"))
			{
				StartCoroutine(ReturnToMenu());
			}
			
			thousandth_dist = Mathf.Clamp(thousandth_dist, 0, max_score);
			
			
		}
		
		dist_t.SetText((thousandth_dist * 1000) + "m");
    }
	
	void ChangeArea()
	{
		for (int i = 0; i < areas.Length; i++)
		{
			if(i == thousandth_dist)
			{
				areas[i].SetActive(true);
				area_t.SetText(areas[i].name);
			}
			else areas[i].SetActive(false);
		}
	}
	
	IEnumerator ReturnToMenu()
	{
		anim.Play("Close");
		yield return new WaitForSeconds(1.0f);
		LoadScene("MainMenu");
	}
	
	IEnumerator StartGame()
	{
		file.journeyScore = thousandth_dist * 1000;
		anim.Play("Close");
		yield return new WaitForSeconds(1.0f);
		LoadScene("GameScene");
	}
	
	public void LoadScene(string sceneName)
    {
		SceneManager.LoadScene(sceneName);
    }
}
