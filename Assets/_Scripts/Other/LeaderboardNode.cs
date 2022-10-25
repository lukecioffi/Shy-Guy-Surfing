using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardNode : MonoBehaviour
{
	public TextMeshPro nameT;
	public TextMeshPro scoreT;
	public SpriteRenderer icon;
	public SpriteRenderer crown;
	public SpriteRenderer box;
	
	void Start()
	{
		Clear();
	}
	
	public void Clear()
	{
		Set(0, "", "", null);
	}
	
	public void Set(int rank, string name, string score, Sprite face)
	{
		if(rank == 1) crown.enabled = true;
		else crown.enabled = false;
		nameT.SetText(name);
		scoreT.SetText(score);
		icon.sprite = face;
	}
	
	public void IsMine(bool mine)
	{
		if(mine)
		{
			box.color = new Color(1, 1, 0, 0.75f);
			icon.color = new Color(1, 1, 1, 1.0f);
		}
		else
		{
			box.color = new Color(1, 1, 1, 0.5f);
			icon.color = new Color(1, 1, 1, 0.75f);
		}
	}
}
