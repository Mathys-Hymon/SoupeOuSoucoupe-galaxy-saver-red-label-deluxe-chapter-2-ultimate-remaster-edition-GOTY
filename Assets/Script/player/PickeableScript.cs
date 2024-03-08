using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickeableScript : MonoBehaviour
{
    void Update()
    {
        transform.position -= Vector3.right * 2 * Time.deltaTime;
    }
}
