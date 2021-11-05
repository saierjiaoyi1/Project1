using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    public bool isFall;
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

    private void Start()
    {
        if (!isFall && nearestCp == null && target == null)
        {
            var rooms = LevelSystem.instance.levelBehaviour.level.rooms;
            var dist = float.MaxValue;
            Checkpoint resCp = null;
            foreach (var iRoom in rooms)
            {
                if (iRoom == room)
                {
                    continue;
                }

                foreach (var exit in iRoom.exits)
                {
                    var pDist = Vector3.Distance(exit.cp.transform.position, transform.position);
                    if (pDist < dist)
                    {
                        dist = pDist;
                        resCp = exit.cp;
                    }
                }
            }

            nearestCp = resCp;
        }
    }
}
