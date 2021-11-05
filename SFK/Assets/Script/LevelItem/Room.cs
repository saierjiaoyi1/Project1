using UnityEngine;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    public RoomCheckpoint[] exits;

    public Vector3 fastBound { get; private set; }

    public Checkpoint stayCp;

    private void Start()
    {
        float left = float.MaxValue;
        float right = float.MinValue;
        foreach (var e in exits)
        {
            var pos = e.cp.transform.position;
            if (pos.x > right)
                right = pos.x;

            if (pos.x < left)
                left = pos.x;
        }

        fastBound = new Vector3(left, stayCp.transform.position.y, right);

        foreach (var e in exits)
        {
            SetCpRoom(e.cp);
        }
        SetCpRoom(stayCp);
    }

    void SetCpRoom(Checkpoint cp)
    {
        if (cp == null || cp.room == this)
            return;

        cp.room = this;
        SetCpRoom(cp.target);
    }

    public Checkpoint GetExitCpAvoidRight()
    {
        List<RoomCheckpoint> candidats = new List<RoomCheckpoint>();
        foreach (var rcp in exits)
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
        foreach (var rcp in exits)
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
        foreach (var rcp in exits)
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
