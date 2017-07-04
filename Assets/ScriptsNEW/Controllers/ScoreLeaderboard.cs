using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreLeaderboard
{
    #region Variables
    private static List<HighScore> _leaderboard = defaultLeaderboard;
    public static List<HighScore> leaderboard
    {
        get { return _leaderboard; }
        private set { _leaderboard = value; }
    }

    /// <summary>
    /// The leaderboard that is first loaded into the game.
    /// </summary>
    public static readonly List<HighScore> defaultLeaderboard = new List<HighScore>(5)
    {
        new HighScore(),
        new HighScore(),
        new HighScore(),
        new HighScore(),
        new HighScore(0, 'A', 'B', 'C')
    };
    #endregion

    #region Functions
    /// <summary>
    /// Checks at what index the high score would be inserted, if at all.
    /// If the number returned is equal to the leaderboard's length, there is no space for the score.
    /// </summary>
    /// <param name="score">The score being checked.</param>
    public static int CheckScoreIndex(int score)
    {
        byte i;
        for (i = 0; i < leaderboard.Count; i++)
            if (score > leaderboard[i].score)
                break;
        return i;
    }

    /// <summary>
    /// Inserts a high score into the leaderboard and removes the lowest score.
    /// </summary>
    /// <param name="highScore"></param>
    public static void InsertHighScore(HighScore highScore)
    {
        int insertAt = CheckScoreIndex(highScore.score);
        leaderboard.Insert(insertAt, highScore);
        leaderboard.RemoveAt(leaderboard.Count - 1);
    }
    #endregion
}