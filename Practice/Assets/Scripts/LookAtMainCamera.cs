using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMainCamera : MonoBehaviour
{
    private void Update()
    {
        Vector3 v = Camera.main.transform.position - transform.GetChild(0).position;
        //point the cost text toward the camera
        v.x = v.z = 0.0f;
        transform.GetChild(0).LookAt(Camera.main.transform.position);
    }

}
