using System;
using System.Collections;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    private const float DEFAULT_ROUTINE_TIMEOUT = 3f;
    
    public static IEnumerator ExecuteAfterCoroutine(this MonoBehaviour monoBehaviour, IEnumerator coroutine, Action callback)
    {
        yield return monoBehaviour.StartCoroutine(coroutine);
        callback();
    }
    
    public static Coroutine ExecuteAfterDelay(this MonoBehaviour monoBehaviour, float delay, Action callback)
    {
        return monoBehaviour.StartCoroutine(DelayCallbackByYieldInstruction(YieldRegistry.WaitForSeconds(delay), callback));
    }

    public static Coroutine ExecuteWhen(this MonoBehaviour monoBehaviour, Func<bool> predicate, Action callback)
    {
        return monoBehaviour.StartCoroutine(DelayCallbackByPredicate(predicate, callback));
    }

    public static Coroutine ExecuteAtEndOfFrame(this MonoBehaviour monoBehaviour, Action callback)
    {
        return monoBehaviour.StartCoroutine(DelayCallbackByYieldInstruction(YieldRegistry.WaitForEndOfFrame, callback));
    }

    public static Coroutine ExecuteWhenOrAfterDelay(
        this MonoBehaviour monoBehaviour, 
        Func<bool> predicate, 
        float delaySeconds, 
        Action callback)
    {
        bool hasDelayElapsed = false;
        monoBehaviour.ExecuteAfterDelay(delaySeconds, () => hasDelayElapsed = true);
        return monoBehaviour.ExecuteWhen(() => hasDelayElapsed || predicate(), callback);
    }
    
    public static Coroutine ExecuteWhenOrTimeout(
        this MonoBehaviour monoBehaviour, 
        Func<bool> predicate,
        Action callback,
        float timeOut = DEFAULT_ROUTINE_TIMEOUT)
    {
        return monoBehaviour.StartCoroutine(DelayCallbackByPredicateOrTimeout(predicate, callback, timeOut));
    }

    private static IEnumerator DelayCallbackByPredicate(Func<bool> predicate, Action callback)
    {
        yield return YieldRegistry.WaitUntil(predicate);
        callback?.Invoke();
    }
    
    private static IEnumerator DelayCallbackByYieldInstruction(YieldInstruction yieldInstruction, Action callback)
    {
        yield return yieldInstruction;
        callback?.Invoke();
    }

    private static IEnumerator DelayCallbackByPredicateOrTimeout(Func<bool> predicate, Action callback, float timeOut)
    {
        float time = Time.time;

        while (!predicate())
        {
            if (Time.time - time > timeOut)
            {
                yield break;
            }

            yield return null;
        }

        callback?.Invoke();
    }
}
