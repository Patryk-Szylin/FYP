using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Creature_Brain : MonoBehaviour
{
    public StackFSM m_brain = new StackFSM();
    public float m_speed = 50f;                     // THIS SHOULD BE IN THE CONTROLLER
    public bool m_moveForward = true;               // THIS SHOULD BE IN THE CONTROLLER
    public float m_outOfRangeDistance = 25f;        // THIS SHOULD BE IN THE CONTROLLER
    public float m_idleMaximumTravelDistance = 10f; // THIS SHOULD BE IN THE CONTROLLER
    public float m_fieldOfViewRange = 5f;           // THIS SHOULD BE IN THE CONTROLLER

    private Player _playerTarget;               // THIS SHOULD BE IN THE CONTROLLER
    private Vector3 _initialPosition;           // THIS SHOULD BE IN THE CONTROLLER

    public float m_rangeDamage = 1f;            // SHOULD BE IN SEPERATE COMPONENT
    public float m_meleeDamage = 1f;            // SHOULD BE IN SEPERATE COMPONENT
    public Projectile_Creature m_projectilePrefab;

    public List<Action> m_behaviours = new List<Action>();
    private Creature_Genetics m_genetics;

    public float m_cooldown;
    public float m_cooldownLeft;
    public float m_nextReadyTime;

    private void Start()
    {
        _playerTarget = null;
        m_brain.PushState(idleWalk);
        _initialPosition = transform.position;
        m_behaviours.Add(Retreat);
        m_behaviours.Add(Hide);
        m_behaviours.Add(Attack);
        m_behaviours.Add(AttackWithProjectile);

        m_genetics = GetComponent<Creature_Genetics>();
    }
    public void idleWalk()
    {
        print("MOVE");

        // The position needs to be transformed from World Space to Local Space
        var worldToLocalPos = transform.InverseTransformPoint(_initialPosition);

        if (m_moveForward)
        {
            transform.Translate(m_speed * Vector3.forward * Time.deltaTime);
            if (worldToLocalPos.z < -m_idleMaximumTravelDistance)
                m_moveForward = false;
        }
        else
        {
            transform.Translate(m_speed * Vector3.back * Time.deltaTime);
            if (worldToLocalPos.z > m_idleMaximumTravelDistance)
                m_moveForward = true;
        }

        // check for nearby players
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_fieldOfViewRange);

        // iF THERE'S nearby player then transition to state Attack and pass the variable of the player that is nearby
        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                var playerMelee = collider.GetComponent<Player_Melee>();
                var playerRange = collider.GetComponent<Player_Range>();

                if (playerMelee != null)
                {
                    print("FOUND MELEE");
                    //m_behaviours[0]();
                    _playerTarget = playerMelee.GetComponent<Player>();
                    m_brain.PushState(m_behaviours[m_genetics.m_chromosomes[0]]);
                    
                }

                if (playerRange != null)
                {
                    print("FOUND RANGED");
                    //m_behaviours[1]();
                    _playerTarget = playerRange.GetComponent<Player>();
                    m_brain.PushState(m_behaviours[m_genetics.m_chromosomes[1]]);
                    print(m_genetics.m_chromosomes[1]);
                }

                //if (p != null)
                //{
                //    print("FOUND A TARGET");
                //    _playerTarget = p;
                //    m_brain.PushState(Attack);
                //}
            }
        }

        // IF THE COLLIDER HAS A PLAYER MELEE component then execute chromosome response [0]
        // if PLAYER RANGE COMPONENT then execute chromosome[1]
            /*this could be executed when creature gets hit but it doesnt see the player yet
             * 
             * 
             */
        // If there are multiple Player_ componenets then execute chromosome[2]
        // If there is healer present then execute chromosome[3]
    }

    public void Hide()
    {
        print("HIDING");
    }

    public void Retreat()
    {
        print("RETREATING");
    }

    public void AttackWithProjectile()
    {
        if (_playerTarget)
        {
            var playerPos = _playerTarget.transform.position;
            var dir = (playerPos - transform.position).normalized;
            print("Shoot");

            if (Time.time > m_nextReadyTime)
            {
                ShootProjectile(dir);
                m_nextReadyTime = m_cooldown + Time.time;
            }

            CheckIfPlayerOutOfRange();
        }
    }



    public void ShootProjectile(Vector3 dir)
    {
        var go = Instantiate(m_projectilePrefab, transform.position, Quaternion.identity);
        go.m_damage = m_rangeDamage;
        go.GetComponent<Rigidbody>().AddForce(dir * 1500f);
    }

    public void Attack()
    {
        print("ATTACKING");
        print(_playerTarget);
        if (_playerTarget)
        {
            print("ATTACK");
            var step = m_speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _playerTarget.transform.position, step);

            CheckIfPlayerOutOfRange();
        }
    }

    public void CheckIfPlayerOutOfRange()
    {
        // If out of range, move back to idle
        if (Vector3.Distance(transform.position, _playerTarget.transform.position) > m_outOfRangeDistance)
        {
            m_brain.PopState();
            m_brain.PushState(MoveToInitialPosition);
        }
    }

    public void MoveToInitialPosition()
    {
        var step = m_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _initialPosition, step);

        if (transform.position == _initialPosition)
        {
            m_brain.PopState();
        }
    }



}
