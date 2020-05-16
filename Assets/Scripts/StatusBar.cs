using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public PlayerController playerController;
    public Image fillImage;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value <= slider.minValue)
        {
            fillImage.enabled = false;
        }

        if (slider.value > slider.minValue && !(fillImage.enabled))
        {
            fillImage.enabled = true;
        }
        float maxHealth = 100;
        float fillValue = playerController.health / maxHealth;
        slider.value = fillValue;
    }
}
