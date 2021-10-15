using UnityEngine;

public class Ticker : MonoBehaviour
{
    public float TickTime;
    private float _tickTimer;

    protected virtual void Update()
    {
        _tickTimer -= Time.deltaTime;
        if (_tickTimer < 0)
        {
            _tickTimer = TickTime;
            Tick();
        }
    }

    protected virtual void Tick()
    {

    }
}