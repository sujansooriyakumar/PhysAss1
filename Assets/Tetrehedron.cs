using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Tetrehedron : MonoBehaviour
{
    // Start is called before the first frame update
    public Mesh mesh;
    
    public Vector3 linearVelocity;
    Vector3[] vertices;
    public Tetrehedron otherShape;
    float t;
    public Vector3 rotationalVelocity;
    int i = 0;

    void Start()
    {
        
        t = 0;
        var thisMatrix = transform.localToWorldMatrix;
        vertices = mesh.vertices;
        for(int i = 0; i < 4; i++)
        {
            Debug.Log(gameObject.name + " vertex " + (i+1) + " at " + vertices[i]);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if(i < 1)
        {
            CollisionCheck(otherShape);
            i++;
        }
        for (int i = 0; i < 4; i++)
        {
           // Debug.Log(gameObject.name + " vertex " + i + " at " + transform.localToWorldMatrix.MultiplyPoint3x4(vertices[i]));
        }
        

        t += Time.deltaTime;
        transform.position += linearVelocity * Time.deltaTime;
        //Vector2 axis = Vector3.Cross(Vector3.up, linearVelocity);
        //rotationalVelocity = axis * (45.0f * Mathf.Deg2Rad);
        Quaternion orientation = transform.rotation;
        Quaternion result = new Quaternion(rotationalVelocity.x * Mathf.Deg2Rad, rotationalVelocity.y * Mathf.Deg2Rad, rotationalVelocity.z*Mathf.Deg2Rad, 0) * transform.rotation;
        result.x *= Time.deltaTime / 2;
        result.y *= Time.deltaTime / 2;
        result.z *= Time.deltaTime / 2;
        result.w *= Time.deltaTime / 2;
        orientation.x += result.x;
        orientation.y += result.y;
        orientation.z += result.z;
        orientation.w += result.w;


        transform.rotation = orientation.normalized;
    }

   private void CollisionCheck(Tetrehedron otherShape_)
    {
        Vector3[] verticesA = new Vector3[4];
        verticesA[0] = new Vector3(0.5f, 0.8f, 0.3f);
        verticesA[1] = new Vector3(0, 0, 0);
        verticesA[2] = new Vector3(0.5f, 0, 0.9f);
        verticesA[3] = new Vector3(1, 0, 0);

        Vector3[] verticesB = new Vector3[4];
        verticesB[0] = new Vector3(0.5f, 0.1f, 0.3f);
        verticesB[1] = new Vector3(0, -0.7f, 0);
        verticesB[2] = new Vector3(0.5f, 0.7f, 0.9f);
        verticesB[3] = new Vector3(1.0f, -0.7f, 0);
        List<Vector3> result = new List<Vector3>();
        Vector3[] otherVertices = otherShape_.GetVertices();
        for(int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                Vector3 minkowski = verticesA[i] - verticesB[j];
                // Vector3 minkowski = transform.localToWorldMatrix.MultiplyPoint3x4(vertices[i]) - transform.localToWorldMatrix.MultiplyPoint3x4(otherVertices[j]);
                Debug.Log(verticesA[i] + " - " + verticesB[j] + " = " + minkowski);
                result.Add(minkowski);
            }
            
        }

        for(int k = 0; k < result.Count; k++)
        {
            //Debug.Log("Result point " + k + " " + result[k]);
        }


    }

    public Vector3[] GetVertices()
    {
        return vertices;
    }
}
