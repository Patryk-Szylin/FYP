using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Networking;

public class GaSystem : NetworkBehaviour
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

    public List<Creature_Genetics> m_creatures = new List<Creature_Genetics>();
    public List<Creature_Genetics> m_orderedCreatures = new List<Creature_Genetics>();
    public List<GameObject> m_creaturesToSpawn = new List<GameObject>();
    public List<Creature_Genetics> m_networkedNPCs = new List<Creature_Genetics>();
    
    private void Start()
    {
        //for (int i = 0; i < m_creaturesToSpawn.Count; i++)
        //{
        //    //var go = Instantiate(m_creaturesToSpawn[i], Vector3.zero, m_creaturesToSpawn[i].transform.rotation);
        //    Instantiate(m_creaturesToSpawn[i], Vector3.zero, m_creaturesToSpawn[i].transform.rotation);
        //    //NetworkServer.Spawn(go);
        //}

        //for (int i = 0; i < m_creatures.Count; i++)
        //{
        //    m_creatures[i].CreateIndividual();

        //    //m_creatures[i].m_totalDamageReceived = Random.Range(10, 100);
        //    //m_creatures[i].m_totalDamageDealt = Random.Range(10, 100);
        //}
    }

    [Server]
    public void SpawnCreatures()
    {
        for (int i = 0; i < m_creaturesToSpawn.Count; i++)
        {
            var go = Instantiate(m_creaturesToSpawn[i], Vector3.zero, m_creaturesToSpawn[i].transform.rotation);
            var creature = go.GetComponent<Creature_Genetics>();
            creature.CreateIndividual();
            m_networkedNPCs.Add(creature);
            //Instantiate(m_creaturesToSpawn[i], Vector3.zero, m_creaturesToSpawn[i].transform.rotation);

            NetworkServer.Spawn(go);
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

        if (Input.GetKey(KeyCode.Alpha2))
        {
            //sortFitness(0);
            for (int i = 0; i < m_creatures.Count; i++)
            {
                m_creatures[i].sortFitness();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            var newCreature = Creature_Genetics.Crossover(m_creatures[0], m_creatures[1]);
            m_prefab.GetComponent<Creature_Genetics>().AssignNewChromosomes(newCreature);

            var go = Instantiate(m_prefab, transform.position, transform.rotation) as GameObject;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            m_orderedCreatures = OrderListByDescending(m_creatures);
        }
    }

    List<Creature_Genetics> OrderListByDescending(List<Creature_Genetics> creatures)
    {
        return creatures.OrderByDescending(x => x.fitness).ToList();
    }

    public List<Creature_Genetics> GetCreatures()
    {
        return m_creatures;
    }

    public Creature_Genetics GetRandomCreature()
    {
        Creature_Genetics randomCreature;
        var randomIndex = Random.Range(0, m_creatures.Count - 1);
        randomCreature = m_creatures[randomIndex];
        return randomCreature;
    }

    public void GenerateNewCreature(Creature_Genetics parent1, Creature_Genetics parent2)
    {
        var newCreature = Creature_Genetics.Crossover(parent1, parent2);
        var mutatedCreature = Creature_Genetics.RandomMutation(newCreature);
        UI_Manager.Instance.m_newCreature = mutatedCreature;
    }

    public void SpawnNewCreature(Creature_Genetics newCreature)
    {
        m_prefab.GetComponent<Creature_Genetics>().AssignNewChromosomes(newCreature);
        var go = Instantiate(m_prefab, transform.position, transform.rotation) as GameObject;
    }

    public List<Creature_Genetics> GetNetworkCreatures()
    {
        //List<Creature_Genetics> creatures = new List<Creature_Genetics>();

        //for (int i = 0; i < m_creaturesToSpawn.Count; i++)
        //{
        //    creatures.Add(m_creaturesToSpawn[i].GetComponent<Creature_Genetics>());
        //}

        //return creatures;

        return m_networkedNPCs;
    }

}
