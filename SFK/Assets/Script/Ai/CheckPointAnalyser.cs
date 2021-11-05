using UnityEngine;

public class CheckPointAnalyser : MonoBehaviour
{
    private Cat _cat;

    private void Awake()
    {
        _cat = GetComponent<Cat>();
    }

    public Checkpoint currentCheckpoint
    {
        get
        {
            return _cat.ccb.currentCheckpoint;
        }
    }

    public Room currentRoom
    {
        get
        {
            var pos = _cat.transform.position;
            var rooms = LevelSystem.instance.levelBehaviour.level.rooms;
            int bestMatchScore = 0;
            Room matchedRoom = null;
            foreach (var room in rooms)
            {
                var above = pos.y - room.fastBound.y;
                var leftDelta = pos.x - room.fastBound.x;
                var rightDelta = pos.x - room.fastBound.x;
                //var roomHeight = 3;
                var heightDelta = Mathf.Abs(1.4f - above);

                var matchScore = 0;
                if (heightDelta < 1.6f)
                    matchScore += 3;

                if (leftDelta > -1f)
                {
                    matchScore += 1;
                    if (leftDelta > 0.5f)
                        matchScore += 1;
                }
                if (rightDelta < 1f)
                {
                    matchScore += 1;
                    if (rightDelta < -0.5f)
                        matchScore += 1;
                }
                if (matchScore > bestMatchScore)
                {
                    matchedRoom = room;
                    bestMatchScore = matchScore;
                }
            }
            return matchedRoom;
        }
    }

    public CatAction GetAction(string activityId, bool enter)
    {
        CatAction action = new CatAction();
        action.activityId = activityId;
        switch (activityId)
        {
            case "run":
                action.dest.isRun = true;
                action.dest.arriveDistance = 0.15f;

                var room = currentRoom;
                var pos = _cat.transform.position;
                if (currentCheckpoint == null)
                {
                    var humanPos = LevelSystem.instance.levelBehaviour.human.transform.position;
                    var deltaPos = humanPos - pos;
                    var deltaX = deltaPos.x;
                    var deltaZY = new Vector2(deltaPos.y, deltaPos.z).magnitude;

                    bool avoidCenter = (deltaZY > Mathf.Abs(deltaX));
                    bool avoidRight = (deltaX > 0);
                    var exit = room.GetExitCpAvoidCenter();
                    if (!avoidCenter || exit == null)
                    {
                        exit = (avoidRight ? room.GetExitCpAvoidRight() : room.GetExitCpAvoidLeft());
                    }
                    action.dest.pos = exit.transform.position;
                }
                else
                {
                    var avoidPos = currentCheckpoint.transform.position;
                    var deltaPos = avoidPos - pos;
                    var deltaX = deltaPos.x;
                    var deltaZY = new Vector2(deltaPos.y, deltaPos.z).magnitude;

                    bool avoidCenter = (deltaZY > Mathf.Abs(deltaX));
                    bool avoidRight = (deltaX > 0);
                    var exit = room.GetExitCpAvoidCenter();
                    if (!avoidCenter || exit == null)
                    {
                        exit = (avoidRight ? room.GetExitCpAvoidRight() : room.GetExitCpAvoidLeft());
                    }
                    action.dest.pos = exit.transform.position;
                }
              
                break;
            case "toilet":
                action.dest.arriveDistance = 0.6f;
                action.isToilet = true;
                break;

            case "eat":
                action.dest.arriveDistance = 0.6f;
                action.isEat = true;
                break;

            case "item":
                action.dest.arriveDistance = 0.15f;
                if (enter)
                {
                    action.isSound = true;
                }
                break;

            case "change":
                action.dest.arriveDistance = 0f;
                break;

            case "stay":
                action.dest.arriveDistance = 0f;
                if (enter)
                {
                    action.isSound = true;
                }
                action.isStay = true;
                break;

            case "walk":
                action.dest.arriveDistance = 0f;
                break;
        }

        return action;
    }
}

public class CatAction
{
    public CatDestination dest;
    public string activityId;//assign ok

    public bool isToilet = false;//assign ok
    public bool isEat = false;//assign ok
    public bool isSound = false;//assign ok
    public bool isStay = false;//assign ok
}

public struct CatDestination
{
    public bool useNavMeshAgent;
    public Vector3 pos;
    public bool isRun;//assign ok
    public float arriveDistance;//assign ok
}
