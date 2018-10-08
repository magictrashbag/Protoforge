using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleHealthBarInterface : MonoBehaviour
{

    [SerializeField] public BaseObject TARGET;
    Fizzik.HealthBarComponent HealthBar;
    [SerializeField] private Material HealthBarMaterial;
    [SerializeField] Fizzik.HealthBarComponent hpbar;

    private void Start()
    {
        if( TARGET == null)
        {
            TARGET = GetComponent<UIAnchor>().objectToFollow.GetComponent<BaseObject>();
        }
    }
    bool isInit = false;
    public void Init(BaseObject obj)
    {
        TARGET = obj;
        if (TARGET == null)
        {
            TARGET = GetComponent<UIAnchor>().objectToFollow.GetComponent<BaseObject>();
        }
        TARGET.PlugHealthBar(this);
        //health bar cant be child of moving shit
        HealthBar = GetComponentInChildren<Canvas>().GetComponentInChildren<Fizzik.HealthBarComponent>();
        hpbar.currentHealth = (int)TARGET.GetHP();
        hpbar.totalHealth = (int)TARGET.GetMAX_HP();
        hpbar.GetComponent<Image>().material = HealthBarMaterial;


        isInit = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isInit) return;


        if (TARGET.isAlive == false)
            Destroy(this.gameObject);

        hpbar.currentHealth = (int)TARGET.GetHP();
        CenterHealthBar();
    }

    public void CenterHealthBar()
    {
        if (HealthBar != null) {
            GetComponent<UIAnchor>().CENTER2();
        }
    }
}