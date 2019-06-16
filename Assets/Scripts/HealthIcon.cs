using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIcon : MonoBehaviour
{
    public Image fillImage;
    public void SetFillAmount(float amount) {
        fillImage.fillAmount = amount;
    }
}
