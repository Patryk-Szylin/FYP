using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class UI_Manager : MonoBehaviour
{
    #region Singleton


    private static UI_Manager _instance;
    public static UI_Manager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UI_Manager>();

                // If the GameObject with GameManager wasn't found, create one.
                if (_instance == null)
                    _instance = new GameObject("UIManager").AddComponent<UI_Manager>();
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


    public Text m_creatureName;
    public Text m_creatureDamageTaken;
    public Text m_creatureDamageDealt;
    public Text m_creatureFitness;
    public List<Text> m_chromosomeScenarios = new List<Text>();
    public List<Text> m_chromosomeResponses = new List<Text>();

    string[] m_chromosomeScenarioSet = new string[4] { "Attacked By Malee", "Attacked by Range", "Attacked by Group", "Healer Present" };
    string[] m_chromosomeResponseSet = new string[10] 
    {
        "Attack using Normal Blade",
        "Attack using Normal Projectile",
        "Attack using Fire Blade",
        "Attack using Fire Projectile",
        "Attack using Fire Magic",
        "Attack using Ice Blade",
        "Attack using Ice Projectile",
        "Attack using Ice Magic",
        "Block attacks",
        "Defend using Resistance Buff"        
    };

    // References
    public UI_StatsPanel m_statsPanel;    


    [Header("Create new creature panel variables")]
    public GameObject m_createNewCreaturePanel;
    [Space]
    public Text m_creature1DmgTaken;
    public Text m_creature1DmgDealt;
    public Text m_creature1Fitness;
    public List<Text> m_creature1Responses = new List<Text>();
    [Space]
    public Text m_creature2DmgTaken;
    public Text m_creature2DmgDealt;
    public Text m_creature2Fitness;
    public List<Text> m_creature2Responses = new List<Text>();
    [Space]
    public List<Text> m_newCreatureResponses = new List<Text>();


    private Creature_Genetics _creature1;
    private Creature_Genetics _creature2;
    public Creature_Genetics m_newCreature;


    public void ToggleCreateNewCreaturePanel()
    {
        if (m_createNewCreaturePanel.activeSelf == true)
            return;

        m_createNewCreaturePanel.SetActive(true);
    }

    public void TurnOffNewCreaturePanel()
    {
        if (m_createNewCreaturePanel.activeSelf)
            m_createNewCreaturePanel.SetActive(false);
    }


    private void Start()
    {
        PopulateScenarioTextElements();
        m_statsPanel = GetComponent<UI_StatsPanel>();
    }

    public void PopulateScenarioTextElements()
    {
        for (int i = 0; i < m_chromosomeScenarios.Count; i++)
        {
            m_chromosomeScenarios[i].text = m_chromosomeScenarioSet[i];
        }
    }

    public void ShowSelectedNPCStats(Creature_Genetics creature)
    {
        m_creatureDamageTaken.text = creature.m_totalDamageReceived.ToString();
        m_creatureDamageDealt.text = creature.m_totalDamageDealt.ToString();
        m_creatureFitness.text = creature.fitness.ToString();

        for (int i = 0; i < creature.m_chromosomes.Length; i++)
        {
            var chromosomeIndex = creature.m_chromosomes[i];
            m_chromosomeResponses[i].text = m_chromosomeResponseSet[chromosomeIndex];
        }
    }


    public void DisplayCreature1()
    {
        var creature = GaSystem.Instance.GetRandomCreature();
        m_creature1DmgDealt.text = creature.m_totalDamageDealt.ToString();
        m_creature1DmgTaken.text = creature.m_totalDamageReceived.ToString();
        m_creature1Fitness.text = creature.fitness.ToString();

        for (int i = 0; i < creature.m_chromosomes.Length; i++)
        {
            var chromosomeIndex = creature.m_chromosomes[i];
            m_creature1Responses[i].text = m_chromosomeResponseSet[chromosomeIndex];
        }
        _creature1 = creature;
    }

    public void DisplayCreature2()
    {
        var creature = GaSystem.Instance.GetRandomCreature();
        m_creature2DmgDealt.text = creature.m_totalDamageDealt.ToString();
        m_creature2DmgTaken.text = creature.m_totalDamageReceived.ToString();
        m_creature2Fitness.text = creature.fitness.ToString();

        for (int i = 0; i < creature.m_chromosomes.Length; i++)
        {
            var chromosomeIndex = creature.m_chromosomes[i];
            m_creature2Responses[i].text = m_chromosomeResponseSet[chromosomeIndex];
        }

        _creature2 = creature;
    }

    public void SpawnNewCreature()
    {

        GaSystem.Instance.SpawnNewCreature(m_newCreature);

    }

    public void GenerateNewCreature()
    {
        if (_creature1 && _creature2)
        {
            GaSystem.Instance.GenerateNewCreature(_creature1, _creature2);
            print("GENERATED NEW CREATURE");
            for (int i = 0; i < m_newCreature.m_chromosomes.Length; i++)
            {
                var chromosomeIndex = m_newCreature.m_chromosomes[i];
                m_newCreatureResponses[i].text = m_chromosomeResponseSet[chromosomeIndex];
            }
        }

    }




}

