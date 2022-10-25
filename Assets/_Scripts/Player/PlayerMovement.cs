using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
	
	public float runSpeed;
	public float dashSpeed;
	private float moveSpeed;
	public float default_grav = 3;
	
	public int flightTimer;
	public int maxFlightTime;
	public bool flying;
	
	//Audio
	public AudioClip jumpSFX;
	public AudioClip splashSFX;
	public AudioClip kickSFX;
	public AudioClip hurtSFX;
	public AudioSource audio;
	public AudioSource skid;
	
	float horizontalMove = 0f;
	
	bool jump;
	int jumpTime = 0;
	
	[Header("Visual FX")]
	[SerializeField] private SpriteRenderer offArrow;
	[SerializeField] public ParticleSystem ps;
	[SerializeField] private ParticleSystem flyPS;
	[SerializeField] private GameObject splashPref;
	[SerializeField] private GameObject puffPrefab;
	[SerializeField] private GameObject fallingPrefab;
	[SerializeField] private GameObject gameOverJPrefab;
	[SerializeField] private GameObject gameOverEPrefab;
	
	public GameObject activeShield;
	
	bool dead = false;
	
	public float drag = 0.35f;
	
	[SerializeField] private SpriteRenderer rend;
	[SerializeField] public int i_frames = 0;
	[SerializeField] private int max_i_frames = 75;
	
	public int coyoteTime;
	
	public DataCarrier file;
	
	public BuddyBehaviour buddy;
	
	// Start is called before the first frame update
    void Start()
    {
		jump = false;
		moveSpeed = runSpeed;
		
		if(file.buddy != null) 
		{
			GameObject newBud = Instantiate(file.buddy.prefab, transform);
			buddy = newBud.GetComponent<BuddyBehaviour>();
			buddy.player = this;
		}
    }

    // Update is called once per frame
    void Update()
    {
        //Get movement vector from input and speed
		moveSpeed = drag * 2 + dashSpeed;
		horizontalMove = (Input.GetAxisRaw("Horizontal") * moveSpeed) - drag;
		
		//Jump
		if(Input.GetButtonDown("Jump"))
		{
			if(controller.m_Grounded || coyoteTime > 0)
			{
				controller.m_Rigidbody2D.velocity = new Vector2(controller.m_Rigidbody2D.velocity.x, 0f);
				audio.volume = 1.0f * Time.timeScale;
				audio.PlayOneShot(jumpSFX, audio.volume);
				jump = true;
				jumpTime = 6;
				coyoteTime = 0;
			}
			else
			{
				if(flightTimer > 0 && coyoteTime <= 0)
					flying = true;
			}
		}
		
		if(flying && Input.GetButtonUp("Jump"))
		{
			flying = false;
			flightTimer = 0;
		}
		
		if(controller.m_OnWater)
		{
			if(!ps.isPlaying)
				ps.Play();
		}
		else
		{
			if(ps.isPlaying)
				ps.Stop();
		}
		
		if(controller.m_Grounded && !controller.m_OnWater)
		{
			if(!skid.isPlaying)
				skid.Play();
			skid.volume = 0.2f * Time.timeScale;
		}
		else
		{
			if(skid.isPlaying)
				skid.Stop();
		}
		
		if(!flying && controller.m_Grounded)
		{
			flightTimer = maxFlightTime;
		}
		
		if(!dead && transform.position.y > 8.75f)
		{
			offArrow.enabled = true;
			offArrow.transform.position = new Vector2(transform.position.x, 7f);
		}
		else offArrow.enabled = false;
    }
	
	void FixedUpdate()
	{
		controller.Move(horizontalMove, false, jump);
		jump = false;
		
		if(jumpTime > 0) jumpTime--;
		else jumpTime = 0;
		
		if(flying)
		{
			controller.m_Rigidbody2D.gravityScale = 0;
			controller.m_Rigidbody2D.velocity = new Vector2(controller.m_Rigidbody2D.velocity.x, 0);
				
			if(flightTimer > 0) flightTimer--;
			else
			{
				flying = false;
			}
			
			if(!flyPS.isPlaying) flyPS.Play();
		}
		else
		{
			controller.m_Rigidbody2D.gravityScale = default_grav;
			if(flyPS.isPlaying) flyPS.Stop();
			
			
		}
		
		if(!Input.GetButton("Jump"))
		{
			if(jumpTime > 0) controller.m_Rigidbody2D.AddForce(-Vector2.up * 300 * transform.localScale.y);
			jumpTime = 0;
		}
		
		if(i_frames > 0)
		{
			if(i_frames % 2 == 1)
				rend.enabled = false;
			else rend.enabled = true;
			
			i_frames--;
		}
		else
		{
			rend.enabled = true;
			i_frames = 0;
		}
		
		if(controller.m_Grounded) coyoteTime = 5;
		if(coyoteTime > 0) coyoteTime--;
	}
	
	public void OnLanding()
	{
		if(controller.m_OnWater)
		{
			Instantiate(splashPref, transform.position, transform.rotation);
			audio.PlayOneShot(splashSFX, audio.volume);
			
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Instakill")
		{
			if(!dead)
			{
				Destroy(activeShield);
				activeShield = null;
				i_frames = 0;
				Die();
			}
		}
		
		if(collider.gameObject.tag == "Enemy")
		{
			if(!dead)
			{
				Die();
			}
		}
		
		if(collider.gameObject.tag == "Fire")
		{
			if(!dead)
			{
				if(GetComponentInChildren<FireShield>() == null) Die();
			}
		}
		
		if(collider.gameObject.tag == "Stompable")
		{
			if(!dead)
			{
				bool stomped = false;
				controller.FixedUpdate();
				foreach(var c in controller.colliders3)
				{
					if(c != null && c == collider)
					{
						Debug.Log("Stomped!");
						stomped = true;
					}
				}
				
				if(stomped)
				{
					audio.PlayOneShot(kickSFX, 0.8f);
					collider.gameObject.SendMessage("TakeDamage");
					Instantiate(puffPrefab, (transform.position - (0.5f * transform.up)), transform.rotation);
					if(Input.GetButton("Jump"))
					{
						flying = false;
						controller.m_Rigidbody2D.velocity = new Vector2(controller.m_Rigidbody2D.velocity.x, 20);
					}
					else controller.m_Rigidbody2D.velocity = new Vector2(controller.m_Rigidbody2D.velocity.x, 10);
					flightTimer = maxFlightTime;
				}
				else
				{
					Die();
				}
			}
		}
		
		if(collider.gameObject.tag == "Platform")
		{
			if(collider.gameObject.tag == "Platform")
			{
				transform.SetParent(collider.transform);
				drag = 0;
			}
		}
		
		if(collider.gameObject.tag == "Bump")
		{
			if(controller.m_Grounded == false)
			{
				collider.gameObject.BroadcastMessage("Bump");
				Instantiate(puffPrefab, (transform.position + (0.55f * transform.up)), transform.rotation);
				controller.m_Rigidbody2D.velocity = new Vector2(controller.m_Rigidbody2D.velocity.x, 0);
			}
		}
	}
	
	void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Platform")
		{
			transform.SetParent(null);
			drag = 0.35f;
		}
	}
	
	void OnColliderStay2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Untagged")
		{
			Die();
		}
	}

	
	public void Die()
	{
		if(i_frames == 0)
		{
			if(activeShield != null)
			{
				Destroy(activeShield);
				i_frames = max_i_frames;
				audio.PlayOneShot(hurtSFX, 0.65f);
				activeShield = null;
			}
			else
			{
				Instantiate(puffPrefab, transform.position, transform.rotation);
				Instantiate(fallingPrefab, transform.position, transform.rotation);
				
				if(FindObjectsOfType<PlayerMovement>().Length <= 1)
				{
					//PlayerPrefs.SetInt("TotalRuns", PlayerPrefs.GetInt("TotalRuns") + 1);
					file.totalRuns++;
					
					ScoreReader _d = FindObjectOfType<ScoreReader>();
					if(_d != null)
					{
						// ADD TO TOTAL DISTANCE
						if(file.mode == GameMode.JOURNEY)
						{
							file.totalDistance += _d.d_score - file.journeyScore;
						}
						else if(file.mode == GameMode.ENDURANCE)
						{
							file.totalDistance += _d.d_score;
						}
						
						file.journeyScore = _d.d_score - (_d.d_score % 1000);
						
						GameTimer _t =  GetComponent<GameTimer>();
						file.totalPlayTime += _t.seconds;
					}
					
					if(file.mode == GameMode.JOURNEY)
					{
						if(gameOverJPrefab != null)
						{
							Instantiate(gameOverJPrefab, new Vector3(0, 4, 0), Quaternion.identity);
						}
					}
					else if(file.mode == GameMode.ENDURANCE)
					{
						if(LeaderboardManager.instance.connected)
						{
							if(gameOverEPrefab != null)
							{
								Instantiate(gameOverEPrefab, new Vector3(0, 4, 0), Quaternion.identity);
							}
						}
						else
						{
							if(gameOverJPrefab != null)
							{
								Instantiate(gameOverJPrefab, new Vector3(0, 4, 0), Quaternion.identity);
							}
						}
						
					}
					else
					{
						if(gameOverJPrefab != null)
						{
							Instantiate(gameOverJPrefab, new Vector3(0, 4, 0), Quaternion.identity);
						}
					}
					
				}

				Destroy(gameObject);
				dead = true;
			}
		}
	}
}
