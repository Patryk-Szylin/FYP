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

//// TODO: FOCUS, meaning that if there's a heal
//public enum RESPONSES
//{
//    RETREAT = 0,
//    HIDE,
//    ATTACK_WITH_STEELBLADE,
//    ATTACK_WITH_PROJECTILE,
//    MAX_RESPONSES
//}

public enum RESPONSES
{
    ATTACK,
    HELP_NEARBY_COMRADES,
    FLEE_TO_NEARBY_COMRADES,
    DEFEND
}


public enum ATTACK_RESPONSES
{
    ATTACK_WITH_NORMAL_BLADE,
    ATTACK_WITH_NORMAL_PROJECTILE,
    ATTACK_WITH_FIRE_BLADE,
    ATTACK_WITH_FIRE_PROJECTILE,
    ATTACK_WITH_FIRE_MAGIC,
    ATTACK_WITH_ICE_BLADE,
    ATTACK_WITH_ICE_PROJECTILE,
    ATTACK_WITH_ICE_MAGIC
}

public enum DEFEND_RESPONSES
{
    RESISTANCE_BUFF,
    BLOCK
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

    public void CreateIndividual()
    {
        switch (Random.Range(1, 11))
        {
            case 1:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)ATTACK_RESPONSES.ATTACK_WITH_NORMAL_BLADE;
                break;
            case 2:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)ATTACK_RESPONSES.ATTACK_WITH_NORMAL_PROJECTILE;
                break;
            case 3:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)ATTACK_RESPONSES.ATTACK_WITH_FIRE_BLADE;
                break;
            case 4:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)ATTACK_RESPONSES.ATTACK_WITH_FIRE_PROJECTILE;
                break;
            case 5:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)ATTACK_RESPONSES.ATTACK_WITH_FIRE_MAGIC;
                break;
            case 6:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)ATTACK_RESPONSES.ATTACK_WITH_ICE_BLADE;
                break;
            case 7:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)ATTACK_RESPONSES.ATTACK_WITH_ICE_PROJECTILE;
                break;
            case 8:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)ATTACK_RESPONSES.ATTACK_WITH_ICE_MAGIC;
                break;
            case 9:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)DEFEND_RESPONSES.BLOCK;
                break;
            case 10:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)DEFEND_RESPONSES.RESISTANCE_BUFF;
                break;

        }

        switch (Random.Range(1, 11))
        {
            case 1:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)ATTACK_RESPONSES.ATTACK_WITH_NORMAL_BLADE;
                break;
            case 2:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)ATTACK_RESPONSES.ATTACK_WITH_NORMAL_PROJECTILE;
                break;
            case 3:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)ATTACK_RESPONSES.ATTACK_WITH_FIRE_BLADE;
                break;
            case 4:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)ATTACK_RESPONSES.ATTACK_WITH_FIRE_PROJECTILE;
                break;
            case 5:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)ATTACK_RESPONSES.ATTACK_WITH_FIRE_MAGIC;
                break;
            case 6:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)ATTACK_RESPONSES.ATTACK_WITH_ICE_BLADE;
                break;
            case 7:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)ATTACK_RESPONSES.ATTACK_WITH_ICE_PROJECTILE;
                break;
            case 8:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)ATTACK_RESPONSES.ATTACK_WITH_ICE_MAGIC;
                break;
            case 9:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)DEFEND_RESPONSES.BLOCK;
                break;
            case 10:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)DEFEND_RESPONSES.RESISTANCE_BUFF;
                break;
        }


        switch (Random.Range(1, 11))
        {
            case 1:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)ATTACK_RESPONSES.ATTACK_WITH_NORMAL_BLADE;
                break;
            case 2:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)ATTACK_RESPONSES.ATTACK_WITH_NORMAL_PROJECTILE;
                break;
            case 3:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)ATTACK_RESPONSES.ATTACK_WITH_FIRE_BLADE;
                break;
            case 4:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)ATTACK_RESPONSES.ATTACK_WITH_FIRE_PROJECTILE;
                break;
            case 5:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)ATTACK_RESPONSES.ATTACK_WITH_FIRE_MAGIC;
                break;
            case 6:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)ATTACK_RESPONSES.ATTACK_WITH_ICE_BLADE;
                break;
            case 7:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)ATTACK_RESPONSES.ATTACK_WITH_ICE_PROJECTILE;
                break;
            case 8:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)ATTACK_RESPONSES.ATTACK_WITH_ICE_MAGIC;
                break;
            case 9:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)DEFEND_RESPONSES.BLOCK;
                break;
            case 10:
                m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)DEFEND_RESPONSES.RESISTANCE_BUFF;
                break;
        }

        switch (Random.Range(1, 11))
        {
            case 1:
                m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)ATTACK_RESPONSES.ATTACK_WITH_NORMAL_BLADE;
                break;
            case 2:
                m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)ATTACK_RESPONSES.ATTACK_WITH_NORMAL_PROJECTILE;
                break;
            case 3:
                m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)ATTACK_RESPONSES.ATTACK_WITH_FIRE_BLADE;
                break;
            case 4:
                m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)ATTACK_RESPONSES.ATTACK_WITH_FIRE_PROJECTILE;
                break;
            case 5:
                m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)ATTACK_RESPONSES.ATTACK_WITH_FIRE_MAGIC;
                break;
            case 6:
                m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)ATTACK_RESPONSES.ATTACK_WITH_ICE_BLADE;
                break;
            case 7:
                m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)ATTACK_RESPONSES.ATTACK_WITH_ICE_PROJECTILE;
                break;
            case 8:
                m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)ATTACK_RESPONSES.ATTACK_WITH_ICE_MAGIC;
                break;
            case 9:
                m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)DEFEND_RESPONSES.BLOCK;
                break;
            case 10:
                m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)DEFEND_RESPONSES.RESISTANCE_BUFF;
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
        newCreature.m_chromosomes[4] = parents[randomIndex].m_chromosomes[4];
        newCreature.m_chromosomes[5] = parents[randomIndex].m_chromosomes[5];
        newCreature.m_chromosomes[6] = parents[randomIndex].m_chromosomes[6];
        newCreature.m_chromosomes[7] = parents[randomIndex].m_chromosomes[7];
        newCreature.m_chromosomes[8] = parents[randomIndex].m_chromosomes[8];
        newCreature.m_chromosomes[9] = parents[randomIndex].m_chromosomes[9];
        newCreature.m_chromosomes[10] = parents[randomIndex].m_chromosomes[10];
        newCreature.m_chromosomes[11] = parents[randomIndex].m_chromosomes[11];


        newCreature.m_totalDamageReceived = 0;
        newCreature.m_totalDamageDealt = 0;
        newCreature.fitness = 0;
        return newCreature;
    }


    public static Creature_Genetics RandomMutation(Creature_Genetics unmutatedCreature)
    {
        //// Do I create a new set of possible behaviours that none of the creatures posses?
        //// This would allow me assign chromosomes to creatrues that are unique, hence mutated creature

        Creature_Genetics newCreature = new Creature_Genetics();

        //if (Random.value < 0.05)
        //{
        //    print("GENERATING CREATURE WITH MUTATION");
        //    switch (Random.Range(1, 5))
        //    {
        //        case 1:
        //            newCreature.m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)RESPONSES.RETREAT;
        //            break;
        //        case 2:
        //            newCreature.m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)RESPONSES.HIDE;
        //            break;
        //        case 3:
        //            newCreature.m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)RESPONSES.ATTACK_WITH_STEELBLADE;
        //            break;
        //        case 4:
        //            newCreature.m_chromosomes[(int)SCENARIOS.ATTACKED_BY_MALEE] = (int)RESPONSES.ATTACK_WITH_PROJECTILE;
        //            break;
        //    }

        //    switch (Random.Range(1, 5))
        //    {
        //        case 1:
        //            newCreature.m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)RESPONSES.RETREAT;
        //            break;
        //        case 2:
        //            newCreature.m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)RESPONSES.HIDE;
        //            break;
        //        case 3:
        //            newCreature.m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)RESPONSES.ATTACK_WITH_STEELBLADE;
        //            break;
        //        case 4:
        //            newCreature.m_chromosomes[(int)SCENARIOS.ATTACKED_BY_RANGE] = (int)RESPONSES.ATTACK_WITH_PROJECTILE;
        //            break;
        //    }


        //    switch (Random.Range(1, 5))
        //    {
        //        case 1:
        //            newCreature.m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)RESPONSES.RETREAT;
        //            break;
        //        case 2:
        //            newCreature.m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)RESPONSES.HIDE;
        //            break;
        //        case 3:
        //            newCreature.m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)RESPONSES.ATTACK_WITH_STEELBLADE;
        //            break;
        //        case 4:
        //            newCreature.m_chromosomes[(int)SCENARIOS.ATTACKED_BY_GROUP] = (int)RESPONSES.ATTACK_WITH_PROJECTILE;
        //            break;
        //    }

        //    switch (Random.Range(1, 5))
        //    {
        //        case 1:
        //            newCreature.m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)RESPONSES.RETREAT;
        //            break;
        //        case 2:
        //            newCreature.m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)RESPONSES.HIDE;
        //            break;
        //        case 3:
        //            newCreature.m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)RESPONSES.ATTACK_WITH_STEELBLADE;
        //            break;
        //        case 4:
        //            newCreature.m_chromosomes[(int)SCENARIOS.HEALER_PRESENT] = (int)RESPONSES.ATTACK_WITH_PROJECTILE;
        //            break;
        //    }

        //    return newCreature;
        //}
        //else
        //{
        //    return unmutatedCreature;
        //}

        return newCreature;
    }

    public void AssignNewChromosomes(Creature_Genetics target)
    {
        for (int i = 0; i < m_chromosomes.Length; i++)
        {
            m_chromosomes[i] = target.m_chromosomes[i];
        }
    }

    public Creature_Genetics AssignChromosomes(Creature_Genetics fromTarget)
    {
        Creature_Genetics newCreature = new Creature_Genetics();

        for (int i = 0; i < m_chromosomes.Length; i++)
        {
            newCreature.m_chromosomes[i] = fromTarget.m_chromosomes[i];
        }

        return newCreature;
    }
}
