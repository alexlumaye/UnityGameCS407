using System.Collections;
using UnityEngine;

public static class Helper
{
    private static int count = 0;

    public static void SetTimeout(System.Action action, float delay)
    {
        GameObject tempGameObject = new GameObject("TempCoroutineRunner");
        tempGameObject.AddComponent<CoroutineRunner>().StartCoroutine(Run(action, delay, tempGameObject));
    }

    public static GameObject SetInterval(System.Action action, float interval)
    {
        count++;
        GameObject tempGameObject = new GameObject("Interval" + count);
        tempGameObject.AddComponent<CoroutineRunner>().StartCoroutine(Repeat(action, interval, tempGameObject));
        return tempGameObject;
    }

    public static void ClearInterval(GameObject tempGameObject)
    {
        GameObject.Destroy(tempGameObject);
    }

    private static IEnumerator Run(System.Action action, float delay, GameObject tempGameObject)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();

        GameObject.Destroy(tempGameObject);
    }

    private static IEnumerator Repeat(System.Action action, float interval, GameObject tempGameObject)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            action?.Invoke();
        }
    }
}

public class CoroutineRunner : MonoBehaviour { }
