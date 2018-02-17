﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_IdleSystem : MonoBehaviour
{
    public StackFSM m_brain = new StackFSM();
    public float m_speed = 50f;

    public Vector3 m_destination;



    private void Start()
    {
        m_brain.PushState(Walk);
    }


    public void Walk()
    {
        print("PLAY IDLE ANIMATION");
        var randomPoint = GenerateIdleDestinationPoint();
        m_destination = randomPoint;

        m_brain.PushState(Move);
    }


    public void Move()
    {
        var step = m_speed * Time.deltaTime;
        var dir = (m_destination - transform.position).normalized;
        var newDir = Vector3.RotateTowards(transform.forward, dir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(new Vector3(newDir.x, 0, newDir.z));
        transform.position = Vector3.MoveTowards(transform.position, m_destination, step);
        

        print("WALKING...");
        if (transform.position == m_destination)
        {            
            m_brain.PopState();
        }            
    }

    public Vector3 GenerateIdleDestinationPoint()
    {
        Vector3[] randomIdleWalkTargetPosiitons = new Vector3[3]
        {
            transform.position + new Vector3(3,0,3),
            transform.position + new Vector3(-1,0,-10),
            transform.position + new Vector3(-5 ,0, 4)
        };

        int randomIndex = UnityEngine.Random.Range(0, randomIdleWalkTargetPosiitons.Length);

        return randomIdleWalkTargetPosiitons[randomIndex];
    }


}
