using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct tetrahedron
{
    public Vector3 A, B, C, D;
    public tetrahedron(Vector3 A_, Vector3 B_, Vector3 C_, Vector3 D_)
    {
        A = A_;
        B = B_;
        C = C_;
        D = D_;
    }
}

public class TesterClass : MonoBehaviour
{
  //  public tetrahedron A, B;
    public Tetrehedron A, B;
    public Vector3 d, pointA, pointB, pointC, pointD;
    Vector3[] verticesA = new Vector3[4];
    Vector3[] verticesB = new Vector3[4];

    public Simplex s1;
    public void Start()
    {
       
        // A = new tetrahedron(new Vector3(0,0,0), new Vector3(1, 0, 0), new Vector3(0.5f, 0, 0.9f), new Vector3(0.5f,0.8f,0.3f));
        //  B = new tetrahedron(new Vector3(0, -0.7f, 0), new Vector3(1, -0.7f, 0), new Vector3(0.5f, -0.7f, 0.9f), new Vector3(0.5f, 0.1f, 0.3f));
        

    }
    private void Update()
    {
        CalculateD(A, B);
        ProjectSimplex(d, A, B);
        CalculateDeterminants();
        s1.pointA = pointA;
        s1.pointB = pointB;
        s1.pointC = pointC;
        s1.pointD = pointD;
    }
    void CalculateD(Tetrehedron A_, Tetrehedron B_)
    {
        Vector3 c1 = A_.FindCenter();

        Vector3 c2 = B_.FindCenter();

       
        d = c2 - c1;
        d.Normalize();


    }

    void ProjectSimplex(Vector3 d_, Tetrehedron A_, Tetrehedron B_)
    {
        pointA = FindPoint(d_);
        d_ *= -1;

        pointB = FindPoint(d_);

        Vector3 ab = pointB - pointA;
        Vector3 ao = pointA - new Vector3(0, 0, 0);
        d_ = Vector3.Cross(Vector3.Cross(ab, ao), ab) * -1;
        pointC = FindPoint(d_);

        Vector3 ac = pointC - pointA;
        Vector3 n1 = Vector3.Cross(ab, ac);
        Vector3 n2 = n1 * -1;

        Vector3 T = new Vector3((pointA.x + pointB.x + pointC.x) / 3,
            (pointA.y + pointB.y + pointC.y) / 3,
            (pointA.z + pointB.z + pointC.z) / 3);

        Vector3 TO = new Vector3(0, 0, 0) - T;
        if (Vector3.Dot(n2, TO) > 0) d_ = n2;
        if (Vector3.Dot(n1, TO) > 0) d_ = n1;

        pointD = FindPoint(d_);

    }

    void CalculateDeterminants()
    {
        Matrix4x4 m1 = new Matrix4x4(new Vector4(pointA.x, pointB.x, pointC.x, pointD.x),
            new Vector4(pointA.y, pointB.y, pointC.y, pointD.y),
            new Vector4(pointA.z, pointB.z, pointC.z, pointD.z),
            new Vector4(1, 1, 1, 1));

        Matrix4x4 m2 = new Matrix4x4(new Vector4(0, pointB.x, pointC.x, pointD.x),
            new Vector4(0, pointB.y, pointC.y, pointD.y),
            new Vector4(0, pointB.z, pointC.z, pointD.z),
            new Vector4(1, 1, 1, 1));

        Matrix4x4 m3 = new Matrix4x4(new Vector4(pointA.x, 0, pointC.x, pointD.x),
           new Vector4(pointA.y, 0, pointC.y, pointD.y),
           new Vector4(pointA.z, 0, pointC.z, pointD.z),
           new Vector4(1, 1, 1, 1));

        Matrix4x4 m4 = new Matrix4x4(new Vector4(pointA.x, pointB.x, 0, pointD.x),
           new Vector4(pointA.y, pointB.y, 0, pointD.y),
           new Vector4(pointA.z, pointB.z, 0, pointD.z),
           new Vector4(1, 1, 1, 1));

        Matrix4x4 m5 = new Matrix4x4(new Vector4(pointA.x, pointB.x, pointC.x, 0),
           new Vector4(pointA.y, pointB.y, pointC.y, 0),
           new Vector4(pointA.z, pointB.z, pointC.z, 0),
           new Vector4(1, 1, 1, 1));

        float d1 = m1.determinant;
        float d2 = m2.determinant;
        float d3 = m3.determinant;
        float d4 = m4.determinant;
        float d5 = m5.determinant;
        //Debug.Log("Determinants: " + "d1 = " + d1 + " d2 = " + d2 + " d3 = " + d3 + " d4 = " + d4 + " d5 = " + d5);

       if((d1 > 0 && d2 > 0 && d3 > 0 && d4 > 0 && d5 > 0) || (d1 <0 && d2 < 0 && d3 < 0 && d4 < 0 && d5 < 0)){
            Debug.Log("collision");
           
        }


    }

    Vector3 FindPoint(Vector3 d_)
    {
        Vector3 result;
        Vector3 p1, p2;
        p1 = new Vector3(0, 0, 0);
        p2 = new Vector3(0, 0, 0);
        float s1A = Vector3.Dot(A.GetVertices()[0], d_);
        float s1B = Vector3.Dot(A.GetVertices()[1], d_);
        float s1C = Vector3.Dot(A.GetVertices()[2], d_);
        float s1D = Vector3.Dot(A.GetVertices()[3], d_);
        float maxs1 = Mathf.Max(s1A, s1B, s1C, s1D);

        if (maxs1 == s1A) p1 = A.GetVertices()[0];
        else if (maxs1 == s1B) p1 = A.GetVertices()[1];
        else if (maxs1 == s1C) p1 = A.GetVertices()[2];
        else if (maxs1 == s1D) p1 = A.GetVertices()[3];


        float s2A = Vector3.Dot(B.GetVertices()[0], d_);
        float s2B = Vector3.Dot(B.GetVertices()[1], d_);
        float s2C = Vector3.Dot(B.GetVertices()[2], d_);
        float s2D = Vector3.Dot(B.GetVertices()[3], d_);
        float minS2 = Mathf.Min(s2A, s2B, s2C, s2D);

        if (minS2 == s2A) p2 = B.GetVertices()[0];
        else if (minS2 == s2B) p2 = B.GetVertices()[1];
        else if (minS2 == s2C) p2 = B.GetVertices()[2];
        else if (minS2 == s2D) p2 = B.GetVertices()[3];

        result = p1 - p2;
        //Debug.Log("Point: " + result);
        return result;
    }

    Vector3 FindNewD(Vector3 first, Vector3 second, Vector3 third)
    {
        Vector3 result = new Vector3(0, 0, 0);
        Vector3 triangleCenter = (first + second + third) / 3.0f;
        Vector3 TO = Vector3.zero - triangleCenter;

        Vector3 ac = second - first;
        Vector3 ad = third - first;
        Vector3 n1 = Vector3.Cross(ac, ad);
        Vector3 n2 = n1 * -1;

        if (Vector3.Dot(TO, n1) > 0)
        {
            result = n1;
        }
        else
        {
            result = n2;
        }
        return result;
    }

    

}
