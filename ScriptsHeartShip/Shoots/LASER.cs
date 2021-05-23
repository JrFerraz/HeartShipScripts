using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LASER : MonoBehaviour
{
    [SerializeField] private float defDistanceRay = 100f;
    public Transform laserFirePoint;
    public Transform laserFirePointI;
    public LineRenderer m_lineRenderer;
    public LineRenderer m_lineRendererI;
    Transform m_transform;
    Transform m_transformI;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        m_transformI = GetComponent<Transform>();
    }

    private void Update()
    {
        shootLaser();
    }


    void shootLaser()
    {
        if (Physics2D.Raycast(m_transform.position, laserFirePoint.up))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, laserFirePoint.up);
            Draw2DRay(laserFirePoint.position, _hit.point);
        }
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.up * defDistanceRay);
        }
        if (Physics2D.Raycast(m_transformI.position, laserFirePointI.up))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, transform.up);
            Draw2DRay(laserFirePoint.position, _hit.point);
        }
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.up * defDistanceRay);
        }
    }
    
    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }
}
