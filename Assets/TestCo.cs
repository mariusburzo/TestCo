using UnityEngine;

public class TestCo : MonoBehaviour
{
    public int numberOfItems = 10000;
    public TestDefaultUpdate testDefaultUpdate;
    public TestTiming testTiming;
    public bool UseTiming;

    void Start()
    {
        if (UseTiming)
        {
            for (int i = 0; i < numberOfItems; i++)
            {
                var t = Instantiate(testTiming);
                t.name += " " + i;
                t.id = i;
            }
        }
        else
        {
            for (int i = 0; i < numberOfItems; i++)
            {
                var t = Instantiate(testDefaultUpdate);
                t.name += " " + i;
                t.id = i;
            }
        }
    }
}
