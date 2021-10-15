using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    public RoomCheckpoint[] roomCps;

    public Checkpoint stayCp;

    public Checkpoint GetExitCpAvoidRight()
    {
        List<RoomCheckpoint> candidats = new List<RoomCheckpoint>();
        foreach (var rcp in roomCps)
        {
            if (rcp.weightAvoidRight > 0)
            {
                for (int i = 0; i < rcp.weightAvoidRight; i++)
                {
                    candidats.Add(rcp);
                }
            }
        }

        return candidats[Random.Range(0, candidats.Count - 1)].cp;
    }

    public Checkpoint GetExitCpAvoidLeft()
    {
        List<RoomCheckpoint> candidats = new List<RoomCheckpoint>();
        foreach (var rcp in roomCps)
        {
            if (rcp.weightAvoidLeft > 0)
            {
                for (int i = 0; i < rcp.weightAvoidLeft; i++)
                {
                    candidats.Add(rcp);
                }
            }
        }

        return candidats[Random.Range(0, candidats.Count - 1)].cp;
    }

    public Checkpoint GetExitCpAvoidCenter()
    {
        List<RoomCheckpoint> candidats = new List<RoomCheckpoint>();
        foreach (var rcp in roomCps)
        {
            if (rcp.weightAvoidCenter > 0)
            {
                for (int i = 0; i < rcp.weightAvoidCenter; i++)
                {
                    candidats.Add(rcp);
                }
            }
        }

        return candidats[Random.Range(0, candidats.Count - 1)].cp;
    }
}
