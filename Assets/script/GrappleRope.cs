using UnityEngine;

public class GrappleRope : MonoBehaviour
{
    [Header("General refrences:")]
    public WireScript m_wireScript;
    [SerializeField] LineRenderer m_lineRenderer;

    [Header("General Settings:")]
    [SerializeField] private int percision = 20;
    [Range(0, 100)] [SerializeField] private float straightenLineSpeed = 4;

    [Header("Animation:")]
    public AnimationCurve ropeAnimationCurve;
    [SerializeField] [Range(0.01f, 4)] private float WaveSize = 20;
    float waveSize;

    [Header("Rope Speed:")]
    public AnimationCurve ropeLaunchSpeedCurve;
    [SerializeField] [Range(1, 50)] private float ropeLaunchSpeedMultiplayer = 4;

    float moveTime = 0;

    [SerializeField] bool isGrappling = false;

    bool drawLine = true;
    bool straightLine = true;


    private void Awake()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
        m_lineRenderer.enabled = false;
        m_lineRenderer.positionCount = percision;
        waveSize = WaveSize;
    }

    private void OnEnable()
    {
        moveTime = 0;
        m_lineRenderer.enabled = true;
        m_lineRenderer.positionCount = percision;
        waveSize = WaveSize;
        straightLine = false;
        LinePointToFirePoint();
    }

    private void OnDisable()
    {
        m_lineRenderer.enabled = false;
        isGrappling = false;
    }

    void LinePointToFirePoint()
    {
        for (int i = 0; i < percision; i++)
        {
            m_lineRenderer.SetPosition(i, m_wireScript.m_firepoint.position);
        }
    }

    void Update()
    {
        moveTime += Time.deltaTime;

        if (drawLine)
        {
            DrawRope();
        }
    }

    void DrawRope()
    {
        if (!straightLine)
        {
            if (Mathf.Abs(m_lineRenderer.GetPosition(percision - 1).x - m_wireScript.m_hitPoint.x) > 0.1f)
            {
                Debug.Log($"line: {m_lineRenderer.GetPosition(percision - 1).x}, grapple: {m_wireScript.m_hitPoint.x}");
                DrawRopeWaves();
            }
            else
            {
                straightLine = true;
            }
        }
        else
        {
            if (!isGrappling)
            {
                m_wireScript.Grapple();
                isGrappling = true;
            }
            if (waveSize > 0)
            {
                waveSize -= Time.deltaTime * straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {
                waveSize = 0;
                DrawRopeNoWaves();
            }
        }
    }

    void DrawRopeWaves()
    {
        for (int i = 0; i < percision; i++)
        {
            float delta = (float)i / ((float)percision - 1f);
            Vector2 offset = Vector2.Perpendicular(m_wireScript.m_distanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
            Vector2 targetPosition = Vector2.Lerp(m_wireScript.m_firepoint.position, m_wireScript.m_hitPoint, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(m_wireScript.m_firepoint.position, targetPosition, ropeLaunchSpeedCurve.Evaluate(moveTime) * ropeLaunchSpeedMultiplayer);

            m_lineRenderer.SetPosition(i, currentPosition);
            
            if (i == 119)
            {
                Debug.Log($"current: {currentPosition.x}, target: {targetPosition.x}");
            }
        }
    }

    void DrawRopeNoWaves()
    {
        m_lineRenderer.positionCount = 2;
        m_lineRenderer.SetPosition(0, m_wireScript.m_hitPoint);
        m_lineRenderer.SetPosition(1, m_wireScript.m_firepoint.position);
    }

}
