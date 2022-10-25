using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
	public bool randomized = false;
	
	[SerializeField]
    public int areaNum;
	[SerializeField]
    private RailSpawner spawner;
	[SerializeField]
    public ScoreReader scoreReader;
	
	[SerializeField]
    public GameObject whiteflash;
	[SerializeField]
    public ParticleSystem rain;
	[SerializeField]
    private GameObject[] areaPrefabs;
	[SerializeField]
    public GameObject activeArea;
	[SerializeField]
    private MusicSet[] musics;
	[SerializeField]
    public SequencedMusicPlayer smp;
	
	[SerializeField]
	public int levelLength;
	
    public float init_vol;
	
	[SerializeField]
    private GameObject[] lakituPrefab;
	[SerializeField]
    private GameObject jackOPrefab;
	[SerializeField]
    public GameObject activeLak;
	[SerializeField]
    private Animator waterAnim;
	
	[SerializeField] private BossController bc;
	
	
	// Start is called before the first frame update
    void Start()
    {
		rain.Stop();
		init_vol = smp.volume;
        areaNum = 1;
		
		musics[areaNum - 1].useFade = true;
    }

    // Update is called once per frame
    void Update()
    {
		if(areaNum < areaPrefabs.Length)
		{
			if(scoreReader.d_score > areaNum * levelLength - 75)
			{
				spawner.isOn = false;
				if(!rain.isPlaying)
				{
					rain.Play();
				}
				
			}
			
			if(scoreReader.d_score > areaNum * levelLength - 10)
			{
				musics[areaNum].Play();
				smp.volume = init_vol;
				Instantiate(whiteflash, new Vector3(0, 0, 0), transform.rotation);
				Invoke("NextArea", 0.81f);
				
				if(randomized)
				{
					int randAreaNum = areaNum;
					while(randAreaNum == areaNum)
					{
						randAreaNum = Random.Range(0, areaPrefabs.Length);
					}
					areaNum = randAreaNum;
				}
				else areaNum++;
				
				spawner.areaNum = areaNum;
				spawner.isOn = true;
			}
		}
		
		//Destroy all enemies before player hits 200 distance
		if(scoreReader.d_score < 200)
		{
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach(GameObject e in enemies)
			{
				Destroy(e);
			}
		}
		
		if(scoreReader.d_score % levelLength == levelLength / 2)
		{
			if(activeLak == null && lakituPrefab[areaNum - 1] != null)
			{
				activeLak = Instantiate(lakituPrefab[areaNum - 1], new Vector3(20, 6.5f, 0), transform.rotation);
			}
		}
		
		// Set bossPrepared to true
		if(scoreReader.d_score % 2000 == 1900)
		{
			bc.bossPrepared = true;
			bc.ptr = areaNum / 2 - 1;
		}
    }
	
	void FixedUpdate()
	{
		if(areaNum < areaPrefabs.Length)
		{
			if(scoreReader.d_score > areaNum * levelLength - 70)
			{
				smp.volume -= 0.002f;
			}
		}
	}
	
	void NextArea()
	{	
		spawner.areaNum = areaNum;
		if(rain.isPlaying)
		{
			Destroy(activeLak);
			rain.Stop();
		}
		
		Destroy(activeArea);
		activeArea = Instantiate(areaPrefabs[areaNum - 1], new Vector3(0, 0, 0), transform.rotation);
		activeArea.transform.SetParent(transform);
		activeArea.transform.position = new Vector3(0, 0, 0);
		
		if(areaNum == 11)
		{
			
			waterAnim.Play("Rising", 0);
		}
	}
}
