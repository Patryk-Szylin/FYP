using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class Creature_Brain : MonoBehaviour
{
    // Finite State Machine reference
    public StackFSM m_brain = new StackFSM();

    // Prefabs
    public Projectile_Creature m_projectilePrefab;
    public Projectile_Creature m_fireProjectilePrefab;
    public Projectile_Creature m_iceProjectilePrefab;
    public Material m_fireBlade;
    public Material m_fireBody;
    public Material m_fireBodyArmor;
    public Material m_iceBlade;
    public Material m_iceBody;
    public Material m_iceBodyArmor;
    public SkinnedMeshRenderer m_bladeMesh;
    public SkinnedMeshRenderer m_bodyMesh;
    public GameObject m_bladeFireEffect;

    // attack attributes
    [Header("Combat properties")]
    public float m_outOfRangeDistance = 25f;        // THIS SHOULD BE IN THE CONTROLLER
    public float m_idleMaximumTravelDistance = 10f; // THIS SHOULD BE IN THE CONTROLLER
    public float m_fieldOfViewRange = 5f;           // THIS SHOULD BE IN THE CONTROLLER
    public float m_rangeDamage = 1f;            // SHOULD BE IN SEPERATE COMPONENT
    public float m_meleeDamage = 1f;            // SHOULD BE IN SEPERATE COMPONENT
    public float m_meleeRange = 2f;
    public float m_meleeAttackSpeed = 2.2f;

    // Recorded stats



    // Range attributes
    // stats
    public float m_cooldown;
    public float m_cooldownLeft;
    public float m_nextReadyTime;


    public Player _playerTarget;               // THIS SHOULD BE IN THE CONTROLLER

    public float m_nextMeleeReadyTime;
    public float m_meleeCooldown;
    public float m_meleeCooldownLeft;

    // Respawn-related variables
    private Vector3 _initialPosition;           // THIS SHOULD BE IN THE CONTROLLER
    public Vector3 m_lastIdlePosition;

    // Others 
    public List<Action> m_behaviours = new List<Action>();

    // Related component references    
    private SkinnedMeshRenderer m_mesh;
   
    private Creature_Genetics m_genetics;
    private Creature_MovementSystem m_movementSystem;
    private Creature_IdleSystem m_idleSystem;


    public Text m_popupText;
    public List<Player> m_nearbyPlayers = new List<Player>();


    // ANIMATIONS
    public Animator m_anim;




    private void Start()
    {
        m_genetics = GetComponent<Creature_Genetics>();
        m_movementSystem = GetComponent<Creature_MovementSystem>();
        m_idleSystem = GetComponent<Creature_IdleSystem>();
        m_anim = GetComponentInChildren<Animator>();
        m_mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        _playerTarget = null;
        m_idleSystem.InitiateIdleSystem();
        m_brain.PushState(IdleState);
        _initialPosition = transform.position;

        // Populate response behaviours 
        m_behaviours.Add(AttackWithBlade);
        m_behaviours.Add(AttackWithProjectile);
        m_behaviours.Add(AttackWithBlade);
        m_behaviours.Add(AttackWithProjectile);
        m_behaviours.Add(AttackWithMagic);
        m_behaviours.Add(AttackWithBlade);
        m_behaviours.Add(AttackWithProjectile);
        m_behaviours.Add(AttackWithMagic);
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

        // Use different textures based on the creature's ability skills
        // THIS SHOULD BE IN THE START FUNCTION
        // The only reason this is in update is so I can switch chromosmes at runtime
        ChangeCreatureTextures();


        // If more players pop up into the fight, then react with executing chromosome for fighting vs group
    }


    public void ChangeCreatureTextures()
    {
        int fireCounter = 0;
        int iceCounter = 0;
        for (int i = 0; i < m_genetics.m_chromosomes.Length; i++)
        {
            if (m_genetics.m_chromosomes[i] == 2 || m_genetics.m_chromosomes[i] == 3 || m_genetics.m_chromosomes[i] == 4)
                fireCounter++;

            if (m_genetics.m_chromosomes[i] == 5 || m_genetics.m_chromosomes[i] == 6 || m_genetics.m_chromosomes[i] == 7)
                iceCounter++;
        }

        switch (fireCounter)
        {
            case 0:
                // do nothing
                m_bladeFireEffect.SetActive(false);
                break;
            case 1:
                // Equip texture with fire blade
                m_bladeMesh.material = m_fireBlade;
                m_bladeFireEffect.SetActive(true);
                break;
            case 2:
                // Equip texture with fire blade + fire eyes + head
                m_bladeMesh.material = m_fireBlade;
                m_bodyMesh.material = m_fireBlade;
                m_bladeFireEffect.SetActive(false);
                break;
            case 3:
                // Equip texture with fire blade + fire eyes + head + fire armor
                m_bladeMesh.material = m_fireBlade;
                m_bodyMesh.material = m_fireBody;
                m_bladeFireEffect.SetActive(false);
                break;
            case 4:
                // Equip texture with fire skeleton + particle effect
                m_bladeMesh.material = m_fireBlade;
                m_bodyMesh.material = m_fireBodyArmor;
                m_bladeFireEffect.SetActive(true);
                break;
        }

        switch (iceCounter)
        {
            case 0:
                // do nothing
                //m_bladeFireEffect.SetActive(false);
                break;
            case 1:
                // Equip texture with fire blade
                m_bladeMesh.material = m_iceBlade;
                //m_bladeFireEffect.SetActive(false);
                break;
            case 2:
                // Equip texture with fire blade + fire eyes + head
                m_bladeMesh.material = m_iceBlade;
                m_bodyMesh.material = m_iceBlade;
                //m_bladeFireEffect.SetActive(false);
                break;
            case 3:
                // Equip texture with fire blade + fire eyes + head + fire armor
                m_bladeMesh.material = m_iceBlade;
                m_bodyMesh.material = m_iceBody;
                //m_bladeFireEffect.SetActive(false);
                break;
            case 4:
                // Equip texture with fire skeleton + particle effect
                m_bladeMesh.material = m_iceBlade;
                m_bodyMesh.material = m_iceBodyArmor;
                //m_bladeFireEffect.SetActive(true);
                break;
        }


        // If there is at least one fire texture across chromosomes then add texture with fire blade

        // 2 fire chromosomes: texture with fire blade and fire eyes + head

        // 3 fire chromosomes: texture with fire blade + fire eyes + head and fire armor

        // 4 fire chromosomes: everything in fire + fire particle effect




        //if (m_genetics.m_chromosomes[1] == 3)
        //{
        //    //go = Instantiate(m_projectilePrefab, transform.position, Quaternion.identity);
        //    m_bladeMesh.material = m_fireBody;
        //}
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





    public void Attack(SCENARIOS scenario, int attackType)
    {
        if (_playerTarget)
        {
            var step = m_movementSystem.m_chaseSpeed * Time.deltaTime;
            var playerPos = _playerTarget.transform.position;
            var distance = Vector3.Distance(playerPos, transform.position);
            var dir = (playerPos - transform.position).normalized;
            var newDir = Vector3.RotateTowards(transform.forward, dir, step, 0.0f);



            switch (attackType)
            {
                case 1:
                    // Attack with normal blade
                    break;
                case 2:
                    // Attack with normal projectile
                    break;
                case 3:
                    // Attack with fire blade
                    break;
                case 4:
                    // Attakc with fire projectile
                    break;
                case 5:
                    // Attack with fire magic
                    break;
                case 6:
                    // Attack with ice blade
                    break;
                case 7:
                    // Attack with ice projectile
                    break;
                case 8:
                    // Attack with ice magic
                    break;
            }
        }


        // Check what attack response is in chromosomes

        // If player target, Check what player type it is 


        // Execute appropriate attack when seeing the player

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
        var step = m_movementSystem.m_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, m_lastIdlePosition, step);

        if (transform.position == m_lastIdlePosition)
        {
            m_brain.PopState();
        }
    }



    #region ATTACK_BEHAVIOURS
    public void ShootProjectile(Vector3 dir)
    {
        Projectile_Creature go = new Projectile_Creature();

        if (m_genetics.m_chromosomes[1] == 1)
        {
            go = Instantiate(m_projectilePrefab, transform.position, Quaternion.identity);
            go.m_owner = this;
        }
        else if (m_genetics.m_chromosomes[1] == 3)  // If the response is to shoot fire projectile
        {
            go = Instantiate(m_fireProjectilePrefab, transform.position, Quaternion.identity);
            m_mesh.materials[0] = m_fireBody;
            go.m_owner = this;
        }
        else if (m_genetics.m_chromosomes[1] == 6)  // If the response is to shoot ice projectile
        {
            go = Instantiate(m_iceProjectilePrefab, transform.position, Quaternion.identity);
            go.m_owner = this;
        }

        go.m_damage = m_rangeDamage;
        go.GetComponent<Rigidbody>().AddForce(dir * 1500f);
    }

    public void ShootMagic(Vector3 targetPos)
    {

    }


    public void AttackWithBlade()
    {
        if (_playerTarget)
        {
            var step = m_movementSystem.m_chaseSpeed * Time.deltaTime;
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
                    GetComponent<Creature_Genetics>().m_totalDamageDealt += m_meleeDamage;
                    m_nextMeleeReadyTime = m_meleeAttackSpeed + Time.time;
                }
            }

            CheckIfPlayerOutOfRange();
        }
    }

    public void AttackWithProjectile()
    {
        if (_playerTarget)
        {
            var playerPos = _playerTarget.transform.position;
            var dir = (playerPos - transform.position).normalized;
            var step = m_movementSystem.m_speed * Time.deltaTime;
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

    public void AttackWithMagic()
    {

        DisplayPopUpText("Attacking with Magic!");
        // TODO: Display UI pop up text for now



        CheckIfPlayerOutOfRange();

    }




    #endregion


}
