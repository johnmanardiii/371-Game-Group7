using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class SwitchManager : PointObject
{
    private SwitchPoint[] _switchPoints;
    public SwitchPoint activePoint;
    private void Awake()
    {
        _switchPoints = GetComponentsInChildren<SwitchPoint>();
        
        // set manager in children to this one:
        // and initialize switch points
        foreach(SwitchPoint point in _switchPoints)
        {
            point._manager = this;
            if (point.isActive)
            {
                activePoint = point;
                point.GetComponent<MeshRenderer>().material = enabledMat;
            }
            else
            {
                point.GetComponent<MeshRenderer>().material = disabledMat;
            }
        }
    }

    public void ChangeSwitchPoint(SwitchPoint point)
    {
        activePoint = point;
        // update some stuff
    }

    public override Transform GetTransform()
    {
        return activePoint.transform;
    }
}
