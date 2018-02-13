using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float m_speed = 15f;
    Player_Ability m_playerAbility;
    Player_Melee m_pMelee;
    Player_Range m_pRange;
    Player_Healer m_pHealer;


    private void Start()
    {
        m_playerAbility = GetComponent<Player_Ability>();
        m_pMelee = GetComponent<Player_Melee>();
        m_pRange = GetComponent<Player_Range>();
        m_pHealer = GetComponent<Player_Healer>();
    }


    // Update is called once per frame
    void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (m_pRange != null)
            {
                m_pRange.ShootProjectile();
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(m_pMelee != null)
            {
                m_pMelee.MeleeAttack();
            }
        }

    }
}
