﻿using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehavior : MonoBehaviour {
    public Slider slider;

    public void SetHealth(int health) {
        slider.value = health;
    }

    public void SetMaxHealth(int health) {
        slider.maxValue = health;
    }

    public int GetHealth() {
        return (int) slider.value;
    }
}
