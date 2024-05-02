using CrashKonijn.Goap.Behaviours;
using CrashKonijn.Goap.Classes.Validators;
using TMPro;
using UnityEngine;
using HTN;

namespace Behaviours.HTN
{
    public class TextBehaviour : MonoBehaviour
    {
        private TextMeshProUGUI text;
        private AIAgent agent;
        private HungerBehaviour hunger;

        private void Awake()
        {
            this.text = this.GetComponentInChildren<TextMeshProUGUI>();
            this.agent = this.GetComponent<AIAgent>();
            this.hunger = this.GetComponent<HungerBehaviour>();
        }

        private void Update()
        {
            this.text.text = this.GetText();
        }

        private string GetText()
        {
            if (this.agent.CurrentTask is null)
                return "Idle";

            return $"{this.agent.CurrentTask.Name}\n{this.agent.State}\nhunger: {this.hunger.hunger:0.00}";
        }
    }
}