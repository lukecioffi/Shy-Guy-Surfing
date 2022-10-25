using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeController : MonoBehaviour
{
    public DataCarrier file;
	public bool inMenu;
	public bool falling;
	public bool locked;
	
	private Animator anim;
	
	public AnimatorOverrideController[] costumes;
	
	public SpriteRenderer rend;
	
	void Awake()
	{
		
	}
	
	// Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();
		rend = GetComponent<SpriteRenderer>();
		
		if(file.costumeID > 0)
		{
			anim.runtimeAnimatorController = costumes[file.costumeID];
		}
		
		if(falling)
		{
			anim.SetBool("Falling", true);
		}
    }

    // Update is called once per frame
    void Update()
    {
		if(file.costumeID == 0 || file.costumeLocks[file.costumeID])
		{
			rend.color = new Color(1, 1, 1, 1);
			locked = false;
		}
		else
		{
			rend.color = new Color(0, 0, 0, 1);
			locked = true;
		}
    }
	
	public void SetCostumeID(int num)
	{
		file.costumeID = num;
		anim.runtimeAnimatorController = costumes[file.costumeID];
	}
}
