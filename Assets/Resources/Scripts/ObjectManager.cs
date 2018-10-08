using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public List<BaseObject> m_Objects = new List<BaseObject>();
    private ObjectFactory m_ObjFactory;
    // Use this for initialization
    void Start () {
        Debug.Log("Initializing Scene Objects");
		foreach(BaseObject b in m_Objects)
            b.Initialize(this, b.m_team);

        m_ObjFactory = GetComponent<ObjectFactory>();
    }
	
	// Update is called once per frame
	public void LoadObject (TEAM team, TYPE type, Vector2 position)
    {
        if (type == TYPE.MINION)
            m_ObjFactory.LoadMinion(team, position);
        else if (type == TYPE.CHAMPION)
        {
            m_ObjFactory.LoadChampion(type, team, position);
        }
        else if (type == TYPE.CRYSTAL)
        { }
        else if (type == TYPE.TOWER)
        { }
        else if (type == TYPE.KEY)
        { }
        else if (type == TYPE.ABILITY)
        { }
    }
    public void RegisterObject(BaseObject obj)
    {
        if (obj != null)
            m_Objects.Add(obj);
    }
    public void DestroyObjectInGame(BaseObject obj)
    {
        if(m_Objects.Contains(obj))
        {
            m_Objects.Remove(obj);
        }
    }

    public BaseObject FindClosestTarget(BaseObject sender)
    {
        //sort all objects by distance
        Vector2 pos = sender.transform.position;
        List<BaseObject> byDistance = m_Objects;
        byDistance.Sort((v1, v2) => ((Vector2)v1.transform.position - pos).sqrMagnitude.CompareTo(((Vector2)v2.transform.position - pos).sqrMagnitude));
        byDistance.Reverse();
        //get the possible targets out of the sorted list
        List<BaseObject> targetList = new List<BaseObject>();
        foreach(BaseObject possibleTarget in byDistance)
        {
            if( possibleTarget.m_team != sender.m_team && possibleTarget.isAlive == true && possibleTarget.isTargetable == true)
            if( Vector2.Distance(sender.transform.position, possibleTarget.transform.position) > sender.GetATK_RANGE() || possibleTarget.m_type == TYPE.TOWER || possibleTarget.m_type == TYPE.CRYSTAL || possibleTarget.m_type == TYPE.KEY )
            {
                targetList.Add( possibleTarget);
            }
        }

        BaseObject finalTarget = null;
        foreach(BaseObject target in targetList)
        {
            if( target.m_type == TYPE.CHAMPION && Vector2.Distance(target.transform.position, sender.transform.position) < sender.GetAGGRO())
                finalTarget = target;
            else if( target.m_type == TYPE.MINION && Vector2.Distance(target.transform.position, sender.transform.position) < sender.GetAGGRO())
                finalTarget = target;
            else if (target.m_type == TYPE.TOWER)
                finalTarget = target;
            else if (target.m_type == TYPE.CRYSTAL)
                finalTarget = target;
            else if (target.m_type == TYPE.KEY)
                finalTarget = target;
            
        }
        return finalTarget;
    }

    

}


