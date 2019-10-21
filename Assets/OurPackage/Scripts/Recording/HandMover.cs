﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class HandMover : HandRecordingManager
{
    [SerializeField] private List<Transform> localTransform;

    /*Position and Rotation recordered Hand*/
    protected List<Vector3> getedPosition = new List<Vector3>();
    protected List<Quaternion> getedRotation= new List<Quaternion>();

    protected int curInxTransform = 0;
    protected SavedData dataTransform;

    // Start is called before the first frame update
    protected override void Awake()
    {
        if (handedness == Chirality.LEFT)
        {
            loadData = new LoadData("LeftHand.json");
            if (PlayerPrefs.GetString("LeftHand.json") != "")
                dataTransform = loadData.Load();
        }
        else
        {
            loadData = new LoadData("RightHand.json");
            if (PlayerPrefs.GetString("RightHand.json") != "")
                dataTransform = loadData.Load();
        }
        if(dataTransform != null)
        {
            getedPosition = dataTransform._handPosition;
            getedRotation = dataTransform._handRotation;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        MoveHand();
    }

    public virtual void MoveHand()
    {
        if(getedPosition.Count != 0 && getedRotation.Count != 0)
        {
            for(int i = 0; i < localTransform.Count; i++)
            {
                localTransform[i].position = getedPosition[curInxTransform];
                localTransform[i].rotation = getedRotation[curInxTransform];
                curInxTransform++;
            }
            if (curInxTransform >= getedPosition.Count - 1 ||
                curInxTransform >= getedRotation.Count - 1)
                curInxTransform = 0;
        }
    }

    public List<Transform> getLocalTransforms()
    {
        return localTransform;
    }
}
