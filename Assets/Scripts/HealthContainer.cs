using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthContainer : MonoBehaviour
{
    private List<HealthIcon> healthIcons = new List<HealthIcon>();

    void UpdateHealthList() {
        healthIcons.Clear();
        for(var i = 0 ; i < transform.childCount ; i++)
        {
            var child = transform.GetChild(i);
            var healthIcon = child.GetComponent<HealthIcon>();
            healthIcons.Add(healthIcon);
        }
    }

    public void SetHealth(float health) {
        UpdateHealthList();
        for(var i = 0 ; i < healthIcons.Count ; i++)
        {
            var healthIcon = healthIcons[i].GetComponent<HealthIcon>();
            var fill = Mathf.Clamp01(health - i);
            healthIcon.SetFillAmount(fill);
        }
    }
}
