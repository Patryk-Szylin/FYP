using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SCENARIOS
{
    ATTACKED_BY_MALEE = 0,
    ATTACKED_BY_RANGE,
    ATTACKED_BY_GROUP,
    HEALER_PRESENT
}

public enum RESPONSES
{
    RETREAT = 0,
    HIDE,
    ATTACK_WITH_BLADE,
    ATTACK_WITH_MAGIC,
    MAX_RESPONSES
}

public class Creature_Genetics : MonoBehaviour
{
    public const int NO_OF_CHROMOSOMES = 4;
    //public const int POPULATION_SIZE = 100;
    //public Creature[] population = new Creature[POPULATION_SIZE];
    public int[] m_chromosomes = new int[NO_OF_CHROMOSOMES];

    public float m_totalDamageDealt;
    public float m_totalDamageReceived;
    public float fitness;


    public void CreateIndividual(int id)
    {
        switch (Random.Range(1, 4))
        {
            case 1:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)RESPONSES.RETREAT;
                break;
            case 2:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)RESPONSES.HIDE;
                break;
            case 3:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)RESPONSES.ATTACK_WITH_BLADE;
                break;
            case 4:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)RESPONSES.ATTACK_WITH_MAGIC;
                break;
        }

        switch (Random.Range(1, 4))
        {
            case 1:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)RESPONSES.RETREAT;
                break;
            case 2:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)RESPONSES.HIDE;
                break;
            case 3:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)RESPONSES.ATTACK_WITH_BLADE;
                break;
            case 4:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)RESPONSES.ATTACK_WITH_MAGIC;
                break;
        }


        switch (Random.Range(1, 4))
        {
            case 1:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)RESPONSES.RETREAT;
                break;
            case 2:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)RESPONSES.HIDE;
                break;
            case 3:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)RESPONSES.ATTACK_WITH_BLADE;
                break;
            case 4:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)RESPONSES.ATTACK_WITH_MAGIC;
                break;
        }

        switch (Random.Range(1, 4))
        {
            case 1:
                m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)RESPONSES.RETREAT;
                break;
            case 2:
                m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)RESPONSES.HIDE;
                break;
            case 3:
                m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)RESPONSES.ATTACK_WITH_BLADE;
                break;
            case 4:
                m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)RESPONSES.ATTACK_WITH_MAGIC;
                break;
        }
    }

    public void sortFitness()
    {
        //for (int i = 0; i < population_size; i++)
        //{
        //    fitness = m_totalDamageDealt / m_totalDamageReceived;
        //}

        this.fitness = m_totalDamageDealt / m_totalDamageReceived;
    }

    public static Creature_Genetics Crossover(Creature_Genetics parent1, Creature_Genetics parent2)
    {
        Creature_Genetics newCreature = new Creature_Genetics();

        // TODO: Randomly pick chromosome between parent 1 and 2
        List<Creature_Genetics> parents = new List<Creature_Genetics>();
        parents.Add(parent1);
        parents.Add(parent2);
        var randomIndex = Random.Range(0, parents.Count - 1);


        // Mutate creature before assigning chromosomes
        // RandomMutation();

        // Todo: This is temporary CrossOver function and I need to pick parents baseed on fitness.
        newCreature.m_chromosomes[0] = parents[randomIndex].m_chromosomes[0];
        newCreature.m_chromosomes[1] = parents[randomIndex].m_chromosomes[1];
        newCreature.m_chromosomes[2] = parents[randomIndex].m_chromosomes[2];
        newCreature.m_chromosomes[3] = parents[randomIndex].m_chromosomes[3];
        newCreature.m_totalDamageReceived = 0;
        newCreature.m_totalDamageDealt = 0;
        newCreature.fitness = 0;
        return newCreature;
    }


    public void RandomMutation()
    {
        // Do I create a new set of possible behaviours that none of the creatures posses?
        // This would allow me assign chromosomes to creatrues that are unique, hence mutated creature

    }

    public void AssignNewChromosomes(Creature_Genetics target)
    {
        m_chromosomes = target.m_chromosomes;
    }
}
