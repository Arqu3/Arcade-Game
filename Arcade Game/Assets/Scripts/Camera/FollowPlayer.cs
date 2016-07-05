using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public float m_DampTime = 0.15f;

    public enum Mode
    {
        Damp,
        Instant,
        OnlyY,
        OnlyYUp,
        Still
    }
    public Mode m_CurrentMode = Mode.Instant;

    GameObject m_Player;
    Vector3 m_Vel = Vector3.zero;
    float m_HighestY = 0.0f;
    float m_CurrentY = 0.0f;
    bool m_HookFollow = false;

	// Use this for initialization
	void Start ()
    {
        m_Player = GameObject.Find("Player");
        transform.position = new Vector3(0, 0, -10.0f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch(m_CurrentMode)
        {
            //Follow player slowly
            case Mode.Damp:
                if (m_Player)
                {
                    Vector3 point = Camera.main.WorldToViewportPoint(m_Player.transform.position);
                    Vector3 delta = m_Player.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
                    Vector3 destination = transform.position + delta;
                    destination.z = -10;
                    transform.position = Vector3.SmoothDamp(transform.position, destination, ref m_Vel, m_DampTime);
                }
                break;

            //Follow player instantly
            case Mode.Instant:
                transform.position = new Vector3(m_Player.transform.position.x, m_Player.transform.position.y, -10);
                break;

            //Follow player only on Y axis
            case Mode.OnlyY:
                transform.position = new Vector3(0, m_Player.transform.position.y, -10);
                break;

            //Follow player only on Y axis when moving up
            case Mode.OnlyYUp:
                m_CurrentY = m_Player.transform.position.y;
                if (m_Player.GetComponent<Rigidbody2D>().velocity.y > 0 || m_HookFollow)
                {
                    if (m_CurrentY > m_HighestY)
                    {
                        m_HighestY = m_CurrentY;
                        transform.position = new Vector3(0, m_Player.transform.position.y, -10);
                    }
                }
                break;

            //Camera still
            case Mode.Still:
                transform.position = new Vector3(0, 0, -10);
                break;
        }
	}

    //Help function to let camera follow player when using grappling hook
    void ToggleHookFollow()
    {
        m_HookFollow = !m_HookFollow;
    }
}
