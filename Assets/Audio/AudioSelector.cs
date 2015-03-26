using UnityEngine;
using System.Collections;

public class AudioSelector : MonoBehaviour {

	public  AudioClip SoundClipMenu,  SoundClipLevel001,  SoundClipLevel002 ;
	private AudioSource SoundSourceMenu, SoundSourceLevel001, SoundSourceLevel002;

	public Game_Controler _gameCon;

	void Awake(){
		_gameCon = GameObject.Find("Main Camera").GetComponent<Game_Controler>();
	}

	void Start()
	{
		//Level 001 Audio
		if (Application.loadedLevelName == "Main Menu")
		{
			//DontDestroyOnLoad(gameObject);
			SoundSourceMenu = gameObject.AddComponent<AudioSource>();
			//SoundSource.playOnAwake = false;
			//SoundSource.rolloffMode = AudioRolloffMode.Logarithmic;
			SoundSourceMenu.volume = 0.8F;
			SoundSourceMenu.panLevel = 0;
			SoundSourceMenu.loop = true;
			//print ("level 1 audio");
			Level001Audio();
		}
		
		//Level 002 Audio
		if (Application.loadedLevelName == "Location_1")
		{
			//DontDestroyOnLoad(gameObject);
			SoundSourceLevel001 = gameObject.AddComponent<AudioSource>();
			//SoundSource.playOnAwake = false;
			//SoundSource.rolloffMode = AudioRolloffMode.Logarithmic;
			SoundSourceLevel001.volume = 0.5F;
			SoundSourceLevel001.panLevel = 0;
			SoundSourceLevel001.loop = true;
		//	print ("level 2 audio");
			Level002Audio();
			
		}
		
		//Level 003 Audio
		if (Application.loadedLevelName == "Location_2")
		{
			//DontDestroyOnLoad(gameObject);
			SoundSourceLevel002 = gameObject.AddComponent<AudioSource>();
			//SoundSource.playOnAwake = false;
			//SoundSource.rolloffMode = AudioRolloffMode.Logarithmic;
			SoundSourceLevel002.volume = 0.7F;
			SoundSourceLevel002.panLevel = 0;
			SoundSourceLevel002.loop = true;
			//print ("level 3 audio");
			Level003Audio();
			
		}
	}
	
	//Plays the audio once picked in Awake
	void Level001Audio()
	{
		SoundSourceMenu.clip = SoundClipMenu;
		SoundSourceMenu.Play();
	}
	
	void Level002Audio()
	{
		SoundSourceLevel001.clip = SoundClipLevel001;
		SoundSourceLevel001.Play();
	}
	
	void Level003Audio()
	{
		SoundSourceLevel002.clip = SoundClipLevel002;
		SoundSourceLevel002.Play();
	}
	
	
	
}

