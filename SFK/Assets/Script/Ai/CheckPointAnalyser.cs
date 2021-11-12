using UnityEngine;
using System.Collections.Generic;

public class CheckPointAnalyser : MonoBehaviour
{
    private Cat _cat;

    private Checkpoint _crtExit;
    private void Awake()
    {
        _cat = GetComponent<Cat>();
    }

    public Room currentRoom
    {
        get
        {
            var myPos = _cat.transform.position;
            var rooms = LevelSystem.instance.levelBehaviour.level.rooms;
            int bestMatchScore = 0;
            Room matchedRoom = null;

            foreach (var room in rooms)
            {
                //Debug.Log(room.gameObject.name);
                var above = myPos.y - room.fastBound.y;
                var leftDelta = myPos.x - room.fastBound.x;
                var rightDelta = myPos.x - room.fastBound.z;
                //roomHeight = 3;
                var matchScore = 0;
                if (above > -0.2f && above < 1f)
                {
                    //Debug.Log("height1");
                    matchScore += 2;
                    if (above > -0.5f && above < 3.3f)
                    {
                        //Debug.Log("height11");
                        matchScore += 1;
                    }
                }
                if (leftDelta > -0.5f && rightDelta < 0.5f)
                {
                    //Debug.Log("hor1");
                    matchScore += 1;
                    if (leftDelta > 0.3f && rightDelta < -0.3f)
                    {
                        //Debug.Log("hor11");
                        matchScore += 1;
                    }
                }
                if (matchScore > bestMatchScore)
                {
                    matchedRoom = room;
                    bestMatchScore = matchScore;
                }
            }
            //Debug.Log(matchedRoom.gameObject);
            //Debug.Log(bestMatchScore);
            return matchedRoom;
        }
    }

    Checkpoint GetOverlapExit()
    {
        Checkpoint overlapExit = null;
        var currentCps = _cat.ccb.currentCps;
        if (currentCps.Count == 0)
        {
            return null;
        }
        if (currentCps.Count == 1)
        {
            overlapExit = currentCps[0];
            if (!overlapExit.isExit())
            {
                return null;
            }
            return overlapExit;
        }

        foreach (var currentCp in currentCps)
        {
            if (currentCp == _crtExit)
            {
                overlapExit = currentCp;
                if (!overlapExit.isExit())
                {
                    return null;
                }
                return overlapExit;
            }
        }

        return null;
    }

    public void Flush()
    {
        _crtExit = null;
    }

