using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
	public SceneChanger scene_changer;
	
	public GameObject[] bossObjects;
	public MusicSet[] bossMusics;
	public GameObject[] bossAreas;
	
	[SerializeField] private ScoreReader score;
	
	[SerializeField] private GameObject activeBoss;
	[SerializeField] private PlayerMovement player;
	
	public int ptr = 0;
	
	
	public bool bossPrepared;
	bool inBossArea;
	
    // Start is called before the first frame update
    void Start()
    {
		if(PlayerPrefs.GetInt("BossesEnabled") == 0) this.enabled = false;
        scene_changer = GetComponent<SceneChanger>();
		player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
		if(!inBossArea && bossPrepared)
		{
			if(scene_changer.scoreReader.d_score > scene_changer.areaNum * scene_changer.levelLength - 20)
			{
				if(scene_changer.scoreReader.d_score < scene_changer.areaNum * scene_changer.levelLength)
				{
					bossMusics[ptr].Play();
					scene_changer.smp.volume = scene_changer.init_vol;
					Instantiate(scene_changer.whiteflash, new Vector3(0, 0, 0), transform.rotation);
					Invoke("BossArea", 0.81f);
					
					inBossArea = true;
				}
			}
		}
		
		
        if(score.randomized)
		{
			if(activeBoss == null || player == null)
			{
				score.Unfreeze();
				inBossArea = false;
			}
		}
    }
	
	void BossArea()
	{
		Destroy(scene_changer.activeLak);
		scene_changer.rain.Stop();
		
		Destroy(scene_changer.activeArea);
		
		scene_changer.activeArea = Instantiate(bossAreas[ptr], new Vector3(0, 0, 0), transform.rotation);
		scene_changer.activeArea.transform.SetParent(transform);
		scene_changer.activeArea.transform.position = new Vector3(0, 0, 0);
		
		activeBoss = Instantiate(bossObjects[ptr], new Vector3(15, -3.5f, 0), transform.rotation);
		scene_changer.scoreReader.distMeasure.transform.Translate(new Vector3(80, 0, 0));
		score.Freeze();
		bossPrepared = false;
	}
}
