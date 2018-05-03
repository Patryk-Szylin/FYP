using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatsPanel : MonoBehaviour
{
    public Button m_openStats;


    [Header("Stats Panel")]
    public GameObject m_changeStatsPanel;
    public Button m_SaveButton;

    [Header("Sliders")]
    public Slider m_healthSlider;
    public Slider m_meleeAttackSlider;
    public Slider m_rangedAttackSlider;
    public Slider m_magicAttackSlider;
    public Slider m_fieldOfViewSlider;
    public Slider m_movementSpeedSlider;
    public Slider m_meleeAtkSpeedSlider;
    public Slider m_rangedAtkSpeedSlider;
    public Slider m_magicAtkSpeedSlider;

    [Header("Display Text ")]
    public Text m_healthText;
    public Text m_meleeAttackText;
    public Text m_rangedAttackText;
    public Text m_magicAttackText;
    public Text m_fieldOfViewText;
    public Text m_movementSpeedText;
    public Text m_meleeAtkSpeedText;
    public Text m_rangedAtkSpeedText;
    public Text m_magicAtkSpeedText;


    public void TurnOnStatsPanel()
    {
        if (m_changeStatsPanel.activeSelf == true)
        {
            m_changeStatsPanel.SetActive(false);
            return;
        }


        m_changeStatsPanel.SetActive(true);
    }

    public void TurnOffStatsPanel()
    {
        if (m_changeStatsPanel.activeSelf)
            m_changeStatsPanel.SetActive(false);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_meleeAttackText.text = m_meleeAttackSlider.value.ToString();
        m_rangedAttackText.text = m_rangedAttackSlider.value.ToString();
        m_magicAttackText.text = m_magicAttackSlider.value.ToString();
        m_fieldOfViewText.text = m_fieldOfViewSlider.value.ToString();
        m_movementSpeedText.text = m_movementSpeedSlider.value.ToString();
        m_meleeAtkSpeedText.text = m_meleeAtkSpeedSlider.value.ToString();
        m_rangedAtkSpeedText.text = m_rangedAtkSpeedSlider.value.ToString();
        m_magicAtkSpeedText.text = m_magicAtkSpeedSlider.value.ToString();

        if (UI_Manager.Instance.m_newCreature != null)
        {
            UI_Manager.Instance.m_newCreature.GetComponent<Creature_Brain>().m_meleeDamage = m_meleeAttackSlider.value;
            UI_Manager.Instance.m_newCreature.GetComponent<Creature_Brain>().m_rangeDamage = m_rangedAttackSlider.value;
            UI_Manager.Instance.m_newCreature.GetComponent<Creature_Brain>().m_magicDamage = m_magicAttackSlider.value;
            UI_Manager.Instance.m_newCreature.GetComponent<Creature_Brain>().m_fieldOfViewRange = m_fieldOfViewSlider.value;
            UI_Manager.Instance.m_newCreature.GetComponent<Creature_Brain>().m_meleeAttackSpeed = m_meleeAtkSpeedSlider.value;

        } else
        {
            print("There's no new creature to change stats for");
        }
            


    }




}
