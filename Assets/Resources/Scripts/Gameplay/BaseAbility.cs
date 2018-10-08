using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum STATUS_EFFECT { ATK_DEBUFF, ATKSPD_DEBUFF, SPEED_DEBUFF, STUN, HEAL, HEAL_OVER_TIME, ATK_BUFF, ATKSPD_BUFF, SPEED_BUFF, DAMAGE, DAMAGE_OVER_TIME };
public enum ABILITY_STATE { PRE_HIT, ON_HIT, POST_HIT };

public class BaseAbility : MonoBehaviour {

    [SerializeField] private float m_Ability_PRE_HIT_duration;
    [SerializeField] private float m_Ability_ON_HIT_duration;
    [SerializeField] private float m_Ability_POST_HIT_duration;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

//ATK_DEBUFF         - amount, duration
//ATKSPD_DEBUFF      - amount, duration
//SPEED_DEBUFF       - amount, duration
//STUN               - amount, duration
//HEAL               - amount, duration
//HEAL_OVER_TIME     - amount, duration, total_ticks
//SHIELD             - amount, duration
//ATK_BUFF           - amount, duration                                                           
//ATKSPD_BUFF        - amount, duration
//SPEED_BUFF         - amount, duration
//DAMAGE             - amount, duration
//DAMAGE_OVER_TIME   - amount, duration, total_ticks