using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class LeaderboardManager : MonoBehaviour
{
	public static LeaderboardManager instance;
	public bool connected;
	public bool connectionFailed;
	
	public DataCarrier file;
	
	public LeaderboardNode[] nodes;
	
	public Sprite[] faces;
	
	public Animator anim;
	
	int leaderboardID = 7969;
	
	public bool showOnAwake = false;
	
	void Awake()
	{
		instance = this;
	}
	
    // Start is called before the first frame update
    void Start()
    {
		connected = false;
		connectionFailed = false;
		
		if(file.mode != GameMode.ENDURANCE)
		{
			this.enabled = false;
			return;
		}
        StartCoroutine(LoginRoutine());
        StartCoroutine(WaitForTimeout());
		
		nodes = GetComponentsInChildren<LeaderboardNode>();
    }
	
	IEnumerator LoginRoutine()
	{
		
		bool done = false;
		LootLockerSDKManager.StartGuestSession((response) =>
		{
			if(response.success)
			{
				Debug.Log("Player was logged in!");
				PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
				connected = true;
				connectionFailed = false;
				done = true;
			}
			else
			{
				Debug.Log("Could not start session.");
				connected = false;
				connectionFailed = true;
				done = true;
			}
		});
		yield return new WaitWhile(() => done == false);
		
        if(connected)
		{
			SetPlayerName();
			yield return new WaitForSeconds(0.25f);
			if(showOnAwake)
			{
				BringScores();
				ShowScores();
			}
		}
	}
	
	public void UploadScore()
	{
		StartCoroutine(SubmitScoreRoutine(file.highScore));
	}
	
	public void ShowScores()
	{
		StartCoroutine(FetchTopHighScoresRoutine());
	}
	
	public void BringScores()
	{
		if(anim == null) return;
		anim.Play("In", 0, 0f);
	}
	
	public void SetPlayerName()
	{
		LootLockerSDKManager.SetPlayerName(file.playerName, (response) =>
		{
			if(response.success)
			{
				Debug.Log("Name set!");
			}
			else
			{
				Debug.Log("Name failed to set." + response.Error);
			}
		});
	}
	
	IEnumerator SubmitScoreRoutine(int scoreToUpload)
	{
		
		bool done = false;
		string playerID = PlayerPrefs.GetString("PlayerID");
		LootLockerSDKManager.SubmitScore(playerID, scoreToUpload, leaderboardID, file.costumeID.ToString(), (response) =>
		{
			if(response.success)
			{
				Debug.Log("Score uploaded!");
				done = true;
			}
			else
			{
				Debug.Log("Upload failed." + response.Error);
				done = true;
			}
		});
		yield return new WaitWhile(() => done == false);
		
		StartCoroutine(FetchTopHighScoresRoutine());
	}
	
	IEnumerator FetchTopHighScoresRoutine()
	{
		bool done = false;
		LootLockerSDKManager.GetScoreListMain(leaderboardID, 7, 0, (response) =>
		{
			if(response.success)
			{
				LootLockerLeaderboardMember[] members = response.items;
				
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
						nodes[i].Set(members[i].rank, tempPlayerName, members[i].score + "m", faces[costumeID]);
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
				Debug.Log("Score download failed." + response.Error);
				done = true;
			}
		});
		yield return new WaitWhile(() => done == false);
	}
	
	IEnumerator WaitForTimeout()
	{
		yield return new WaitForSeconds(5.0f);
		if(!connected) connectionFailed = true;
	}
	
	public int GetLeaderboardID()
	{
		return leaderboardID;
	}
}
