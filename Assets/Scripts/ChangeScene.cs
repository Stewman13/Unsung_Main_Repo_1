using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

    public bool isQuit = false;

    public void QuitClick()
    {
        Application.Quit();
    }

    public void SceneChanger(int sceneToChangeTo)
    {
        Application.LoadLevel(sceneToChangeTo);
    }
}
