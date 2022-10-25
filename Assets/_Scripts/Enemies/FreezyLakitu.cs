using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezyLakitu : MonoBehaviour
{
    private PlayerMovement player;
	private Animator anim;
	private AudioSource audio;
	[SerializeField] private ParticleSystem wind_ps;
	[SerializeField] private GameObject iceblockPrefab;
	
	private int age;
	public int firerate;
	
	GameObject ice_block;
	
	// Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
		anim = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
		
		wind_ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(age >= firerate)
		{
			anim.Play("Blow");
			age = 0;
		}
    }
	
	void FixedUpdate()
	{
		if(player != null && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
		{
			transform.position = new Vector2
			(
				Mathf.Lerp(transform.position.x, player.transform.position.x, Time.deltaTime),
				transform.position.y
			);
		}
		
		age++;
	}
	
	void WindOn()
	{
		wind_ps.Play();
		audio.Play();
	}
	
	void WindOff()
	{
		wind_ps.Stop();
		audio.Stop();
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player" && ice_block == null)
		{
			ice_block = Instantiate
			(
				iceblockPrefab, player.transform.position, player.transform.rotation
			);
				
			ice_block.transform.SetParent(player.transform);
			ice_block.transform.localPosition = new Vector2(0, 0);
		}
	}
}
