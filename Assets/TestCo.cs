using UnityEngine;

public class TestCo : MonoBehaviour
{
    private const int numberOfItems = 10000;
    private bool[] bools = new bool[numberOfItems];
    public Test2 test2;

    void Start()
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            var t = Instantiate(test2);
            t.name += " " + i;
            t.id = i;
        }
    }
}
