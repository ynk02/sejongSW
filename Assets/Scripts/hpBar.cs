using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public Transform hpBarTransform;
    
    public void Start()
    {
        hpBarTransform = gameObject.transform;
    }

    // 체력 정보를 설정하여 HP바 크기 변경
    public void SetHealth(float currentHealth, float maxHealth)
    {
        float hpRatio = currentHealth / maxHealth; // 체력 비율 계산
        hpBarTransform.localScale = new Vector3(hpRatio, 0.2f, 0); // X축 크기를 체력 비율에 맞게 조절
    }
}
