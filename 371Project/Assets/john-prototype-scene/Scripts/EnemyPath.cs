using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class EnemyPath : MonoBehaviour
{
    private PathCreator _enemyPath;
    public GameObject pathPointsObject;

    private Vector3[] _pathPoints;
    
    /*
     * Creates a list of points from pathPointsObject in the order they are in the heirarchy.
     * uses transforms from children.
     */
    void GetPointsByOrder()
    {
        PointObject[] temp = pathPointsObject.GetComponentsInChildren<PointObject> () as PointObject[];
        _pathPoints = new Vector3[temp.Length];
        int index = 0;
        int noOfChildren = pathPointsObject.transform.childCount;
        for (int i=0; i<noOfChildren;i++)
        {
            PointObject childComponent = pathPointsObject.transform.GetChild(i).GetComponent<PointObject>();
            if(childComponent!=null)
            {
                _pathPoints[index] = childComponent.GetTransform().position;
                index++;
            }
        }
    }

    /*
     * This method assumes _pathPoints is updated with the most recent path transforms. This should be called
     * every time a path point is changed / moved
     */
    public void UpdatePath()
    {
        if (_pathPoints.Length > 0) {
            // Create a new bezier path from the waypoints.
            BezierPath bezierPath = new BezierPath(_pathPoints, false, PathSpace.xz);
            GetComponent<PathCreator>().bezierPath = bezierPath;
        }
    }

    private void Start()
    {
        _enemyPath = GetComponent<PathCreator>();
        
        // generate path from other game object that contains a list of points, some moveable, some not.
        GetPointsByOrder();
        // create new path!
        UpdatePath();
    }

    public void OnEnemyPathChanged()
    {
        
    }
}
