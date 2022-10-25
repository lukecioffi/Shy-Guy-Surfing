using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
	public DataCarrier file;
	
	[Header("Base Data")]
	public List<AreaData> areas;
	public int areaCtr;
    [SerializeField] private RailSpawner spawner;
    public ScoreReader scoreReader;
	public int levelLength;
	public int destination;
	
	[Header("Visual Transition")]
    public GameObject whiteflash;
    public ParticleSystem rain;
	
	[Header("Music")]
    public MusicSet music;
    public SequencedMusicPlayer smp;
	public float init_vol;
	
	[Header("Active Objects")]
    public GameObject activeChaser;
    public Animator waterAnim;
    public GameObject activeArea;
	
    // Start is called before the first frame update
    void Start()
    {
		rain.Stop();
		init_vol = smp.volume;
		
		//music.useFade = true;
		
		if(areaCtr >= 0) spawner.area = areas[areaCtr];
		
		if(scoreReader == null) return;
		destination = scoreReader.d_score - (scoreReader.d_score % levelLength) + levelLength;
		
		if(file.mode == GameMode.JOURNEY)
		{
			destination = file.journeyScore + levelLength;
			areaCtr = file.journeyScore / 1000;
			scoreReader.d_score = file.journeyScore;
		}
		SetArea();
    }

    // Update is called once per frame
    void Update()
    {
        if(areaCtr < areas.Count)
		{
			// START TRANSITION
			if(scoreReader == null) return;
			
			if(scoreReader.d_score < destination && scoreReader.d_score >= destination - 75 && spawner.isOn)
			{
				StartCoroutine(StartAreaTransition());
				spawner.isOn = false;
			}
			
			if(scoreReader.d_score < destination && scoreReader.d_score >= destination - (levelLength / 2) && activeChaser == null)
			{
				SpawnChaser();
			}
		}
    }
	
	void FixedUpdate()
	{
		if(!spawner.isOn)
			smp.volume -= 0.001f;
	}
	
	public void SpawnChaser()
	{
		if(areas[areaCtr].chaserPrefab != null) activeChaser = Instantiate(areas[areaCtr].chaserPrefab, new Vector2(20, 6.5f), Quaternion.identity);
	}
	
	public void SetArea()
	{
		spawner.area = areas[areaCtr]; // PUT OBSTACLES IN SPAWNER
		
		if(activeChaser != null) Destroy(activeChaser); // DESTROY ACTIVE CHASER
		
		//AND STOP RAIN
		if(rain.isPlaying)
		{
			rain.Stop();
		}
		
		Destroy(activeArea);
		activeArea = Instantiate(areas[areaCtr].bgPrefab, transform);
		activeArea.transform.position = Vector2.zero;
		
		if(areas[areaCtr].risingWater)
		{
			waterAnim.Play("Rising", 0);
		}
		else
		{
			waterAnim.Play("Idle", 0);
		}
		
		foreach(HotWater h in FindObjectsOfType<HotWater>())
		{
			h.enabled = false;
		}
		
		// RESET MUSIC
		music.introClip = areas[areaCtr].introClip;
		music.loopClip = areas[areaCtr].loopClip;
		music.title = areas[areaCtr].songTitle;
		music.Play();
		smp.volume = init_vol;
		
		// SET JOURNEY HIGH SCORE
		if(file.mode == GameMode.JOURNEY)
			if(scoreReader.d_score > file.journeyHigh)
				file.journeyHigh = scoreReader.d_score - (scoreReader.d_score % levelLength);
		
		// SET NEW DESTINATION
		destination = scoreReader.d_score - (scoreReader.d_score % levelLength) + levelLength;
		spawner.isOn = true;
		
		foreach(BuddyBehaviour b in FindObjectsOfType<BuddyBehaviour>())
		{
			b.NewArea();
		}
	}
	
	IEnumerator StartAreaTransition()
	{
		Debug.Log("Started the transition coroutine!");
		if(!rain.isPlaying) rain.Play();
		
		yield return new WaitUntil(() => scoreReader.d_score > destination - 10);
		
		Instantiate(whiteflash, new Vector3(0, 0, 0), transform.rotation);
		
		yield return new WaitUntil(() => scoreReader.d_score >= destination);
		
		if(file.mode == GameMode.ENDURANCE && file.randomize_E_Mode) areaCtr = Random.Range(0, 10);
		else areaCtr++;
		
		areaCtr %= areas.Count; // LOOP AFTER LAST AREA
		SetArea();
	}
	
	public void InitFlash()
	{
		Instantiate(whiteflash, new Vector3(0, 0, 0), transform.rotation);
		return;
	}
}
