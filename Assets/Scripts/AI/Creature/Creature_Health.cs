﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Creature_Health : MonoBehaviour
{
    public float m_currentHealth;
    public float m_maxHealth = 3f;
    public float m_respawnDelay = 2f;

    public bool m_isDead = false;
    public RectTransform m_healthBar;
    private Creature_Genetics m_genetics;


    Vector3 _initialPosition;   // THIS SHOULD BE IN THE CONTROLLER

    public void Start()
    {
        m_currentHealth = m_maxHealth;
        //StartCoroutine("CountDown");
        _initialPosition = transform.position;
        m_genetics = GetComponent<Creature_Genetics>();
    }

    public void Damage(float dmg)
    {
        m_currentHealth -= dmg;
        UpdateHealthBar(m_currentHealth);
        if(m_currentHealth <= 0 && !m_isDead)
        {
            m_isDead = true;
            // Generate new chromosomes 
            //m_genetics.CreateIndividual();
            SetActiveState(false);
            StartCoroutine(Respawn());
        }
    }

    void UpdateHealthBar(float value)
    {
        if(m_healthBar != null)
        {
            m_healthBar.sizeDelta = new Vector2(value / m_maxHealth * 300f, m_healthBar.sizeDelta.y);
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

        GetComponent<Creature_Brain>().enabled = state;
    }

}
