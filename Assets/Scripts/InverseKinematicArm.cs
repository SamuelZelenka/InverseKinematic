using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseKinematicArm : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    private float length = 1f;
    private Vector3[] segmentPoints = new Vector3[8];

    [SerializeField] private Transform target;
    private Vector3 anchor;
    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
     
        for (int i = 1; i < segmentPoints.Length; i += 2)
        {
            AddSegment(i - 1);
            AddSegment(i);

            void AddSegment(int index)
            {
                segmentPoints[index] = new Vector3(length, 0, 0);
            }
        }

        anchor = segmentPoints[segmentPoints.Length - 1];
    }

    void Update()
    {
        if (target.position == segmentPoints[0])
        {
            return;
        }
        
        CalculatePoints(target.position, ref segmentPoints[0], ref segmentPoints[1]);
        
        for (int i = 3; i < segmentPoints.Length ; i += 2)
        {
            CalculatePoints(segmentPoints[i - 2], ref segmentPoints[i - 1], ref segmentPoints[i]);

        }

      //  _lineRenderer.positionCount = segmentPoints.Length;
      //  _lineRenderer.SetPositions(segmentPoints);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 1; i < segmentPoints.Length; i += 2)
        {
            Gizmos.DrawLine(segmentPoints[i-1], segmentPoints[i]); 
        }
    }

    public void CalculatePoints(Vector3 targetPos, ref Vector3 headPoint, ref Vector3 tailPoint)
    {
        if (Vector3.Distance(targetPos,headPoint) < 0.2f)
        {
            return;
        }
        Vector3 direction = targetPos  - headPoint;
        direction = direction.normalized;
        direction *= -length;

        tailPoint = targetPos + direction;
        headPoint = targetPos;
    }
}
