using UnityEngine;
using TMPro;

public class MusicSet : MonoBehaviour
{
	public AudioClip introClip;
	public AudioClip loopClip;
	public bool useFade = true;
	public bool playOnAwake = true;
	public string title;
	
	[Header("Track Name Thing")]
	public TextMeshPro title_t;
	public Animator title_a;

	void Start()
	{
		if (playOnAwake)
		{
			SequencedMusicPlayer smp = FindObjectOfType<SequencedMusicPlayer>();
			if (smp != null)
			{
				smp.SetTracks(this);
			}
		}
	}
	
	public void Play()
	{
		SequencedMusicPlayer smp = FindObjectOfType<SequencedMusicPlayer>();
		if (smp != null)
		{
			smp.SetTracks(this);
		}
		
		if(title_t != null && title_a != null)
		{
			title_t.SetText(title);
			title_a.Play("Swoop", 0, 0f);
		}
	}
	
	public void SetTitle(string newTitle)
	{
		title = newTitle;
	}
}