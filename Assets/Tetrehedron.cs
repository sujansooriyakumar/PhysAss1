using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tetrehedron : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 linearVelocity;
    float t;
    public Vector3 rotationalVelocity;
    void Start()
    {
        t = 0;
    }

    // Update is called once per frame
    void Update()
    {
        t+= Time.deltaTime;
        transform.position += linearVelocity * Time.deltaTime;
        //Vector2 axis = Vector3.Cross(Vector3.up, linearVelocity);
        //rotationalVelocity = axis * (45.0f * Mathf.Deg2Rad);
        Quaternion result = new Quaternion(rotationalVelocity.x * Mathf.Deg2Rad, rotationalVelocity.y * Mathf.Deg2Rad, rotationalVelocity.z*Mathf.Deg2Rad, 0) * transform.rotation;
        Quaternion orientation = transform.rotation;
        result.x *= Time.deltaTime / 2;
        result.y *= Time.deltaTime / 2;
        result.z *= Time.deltaTime / 2;
        result.w *= Time.deltaTime / 2;
        orientation.x += result.x;
        orientation.y += result.y;
        orientation.z += result.z;
        orientation.w += result.w;
        

        Quaternion q = new Quaternion(0, 0, 0, 1);
        Quaternion w = new Quaternion(0, 2, 0, 0);
        Quaternion r = w * q;
        r.x *= 0.5f;
        r.y *= 0.5f;
        r.z *= 0.5f;
        r.w *= 0.5f;
        r.w += q.w;
        r.x += q.x;
        r.y += q.y;
        r.z += q.z;

        transform.rotation = orientation.normalized;
        Debug.Log(r.normalized);
    }
}
