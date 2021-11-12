using UnityEngine;
using System.Collections.Generic;

public class CatCollisionBehaviour : MonoBehaviour
{
    public List<Checkpoint> currentCps;

    private Cat _cat;

    private void Awake()
    {
        _cat = GetComponentInParent<Cat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckToiletAndEatPlace(other.gameObject);

        var cp = other.gameObject.GetComponent<Checkpoint>();
        if (cp == null)
            return;

        if (currentCps.Contains(cp))
            return;

        currentCps.Add(cp);
    }

    private void OnTriggerExit(Collider other)
    {
        var cp = other.gameObject.GetComponent<Checkpoint>();
        if (cp == null)
            return;

        if (!currentCps.Contains(cp))
            return;

        currentCps.Remove(cp);
    }

    private void CheckToiletAndEatPlace(GameObject other)
    {
        //Debug.Log(other);
        //Debug.Log(WantsToEat() + " e " + IsEatPlace(other));
        if (WantsToEat() && IsEatPlace(other))
        {
            //Debug.Log("Eat");
            Eat();
            return;
        }
        //Debug.Log(WantsToToilet() + " t " + IsToilet(other));
        if (WantsToToilet() && IsToilet(other))
        {
            //Debug.Log("Toilet");
            Toilet();
            return;
        }
    }

    bool WantsToEat()
    {
        if (_cat.cab.currentActivity != null && _cat.cab.currentActivity.id == "eat" && _cat.cab.aiConditionFetcher.IsHungry)
        {
            return true;
        }

        return false;
    }

    bool IsEatPlace(GameObject other)
    {
        if (other.GetComponentInParent<EatPlace>() != null)
        {
            return true;
        }

        return false;
    }

    void Eat()
    {
        _cat.cab.StartEat();
    }

    bool WantsToToilet()
    {
        if (_cat.cab.currentActivity != null && _cat.cab.currentActivity.id == "toilet" && _cat.cab.aiConditionFetcher.HasNatureCall)
        {
            return true;
        }

        return false;
    }

    bool IsToilet(GameObject other)
    {
        if (other.GetComponent<Toilet>() != null)
        {
            return true;
        }

        return false;
    }

    void Toilet()
    {
        _cat.cab.StartToilet();
    }
}