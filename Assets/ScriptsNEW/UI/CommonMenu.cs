using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Contains basic menu functions.
/// </summary>
public class CommonMenu : MonoBehaviour
{
    #region Functions
    public virtual void Quit() { Application.Quit(); }
    public virtual void GoToScene(int index) { SceneManager.LoadScene(index); }
    public virtual void GoToScene(string name) { SceneManager.LoadScene(name); }
    #endregion
}
