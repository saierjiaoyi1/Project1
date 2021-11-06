using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    public bool isJump;
    public Checkpoint target;

    public Room room;
    public Checkpoint nearestCp;

    public Room connectedRoom
    {
        get
        {
            if (nearestCp != null)
            {
                return nearestCp.room;
            }
            return null;
        }
    }

    public bool isExit()
    {
        if (room != null)
        {
            foreach (var e in room.exits)
            {
                if (e.cp == this)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void AssignNearestCp()
    {
        if (nearestCp == null && target == null && room != null)
        {
            var rooms = LevelSystem.instance.levelBehaviour.level.rooms;
            var dist = float.MaxValue;
            Checkpoint resCp = null;
            foreach (var otherRoom in rooms)
            {
                if (otherRoom == room)
                {
                    continue;
                }

                foreach (var exit in otherRoom.exits)
                {
                    var pDist = Vector3.Distance(exit.cp.transform.position, transform.position);
                    if (pDist < dist)
                    {
                        dist = pDist;
                        resCp = exit.cp;
                    }
                }
            }

            if (resCp == this)
            {
                Debug.LogError("!!!");
                Debug.Log(room.gameObject.name);
                Debug.Log(gameObject.name);
            }
            nearestCp = resCp;
        }
    }
}
