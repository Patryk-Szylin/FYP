using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : Creature_Genetics
{

    // Use this for initialization
    void Start()
    {
        CreateIndividual(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            m_totalDamageDealt++;
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            m_totalDamageReceived++;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            //print(population.Length);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            CreateIndividual(0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            //sortFitness(0);
        }


    }
}
