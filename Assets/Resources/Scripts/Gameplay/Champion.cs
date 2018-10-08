using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Champion : BaseObject
{
    [SerializeField] ParticleSystem DeathVFX;
    [SerializeField] SpriteRenderer sprite;
    // Use this for initialization
    void Start()
    {
        bo = this.GetComponent<BaseObject>();
    }

    // Update is called once per frame
    BaseObject bo;
    bool played = false;
    void Update()
    {
        if (!played)
            if (bo.m_currentState == STATE.DYING)
            {
                DeathVFX.Play();
                StartCoroutine("FadeOut");
                sprite.color = new Color(0, 0, 0, 0); 
                played = true;
            }

    }

    Color white = Color.white;
    IEnumerator FadeOut()
    {
        while (white.a > 0)
        {
            white.a -= Time.deltaTime;
            sprite.color = white;
            yield return null;
        }
    }

}

