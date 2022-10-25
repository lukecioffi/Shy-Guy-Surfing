using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationBox : MonoBehaviour
{
	[SerializeField] Animator anim;
	[SerializeField] AudioSource audio;
	[SerializeField] TextMeshPro text;
	
	public void SendNotif(string message)
	{
		text.SetText(message);
		anim.Play("Notification", 0, 0f);
		audio.Play();
	}
}
