using UnityEngine;
using System.Collections;

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

    public struct currentCondition
    {
        public Room currentRoom
        {
            get
            {
                return null;
                //when reach a checkpoint, check the current room
            }
        }
    }

    public CatAction GetAction(string activityId, bool enter)
    {
        //Transform leave, Transform go
        CatAction action = new CatAction();
        action.activityId = activityId;
        switch (activityId)
        {
            case "run":
                action.dest.isRun = true;
                action.dest.arriveDistance = 0.15f;
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
