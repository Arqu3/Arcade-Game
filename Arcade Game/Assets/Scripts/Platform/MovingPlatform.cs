using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public float m_MaxX = 4.0f;
    public float m_Speed = 2.0f;
    public bool m_IsOffset = true;

    float m_MinX;
    Vector3 m_CurDir = Vector3.zero;
    Vector3 m_InitPos;

    public enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }

    public Direction m_CurrentDirection = Direction.Right;

	// Use this for initialization
	void Start ()
    {
        m_MinX = m_MaxX * -1;
        m_InitPos = transform.position;

        //Set position if maxX is outside screen
        if (m_InitPos.x + m_MaxX >= 8.6f)
        {
            transform.position = new Vector3(8.6f - m_MaxX, transform.position.y);
            m_InitPos = transform.position;
        }
        else if (m_InitPos.x + m_MinX <= -8.6f)
        {
            transform.position = new Vector3(-8.6f - m_MinX, transform.position.y);
            m_InitPos = transform.position;
        }

        //Random starting direction
        if (Random.Range(0, 2) == 1)
        {
            m_CurrentDirection = Direction.Right;
        }
        else
        {
            m_CurrentDirection = Direction.Left;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //If offset position is active
        if (m_IsOffset)
        {
            if (transform.position.x > m_MaxX + m_InitPos.x)
            {
                SetDirection(Direction.Left);
            }
            else if (transform.position.x < m_MinX + m_InitPos.x)
            {
                SetDirection(Direction.Right);
            }
        }
        else
        {
            if (transform.position.x > m_MaxX)
            {
                SetDirection(Direction.Left);
            }
            else if (transform.position.x < m_MinX)
            {
                SetDirection(Direction.Right);
            }
        }

        //What happens during different states
        switch(m_CurrentDirection)
        {
            case Direction.Right:
                m_CurDir = new Vector2(1, 0);
                transform.position += m_CurDir * m_Speed * Time.deltaTime;
                break;

            case Direction.Left:
                m_CurDir = new Vector2(-1, 0);
                transform.position += m_CurDir * m_Speed * Time.deltaTime;
                break;

            case Direction.Up:
                m_CurDir = new Vector2(0, 1);
                transform.position += m_CurDir * m_Speed * Time.deltaTime;
                break;

            case Direction.Down:
                m_CurDir = new Vector2(0, -1);
                transform.position += m_CurDir * m_Speed * Time.deltaTime;
                break;
        }
	}

    void SetDirection(Direction dir)
    {
        m_CurrentDirection = dir;
    }

    void SetSpeed(float temp)
    {
        m_Speed = temp;
    }
}
