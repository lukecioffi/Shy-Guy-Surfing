using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadGame : MonoBehaviour
{
	public DataCarrier file;
	public MeshRenderer text;
	public MeshRenderer text_shadow;
	public ParticleSystem ps;
	
	[SerializeField]
	private Animator curtain_anim;
	[SerializeField]
	private Animator gameover_anim;
	
	private ScoreReader score;
	
	private string currentScene;
	
	public static ReloadGame instance;
	
	// Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this) 
        {
            Destroy(this.gameObject);
        }
 
        instance = this;
		curtain_anim.Play("Empty", 0); // Play curtain animation
	}
	
    // Start is called before the first frame update
    void Start()
    {
		ps.Stop();
		
		score = FindObjectOfType<ScoreReader>();
		if(score.h_score > score.old_h_score)
		{
			ps.Play();
			text_shadow.enabled = true;
			text.enabled = true;
		}
		
        currentScene = SceneManager.GetActiveScene().name; // Get active scene name
		file.Save();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
		{
			if(gameover_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
				StartCoroutine(Retry());
		}
		
		if(Input.GetButtonDown("Pause"))
		{
			if(gameover_anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
				StartCoroutine(Quit());
		}
		
    }
	
	IEnumerator Retry()
	{
		file?.Save();
		curtain_anim.Play("FallFast", 0); // Play curtain animation
		yield return new WaitForSeconds(0.75f); // Wait for 0.8 seconds before restarting level
		
		LoadScene(currentScene);
	}
	
	IEnumerator Quit()
	{
		file?.Save();
		curtain_anim.Play("FallFast", 0); // Play curtain animation
		yield return new WaitForSeconds(0.75f); // Wait for 0.8 seconds before restarting level
		
		LoadScene("MainMenu");
	}
	
	public void LoadScene(string sceneName)
    {
		SceneManager.LoadScene(sceneName);
    }
}
