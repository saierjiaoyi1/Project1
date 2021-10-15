using UnityEngine;
using System.Collections;

[System.Serializable]
public struct RoomCheckpoint 
{
    public Checkpoint cp;

    public int weightAvoidCenter;
    public int weightAvoidRight;
    public int weightAvoidLeft;
}
