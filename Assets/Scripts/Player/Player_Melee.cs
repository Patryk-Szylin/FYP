﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Melee : MonoBehaviour
{
    public float m_meleeAttack = 2;

    public void MeleeAttack()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.forward, out hit, 3f))
        {
            print("I'm hitting : " + hit.collider.name);
            Debug.DrawRay(transform.position, Vector3.forward, Color.yellow, 2f);

            var creature = hit.collider.GetComponent<Creature_Health>();

            if (creature != null)
            {
                creature.Damage(m_meleeAttack);
            }
        }
    }
}