    public CatAction GetAction(string activityId, bool enter, CatAction lastAction)
    {
        if (lastAction == null)
            enter = true;
        Debug.LogWarning("--------------------- GetAction " + activityId + " " + Time.time);

        CatAction newAction = new CatAction();
        newAction.activityId = activityId;
        var myPos = _cat.transform.position;
        var crtRoom = currentRoom;
        newAction.lastRoom = crtRoom;
        switch (activityId)
        {
            case "run":
                newAction.dest = new CatDestination();
                newAction.dest.isRun = true;
                newAction.dest.useNavMeshAgent = true;

                var overlapExit = GetOverlapExit();
                Room conRoom = null;
                Checkpoint overlapExitTarget = null;
                if (overlapExit != null)
                {
                    Debug.Log("has overlapExit " + overlapExit.gameObject.name);
                    conRoom = overlapExit.connectedRoom;
                    overlapExitTarget = overlapExit.target;
                }
                else
                {
                    Debug.Log("no overlapExit ");
                }

                bool toFindAnExit = (overlapExit == null ||
                    (conRoom == null && overlapExitTarget == null));

                if (toFindAnExit)
                {
                    Debug.Log("toFindAnExit");
                    var humanPos = LevelSystem.instance.levelBehaviour.human.transform.position;
                    var deltaPos = humanPos - myPos;
                    var deltaX = deltaPos.x;
                    var deltaZY = new Vector2(deltaPos.y, deltaPos.z).magnitude;

                    bool avoidCenter = (deltaZY > Mathf.Abs(deltaX));
                    bool avoidRight = (deltaX > 0);
                    var exit = crtRoom.GetExitCpAvoidCenter();
                    if (!avoidCenter || exit == null)
                    {
                        exit = (avoidRight ? crtRoom.GetExitCpAvoidRight() : crtRoom.GetExitCpAvoidLeft());
                    }
                    _crtExit = exit;
                    newAction.dest.pos = exit.transform.position;
                }
                else
                {
                    Debug.Log("go to another room");
                    if (overlapExitTarget != null)
                    {
                        Debug.Log("has overlapExitTarget");
                        var crtCpTargetPos = overlapExitTarget.transform.position;
                        if (overlapExit.isJump)
                        {
                            Debug.Log("to jump");
                            newAction.dest.useNavMeshAgent = false;
                            newAction.dest.pos = crtCpTargetPos;
                            newAction.isJump = true;
                        }
                        else
                        {
                            Debug.Log("to follow");
                            newAction.dest.useNavMeshAgent = false;
                            newAction.dest.pos = crtCpTargetPos;
                            newAction.isFollow = true;
                        }
                    }
                    else
                    {
                        Debug.Log("crtRoom " + crtRoom.gameObject.name);
                        Debug.Log("has conRoom " + conRoom.gameObject.name);
                        var averageX = 0.5f * (conRoom.fastBound.x + conRoom.fastBound.z);
                        var deltaX = transform.position.x - averageX;
                        var deltaY = Mathf.Abs(transform.position.y - conRoom.fastBound.y);
                        Checkpoint exitConRoom = null;
                        if (deltaY > 1.4f || Mathf.Abs(deltaX) < 0.6f)
                        {
                            exitConRoom = conRoom.GetExitCpAvoidCenter();
                        }
                        if (exitConRoom == null && deltaX > 0)
                        {
                            exitConRoom = conRoom.GetExitCpAvoidRight();
                        }
                        if (exitConRoom == null)
                        {
                            exitConRoom = conRoom.GetExitCpAvoidLeft();
                        }
                        newAction.dest.pos = exitConRoom.transform.position;
                    }
                }
                break;

            case "toilet":
                if (!enter)
                { return lastAction; }
                newAction.dest = new CatDestination();
                newAction.dest.useNavMeshAgent = true;
                newAction.dest.pos = LevelSystem.instance.levelBehaviour.toilet.transform.position;
                newAction.isGoToilet = true;
                break;

            case "eat":
                if (!enter)
                { return lastAction; }
                newAction.dest = new CatDestination();
                newAction.dest.useNavMeshAgent = true;
                newAction.dest.pos = LevelSystem.instance.levelBehaviour.eatPlace.transform.position;
                newAction.isGoEat = true;
                break;

            case "item":
                //action.dest.arriveDistance = 0.15f;
                // action.isDirectlySound = true; sound after arrive
                break;

            case "change":
                if (!enter)
                { return lastAction; }

                newAction.dest = new CatDestination();
                newAction.dest.useNavMeshAgent = true;
                var rooms = LevelSystem.instance.levelBehaviour.level.rooms;
                var candidatRooms = new List<Room>();
                foreach (var iRoom in rooms)
                {
                    if (iRoom != newAction.lastRoom)
                    {
                        var distRoom = Vector3.Distance(iRoom.stayCp.transform.position, newAction.lastRoom.stayCp.transform.position);
                        if (distRoom < 15)
                        {
                            candidatRooms.Add(iRoom);
                        }
                    }
                }
                newAction.dest.pos = candidatRooms[Random.Range(0, candidatRooms.Count)].stayCp.transform.position;
                break;

            case "stay":
                if (enter)
                {
                    newAction.isDirectlySound = true;
                }
                break;

            case "walk":
                if (!enter)
                { return lastAction; }

                newAction.dest = new CatDestination();
                newAction.dest.useNavMeshAgent = true;
                var room_walk_left = newAction.lastRoom.fastBound.x;
                var room_walk_right = newAction.lastRoom.fastBound.z;
                //Debug.Log(currentRoom.gameObject.name);
                //Debug.Log(currentRoom.fastBound);
                var room_walk_randomPos =
                    new Vector3(Random.Range(room_walk_left, room_walk_right)
                    , newAction.lastRoom.fastBound.y
                    , Random.Range(-0.5f, 2.1f));
                newAction.dest.pos = room_walk_randomPos;
                break;
        }

        return newAction;
    }

    public CatAction CheckNextFollow(CatAction lastAction)
    {
        var currentCps = _cat.ccb.currentCps;
        foreach (var currentCp in currentCps)
        {
            if (currentCp.room == lastAction.lastRoom && currentCp.target != null)
            {
                lastAction.dest.pos = currentCp.target.transform.position;
                lastAction.isJump = currentCp.isJump;
                lastAction.isFollow = !lastAction.isJump;
                return lastAction;
            }
        }

        return null;
    }
}

public class CatAction
{
    public CatDestination dest = null;
    public Room lastRoom;
    public string activityId;//assign ok

    public bool isGoToilet = false;//assign ok
    public bool isGoEat = false;//assign ok
    public bool isJump = false;//assign ok
    public bool isDirectlySound = false;//assign ok
    public bool isFall = false;//assign ok
    public bool isFollow = false;//assign ok
}

public class CatDestination
{
    public bool useNavMeshAgent;
    public Vector3 pos;
    public bool isRun;//assign ok
    public float arriveDistance = 0.15f;//assign ok
}
