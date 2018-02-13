using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Range : MonoBehaviour
{
    public float m_rangeAttack = 1;
    public Projectile m_prefab;
    public float m_force;

    public void ShootProjectile()
    {
        var go = Instantiate(m_prefab, transform.position, Quaternion.identity);
        go.m_damage = m_rangeAttack;
        go.GetComponent<Rigidbody>().AddForce(Vector3.forward * m_force);
    }
}
