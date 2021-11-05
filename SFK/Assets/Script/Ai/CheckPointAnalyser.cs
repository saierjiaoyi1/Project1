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
        var myPos = _cat.transform.position;

        switch (activityId)
        {
            case "run":
                action.dest = new CatDestination();
                action.dest.isRun = true;
                action.dest.arriveDistance = 0.15f;

                var room = currentRoom;
                if (currentCheckpoint == null)
                {
                    var humanPos = LevelSystem.instance.levelBehaviour.human.transform.position;
                    var deltaPos = humanPos - myPos;
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
                    var deltaPos = avoidPos - myPos;
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
                //action.dest.arriveDistance = 0.6f;
                action.isGoToilet = true;
                break;

            case "eat":
                //action.dest.arriveDistance = 0.6f;
                action.isGoEat = true;
                break;

            case "item":
                //action.dest.arriveDistance = 0.15f;
                // action.isDirectlySound = true; sound after arrive
                break;

            case "change":
                break;

            case "stay":
                if (enter)
                {
                    action.isDirectlySound = true;
                }
                break;

            case "walk":
                action.dest = new CatDestination();
                action.dest.useNavMeshAgent = true;
                action.dest.arriveDistance = 0f;
                var room_walk = currentRoom;
                var room_walk_left = room_walk.fastBound.x;
                var room_walk_right = room_walk.fastBound.z;
                var room_walk_randomPos =
                    new Vector3(Random.Range(room_walk_left, room_walk_right)
                    , room_walk.fastBound.y
                    , Random.Range(-0.5f, 2.1f));
                action.dest.pos = room_walk_randomPos;
                break;
        }

        return action;
    }
}

public class CatAction
{
    public CatDestination dest = null;
    public string activityId;//assign ok

    public bool isGoToilet = false;//assign ok
    public bool isGoEat = false;//assign ok
    public bool isDirectlySound = false;//assign ok
}

public class CatDestination
{
    public bool useNavMeshAgent;
    public Vector3 pos;
    public bool isRun;//assign ok
    public float arriveDistance;//assign ok
}
