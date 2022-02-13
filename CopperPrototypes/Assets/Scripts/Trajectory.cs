using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Trajectory : MonoBehaviour
{
    // Reference to line renderer component
    protected LineRenderer lr;

    // Array of point positions
    protected Vector3[] linePoints;

    // Reference to the edge collider component
    protected EdgeCollider2D ec;
    // Array of edge collider points
    protected Vector2[] edgePoints;

    protected PlayerMovement playerMovement;

    // Start is called before the first frame update
    protected void Start()
    {
        // Fetch the reference to the line renderer component
        lr = GetComponent<LineRenderer>();

        // Get the number of line points
        int numLinePoints = lr.positionCount;
        // Initialise the points position array
        linePoints = new Vector3[numLinePoints];

        // Fetch the reference to the edge collider component
        // ec = GetComponent<EdgeCollider2D>();
        // Initialise the edge points array
        edgePoints = new Vector2[numLinePoints];

        playerMovement = GetComponent<PlayerMovement>();

        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        // gradient.SetKeys(
        //     new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.red, 1.0f) },
        //     new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        // );
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );
        lr.colorGradient = gradient;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            lr.enabled = true;

            // lr.SetPosition(0, transform.position);

            // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Vector3 offsetPos = mousePos - transform.position;
            // Vector3 newVec = offsetPos.normalized * 20f; // this is the important line
            // newVec += transform.position;
            // newVec.z = -1;

            // here is where the failing starts. i need to calculate the end position.
            // lr.SetPosition(1, newVec);

            // Debug.Log(newVec);

            float distance = 2.5f;

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.up, distance);
            Debug.DrawRay(transform.position, transform.up);

            Vector3 initialPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1);

            lr.SetPosition(0, initialPos);
            lr.SetPosition(1, initialPos + (transform.up * distance));

            foreach (RaycastHit2D hit in hits)
            {
                if ((hit.transform.gameObject.tag == "Enemy" && hit.collider.name != "Vision") || hit.transform.gameObject.tag == "Wall")
                {
                    lr.SetPosition(1, hit.point);
                    break;
                }
            }
            // lr.SetPosition(0, transform.GetChild(0).position);
            // lr.SetPosition(1, hit.point);
            // if (hit != null)
            // {
            //     Vector2 reflectedRay = Vector2.Reflect(transform.GetChild(0).up * 10f, hit.normal);
            //     // Debug.DrawRay(hit.point, reflectedRay);

            //     lr.SetPosition(2, reflectedRay);
            //     // Debug.Log(hit.transform.gameObject);
            // }

            // // Get the number of line points
            // int numLinePoints = lr.positionCount;

            // // Left-most and right-most points (in local coordinates),
            // // between which line is rendered
            // float xLeft = -10f;
            // float xRight = 10f;

            // // Distance between points on the line (calculated to fit exactly
            // // numPoints from xLeft to xRight)
            // float dx = (xRight - xLeft) / (float)(numLinePoints - 1);
            // linePoints[0] = transform.GetChild(0).position;
            // for (int i = 1; i < numLinePoints; i++)
            // {
            //     // Horizontal position of point i
            //     linePoints[i].x = xLeft + i * dx;
            //     // Vertical position of point i is changing with time
            //     //linePoints[i].y = 0;
            //     linePoints[i].y = 0.2f * Mathf.Sin(linePoints[i].x / 20f + Time.timeSinceLevelLoad);

            //     // Z-coordinate of point i (taken from game object's
            //     // z coordinate)
            //     linePoints[i].z = transform.position.z;
            // }

            // // Set the line renderer points to the positions from point array
            // lr.SetPositions(linePoints);

            // // Get the number of line points
            // numLinePoints = lr.positionCount;

            // for (int i = 0; i < numLinePoints; i++)
            // {
            //     // Horizontal location of edge point i follows the line
            //     edgePoints[i].x = linePoints[i].x;
            //     edgePoints[i].y = linePoints[i].y;
            // }

            // // Set the edge point to the positions from edge point array
            // ec.points = edgePoints;
        }
        else
        {
            lr.enabled = false;
        }
    }
}
