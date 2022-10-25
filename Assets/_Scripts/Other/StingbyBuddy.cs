using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StingbyBuddy : BuddyBehaviour
{
    // Update is called once per frame
    public void Start()
    {
        player.maxFlightTime = 150;
    }
}
