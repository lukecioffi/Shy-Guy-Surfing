using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRing : MonoBehaviour
{
	public SpriteRenderer ring_rend;
	public SpriteRenderer icon_rend;
	
    public Sprite[] ring_sprites;
    public Sprite[] icon_sprites;
	
	public GameObject[] shieldPrefabs;
	public GameObject sparklePrefab;
	
	int timer;
	bool shrinking;
	public int ptr;
	public int changeRate;
	public bool randomized = true;
	public bool cycling = false;
	
	public static float spawnChance = 0.00f;
	
	void Awake()
	{
		if(Random.Range(0.0f, 1.0f) > spawnChance)
		{
			spawnChance += 0.02f;
			Destroy(transform.parent.gameObject);
		}
		else spawnChance = 0.00f;
	}
	
	// Start is called before the first frame update
    void Start()
    {
        if(cycling || randomized)
		{
			ptr = Random.Range(0, ring_sprites.Length);
		}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(timer % changeRate == 0)
		{
			if(cycling) ptr++;
		}
		
		if(ptr > 2)
		{
			ptr = 0;
		}
		
		if(shrinking)
		{
			transform.localScale = transform.localScale * 0.75f;
		}
		
		timer++;
		
		ring_rend.sprite = ring_sprites[ptr];
		icon_rend.sprite = icon_sprites[ptr];
    }
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player" && !shrinking)
		{
			GameObject new_shield = Instantiate(shieldPrefabs[ptr], collider.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
			new_shield.transform.SetParent(collider.GetComponent<PlayerMovement>().transform);
			Destroy(transform.parent.gameObject, 0.5f);
			
			GameObject sparkle = Instantiate(sparklePrefab, transform.position, Quaternion.Euler(0, 0, 0));
			sparkle.transform.SetParent(transform.parent);
			shrinking = true;
		}
	}
}
