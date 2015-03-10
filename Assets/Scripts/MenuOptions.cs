using UnityEngine;
using System.Collections;

public class MenuOptions : MonoBehaviour {

    public bool isQuit = false;
    public bool isMenu = false;
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
        else if (isMenu)
        {
            Application.LoadLevel(0);
        }
        else
        {
            // audio.PlayOneShot(confirmSound);
            Application.LoadLevel(1);
        }
    }
}
