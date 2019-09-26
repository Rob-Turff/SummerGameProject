using Microsoft.Xna.Framework;
using SummerGameProject.Src.Client.Components.Player;
using SummerGameProject.Src.Components.Player;
using System;

public class PlayerStats
{
    public Guid playerID;
    public string playerName;
    public Vector2 velocity = new Vector2(0, 0);
    public Vector2 position = new Vector2(0, 0);
    public PlayerMove currentMove = new PlayerMove(0, 0);
    public PlayerMovementHandler movementHandler;
    public bool isHost = false;
    public Player player;

    public PlayerStats(string playerName, Guid playerID, bool isHost)
    {
        this.playerName = playerName;
        this.playerID = playerID;
        this.isHost = isHost;
    }

    public PlayerStats(string playerName, Guid playerID, bool isHost, Vector2 position)
    {
        this.playerName = playerName;
        this.playerID = playerID;
        this.isHost = isHost;
        this.position = position;
    }
}
