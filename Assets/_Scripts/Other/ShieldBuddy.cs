using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBuddy : BuddyBehaviour
{
	public GameObject shieldPrefab;
	
    public override void NewArea()
	{
		Instantiate(shieldPrefab, player.transform);
	}
}
