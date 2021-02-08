using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! @file LineController.cs
 *
 *  @brief A script used for setting up lines
 *
 *  @author Sunan Regi Maunakea
 *
 *  A line controller consists of an array of points and a line
 *  renderer.
 */
public class LineController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Transform[] points;
    public bool isStraight;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    /*!
     *  Set up the points for the line before drawing.
     */
    public void SetUpLine(Transform[] points)
    {
        lineRenderer.positionCount = points.Length;
        this.points = points;
    }

    /*! @brief Update method of the script
     *  
     *  Set the position of the line points
     */
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i].position);
        }
    }
}
