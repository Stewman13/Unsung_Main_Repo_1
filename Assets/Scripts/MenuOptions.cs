using UnityEngine;
using System.Collections;

public class MenuOptions : MonoBehaviour {

    public bool isQuit = false;
	public bool isInstruct = false;
    public bool isMenu = false;
	public bool isPlay = false;
    // public AudioClip buttonEnter;
    //public AudioClip confirmSound;

    private GUIText _text;

    void Start()
    {
        _text = GetComponent<GUIText>();
    }

    void OnMouseEnter()
    {
        _text.color = Color.green;
        //audio.PlayOneShot(buttonEnter, 0.5f);
    }

    void OnMouseExit()
    {
        _text.color = Color.white;
    }

    void OnMouseDown()
    {
        if (isQuit)
        {
            //audio.PlayOneShot(confirmSound, 0.5f);
            Application.Quit();
        }
		if (isInstruct)
		{
			//audio.PlayOneShot(confirmSound, 0.5f);
			Application.LoadLevel(7);
		}
		if (isPlay)
		{
			//audio.PlayOneShot(confirmSound, 0.5f);
			Application.LoadLevel(1);
		}
        else if (isMenu)
        {
            Application.LoadLevel(0);
        }
    }
}
