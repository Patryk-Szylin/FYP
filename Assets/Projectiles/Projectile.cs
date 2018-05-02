using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float m_damage;
    public Player m_owner;

    // DO I NEED TO STORE WHAT SORT OF PROJECTILE IT IS ?
    // FIRE / ICE / NORMAL

    private void OnTriggerEnter(Collider other)
    {
        var creature = other.GetComponent<Creature_Health>();
        if (creature != null)
        {
            creature.Damage(m_damage);
            creature.GetComponent<Creature_Genetics>().m_totalDamageReceived += m_damage;
            creature.GetComponent<Creature_Brain>().m_playerTarget = m_owner;
            Destroy(this.gameObject);
        }
    }

}
