using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelector : MonoBehaviour
{
    // Update is called once per frame
    public Creature_Genetics m_selectedNPC;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //print("Mouse is down");
            RaycastHit hitInfo = new RaycastHit();

            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

            if (hit)
            {
                //print("Hit : " + hitInfo.transform.gameObject.name);
                var npc = hitInfo.transform.GetComponent<Creature_Genetics>();

                if (npc != null)
                {
                    //print("It's working");

                    m_selectedNPC = npc;
                    UI_Manager.Instance.ShowSelectedNPCStats(m_selectedNPC);
                }
                else
                {
                    //print("Nope");
                }
            }
            else
            {
                //print("No hit");
            }

            //print("Mouse is down");
        }
    }
}
