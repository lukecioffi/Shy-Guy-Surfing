using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Buddy", menuName = "Data/Buddy")]
public class BuddyObject : ScriptableObject
{
	public string name;
	[TextArea]
	public string description;
	public GameObject prefab;
}
