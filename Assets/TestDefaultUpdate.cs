using System.Collections;
using UnityEngine;

public class TestDefaultUpdate : TimingBehaviour
{
    public int id;
    private int number;
    private bool[] bools;
    private const float timer = 0.1f;
    private float time;

    private void Start()
    {
        bools = new bool[10000];
        number = id;
    }

    bool UpdateActive()
    {
        return true;
    }
    private void Update()
    {
        if (UpdateActive())
            TestFunc();
    }

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
