using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGoToScene : MonoBehaviour
{
    [SerializeField]
    private string _goToScene;

    public void LoadScene()
    {
        SceneLoader.LoadScene(_goToScene);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
