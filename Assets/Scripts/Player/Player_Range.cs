using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Range : MonoBehaviour
{
    public float m_rangeAttack = 1;

    // 0 = fire
    // 1 = ice 
    // 2 = normal
    public List<Projectile> m_prefabs;

    public float m_force;





    public void ShootProjectile(Creature_Health target = null)
    {
        var randomIndex = Random.Range(0, m_prefabs.Count - 1);
        var randomProjectile = m_prefabs[randomIndex];
        var projectileDmg = randomProjectile.m_damage;
        var go = Instantiate(randomProjectile, transform.position, Quaternion.identity);
        go.m_damage = projectileDmg;

        Vector3 projectileDir;

        if (target != null)
        {
            projectileDir = (target.transform.position - transform.position).normalized;
        }
        else
        {
            projectileDir = Vector3.forward;
        }
        
        Vector3 dir = new Vector3(projectileDir.x, 0, projectileDir.z);
        go.GetComponent<Rigidbody>().AddForce(dir * m_force);
    }


}
