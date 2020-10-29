using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // direction to move in
    public Vector3 moveDir;

    // speed of the obstacle
    public float moveSpeed;

    // rotation speed
    public float rotateSpeed;

    // time before destroying object
    public float aliveTime = 4.0f;

    void OnEnable ()
    {
        CancelInvoke("Deactivate");
        Invoke("Deactivate", aliveTime);
    }

    void Update ()
    {
        // move the obstacle in the direction over time
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // rotate the obstacle overtime
        transform.Rotate(Vector3.back * moveDir.x * rotateSpeed * Time.deltaTime);
    }

    void Deactivate ()
    {
        gameObject.SetActive(false);
    }
}