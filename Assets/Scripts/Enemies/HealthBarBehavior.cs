using UnityEngine;
using UnityEngine.UI;

namespace Enemies {
    public class HealthBarBehavior : MonoBehaviour {
        public Slider slider;

        public void SetHealth(int health) {
            if (health == (int) slider.maxValue) {
                slider.gameObject.SetActive(false);
            }
            else {
                slider.gameObject.SetActive(true);
            }
            slider.value = health;
        }
    
        public void SetMaxHealth(int health) {
            slider.maxValue = health;
        }

        public int GetHealth() {
            return (int) slider.value;
        }
    }
}