using UnityEngine;

public class GameData
{
    public int PreviousScore {
        get { return PlayerPrefs.GetInt("PreviousScore", 0); }
        set { PlayerPrefs.SetInt("PreviousScore", value); }
    }

    public int Score { get; set; }

    public int Attempts { get; set; }
}
