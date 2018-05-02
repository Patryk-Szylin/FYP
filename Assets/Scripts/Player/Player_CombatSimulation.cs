using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum PLAYER_TYPE
{
    RANGE = 0,
    MELEE,
    HEAL
}

public class Player_CombatSimulation : MonoBehaviour
{
    public StackFSM m_brain = new StackFSM();
    public List<Action> m_behaviours = new List<Action>();
    //public List<Action> m_rangeBehaviours = new List<Action>();
    //public List<Action> m_healBehaviours = new List<Action>();
    public PLAYER_TYPE m_playerType;

    // Prefabs
    public Projectile m_projectilePrefab;

    // Stats
    public float m_meleeDamage = 2f;
    public float m_meleeAttackSpeed = 1f;
    public float m_movementSpeed = 15f;
    public float m_meleeRange = 5f;
    public float m_fieldOfViewRange = 15f;
    public float m_projectileRange = 10f;

    // Cooldowns
    public float m_nextMeleeReadyTime = 2f;
    public float m_meleeCooldown;
    public float m_meleeCooldownLeft;
    public float m_cooldown;
    public float m_cooldownLeft;
    public float m_nextReadyTime;

    // references
    private GaSystem _gaSystem;
    public List<Creature_Genetics> m_creatures = new List<Creature_Genetics>();

    public Creature_Health m_target;

    private void Start()
    {
        SetPlayerTypeEnum();

        _gaSystem = GameObject.FindObjectOfType<GaSystem>();
        m_creatures = _gaSystem.GetCreatures();
        //m_behaviours.Add(MeleeAttack);
        m_behaviours.Add(AttackWithProjectile);
        m_behaviours.Add(MeleeAttack);
 

        //m_brain.PushState(IdleState);
        m_brain.PushState(LookForClosestTarget);

    }


    public void SetPlayerTypeEnum()
    {
        if (GetComponent<Player_Melee>() != null)
        {
            m_playerType = PLAYER_TYPE.MELEE;
        }

        if (GetComponent<Player_Healer>() != null)
        {
            m_playerType = PLAYER_TYPE.HEAL;
        }


        if (GetComponent<Player_Range>() != null)
        {
            m_playerType = PLAYER_TYPE.RANGE;
        }




    }

    public void LookForClosestTarget()
    {
        if (m_target == null)
        {
            //print("look for new target");
            Creature_Genetics closestTarget = null;
            float previousDistance = 9999;

            for (int i = 0; i < m_creatures.Count; i++)
            {
                var distanceToClosest = Vector3.Distance(m_creatures[i].transform.position, this.transform.position);
                
                if (distanceToClosest < previousDistance)
                {
                    previousDistance = distanceToClosest;
                    closestTarget = m_creatures[i];
                }
                    
            }

            m_target = closestTarget.GetComponent<Creature_Health>();
            //m_brain.PopState();
            m_brain.PushState(MoveTowardsTarget);
            //m_brain.PushState(AttackWithRandomAbility;
        }
    }

    public void MoveTowardsTarget()
    {
        if (m_target != null)
        {
            var dir = (m_target.transform.position - this.transform.position).normalized;
            var distance = Vector3.Distance(m_target.transform.position, this.transform.position);
            
            if(m_playerType == PLAYER_TYPE.RANGE && GetComponent<Player_Range>() != null)
            {              
                if (distance >= m_projectileRange)
                {
                    transform.Translate(m_movementSpeed * dir * Time.deltaTime);
                    
                } else
                {
                    //print("move back");
                    m_brain.PopState();
                    m_brain.PushState(AttackWithRandomAbility);
                    //transform.Translate(m_movementSpeed * -target.transform.forward * Time.deltaTime);
                }                
            }

            if (m_playerType == PLAYER_TYPE.MELEE && GetComponent<Player_Melee>() != null)
            {
                if (distance >= m_meleeRange)
                {
                    transform.Translate(m_movementSpeed * dir * Time.deltaTime);
                } else
                {
                    m_brain.PushState(AttackWithRandomAbility);
                }
            }



        }
    }

    public void AttackWithRandomAbility()
    {


        if (m_target && m_target.m_isDead == false)
        {
            //print("Attacking with random ability");

            int randomIndex = UnityEngine.Random.Range(0, m_behaviours.Count);
            var randomAbility = m_behaviours[randomIndex];

            m_brain.PopState();
            m_brain.PushState(randomAbility);



            //var creaturePos = m_target.transform.position;
            //var dir = (creaturePos - transform.position).normalized;
            //var step = m_movementSpeed * Time.deltaTime;
            //var newDir = Vector3.RotateTowards(transform.forward, dir, step, 0.0f);
            //transform.rotation = Quaternion.LookRotation(new Vector3(newDir.x, 0, newDir.z));

            //if (Time.time > m_nextReadyTime)
            //{
            //    // Play attacking animation
            //    ShootProjectile(dir);
            //    m_nextReadyTime = m_cooldown + Time.time;
            //    m_brain.PopState();
            //}

            //CheckIfPlayerOutOfRange();
        }
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
            //print("Melee attack");
            //MoveTowardsTarget(m_target);
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
                if (m_target.GetComponent<Creature_Health>().m_isDead == true)
                {
                    m_target = null;
                    m_brain.PopState();
                    return;
                }

                if (Time.time > m_nextMeleeReadyTime)
                {
                    // Play Attacking animation
                    //m_anim.SetBool("IsAttacking", true);
                    //m_anim.SetBool("playerInRange", true);
                    //m_anim.SetBool("IsChasing", false);
                    m_target.Damage(m_meleeDamage);
                    m_target.GetComponent<Creature_Genetics>().m_totalDamageReceived += m_meleeDamage;
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
            //print("Projectile attack");
            //MoveTowardsTarget(m_target);
            var creaturePos = m_target.transform.position;
            var dir = (creaturePos - transform.position).normalized;
            var step = m_movementSpeed * Time.deltaTime;
            var newDir = Vector3.RotateTowards(transform.forward, dir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(new Vector3(newDir.x, 0, newDir.z));

            if (m_target.GetComponent<Creature_Health>().m_isDead == true)
            {
                //print("Target is ded");
                m_target = null;
                m_brain.PopState();
                return;
            }

            if (Time.time > m_nextReadyTime)
            {
                // Play attacking animation
                ShootProjectile(dir);
                m_nextReadyTime = m_cooldown + Time.time;
            }

            //CheckIfPlayerOutOfRange();
        }
    }

    public void ShootProjectile(Vector3 dir)
    {
        Projectile go = new Projectile();
        go = Instantiate(m_projectilePrefab, transform.position, Quaternion.identity);
        go.m_owner = this.GetComponent<Player>();

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
