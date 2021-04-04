using System;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    private static TimingManager instance;
    private HashSet<UpdateBehaviour> behaviours;

    private void Awake()
    {
        instance = this;
        behaviours = new HashSet<UpdateBehaviour>();
    }

    private void Update()
    {
        foreach (var behaviour in behaviours)
        {
            behaviour.TimingUpdate();
        }
    }

    public static void AddUpdateBehaviour(UpdateBehaviour behaviour)
    {
        instance.behaviours.Add(behaviour);
    }

    public static void RemoveUpdateBehaviour(UpdateBehaviour behaviour)
    {
        instance.behaviours.Remove(behaviour);
    }
}

public abstract class UpdateBehaviour : MonoBehaviour
{
    protected Action onUpdate;
    private bool updateActive;

    public virtual void TimingUpdate()
    {
        if (updateActive)
        {
            onUpdate();
        }
    }

    protected void Resume() { updateActive = true; }

    protected void Pause() { updateActive = false; }

    protected void OnEnable() { TimingManager.AddUpdateBehaviour(this); }

    protected void OnDisable() { TimingManager.RemoveUpdateBehaviour(this); }
}

public abstract class TimingBehaviour : UpdateBehaviour
{
    private readonly List<TimerFunction> timerFunctions = new List<TimerFunction>();

    public override void TimingUpdate()
    {
        base.TimingUpdate();
        for (int i = 0; i < timerFunctions.Count; i++)
        {
            if (timerFunctions[i].TimingUpdate())
            {
                timerFunctions.RemoveAt(i--);
            }
        }
    }

    protected TimerFunction StartTimerFunction(Action action, float timer, bool periodic = false, bool useUnscaledDeltaTime = false)
    {
        var timerFunction = new TimerFunction(action, timer, periodic, useUnscaledDeltaTime);
        timerFunctions.Add(timerFunction);
        return timerFunction;
    }
}

public class TimerFunction
{
    public int Ticks;
    private readonly Action action;
    private float timer;
    private float baseTimer;
    private readonly bool periodic;
    private readonly bool useUnscaledDeltaTime;
    private bool updateActive;
    private bool stopped;

    public TimerFunction(Action action, float timer, bool periodic, bool useUnscaledDeltaTime)
    {
        this.action = action;
        this.timer = timer;
        baseTimer = timer;
        this.periodic = periodic;
        this.useUnscaledDeltaTime = useUnscaledDeltaTime;
        updateActive = true;
    }

    public void SkipTimerTo(float timer)
    {
        this.timer = timer;
        baseTimer = timer;
    }

    public bool TimingUpdate()
    {
        if (stopped)
        {
            return true;
        }
        if (updateActive)
        {
            if (useUnscaledDeltaTime)
            {
                timer -= Time.unscaledDeltaTime;
            }
            else
            {
                timer -= Time.deltaTime;
            }
            if (timer <= 0)
            {
                Ticks++;
                action();
                if (periodic)
                {
                    timer = baseTimer;
                } else
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void Resume() { updateActive = true; }

    public void Pause() { updateActive = false; }

    public void Stop() { stopped = true; }
}
