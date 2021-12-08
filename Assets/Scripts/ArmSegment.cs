using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmSegment
{
    public Vector3 point1 = new Vector3();

    public Vector3 point2 = new Vector3();

    public float length;
    


    public ArmSegment(Vector3 startPos, float length)
    {
        point1 = startPos;
        point2 = startPos + new Vector3(length,0,0);
        this.length = length;
    }
    public void CalculatePoints(Vector3 target)
    {
        Vector3 direction = (target  - point1).normalized;
        direction *= -length;

        point2 = target + direction;
        point1 = target;
    }
}
