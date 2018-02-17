using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Creature_Brain : MonoBehaviour
{
    public StackFSM m_brain = new StackFSM();
    public float m_speed = 2f;                     // THIS SHOULD BE IN THE CONTROLLER
    public float m_chaseSpeed = 5f;
    public bool m_moveForward = true;               // THIS SHOULD BE IN THE CONTROLLER
    public bool m_isWalking = false;


    [Header("Combat properties")]
    public float m_outOfRangeDistance = 25f;        // THIS SHOULD BE IN THE CONTROLLER
    public float m_idleMaximumTravelDistance = 10f; // THIS SHOULD BE IN THE CONTROLLER
    public float m_fieldOfViewRange = 5f;           // THIS SHOULD BE IN THE CONTROLLER
    public float m_rangeDamage = 1f;            // SHOULD BE IN SEPERATE COMPONENT
    public float m_meleeDamage = 1f;            // SHOULD BE IN SEPERATE COMPONENT
    public float m_meleeRange = 2f;
    public float m_meleeAttackSpeed = 2.2f;


    public Player _playerTarget;               // THIS SHOULD BE IN THE CONTROLLER
    private Vector3 _initialPosition;           // THIS SHOULD BE IN THE CONTROLLER
    public Text m_popupText;
    public Vector3 m_lastIdlePosition;
    public List<Player> m_nearbyPlayers = new List<Player>();


    public Projectile_Creature m_projectilePrefab;

    public List<Action> m_behaviours = new List<Action>();
    private Creature_Genetics m_genetics;
    private Creature_MovementSystem m_movementSystem;
    private Creature_IdleSystem m_idleSystem;

    public float m_cooldown;
    public float m_cooldownLeft;
    public float m_nextReadyTime;

    public float m_nextMeleeReadyTime;
    public float m_meleeCooldown;
    public float m_meleeCooldownLeft;


    // ANIMATIONS
    public Animator m_anim;




    private void Start()
    {
        m_genetics = GetComponent<Creature_Genetics>();
        m_movementSystem = GetComponent<Creature_MovementSystem>();
        m_idleSystem = GetComponent<Creature_IdleSystem>();
        m_anim = GetComponentInChildren<Animator>();

        _playerTarget = null;
        m_idleSystem.InitiateIdleSystem();
        m_brain.PushState(IdleState);
        _initialPosition = transform.position;
        m_behaviours.Add(Retreat);
        m_behaviours.Add(Hide);
        m_behaviours.Add(Attack);
        m_behaviours.Add(AttackWithProjectile);

    }

    private void Update()
    {
        // The creature needs to constantly check for nearby players
        // check for nearby players
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_fieldOfViewRange);
        List<Player> players = new List<Player>();

        foreach (var player in colliders)
        {
            var p = player.GetComponent<Player>();
            if (p != null)
            {
                players.Add(p);
            }
        }

        if (_playerTarget)
            m_anim.SetBool("foundPlayer", true);
        else
            m_anim.SetBool("foundPlayer", false);

        m_nearbyPlayers = players;

        // If more players pop up into the fight, then react with executing chromosome for fighting vs group
    }



    // Use genetic algorithm to also optimize idle behaviour ?
    // Some creatures could turn around faster or randomly walk to a different points in the map?
    public void IdleState()
    {
        if (m_nearbyPlayers.Count == 1)
        {
            foreach (var p in m_nearbyPlayers)
            {
                var playerMelee = p.GetComponent<Player_Melee>();
                var playerRange = p.GetComponent<Player_Range>();

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
                }
            }
        }

        // Then if there's more than one player, then it doesn't matter which type of player it is
        // Beacuse I'm gonna execute chromosome to respond to group attacks
        // just check if healer is present
        if (m_nearbyPlayers.Count > 1)
        {
            print("GROUP ENCOUNTERED");
        }
    }

    public void Hide()
    {
        DisplayPopUpText("HIDING!");
        // TODO: Display UI pop up text for now



        CheckIfPlayerOutOfRange();
    }

    public void DisplayPopUpText(string text)
    {
        m_popupText.gameObject.SetActive(true);
        m_popupText.text = text;
    }

    public void Retreat()
    {
        DisplayPopUpText("RETREATING!");
        // TODO: Display UI pop up text for now

        CheckIfPlayerOutOfRange();
    }

    public void AttackWithProjectile()
    {
        if (_playerTarget)
        {
            var playerPos = _playerTarget.transform.position;
            var dir = (playerPos - transform.position).normalized;
            var step = m_speed * Time.deltaTime;
            var newDir = Vector3.RotateTowards(transform.forward, dir, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(new Vector3(newDir.x, 0, newDir.z));

            if (Time.time > m_nextReadyTime)
            {
                // Play attacking animation
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
        print(_playerTarget);
        if (_playerTarget)
        {
            var step = m_chaseSpeed * Time.deltaTime;
            var playerPos = _playerTarget.transform.position;
            var distance = Vector3.Distance(playerPos, transform.position);
            var dir = (playerPos - transform.position).normalized;
            var newDir = Vector3.RotateTowards(transform.forward, dir, step, 0.0f);
            

            if (distance >= m_meleeRange)
            {
                // Play running animation
                m_anim.SetBool("playerInRange", false);
                m_anim.SetBool("IsAttacking", false);
                m_anim.SetBool("IsChasing", true);
                transform.position = Vector3.MoveTowards(transform.position, _playerTarget.transform.position, step);
                transform.rotation = Quaternion.LookRotation(new Vector3(newDir.x, 0, newDir.z));
            }
            else
            {
                print("Deal damage to the player");
                if (Time.time > m_nextMeleeReadyTime)
                {
                    // Play Attacking animation
                    m_anim.SetBool("IsAttacking", true);
                    m_anim.SetBool("playerInRange", true);
                    m_anim.SetBool("IsChasing", false);
                    _playerTarget.Damage(m_meleeDamage);
                    m_nextMeleeReadyTime = m_meleeAttackSpeed + Time.time;
                }
            }

            CheckIfPlayerOutOfRange();
        }
    }

    public void CheckIfPlayerOutOfRange()
    {
        // If out of range, move back to idle
        if (Vector3.Distance(transform.position, _playerTarget.transform.position) > m_outOfRangeDistance && _playerTarget)
        {
            // Set animation's bool in range to false
            m_anim.SetBool("playerInRange", false);
            m_anim.SetBool("IsAttacking", false);
            m_brain.PopState();
            //m_brain.PushState(MoveToInitialPosition);
            m_idleSystem.InitiateIdleSystem();
            _playerTarget = null;
            m_popupText.gameObject.SetActive(false);
        }
    }

    public void MoveToInitialPosition()
    {
        var step = m_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, m_lastIdlePosition, step);

        if (transform.position == m_lastIdlePosition)
        {
            m_brain.PopState();
        }
    }



}
