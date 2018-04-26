using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Creature : MonoBehaviour
{
    public Creature_Brain m_owner;
    public float m_damage;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            //player.Damage(m_damage);
            player.Damage(m_damage);
            m_owner.GetComponent<Creature_Genetics>().m_totalDamageDealt += m_damage;
            Destroy(this.gameObject);
        }
    }
}
