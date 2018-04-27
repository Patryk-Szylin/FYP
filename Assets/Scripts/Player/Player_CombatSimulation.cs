using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_CombatSimulation : MonoBehaviour
{
    public StackFSM m_brain = new StackFSM();
    public List<Action> m_behaviours = new List<Action>();

    // Prefabs
    public Projectile m_projectilePrefab;

    // Stats
    public float m_meleeDamage = 2f;
    public float m_meleeAttackSpeed = 1f;
    public float m_movementSpeed = 15f;
    public float m_meleeRange = 5f;
    public float m_fieldOfViewRange = 5f;

    // Cooldowns
    public float m_nextMeleeReadyTime = 2f;
    public float m_meleeCooldown;
    public float m_meleeCooldownLeft;
    public float m_cooldown;
    public float m_cooldownLeft;
    public float m_nextReadyTime;


    public Creature_Health m_target;

    private void Start()
    {
        //m_behaviours.Add(MeleeAttack);
        m_brain.PushState(IdleState);
    }

    private void Update()
    {
        if (m_target == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, m_fieldOfViewRange);

            foreach (var creature in colliders)
            {
                var c = creature.GetComponent<Creature_Health>();
                if (c != null)
                {
                    m_target = c;                    
                }
            }
        }
    }

    public void IdleState()
    {
        if (m_target)
        {
            m_brain.PushState(AttackWithProjectile);
        }
    }





    public void MeleeAttack()
    {
        if (m_target)
        {
            var step = m_movementSpeed * Time.deltaTime;
            var creaturePos = m_target.transform.position;
            var distance = Vector3.Distance(creaturePos, transform.position);
            var dir = (creaturePos - transform.position).normalized;
            var newDir = Vector3.RotateTowards(transform.forward, dir, step, 0.0f);


            if (distance >= m_meleeRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, m_target.transform.position, step);
                transform.rotation = Quaternion.LookRotation(new Vector3(newDir.x, 0, newDir.z));
            }
            else
            {
                print("Deal damage to the player");
                if (Time.time > m_nextMeleeReadyTime)
                {
                    // Play Attacking animation
                    //m_anim.SetBool("IsAttacking", true);
                    //m_anim.SetBool("playerInRange", true);
                    //m_anim.SetBool("IsChasing", false);
                    m_target.Damage(m_meleeDamage);
                    GetComponent<Creature_Genetics>().m_totalDamageDealt += m_meleeDamage;
                    m_nextMeleeReadyTime = m_meleeAttackSpeed + Time.time;
                }
            }

            //CheckIfPlayerOutOfRange();
        }
    }


    public void AttackWithProjectile()
    {
        if (m_target)
        {
            var creaturePos = m_target.transform.position;
            var dir = (creaturePos - transform.position).normalized;
            var step = m_movementSpeed * Time.deltaTime;
            var newDir = Vector3.RotateTowards(transform.forward, dir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(new Vector3(newDir.x, 0, newDir.z));

            if (Time.time > m_nextReadyTime)
            {
                // Play attacking animation
                ShootProjectile(dir);
                m_nextReadyTime = m_cooldown + Time.time;
                m_brain.PopState();
            }

            //CheckIfPlayerOutOfRange();
        }
    }

    public void ShootProjectile(Vector3 dir)
    {
        Projectile go = new Projectile();
        go = Instantiate(m_projectilePrefab, transform.position, Quaternion.identity);


        //go.m_damage = m_rangeDamage;
        go.GetComponent<Rigidbody>().AddForce(dir * 1500f);

        // Pop state when done shooting
    }

    //public void MeleeAttack()
    //{
    //    RaycastHit hit;

    //    if (Physics.Raycast(transform.position, Vector3.forward, out hit, 3f))
    //    {
    //        print("I'm hitting : " + hit.collider.name);
    //        Debug.DrawRay(transform.position, Vector3.forward, Color.yellow, 2f);

    //        var creature = hit.collider.GetComponent<Creature_Health>();

    //        if (creature != null)
    //        {
    //            creature.Damage(m_meleeAttack);
    //            creature.GetComponent<Creature_Genetics>().m_totalDamageReceived += m_meleeAttack;
    //        }
    //    }
    //}


}
