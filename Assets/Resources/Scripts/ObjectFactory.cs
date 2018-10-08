using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ObjectManager))]
public class ObjectFactory : MonoBehaviour {

    [Header("Game Prefabs")]
    [SerializeField] private GameObject m_KeyPrefab;
    [SerializeField] private GameObject m_CrystalPrefab;
    [SerializeField] private GameObject m_TowerPrefab;
    [SerializeField] private GameObject m_MinionPrefab;
    [SerializeField] private GameObject m_ChampionPrefab;
    [Header("UI Prefabs")]
    [SerializeField] private Transform m_HealthBarCanvas;
    [SerializeField] private GameObject m_HPSmall;
    [SerializeField] private GameObject m_HPMedium;
    [SerializeField] private GameObject m_HPLarge;

    private ObjectManager m_ObjectManager;

    // Use this for initialization
    void Start () {

        m_ObjectManager= GetComponent<ObjectManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadMinion(TEAM team, Vector2 position)
    {
        GameObject new_HealthBar = Instantiate(m_HPSmall);
        GameObject new_Minion = Instantiate(m_MinionPrefab);
        new_Minion.transform.position = position;
        BaseObject obj = new_Minion.GetComponent<BaseObject>();

        new_HealthBar.transform.parent = m_HealthBarCanvas;
        new_HealthBar.transform.localScale *= m_HealthBarCanvas.transform.localScale.x;
        new_HealthBar.GetComponent<UIAnchor>().objectToFollow = new_Minion.transform;

        new_HealthBar.GetComponent<SimpleHealthBarInterface>().Init(obj);

        obj.Initialize(m_ObjectManager, team);
        m_ObjectManager.RegisterObject(obj);
    }
    public void LoadStructure(TYPE type, TEAM team, Vector2 position)
    {
        GameObject new_HealthBar = Instantiate(m_HPLarge);

        //What type of structure?
        GameObject new_Structure = null;
        if (type == TYPE.KEY)
            new_Structure = Instantiate(m_KeyPrefab);
        else if (type == TYPE.CRYSTAL)
            new_Structure = Instantiate(m_CrystalPrefab);
        else if (type == TYPE.TOWER)
            new_Structure = Instantiate(m_TowerPrefab);
        
        if (team == TEAM.RED && type == TYPE.TOWER)
            new_Structure.transform.Rotate(new Vector3(0, 0, 180));


        new_Structure.transform.position = position;
        BaseObject obj = new_Structure.GetComponent<BaseObject>();

        new_HealthBar.transform.parent = m_HealthBarCanvas;
        new_HealthBar.transform.localScale *= m_HealthBarCanvas.transform.localScale.x;
        new_HealthBar.GetComponent<UIAnchor>().objectToFollow = new_Structure.transform;

        new_HealthBar.GetComponent<SimpleHealthBarInterface>().Init(obj);

        obj.Initialize(m_ObjectManager, team);
        m_ObjectManager.RegisterObject(obj);
    }
    public void LoadChampion(TYPE type, TEAM team, Vector2 position)
    {
        GameObject new_HealthBar = Instantiate(m_HPMedium);
        GameObject new_Champion = Instantiate(m_ChampionPrefab);
        new_Champion.transform.position = position;
        BaseObject obj = new_Champion.GetComponent<BaseObject>();

        new_HealthBar.transform.parent = m_HealthBarCanvas;
        new_HealthBar.transform.localScale *= m_HealthBarCanvas.transform.localScale.x;
        new_HealthBar.GetComponent<UIAnchor>().objectToFollow = new_Champion.transform;

        new_HealthBar.GetComponent<SimpleHealthBarInterface>().Init(obj);

        obj.Initialize(m_ObjectManager, team);
        m_ObjectManager.RegisterObject(obj);
    }
}