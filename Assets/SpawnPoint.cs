using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    private ObjectManager m_objectManager;
    [SerializeField] private TEAM m_Team;
    [SerializeField] private float m_TimeIntervals;


    // Use this for initialization
    void Start()
    {
        m_objectManager = GetComponentInParent<ObjectManager>();
        StartSpawning();

    }
    bool isSpawning = false;
    public void StartSpawning()
    {
        isSpawning = true;
        StartCoroutine(Spawn());
    }
    public void StopSpawning()
    {
        isSpawning = false;
    }
    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1);
        while (isSpawning)
        {
            m_objectManager.LoadObject(m_Team, TYPE.MINION, this.transform.position);
            yield return new WaitForSeconds(m_TimeIntervals);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
