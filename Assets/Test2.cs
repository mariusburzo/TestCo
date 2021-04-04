using System.Collections;
using UnityEngine;

public class Test2 : TimingBehaviour
{
    public int id;
    private int number;
    private bool[] bools;
    private const float timer = 0.1f;
    private float time;
    private TimerFunction tf;
    private TimerFunction pf;

    private void Start()
    {
        bools = new bool[10000];
        number = id;
        // StartCoroutine(FlipItemTimer());

        if (id == 0)
        {
            Debug.Log("Timers start: " + Time.realtimeSinceStartup);
            pf = StartTimerFunction(PeriodicFunc, 0.5f, true);
            tf = StartTimerFunction(TimerFunc, 0.5f);
        }

        onUpdate = () => TestFunc();
        Resume();
    }

    private void TimerFunc()
    {
        Debug.Log("Timer Done: " + Time.realtimeSinceStartup);
        tf.Stop();
    }

    private void PeriodicFunc()
    {
        Debug.Log("Periodic Timer Tick: " + Time.realtimeSinceStartup);
        if (pf.Ticks == 5)
        {
            pf.SkipTimerTo(1f);
        }
        if (pf.Ticks == 10)
        {
            pf.Pause();
            pf.Resume();
        }
        if (pf.Ticks == 15)
        {
            pf.Stop();
            Debug.Log("Periodic Timer Done: " + Time.realtimeSinceStartup);
        }
    }

    //protected override void OnUpdate()
    //{
    //    TestFunc();
    //}

    //bool UpdateActive()
    //{
    //    return true;
    //}
    //private void Update()
    //{
    //    if (UpdateActive())
    //        TestFunc();
    //}

    //private WaitForSeconds wait = new WaitForSeconds(timer);
    //private IEnumerator FlipItem()
    //{
    //    while (number >= 0)
    //    {
    //        TestFunc();
    //        yield return null;
    //    }
    //}

    //private IEnumerator FlipItemTimer()
    //{
    //    while (number >= 0)
    //    {
    //        TestFunc();
    //        yield return wait;
    //    }
    //}

    private void TestFunc()
    {
        if (number == bools.Length) number = 0;
        bools[number] = number % 2 == 0;
        number++;
    }

    private void TestFuncTimer()
    {
        time += Time.deltaTime;
        if (time >= timer)
        {
            time -= timer;
            if (number == bools.Length) number = 0;
            bools[number] = number % 2 == 0;
            number++;
        }
    }

}
