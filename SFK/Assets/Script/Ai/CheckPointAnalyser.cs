﻿using UnityEngine;
using System.Collections.Generic;

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

    public CatAction GetAction(string activityId, bool enter, CatAction lastAction)
    {
        if (lastAction == null)
            enter = true;

        CatAction newAction = new CatAction();
        newAction.activityId = activityId;
        var myPos = _cat.transform.position;

        switch (activityId)
        {
            case "run":
                newAction.dest = new CatDestination();
                newAction.dest.isRun = true;

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
                    newAction.dest.pos = exit.transform.position;
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
                    newAction.dest.pos = exit.transform.position;
                }

                break;
            case "toilet":
                if (!enter)
                { return lastAction; }
                //action.dest.arriveDistance = 0.6f;
                newAction.isGoToilet = true;
                break;

            case "eat":
                if (!enter)
                { return lastAction; }
                //action.dest.arriveDistance = 0.6f;
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
                var room_change = currentRoom;
                var rooms = LevelSystem.instance.levelBehaviour.level.rooms;
                var candidatRooms = new List<Room>();
                foreach (var iRoom in rooms)
                {
                    if (iRoom != room_change)
                    {
                        var distRoom = Vector3.Distance(iRoom.stayCp.transform.position, room_change.stayCp.transform.position);
                        if (distRoom < 12)
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
                var room_walk = currentRoom;
                var room_walk_left = room_walk.fastBound.x;
                var room_walk_right = room_walk.fastBound.z;
                //Debug.Log(currentRoom.gameObject.name);
                //Debug.Log(currentRoom.fastBound);
                var room_walk_randomPos =
                    new Vector3(Random.Range(room_walk_left, room_walk_right)
                    , room_walk.fastBound.y
                    , Random.Range(-0.5f, 2.1f));
                newAction.dest.pos = room_walk_randomPos;
                break;
        }

        return newAction;
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
    public float arriveDistance=0.25f;//assign ok
}
