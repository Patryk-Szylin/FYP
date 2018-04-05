using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_MovementSystem : MonoBehaviour
{
    public float m_speed = 2f;                     // THIS SHOULD BE IN THE CONTROLLER
    public float m_chaseSpeed = 5f;
    public bool m_moveForward = true;               // THIS SHOULD BE IN THE CONTROLLER
    public bool m_isWalking = false;



    public void Move(Vector3 target, float speed)
    {
        var step = speed * Time.deltaTime;
        var dir = (target - transform.position).normalized;
        var newDir = Vector3.RotateTowards(transform.forward, dir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(new Vector3(newDir.x, 0, newDir.z));
        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }
}
