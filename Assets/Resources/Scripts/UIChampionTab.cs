using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum INPUT_TYPE { SUMMON, UNSUMMON, CAST };
public enum TAB_STATE { CHAMP_AVAILABLE, CHAMP_RECHARGE, ABILITY_AVAILABLE, ABILITY_RECHARGE }
public class UIChampionTab : MonoBehaviour {

    private BaseObject Champion;
    private bool isFlipped = false; // or false for ability;
    private bool isAvailable = true;
    private bool isSelected = true;
    private float MIN_UNSUMMON_TIMER = 8f;
    private float MAX_UNSUMMON_TIMER = 20f;

    private Plane m_Plane;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private GameSession m_GameSession;
    [SerializeField] private GameObject m_PlaneMesh;
    [SerializeField] private GameObject m_SpawnIndicator;
    [SerializeField] private Image m_ChampionPortrait;
    [SerializeField] private Image m_AbilityPortrait;
    [SerializeField] private Image m_VFX_isSelected_front;
    [SerializeField] private Image m_VFX_isSelected_back;

    // Use this for initialization
    void Start ()
    {
        m_ChampionPortrait.material.SetColor("_EffectsLayer1Color", transparent);
        m_ChampionPortrait.material.SetColor("_EffectsLayer2Color", transparent);
        m_ChampionPortrait.material.SetColor("_EffectsLayer3Color", transparent);
    }

