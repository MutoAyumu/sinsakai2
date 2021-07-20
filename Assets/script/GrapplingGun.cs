using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("Scripts:")]
    public GrappleRope grappleRope;
    [Header("Layer Settings:")]
    //[SerializeField] private bool grappleToAll = false;
    //[SerializeField] private int grappableLayerNumber = 9;
    [SerializeField] LayerMask m_grappableLayer = 0;

    //[Header("Main Camera")]
    //public Camera m_camera;

    [Header("Transform Refrences:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 80)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = true;
    [SerializeField] private float maxDistance = 4;

    [Header("Launching")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType Launch_Type = LaunchType.Physics_Launch;
    [Range(0, 5)] [SerializeField] private float launchSpeed = 5;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoCongifureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequency = 3;
    [SerializeField] float m_ropeLength = 5f;
    [SerializeField] Transform m_grapplePoint = default;

    float m_Rh;
    float m_Rv;

    private enum LaunchType
    {
        //Transform_Launch,
        Physics_Launch,
    }

    [Header("Component Refrences:")]
    public SpringJoint2D m_springJoint2D;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 DistanceVector;
    //Vector2 Mouse_FirePoint_DistanceVector;
    //new Vector2(m)

    public Rigidbody2D ballRigidbody;
    float m_rb;

    private void Start()
    {
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
        m_rb = ballRigidbody.gravityScale;
    }

    private void Update()
    {
        //Vector2 Mouse_FirePoint_DistanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        m_Rh = Input.GetAxisRaw("Horizontal");
        m_Rv = Input.GetAxisRaw("Vertical");
        Debug.DrawRay(firePoint.position, new Vector2(m_Rh,m_Rv), Color.green);

        if (Input.GetButtonDown("Fire1"))
        {
            SetGrapplePoint();
        }
        //else if (Input.GetButton("Fire1"))
        //{
            //if (grappleRope.enabled)
            //{
            //    RotateGun(grapplePoint, false);
            //}
            //else
            //{
            //    RotateGun(new Vector3(m_Rh,m_Rv,0), false);
            //}

            //if (launchToPoint && grappleRope.isGrappling)
            //{
                //if (Launch_Type == LaunchType.Transform_Launch)
                //{
                //    gunHolder.position = Vector3.Lerp(gunHolder.position, grapplePoint, Time.deltaTime * launchSpeed);
                //}
            //}

        //}
        else if (Input.GetButtonUp("Fire1"))
        {
            grappleRope.enabled = false;
            m_springJoint2D.enabled = false;
            ballRigidbody.gravityScale = m_rb;
            m_grapplePoint.gameObject.SetActive(false);
        }
        //else
        //{
            //RotateGun(new Vector3(m_Rh, m_Rv, 0), true);
        //}
    }

    //void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    //{
    //    Vector3 distanceVector = lookPoint - gunPivot.position;

    //    float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
    //    if (rotateOverTime && allowRotationOverTime)
    //    {
    //        Quaternion startRotation = gunPivot.rotation;
    //        gunPivot.rotation = Quaternion.Lerp(startRotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
    //    }
    //    else
    //        gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    //}

    void SetGrapplePoint()
    {
        if (Physics2D.Raycast(firePoint.position, new Vector2(m_Rh,m_Rv).normalized, m_ropeLength, m_grappableLayer))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, new Vector2(m_Rh,m_Rv).normalized,m_ropeLength, m_grappableLayer);
            if (((Vector2.Distance(_hit.point, firePoint.position) <= maxDistance) || !hasMaxDistance))
            {
                grapplePoint = _hit.point;
                DistanceVector = grapplePoint - (Vector2)gunPivot.position;
                grappleRope.enabled = true;
                m_grapplePoint.gameObject.SetActive(true);
                m_grapplePoint.position = grapplePoint;
            }
        }
    }

    public void Grapple()
    {

        if (!launchToPoint && !autoCongifureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequency;
        }

        if (!launchToPoint)
        {
            if (autoCongifureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
            }
            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
        }

        else
        {
            //if (Launch_Type == LaunchType.Transform_Launch)
            //{
            //    ballRigidbody.gravityScale = 0;
            //    ballRigidbody.velocity = Vector2.zero;
            //}
            if (Launch_Type == LaunchType.Physics_Launch)
            {
                m_springJoint2D.connectedAnchor = grapplePoint;
                m_springJoint2D.distance = 0;
                m_springJoint2D.frequency = launchSpeed;
                m_springJoint2D.enabled = true;
                ballRigidbody.gravityScale = 0;
            }
        }
    }

    private void OnDrawGizmos()//グラップルの範囲を視覚的に表示
    {
        if (hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistance);
        }
    }

}
