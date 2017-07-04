using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore
{
    #region Variables
    private int _score;
    public int score
    {
        get { return _score; }
        private set { _score = value; }
    }

    private char[] _initials;
    public char[] initials
    {
        get { return _initials; }
        private set { _initials = value; }
    }
    #endregion

    #region Constructors
    /// <summary>
    /// Default Constructor:
    /// Creates a high score of 0 with a name of ---.
    /// </summary>
    public HighScore()
    {
        score = 0;
        initials = new char[3] { '-', '-', '-' };
    }

    /// <summary>
    /// Char Constructor:
    /// Any null characters are replaced with spaces.
    /// </summary>
    /// <param name="score">The score being recorded.</param>
    /// <param name="initial1">The user's first initial.</param>
    /// <param name="initial2">The user's second initial.</param>
    /// <param name="initial3">The user's third initial.</param>
    public HighScore(int score, char initial1, char initial2, char initial3)
    {
        this.score = score;
        char[] i = { initial1, initial2, initial3 };
        for (uint x = 0; x < 3; x++)
        {
            if (i[x] == null)
                initials[x] = ' ';
            else
                initials[x] = i[x];
        }

    }
    #endregion
}