    Vector3 OFF_SCREEN = new Vector3(-10000, 10000, 0);
	// Update is called once per frame
	void Update ()
    {
        //Detect when there is a mouse click
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;

                // Do something with the object that was hit by the raycast.
                if (dragging)
                {
                    inBounds = true;
                    m_summonPosition = hit.point;
                    m_SpawnIndicator.transform.position = m_summonPosition;
                }

            }
            else
            {
                inBounds = false;
                m_SpawnIndicator.transform.position = OFF_SCREEN;
            }
        }
    }
    private Vector2 m_summonPosition;

    bool dragging = false;
    bool isTapped = false;
    bool inBounds = false;
    Color transparent = new Color(1, 1, 1, 0);
    Color ColorA = new Color(0, 255/225f, 255/225f, 120/255f);
    Color ColorB = new Color(0, 181/225f, 255/225f, 50/255f);
    Color ColorC = new Color(0, 255/225f, 255 / 225f, 150/255f);
    public void OnClicky()
    {
        isTapped = true;
        StartCoroutine("ToggleSelected");
    }
    public void OnMouseUpy()
    {
        if (isAvailable && isFlipped)
        {
            isReturning = false;
            return;
        }

        dragging = false;
        isTapped = false;
        //NETWORK CODE GOES HERE
        //NETWORK CODE GOES HERE

        if (isAvailable && !isFlipped)
        {
            SendInput(INPUT_TYPE.SUMMON, m_summonPosition);
            m_Animator.SetTrigger("flip"); isFlipped = true;
            m_SpawnIndicator.transform.position = OFF_SCREEN;
        }
        //NETWORK CODE GOES HERE
        //NETWORK CODE GOES HERE
        StartCoroutine("VFX_Fade", true);
        Debug.Log("EndDrag");
    }

    bool isReturning = false;
    IEnumerator Returning( )
    {
        Debug.Log("RETURNING");
        float our_time = Mathf.Lerp(MAX_UNSUMMON_TIMER, MIN_UNSUMMON_TIMER, Champion.GetHP() / Champion.GetMAX_HP());
        float timer = 0;
        while ((timer < 2 && isReturning) || isAvailable && isFlipped)
        {
            timer += Time.deltaTime;
            m_AbilityPortrait.fillAmount = timer * 0.5f;
            yield return null;
        }

        while ((timer < our_time && isReturning) || isAvailable && isFlipped)
        {
            timer += Time.deltaTime;
            m_AbilityPortrait.fillAmount = timer * 0.5f;
            yield return null;
        }
    }

    public void OnMouseDowny(string sender)
    {
        if( isAvailable && sender == "return")
        {
            isReturning = true;
            StartCoroutine(Returning());
            return;
        }

        dragging = true;
        StartCoroutine("VFX_Fade", false);


        Debug.Log("StartDrag");
    }
    //VFX Fade on a coroutine for the timer
    IEnumerator VFX_Fade(bool fade_out)
    {
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime * 2;
            if (!fade_out)
            {
                m_ChampionPortrait.material.SetColor("_EffectsLayer1Color", Color.Lerp(transparent, ColorA, timer));
                m_ChampionPortrait.material.SetColor("_EffectsLayer2Color", Color.Lerp(transparent, ColorB, timer));
                m_ChampionPortrait.material.SetColor("_EffectsLayer3Color", Color.Lerp(transparent, ColorC, timer));
                if (!dragging)
                    break;
            } else
            {
                m_ChampionPortrait.material.SetColor("_EffectsLayer1Color", Color.Lerp(ColorA, transparent, timer));
                m_ChampionPortrait.material.SetColor("_EffectsLayer2Color", Color.Lerp(ColorB, transparent, timer));
                m_ChampionPortrait.material.SetColor("_EffectsLayer3Color", Color.Lerp(ColorC, transparent, timer));
            }
            yield return null;
        }
    }

    IEnumerator ToggleSelected()
    {
        isSelected = !isSelected;
        float timer = 0;
        while (timer < 1)
        {
            timer += Time.deltaTime * 2;
            if (isSelected)
            {
                m_VFX_isSelected_back.enabled = true;
                m_VFX_isSelected_front.enabled = true;
                //m_VFX_isSelected_back.material.SetColor("_EffectsLayer1Color", Color.Lerp(transparent, ColorA, timer));
                //m_VFX_isSelected_back.material.SetColor("_EffectsLayer2Color", Color.Lerp(transparent, ColorB, timer));
                //m_VFX_isSelected_back.material.SetColor("_EffectsLayer3Color", Color.Lerp(transparent, ColorC, timer));
                //m_VFX_isSelected_front.material.SetColor("_EffectsLayer1Color", Color.Lerp(transparent, ColorA, timer));
                //m_VFX_isSelected_front.material.SetColor("_EffectsLayer2Color", Color.Lerp(transparent, ColorB, timer));
                //m_VFX_isSelected_front.material.SetColor("_EffectsLayer3Color", Color.Lerp(transparent, ColorC, timer));
            } else
            {
                m_VFX_isSelected_back.enabled = false;
                m_VFX_isSelected_front.enabled = false;
                //m_VFX_isSelected_back.material.SetColor("_EffectsLayer1Color", Color.Lerp(ColorA, transparent, timer));
                //m_VFX_isSelected_back.material.SetColor("_EffectsLayer2Color", Color.Lerp(ColorB, transparent, timer));
                //m_VFX_isSelected_back.material.SetColor("_EffectsLayer3Color", Color.Lerp(ColorC, transparent, timer));
                //m_VFX_isSelected_front.material.SetColor("_EffectsLayer1Color", Color.Lerp(ColorA, transparent, timer));
                //m_VFX_isSelected_front.material.SetColor("_EffectsLayer2Color", Color.Lerp(ColorB, transparent, timer));
                //m_VFX_isSelected_front.material.SetColor("_EffectsLayer3Color", Color.Lerp(ColorC, transparent, timer));
            }
            yield return null;
        }
    }


    void SendInput(INPUT_TYPE input_type, Vector3 position )
    {
        if( input_type == INPUT_TYPE.CAST)
        {

        }else if(input_type == INPUT_TYPE.SUMMON)
        {
            m_GameSession.LoadChampion(m_summonPosition);
        }
        else if (input_type == INPUT_TYPE.UNSUMMON)
        { 

        }
    }
}
