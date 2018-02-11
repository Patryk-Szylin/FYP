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
    public List<Text> m_chromosomeScenarios = new List<Text>();
    public List<Text> m_chromosomeResponses = new List<Text>();

    string[] m_chromosomeScenarioSet = new string[4] { "Attacked By Malee", "Attacked by Range", "Attacked by Group", "Healer Present" };
    string[] m_chromosomeResponseSet = new string[4] { "Retreat", "Hide", "Attack with Blade", "Attack with Magic" };


    private void Update()
    {

    }

    private void Start()
    {

    }




    public void ShowSelectedNPCStats(Creature_Genetics creature)
    {
        m_creatureDamageTaken.text = creature.m_totalDamageReceived.ToString();


    }

}

