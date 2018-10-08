using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum TEAM { RED, BLUE, NONE };
public enum TYPE { MINION, CHAMPION, TOWER, CRYSTAL, KEY, ABILITY };
public enum STATE { SUMMON, UNSUMMON, IDLE, SEEKING, MOVING, ATTACKING, CASTING, DYING };

public class BaseObject : MonoBehaviour {

    public TEAM m_team;
    public TYPE m_type;
    public STATE m_currentState;

    [Header("Attributes")]
    public bool isAlive = true;
    public bool isTargetable = true;
    [SerializeField] // Damage given per attack
    private float HP = 100; public float GetHP() { return HP; }
    [SerializeField] // Damage given per attack
    private float MAX_HP = 100; public float GetMAX_HP() { return MAX_HP; }
    [SerializeField] // Damage given per attack
    private float ATK = 10f; public float GetATK() { return ATK; }
    [SerializeField] // Attacks per second 0.7 = slow | 1.8 = fast
    private float ATK_SPD = 1; public float GetATK_SPD() { return ATK_SPD; }
    [SerializeField] // min distance to start attacking
    private float ATK_RANGE = 1f; public float GetATK_RANGE() { return ATK_RANGE; }
    [SerializeField] // how fast i move
    private float SPEED = 10f; public float GetSPEED() { return SPEED; }
    [SerializeField] // max respawn timer
    private float COOLDOWN = 15f; public float GetCOOLDOWN() { return COOLDOWN; }
    [SerializeField] // aggro radius
    private float AGGRO = 1f; public float GetAGGRO() { return AGGRO; }

    [Header("Spawn Settings")]
    [SerializeField] private float SPAWN_TIMER = 1f;

    private ObjectManager m_ObjectManager;
    [SerializeField] private BaseObject m_CurrentTarget;
    private AIDestinationSetter m_AIPathfinder;

    // Use this for initialization
    void Start () {
		
	}
	
    public void Initialize(ObjectManager Instance, TEAM team)
    {

        m_ObjectManager = Instance;
        m_team = team;
        m_AIPathfinder = GetComponent<AIDestinationSetter>();
        StartCoroutine(StateMachine());
    }

    //STATE { SPAWN, UNSUMMON, IDLE, SEEKING, MOVING, ATTACKING, CASTING, DYING };
    bool isRunning = true;
    IEnumerator StateMachine()
    {
        while (isRunning) {
            yield return StartCoroutine(m_currentState.ToString());
        }
    }

    IEnumerator SUMMON()
    {
        yield return new WaitForSeconds(SPAWN_TIMER);
        m_currentState = STATE.SEEKING;
    }
    IEnumerator UNSUMMON()
    {
        yield return new WaitForSeconds(SPAWN_TIMER); 

    }

    IEnumerator IDLE()
    {
        yield return new WaitForSeconds(0.1f);
        m_currentState = STATE.SEEKING;
    }

    IEnumerator SEEKING()
    {
        if(m_CurrentTarget == null)
            m_CurrentTarget = m_ObjectManager.FindClosestTarget(this);

        if (m_CurrentTarget.m_type != TYPE.MINION || m_CurrentTarget.m_type != TYPE.CHAMPION)
            m_CurrentTarget = m_ObjectManager.FindClosestTarget(this);

        if (m_type != TYPE.TOWER || m_type != TYPE.CRYSTAL || m_type != TYPE.KEY) {
            if (m_AIPathfinder.SetTarget(m_CurrentTarget.transform)) { 
                m_currentState = STATE.MOVING;
            }
        }
        if (m_CurrentTarget == null)
            m_currentState = STATE.IDLE;

        yield return null;
        if (!isAlive) m_currentState = STATE.DYING;
    }

    IEnumerator MOVING()
    {
        if (!m_CurrentTarget.isAlive || !m_CurrentTarget.isTargetable)
        {
            m_currentState = STATE.SEEKING;
        }
        else if(Vector2.Distance(transform.position, m_CurrentTarget.transform.position) < ATK_RANGE)
        {
            m_currentState = STATE.ATTACKING;
            m_AIPathfinder.StopPathfinding();
        }
        else if(Vector2.Distance(transform.position, m_CurrentTarget.transform.position) > AGGRO)
        {
            m_currentState = STATE.SEEKING;
        }

        yield return null;
        if (!isAlive) m_currentState = STATE.DYING;
    }

    IEnumerator ATTACKING()
    {
        yield return new WaitForSeconds(ATK_SPD*0.1f);
        if (m_CurrentTarget.isAlive || m_CurrentTarget.isTargetable) { 
            m_CurrentTarget.StatusEffect(STATUS_EFFECT.DAMAGE, ATK);
        }

        yield return new WaitForSeconds(ATK_SPD * 0.9f);

        if (!m_CurrentTarget.isAlive || !m_CurrentTarget.isTargetable)
            m_currentState = STATE.SEEKING;
        else if (Vector2.Distance(this.transform.position, m_CurrentTarget.transform.position) < ATK_RANGE)
            m_currentState = STATE.MOVING;
        
        if (!isAlive) m_currentState = STATE.DYING;
    }
    IEnumerator CASTING()
    {
        yield return null;
    }

    IEnumerator DYING()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        m_ObjectManager.DestroyObjectInGame(this);
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }

    public void StatusEffect(STATUS_EFFECT effect, float amount)
    {
        StartCoroutine(effect.ToString(), amount);
    }

    public SimpleHealthBarInterface healthBar;
    public void PlugHealthBar(SimpleHealthBarInterface bar)
    {
        healthBar = bar;
    }
    IEnumerator DAMAGE(float amount)
    {
        HP -= amount;
        if ( HP <= 0)
        {
            HP = 0;
            isAlive = false;
            m_currentState = STATE.DYING;
        }
        yield return null;
    }

    void Attack()
    {

    }
    void Ability()
    {
        
    }

   
}
