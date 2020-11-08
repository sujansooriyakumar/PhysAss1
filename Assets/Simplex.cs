using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Simplex : MonoBehaviour
{
    public Vector3 pointA, pointB, pointC, pointD;

   public Simplex(Vector3 pointA_, Vector3 pointB_, Vector3 pointC_, Vector3 pointD_)
    {
        pointA = pointA_;
        pointB = pointB_;
        pointC = pointC_;
        pointD = pointD_;
    }

}
