using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGC : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Singleton instance of the GameController.
    /// </summary>
    public static NewGC gc;

    private bool _isPaused = false;
    public bool isPaused
    {
        get { return _isPaused; }
        set
        {
            _isPaused = value;
            if (value)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
        }
    }

    private bool _gameOver = false;
    public bool gameOver
    {
        get { return _gameOver; }
        private set
        {
            if (value)
                gameOverMenu.SetActive(true);
            _gameOver = value;

            gameOverMenu.SetActive(true);
        }
    }

    [SerializeField] private GameObject _pauseMenu;
    /// <summary>
    /// The GameObject all parts of the pause menu are grouped under.
    /// </summary>
    public GameObject pauseMenu { get { return _pauseMenu; } }

    [SerializeField] private GameObject _gameOverMenu;
    /// <summary>
    /// The GameObject all parts of the game over menu are grouped under.
    /// </summary>
    public GameObject gameOverMenu { get { return _gameOverMenu; } }    
    #endregion

    #region MonoBehaviors
    private void Awake()
    {
        if (gc == null)
            gc = this;
        else
            Destroy(this);
    }
    #endregion
}