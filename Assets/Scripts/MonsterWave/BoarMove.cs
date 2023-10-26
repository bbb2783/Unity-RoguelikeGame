using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarMove : MonoBehaviour
{
        void Update () {
                transform.position += Vector3.left * 20 * Time.deltaTime;
        }
}
