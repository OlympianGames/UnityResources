// MinMaxSlider Attribute made by Hadjime
// https://gist.github.com/Hadjime/6bd70078456cf5edae760bed7c430559

// Script made by OlympianGames
// https://github.com/OlympianGames/UnityResources

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMaxSliderDemo : MonoBehaviour
{
    [Space(25)]
    [MinMaxSlider(0, 5)] public Vector2 minMaxSlider;
    void Start()
    {
        Debug.Log($"{minMaxSlider.x} to {minMaxSlider.y}");
    }
}
