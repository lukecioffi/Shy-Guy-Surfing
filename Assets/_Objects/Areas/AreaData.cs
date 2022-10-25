using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Area", menuName = "Data/Area")]
public class AreaData : ScriptableObject
{
	public string name;
	public bool risingWater;
	public GameObject bgPrefab;
	public GameObject chaserPrefab;
	
	[Header("Music")]
	public AudioClip introClip;
	public AudioClip loopClip;
	[TextArea]
	public string songTitle;
	
	[Header("Obstacles")]
	public List<GameObject> obstacles = new List<GameObject>();
	
	public void OnValidate()
	{
		if(name == "Fury")
			obstacles = Resources.LoadAll<GameObject>("Obstacle Squares").ToList();
	}
}
