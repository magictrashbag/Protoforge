using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpritesheetAnimator : MonoBehaviour
{
    private Transform parent;
    private SpriteRenderer m_SpriteRenderer;
    private Animator m_Animator;
    private BaseObject m_BaseObject;
    // Use this for initialization
    void Start () {
        parent = GetComponentInParent<Transform>();
        m_SpriteRenderer = GetComponentInParent<SpriteRenderer>();
        m_Animator = GetComponent<Animator>();
        m_BaseObject = parent.parent.GetComponent<BaseObject>();
    }
    Vector3 cameraAngle = Vector3.right;

    // Update is called once per frame
    Vector2[] Cardinals = new Vector2[]
        {
            new Vector2(0, 1),
            new Vector2(1, 1),
            new Vector2(1, 0),
            new Vector2(1, -1),
            new Vector2(0, -1),
            new Vector2(-1, -1),
            new Vector2(-1, 0),
            new Vector2(-1, 1),
        };
    Vector2 degToCompass(float angle)
    {
        float val = Mathf.Floor((angle / 45f) + 0.5f);
        return Cardinals[(int)(val % 8)];
    }
    STATE previous_state = STATE.IDLE;
    STATE state = STATE.IDLE;
    void Update ()
    {
        transform.rotation = Quaternion.identity;
        transform.Rotate(cameraAngle * 40);
        if (m_Animator)
        {
            float angle;
            Vector3 vect = new Vector3(0, 0, 1);
            //parent.rotation.ToAngleAxis(out angle, out vect);

            //set the angle in the animation compotnent
            angle = parent.parent.transform.eulerAngles.z;
            Vector2 xy = degToCompass(angle);
            m_Animator.SetFloat("DirectionX", Mathf.RoundToInt(-xy.x));
            m_Animator.SetFloat("DirectionY", Mathf.RoundToInt(xy.y));

            previous_state = state;
            state = m_BaseObject.m_currentState;

            if (m_BaseObject.m_type == TYPE.CHAMPION)
            {
                if (state == STATE.MOVING || state == STATE.SEEKING )
                    m_Animator.SetBool("walking", true);
                else if (state == STATE.ATTACKING && previous_state != STATE.ATTACKING)
                    m_Animator.SetTrigger("attack");
                else
                    m_Animator.SetBool("walking", false);
                
            }
        }
    }
    
}
