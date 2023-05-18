using System;

[Serializable]
public class HighscoreElement
{
    public string playerName;
    public int points;

    public HighscoreElement(string time, int points)
    {
        playerName = time;
        this.points = points;
    }
}
