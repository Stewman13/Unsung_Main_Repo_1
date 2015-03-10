using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

    //Gathering up all the public values within this script
    private static GUIManager _instance;

    //returns the instance
    public static GUIManager Instance
    {
        get                                                         //get public information in this script
        {
            //If the game object doesnt exist create one
            if (_instance == null)
            {
                GameObject go = new GameObject("GUIManager");       //creates new "named" gameobject
                go.AddComponent<GUIManager>();                      //Adds this component to the gameobject once its created
            }
            return _instance;                                       //If gameobject already exists return all public info within this script
        }
    }

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
