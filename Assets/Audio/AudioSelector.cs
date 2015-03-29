using UnityEngine;
using System.Collections;

public class AudioSelector : MonoBehaviour {

	public  AudioClip SoundClipTitleScreen, SoundClipMenu,  SoundClipLevel001, SoundClipLevel002_L1, SoundClipLevel002_L2 ;
	private AudioSource SoundSourceTitle, SoundSourceMenu, SoundSourceLevel001, SoundSourceLevel002_L1, SoundSourceLevel002_L2;

	public Game_Controler _gameCon;

	public bool Level01Audio = false;
	public bool Level02Audio = false;
	public bool Level03Audio = false;



	void Awake(){
		_gameCon = GameObject.Find("Main Camera").GetComponent<Game_Controler>();
	}

	void OnLevelWasLoaded(int level)
	{
		print ("level loaded");
		if (level == 1){
			print ("Level01 loaded");
			Level01Audio = true;
			Level02Audio = false;
			Level03Audio = false;
		}
		if (level == 4){
			print ("Level02 loaded");
			Level01Audio = false;
			Level02Audio = true;
			Level03Audio = false;
		}
		if (level == 5){
			print ("Level03 loaded");
			Level01Audio = false;
			Level02Audio = false;
			Level03Audio = true;
		}
	}

	void Start()
	{
		//Level Title Screen
		if (Application.loadedLevelName == "Title Screen")
		{
			//DontDestroyOnLoad(gameObject);
			SoundSourceTitle = gameObject.AddComponent<AudioSource>();
			//SoundSource.playOnAwake = false;
			//SoundSource.rolloffMode = AudioRolloffMode.Logarithmic;
			SoundSourceTitle.volume = 0.8F;
			SoundSourceTitle.panLevel = 0;
			SoundSourceTitle.loop = true;
			//print ("level 1 audio");
			TitleScreenAudio();
		}

		//Level Menu Audio
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
			MenuAudio();
		}
		
		//Level 001 Audio, only layer 1
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
			Level001Audio();
			
		}
		
		//Level 002 Audio,  layer 1 and 2
		if (Application.loadedLevelName == "Location_2")
		{
			//DontDestroyOnLoad(gameObject);
			SoundSourceLevel002_L1 = gameObject.AddComponent<AudioSource>();
			SoundSourceLevel002_L2 = gameObject.AddComponent<AudioSource>();
			//SoundSource.playOnAwake = false;
			//SoundSource.rolloffMode = AudioRolloffMode.Logarithmic;
			SoundSourceLevel002_L1.volume = 0.7F;
			SoundSourceLevel002_L1.panLevel = 0;
			SoundSourceLevel002_L1.loop = true;
			SoundSourceLevel002_L2.volume = 0.7F;
			SoundSourceLevel002_L2.panLevel = 0;
			SoundSourceLevel002_L2.loop = true;
			//print ("level 3 audio");
			Level002Audio();
			
		}
	}

	void Update(){
		if (Input.GetKeyUp(KeyCode.Backspace))
			Application.LoadLevel("Location_2");
		if (_gameCon.paused == true){
			if (Level01Audio == true)
				SoundSourceLevel001.volume = 0.1F;
			if (Application.loadedLevelName == "Location_2"){
				SoundSourceLevel002_L1.volume = 0.1F;
				SoundSourceLevel002_L2.volume = 0.1F;
			}
		}
		if (_gameCon.paused == false){
			if (Level01Audio == true)
				SoundSourceLevel001.volume = 0.5F;
			if (Level02Audio == true){
				SoundSourceLevel002_L1.volume = 0.4F;
				SoundSourceLevel002_L2.volume = 0.4F;
			}
		}


	}
	
	//Plays the audio once picked in Awake
	void TitleScreenAudio()
	{
		SoundSourceTitle.clip = SoundClipTitleScreen;
		SoundSourceTitle.Play();
	}
	void MenuAudio()
	{
		SoundSourceMenu.clip = SoundClipMenu;
		SoundSourceMenu.Play();
	}
	
	void Level001Audio()
	{
		SoundSourceLevel001.clip = SoundClipLevel001;
		SoundSourceLevel001.Play();
	}
	
	void Level002Audio()
	{
		SoundSourceLevel002_L1.clip = SoundClipLevel002_L1;
		SoundSourceLevel002_L2.clip = SoundClipLevel002_L2;
		SoundSourceLevel002_L1.Play();
		SoundSourceLevel002_L2.Play();
	}
	
	
	
}

