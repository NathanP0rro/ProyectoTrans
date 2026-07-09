using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    public List<DialogLine> lines;
    public bool playOnStart = false;

    private void Start()
    {
        Debug.Log("DialogActivator Start");
        if (playOnStart)
        {
            Activate();
        }
    }
    private void Activate()
    {
        CutSceneSystem.instance.StartDialog(this);
    }
}


