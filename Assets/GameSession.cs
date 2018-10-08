using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour {

    private ObjectManager m_objectManager;
    private ObjectFactory m_objectFactory;

    [Header("Red Side Settings")]
    [SerializeField] private Transform[] m_RED_TowerSpawns;
    [SerializeField] private Transform[] m_RED_CrystalSpawns;
    [SerializeField] private Transform m_RED_KeySpawn;

    [Header("Blue Side Settings")]
    [SerializeField] private Transform[] m_BLUE_TowerSpawns;
    [SerializeField] private Transform[] m_BLUE_CrystalSpawns;
    [SerializeField] private Transform   m_BLUE_KeySpawn;

    // Use this for initialization
    void Start () {

        StartCoroutine("LOAD_LEVEL");

        //assign players

        //load champions oob



    }
	IEnumerator LOAD_LEVEL()
    {
        yield return new WaitForSeconds(1);
        m_objectManager = GetComponent<ObjectManager>();
        m_objectFactory = GetComponent<ObjectFactory>();
        //Load structures for each team at spawn points
        //Red Side
        m_objectFactory.LoadStructure(TYPE.KEY, TEAM.RED, m_RED_KeySpawn.position); // Key
        foreach (Transform spawnPoint in m_RED_TowerSpawns)
            m_objectFactory.LoadStructure(TYPE.TOWER, TEAM.RED, spawnPoint.position); // Towers
        foreach (Transform spawnPoint in m_RED_CrystalSpawns)
            m_objectFactory.LoadStructure(TYPE.CRYSTAL, TEAM.RED, spawnPoint.position); // Crystals

        //Blue Side
        m_objectFactory.LoadStructure(TYPE.KEY, TEAM.BLUE, m_BLUE_KeySpawn.position); // Key
        foreach (Transform spawnPoint in m_BLUE_TowerSpawns)
            m_objectFactory.LoadStructure(TYPE.TOWER, TEAM.BLUE, spawnPoint.position); // Towers
        foreach (Transform spawnPoint in m_BLUE_CrystalSpawns)
            m_objectFactory.LoadStructure(TYPE.CRYSTAL, TEAM.BLUE, spawnPoint.position); // Crystals



    }

    public void LoadChampion(Vector3 position)
    {
        m_objectManager.LoadObject(TEAM.BLUE, TYPE.CHAMPION, position);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
