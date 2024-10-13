using System.Collections;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    Text scenarioMessage;
    List<Scenario> scenarios = new List<Scenario>();

    Scenario currentScenario;
    int index = 0;

    class Scenario
    {
        public string ScenarioID;
        public List<string> Texts;
        public List<string> Options;
        public string NextScenarioId;
    }
    void Start()
    {
        var scenario01 = new Scenario()
        {
            ScenarioID = "scenario01",
            Texts = new List<string>()
            {
                "テスト文章1",
                "テスト文章2",
                "テスト文章3",
                "テスト文章4",
                "テスト文章5"
            }
        };

        SetScenario(scenario01);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScenario != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("左クリック");
                SetNextMessage();
            }
        }
    }

    void SetScenario(Scenario scenario)
    {
        currentScenario = scenario;
        scenarioMessage.text = currentScenario.Texts[0];
    }

    void SetNextMessage()
    {
        if (currentScenario.Texts.Count > index + 1)
        {
            index++;
            scenarioMessage.text = currentScenario.Texts[index];
        }
        else
        {
            ExitScenario();
        }
    }

    void ExitScenario()
    {
        scenarioMessage.text = "";
        index = 0;
        if (string.IsNullOrEmpty(currentScenario.NextScenarioId))
        {
            currentScenario = null;
        }
        else
        {
            var nextScenario = scenarios.Find(s => s.ScenarioID == currentScenario.NextScenarioId);
            currentScenario = nextScenario;
        }
    }
}
