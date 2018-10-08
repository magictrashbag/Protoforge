using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugVFX : MonoBehaviour {

    public LineRenderer m_Laser;
    public ParticleSystem m_HitFx;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DrawLaser(Vector2 from, Vector2 to)
    {
        yield return new WaitForSeconds(5);
        float timer = 1;
        m_Laser.SetPosition(0, from);
        m_Laser.SetPosition(1, to);
        m_HitFx.transform.position = to;
        m_HitFx.transform.LookAt(from);

        while (timer > 0)
        {
            m_Laser.startWidth = 0;
            m_Laser.endWidth = 0;
            timer -= Time.deltaTime;
            yield return null;
        }


        yield return null;
    }

}
