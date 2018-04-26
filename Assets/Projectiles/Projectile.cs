using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float m_damage;

    private void OnTriggerEnter(Collider other)
    {
        var creature = other.GetComponent<Creature_Health>();
        if (creature != null)
        {
            creature.Damage(m_damage);
            creature.GetComponent<Creature_Genetics>().m_totalDamageReceived += m_damage;
            Destroy(this.gameObject);
        }
    }

}
