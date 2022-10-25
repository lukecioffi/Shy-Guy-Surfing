using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAreaSpawner : MonoBehaviour
{
    public bool firstTime;
	public GameObject[] areaPrefabs;
	
	public GameObject activeArea;
	
	void Awake()
    {
		if(PlayerPrefs.GetInt("first_load") == 0)
		{
			SceneManager.LoadScene("TutorialScene");
		}
		
		if(PlayerPrefs.GetInt("first_load") == 1)
		{
			firstTime = false;
		}
    }
	
	// Start is called before the first frame update
    void Start()
    {
		int areaNum = Random.Range(0, areaPrefabs.Length);
		if(firstTime) areaNum = 0;
        activeArea = Instantiate(areaPrefabs[areaNum], new Vector3(0, 0, 0), transform.rotation);
		
		PlayerPrefs.SetInt("first_load", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
