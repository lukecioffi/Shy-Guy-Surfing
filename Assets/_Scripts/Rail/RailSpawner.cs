using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailSpawner : MonoBehaviour
{
    public int areaNum = 0;
	
	public Transform spawnRef;
	public Transform spawnPoint;
	public Transform grid;
	public float startX;
	public float obs_distance = 27;
	public bool isOn;
	
	public AreaData area;
	
    // Start is called before the first frame update
    void Start()
    {
        startX = transform.position.x + obs_distance;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnRef.position.x <= 0)
		{
			if(isOn) CreateObstacle();
			spawnRef.position = new Vector3(startX, spawnRef.position.y, 0);
		}
    }
	
	void CreateObstacle()
	{
		if(area == null) return;
		Vector3 newPosition = new Vector3(spawnPoint.position.x - Random.Range(0, 4), spawnPoint.position.y, 0);
		GameObject obs = Instantiate(area.obstacles[Random.Range(0, area.obstacles.Count)], newPosition, spawnRef.rotation);
		//GameObject obs = Instantiate(obsArr[areaNum - 1].obstacles[Random.Range(0, obsArr[areaNum - 1].obstacles.Length)], newPosition, spawnRef.rotation);
		obs.transform.SetParent(grid);
	}
}
