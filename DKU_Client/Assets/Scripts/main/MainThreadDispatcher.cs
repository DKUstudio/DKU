using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainThreadDispatcher : MonoBehaviour
{
    internal static MainThreadDispatcher mtd;
    Queue<Action> jobs = new Queue<Action>();

    private void Awake()
    {
        mtd = this;
    }

    private void Update()
    {
        while (jobs.Count > 0)
            jobs.Dequeue().Invoke();
    }

    internal void AddJob(Action v_job)
    {
        jobs.Enqueue(v_job);
    }
}
