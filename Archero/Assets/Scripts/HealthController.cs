using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public float totalHP;
    public float currentHP;

    public bool isDead;

    public Image hpBar;
    public Text hpVal;

    private void OnEnable()
    {
        isDead = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.fillAmount = currentHP / totalHP;
        hpVal.text = currentHP.ToString();

        if (currentHP <= 0)
            isDead = true;
    }

    public void TakeDamage(float dmg)
    {
        Debug.Log("TakeDamage");
        currentHP -= dmg;
    }

    public void Heal(int amt)
    {
        if ((currentHP + amt) <= totalHP)
        {
            currentHP += amt;
        }
    }
}
