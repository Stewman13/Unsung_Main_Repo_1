using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {


    private Game_Controler _isPaused;
    private int _buttonWidth = 200;
    private int _buttonHeight = 50;
    private int _groupWidth = 400;
    private int _groupHeight = 200;

	// Use this for initialization
	void Start () 
    {
	    _isPaused = GameObject.Find("Main Camera").GetComponent<Game_Controler>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (_isPaused.paused)
        {
            GUI.BeginGroup(new Rect(((Screen.width / 2) - (_groupWidth / 2)), (((Screen.height / 2) - (_groupHeight / 2))) - 100, _groupWidth, _groupHeight));
            if (GUI.Button(new Rect(50, 0, _buttonWidth, _buttonHeight), "Main Menu"))
            {
                Application.LoadLevel(0);
            }
            if (GUI.Button(new Rect(50, 70, _buttonWidth, _buttonHeight), "Restart Game"))
            {
                Time.timeScale = 1.0f;
                Application.LoadLevel(Application.loadedLevel);
            }
            if (GUI.Button(new Rect(50, 140, _buttonWidth, _buttonHeight), "Quit Game"))
            {
                Application.Quit();
            }
            GUI.EndGroup();
        }
    }
}
