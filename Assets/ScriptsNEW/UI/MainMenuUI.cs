using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    #region Functions
    public void Quit() { Application.Quit(); }
    public void GoToScene(int index) { SceneManager.LoadScene(index); }
    public void GoToScene(string name) { SceneManager.LoadScene(name); }
    #endregion
}
