using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! @file DrawLine.cs
 *
 *  @brief A script used for drawing the line
 *
 *  @author Sunan Regi Maunakea
 *
 *  A class consists of an array of the positions of each points.
 */
public class DrawLine : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] private LineController line;

    /*! @brief Start method of the script
     *  
     *  Setting up line using the line controller
     */
    void Start()
    {
        line.SetUpLine(points);
    }

    /*!
     *  Set the points to draw the line
     */
    public void SetPoints(Transform[] points)
    {
        this.points = points;
    }
}
