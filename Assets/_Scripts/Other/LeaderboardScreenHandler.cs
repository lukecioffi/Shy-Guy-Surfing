using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using LootLocker.Requests;

public class LeaderboardScreenHandler : MonoBehaviour
{	
	public DataCarrier file;
	public LeaderboardNode myNode;
	public TextMeshPro connect_T;
	public TextMeshPro rank_T;
	
	public AudioSource audio;
	public AudioClip sfx_message;
	public Animator anim;
	
	public LeaderboardNode[] nodes;
	
	public Animator[] buttonAnims;
	public TextMeshPro[] buttons;
	
	bool axisDownX;
	int ctr = 0;
	
	void Awake()
	{
		file.mode = GameMode.ENDURANCE;
	}
	
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForConnection());
    }

    // Update is called once per frame
    void Update()
    {
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Stay"))
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
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Stay"))
		{
			for(int i = 0; i < buttons.Length; i++)
			{
				if (i == ctr)
				{
					buttons[i].color = Color.yellow;
				}
				else buttons[i].color = Color.white;
			}
		}
	}
	
	IEnumerator WaitForConnection()
	{
		anim.Play("Close", 0, 1f);
		connect_T.color = Color.white;
		connect_T.gameObject.SetActive(true);
		while(!LeaderboardManager.instance.connected && !LeaderboardManager.instance.connectionFailed)
		{
			connect_T.SetText("Connecting");
			yield return new WaitForSeconds(0.25f);
			connect_T.SetText("Connecting.");
			yield return new WaitForSeconds(0.25f);
			connect_T.SetText("Connecting..");
			yield return new WaitForSeconds(0.25f);
			connect_T.SetText("Connecting...");
			yield return new WaitForSeconds(0.25f);
		}
		
		
		if(!LeaderboardManager.instance.connected)
		{
			connect_T.color = Color.red;
			connect_T.SetText("Connection failed.");
			yield return new WaitForSeconds(1.0f);
			SceneManager.LoadScene("MainMenu");
			yield break;
		}
		else
		{
			connect_T.gameObject.SetActive(false);
			anim.Play("Open", 0, 0f);
			StartCoroutine(GetPlayerScore());
		}
		
	}
	
	IEnumerator GetPlayerScore()
	{
		bool done = false;
		int leaderboardID = LeaderboardManager.instance.GetLeaderboardID();
		string playerID = PlayerPrefs.GetString("PlayerID");
		
		nodes = GetComponentsInChildren<LeaderboardNode>();
		
		LootLockerSDKManager.GetMemberRank(leaderboardID, playerID, (response) =>
		{
			if (response.success)
			{
				int rank = response.rank;
				int count = 3;
				int after = rank <= 3 ? 0 : rank - 2;

				LootLockerSDKManager.GetScoreListMain(leaderboardID, count, after, (response2) =>
				{
					if(response2.success)
					{
						LootLockerLeaderboardMember[] members = response2.items;
						
						for(int i = 0; i < nodes.Length; i++)
						{
							if(i < members.Length)
							{
								string tempPlayerName = members[i].rank + ".";
								int costumeID = int.Parse(members[i].metadata);
								if(members[i].player.name != "")
									tempPlayerName += members[i].player.name;
								else
									tempPlayerName += members[i].player.id;
								nodes[i].Set(members[i].rank, tempPlayerName, members[i].score + "m", LeaderboardManager.instance.faces[costumeID]);
								nodes[i].IsMine(members[i].player.id.ToString() == PlayerPrefs.GetString("PlayerID"));
							}
							else
							{
								nodes[i].Clear();
							}
						}
						done = true;
					}
					else
					{
						Debug.Log("Score download failed." + response2.Error);
						done = true;
					}
				});
				
				rank_T.SetText("You are ranked #" + rank + " on the global leaderboard.");
			}
			else
			{
				Debug.Log("failed: " + response.Error);
			}
		});
		yield return new WaitWhile(() => done == false);
		
		StartCoroutine(WaitForExit());
	}
	
	IEnumerator WaitForExit()
	{
		yield return new WaitUntil(() => Input.GetButtonDown("Jump"));
		
		audio.PlayOneShot(sfx_message);
		anim.Play("Close", 0, 0f);
		LeaderboardManager.instance.anim.Play("Exit", 0, 0f);
		yield return new WaitForSeconds(1.0f);
		
		if(ctr == 0) // RETURN
		{
			SceneManager.LoadScene("MainMenu");
		}
		
		if(ctr == 1) // CHANGE NAME
		{
			SceneManager.LoadScene("EnterName");
		}
	}
}
