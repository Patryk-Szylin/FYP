using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Creature : MonoBehaviour
{

    public float m_damage;

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            //player.Damage(m_damage);
            print("HIT PLAYER");
            player.Damage(m_damage);
            Destroy(this.gameObject);
        }
    }
}
