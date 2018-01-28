using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GaSystem : MonoBehaviour
{
    #region Singleton

    private static GaSystem _instance;
    public static GaSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GaSystem>();

                // If the GameObject with GameManager wasn't found, create one.
                if (_instance == null)
                    _instance = new GameObject("Genetic Algorithm System").AddComponent<GaSystem>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
    }
    #endregion

    public GameObject m_prefab;

    public List<Creature> m_creatures = new List<Creature>();
    public List<Creature> m_orderedCreatures = new List<Creature>();
    public List<Creature> m_newCreatures = new List<Creature>();


    private void Start()
    {
        for (int i = 0; i < m_creatures.Count; i++)
        {
            m_creatures[i].CreateIndividual(i);

            m_creatures[i].m_totalDamageReceived = Random.Range(10, 100);
            m_creatures[i].m_totalDamageDealt = Random.Range(10, 100);
        }
    }

    private void Update()
    {

        if (Input.GetKey(KeyCode.Alpha1))
        {
            for (int i = 0; i < m_creatures.Count; i++)
            {

                m_creatures[i].m_totalDamageDealt++;
            }
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            for (int i = 0; i < m_creatures.Count; i++)
            {
                m_creatures[i].m_totalDamageReceived++;
            }
        }

        if (Input.GetKey(KeyCode.Q))
        {
            //sortFitness(0);
            for (int i = 0; i < m_creatures.Count; i++)
            {
                m_creatures[i].sortFitness();
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            //for (int i = 0; i < m_creatures.Count; i++)
            //{
            //    for (int j = i + 1; j < m_creatures.Count; j++)
            //    {
            //        //print(m_creatures[i].m_chromosomes[0]);

            //        //var newCreature = m_creatures[i].Crossover(m_creatures[i], m_creatures[j]);
            //        //m_newCreatures.Add(newCreature);
            //    }
            //}

            var newCreature = Creature.Crossover(m_creatures[0], m_creatures[1]);
            m_prefab.GetComponent<Creature>().AssignNewChromosomes(newCreature);

            var go = Instantiate(m_prefab, transform.position, transform.rotation) as GameObject;

            //print(newCreature.m_chromosomes[0]);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            m_orderedCreatures = OrderListByDescending(m_creatures);
        }
    }

    List<Creature> OrderListByDescending(List<Creature> creatures)
    {
        return creatures.OrderByDescending(x => x.fitness).ToList();
    }


}
