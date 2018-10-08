using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : BaseObject
{
    [SerializeField] Animator DeathVFX;
    [SerializeField] Sprite AltCrystalColor;
    [SerializeField] SpriteRenderer CrystalSprite;

    BaseObject bo;
    bool played = false;

    void Start()
    {
        bo = this.GetComponent<BaseObject>();
        if (bo.m_team == TEAM.BLUE && bo.m_type == TYPE.CRYSTAL)
            CrystalSprite.sprite = AltCrystalColor;
        if (bo.m_team == TEAM.BLUE && bo.m_type == TYPE.KEY)
            CrystalSprite.sprite = AltCrystalColor;
    }
    void Update()
    {
        if (!played)
            if (bo.m_currentState == STATE.DYING)
            {
                DeathVFX.SetTrigger("play");
                played = true;
            }

    }
    new void Initialize()
    {

    }

    new void Attack()
    {

    }

    new void Ability()
    {

    }

}