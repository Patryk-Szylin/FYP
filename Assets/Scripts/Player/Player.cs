using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public bool m_enableSimulation = false;

    public float m_speed = 15f;                 // SEPERATE IN ANOTHER COMPONENT
    public float m_currentHealth;               // SEPERATE IN ANOTHER COMPONENT
    public float m_maxHealth = 5f;              // SEPERATE IN ANOTHER COMPONENT
    public RectTransform m_healthBar;
    public bool m_isDead = false;
    public float m_respawnDelay = 2f;
    Vector3 _initialPosition;                   // THIS SHOULD BE IN THE CONTROLLER
    public Creature_Health m_target;


    Player_Ability m_playerAbility;
    Player_Melee m_pMelee;
    Player_Range m_pRange;
    Player_Healer m_pHealer;
    ObjectSelector m_objectSelector;




    private void Start()
    {
        m_playerAbility = GetComponent<Player_Ability>();
        m_pMelee = GetComponent<Player_Melee>();
        m_pRange = GetComponent<Player_Range>();
        m_pHealer = GetComponent<Player_Healer>();
        m_objectSelector = GameObject.FindObjectOfType<ObjectSelector>();
        m_currentHealth = m_maxHealth;
        _initialPosition = transform.position;

        // Populating simulation behaviours

    }

    public void ChangeClass(int index)
    {
        switch (index)
        {
            case 1:
                m_pRange.enabled = false;
                break;
        }
    }


    public void Damage(float dmg)
    {
        m_currentHealth -= dmg;
        UpdateHealthBar(m_currentHealth);
        if (m_currentHealth <= 0 && !m_isDead)
        {
            m_isDead = true;
            SetActiveState(false);
            StartCoroutine(Respawn());
        }
    }

    public void UpdateHealthBar(float value)
    {
        if (m_healthBar != null)
        {
            m_healthBar.sizeDelta = new Vector2(value / m_maxHealth * 300f, m_healthBar.sizeDelta.y);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(m_enableSimulation == true)
            GetComponent<Player_CombatSimulation>().enabled = true;
        else
            GetComponent<Player_CombatSimulation>().enabled = false;

        if (m_objectSelector.m_selectedNPC != null)
        {
            print("got target");

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {

                if (m_pMelee != null)
                {
                    print("Attackign");
                    m_pMelee.MeleeAttack();
                }

                if (m_pRange != null)
                {
                    m_pRange.ShootProjectile(m_objectSelector.GetSelectedCreature());
                }


            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            m_enableSimulation = !m_enableSimulation;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(m_speed * Vector3.right * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(m_speed * Vector3.left * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(m_speed * Vector3.forward * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(m_speed * Vector3.back * Time.deltaTime);
        }
    }


    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(m_respawnDelay);
        transform.position = _initialPosition;
        m_currentHealth = m_maxHealth;
        UpdateHealthBar(m_currentHealth);
        m_isDead = false;
        SetActiveState(true);
    }


    public void EnableSimulation(bool state)
    {

    }


    void SetActiveState(bool state)
    {
        foreach (Collider c in GetComponentsInChildren<Collider>())
        {
            c.enabled = state;
        }

        foreach (Canvas c in GetComponentsInChildren<Canvas>())
        {
            c.enabled = state;
        }

        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            r.enabled = state;
        }
    }
}
