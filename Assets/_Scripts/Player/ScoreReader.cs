using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreReader : MonoBehaviour
{
	public DataCarrier file;
	public AreaManager manager;
	
	public bool start_at_random_area;
	
	[SerializeField] public Transform distMeasure;
	private Vector3 startPos;
	[SerializeField] public int d_score;
	[SerializeField] public int h_score;
	[SerializeField] public int old_h_score;
	[SerializeField] private PlayerMovement player;
	
	[Header("TextMeshPro Objects")]
	[SerializeField] private TextMeshPro d_score_t;
	[SerializeField] private TextMeshPro h_score_t;
	[SerializeField] private TextMeshPro best_t;
	[SerializeField] private TextMeshPro name_t;
	
	[SerializeField] private Animator anim;
	[SerializeField] private SpriteRenderer icon;
	
	[Header("Randomization")]
	[SerializeField] public bool randomized;
	string[] randomChars = {"!", "@", "#", "$", "%", "&", "?", "X"};
	
	bool newHigh;
	NotificationBox notif;
	
	
    // Start is called before the first frame update
    void Start()
    {	
        startPos = new Vector3(0, 0, 0);
		old_h_score = file.highScore;
		h_score = old_h_score;
		name_t.SetText(file.playerName);
		
		EnemyRailMovement _r = FindObjectOfType<EnemyRailMovement>();
		
		if(file.mode == GameMode.JOURNEY)
		{
			distMeasure.position = new Vector3(-1 * file.journeyScore, 0, 0);
		}
		
		if(start_at_random_area)
		{
			int random_area_num = Random.Range(0, 8);
			
			distMeasure.position = new Vector3(-1000 * random_area_num, 0, 0);
			
			
		}
		//_r.speed = 7.25f + ((-distMeasure.position.x / 1000) * 0.25f);
		
		if(LeaderboardManager.instance != null) icon.sprite = LeaderboardManager.instance.faces[file.costumeID];
		notif = FindObjectOfType<NotificationBox>(); // Find NotificationBox
    }

    // Update is called once per frame
    void Update()
    {
		// PRINT CURRENT SCORE
		if(!randomized)
			d_score_t.SetText(d_score.ToString() + "m");
		
		// PRINT HIGH SCORE
		if(file.mode == GameMode.JOURNEY)
		{
			best_t.SetText("GOAL:");
			h_score_t.SetText(manager.destination + "m");
			
			anim.SetBool("Endurance", false);
		}
		if(file.mode == GameMode.ENDURANCE)
		{
			best_t.SetText("BEST:");
			h_score_t.SetText(h_score + "m");
			
			if(LeaderboardManager.instance.connected) anim.SetBool("Endurance", true);
			else anim.SetBool("Endurance", false);
		}
		
		if(player == null)
		{
			anim.SetBool("After", true);
		}
		
		
    }
	
	void FixedUpdate()
	{
		if(randomized)
		{
			d_score_t.SetText(randomChars[Random.Range(0, 8)] + randomChars[Random.Range(0, 8)] + randomChars[Random.Range(0, 8)]
				+ randomChars[Random.Range(0, 8)] + "m");
		}
		
		
		if(player != null)
		{
			d_score = (int)Vector3.Distance(startPos, distMeasure.position);
			
			if(file.mode == GameMode.ENDURANCE)
			{
				if(d_score > old_h_score)
					h_score = d_score;
				
				if(!newHigh && d_score >= h_score && old_h_score > 0)
				{
					if(notif != null) notif.SendNotif("NEW HIGH SCORE REACHED!");
					
					newHigh = true;
				}
			}
		}
		
		if(player == null)
		{
			file.highScore = h_score;
			if(file.mode == GameMode.ENDURANCE && LeaderboardManager.instance.connected)
			{
				LeaderboardManager.instance.UploadScore();
				LeaderboardManager.instance.BringScores();
			}
			this.enabled = false;
			
			return;
		}
	}
	
	public void Freeze()
	{
		var p = distMeasure.GetComponent<Parallax>();
		p.enabled = true;
		randomized = true;
	}
	
	public void Unfreeze()
	{
		var p = distMeasure.GetComponent<Parallax>();
		p.enabled = false;
		randomized = false;
	}
}
