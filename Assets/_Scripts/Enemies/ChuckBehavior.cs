using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuckBehavior : MonoBehaviour
{
    private PlayerMovement player;
	[SerializeField] private Rigidbody2D rb;
	[SerializeField] private GameObject ballPrefab;
	[SerializeField] private GameObject puffPrefab;
	[SerializeField] private Animator anim;
	[SerializeField] private Transform passPoint;
	
	
	private int age;
	public int firerate;
	private AudioSource audio;
	
	// Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
		audio = GetComponent<AudioSource>();
		transform.position = new Vector2(15, -3.75f);
		rb.AddForce(new Vector2(-1000, 0));
    }

    // Update is called once per frame
    void Update()
    {
        if(age >= firerate)
		{
			anim.Play("Chuck_Pass");
			age = 0;
		}
    }
	
	void FixedUpdate()
	{	
		age++;
	}
	
	void Pass()
	{
		audio.Play();
		Instantiate(puffPrefab, passPoint.position, passPoint.rotation);
		Instantiate(ballPrefab, passPoint.position, passPoint.rotation);
	}
	
	void TakeDamage()
	{
		age = 0;
	}
}
