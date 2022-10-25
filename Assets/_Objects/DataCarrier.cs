using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public enum GameMode { JOURNEY, ENDURANCE }

[CreateAssetMenu(fileName = "New Data Carrier", menuName = "Data/Carrier")]

[System.Serializable]
public class DataCarrier : ScriptableObject, ISerializationCallbackReceiver
{
	public string savePath;
	public string playerName;
	public GameMode mode;
	public bool randomize_E_Mode;
	
    public int highScore;
	public int journeyScore;
	public int journeyHigh;
	public int totalDistance;
	public int totalRuns;
	public int bossesDefeated;
	public int costumeID;
	public bool[] costumeLocks;
	public int totalPlayTime;
	
	public int coins;
	
	public int border_id;
	public bool has_border;
	public Vector2 resolution;
	
	public float master_volume;
	public float music_volume;
	
	public BuddyObject buddy;
	
	public void Save()
	{	
		string saveData = JsonUtility.ToJson(this, true);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(string.Concat(Application.persistentDataPath, savePath));
		bf.Serialize(file, saveData);
		file.Close();
	}
	
	public void Load()
	{
		if(File.Exists(string.Concat(Application.persistentDataPath, savePath)))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(string.Concat(Application.persistentDataPath, savePath), FileMode.Open);
			JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
			file.Close();
		}
		
		
	}
	
	public void Erase()
	{
		playerName = "";
		
		highScore = 0;
		journeyScore = 0;
		journeyHigh = 0;
		totalDistance = 0;
		totalRuns = 0;
		bossesDefeated = 0;
		costumeID = 0;
		costumeLocks = new bool[13];
		costumeLocks[0] = true;
		totalPlayTime = 0;
	}
	
	public void OnAfterDeserialize()
	{
		
	}
	
	public void OnBeforeSerialize()
	{
		
	}
}
