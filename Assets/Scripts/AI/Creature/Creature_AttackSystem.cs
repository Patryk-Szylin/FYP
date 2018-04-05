using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_AttackSystem : MonoBehaviour
{
    //Prefabs#


    


    public float m_nextMeleeReadyTime;
    public float m_meleeCooldown;
    public float m_meleeCooldownLeft;
    // player target



    // Component references
    private Creature_MovementSystem m_movement;


    private void Start()
    {
        m_movement = GetComponent<Creature_MovementSystem>();
    }



 

}
