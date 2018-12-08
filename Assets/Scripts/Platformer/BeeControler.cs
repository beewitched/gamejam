using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BeeControler : MonoBehaviour
{
    // --- | Serialized | -------------------------------------------------------------------------

    [Header("Keys")]
    [SerializeField]
    private KeyCode drawKey = KeyCode.Mouse1;
    [SerializeField]
    private KeyCode callKey = KeyCode.Alpha1;
    [SerializeField]
    private KeyCode stayKey = KeyCode.Alpha2;

    [Header("References")]
    [SerializeField]
    private Bee bee;

    [Header("Layers")]
    [SerializeField]
    private LayerMask obstacleLayers;

    // --- | Componetns | -------------------------------------------------------------------------

    private LineRenderer path;


    // --- | Variables | --------------------------------------------------------------------------

    private List<Vector3> controllpoints;
    private float minDistance = 0.2f; // min distance between controlpoints
    private Vector2 currentMousePos;


    // --- | Methods | ----------------------------------------------------------------------------

    // MonoBehaviour --------------------------------------

    private void Awake()
    {
        path = GetComponent<LineRenderer>();
        path.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKey(drawKey) && bee.IsAutoControlled)
        {
            DrawPath();
        }

        else if (Input.GetKeyUp(drawKey))
        {
            SendBee();
        }

        else if (Input.GetKeyDown(stayKey))
        {
            bee.SwitchTarget(bee.transform.position);
        }

        else if (Input.GetKeyDown(callKey))
        {
            bee.TargetPlayer();
        }
    }

    // Public Methods -------------------------------------

    /// <summary>
    /// Clears the bees path.
    /// </summary>
    public void ClearPath()
    {
        path.enabled = false;
    }

    // Private Methods ------------------------------------

    /// <summary>
    /// Draws a path for the bee, following the mouse cursor.
    /// </summary>
    private void DrawPath()
    {
        if (Input.GetKeyDown(drawKey))
        {
            path.enabled = true;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (controllpoints == null)
        {
            controllpoints = new List<Vector3>();
            controllpoints.Add(bee.transform.position);
        }

        Vector2 prevPoint = (Vector2)controllpoints[controllpoints.Count - 1];
        Vector2 deltaDirection = mousePos - prevPoint;

        RaycastHit2D hit = Physics2D.Raycast(prevPoint, deltaDirection, deltaDirection.magnitude, obstacleLayers);
        if (hit)
        {
            if (hit.distance > minDistance)
            {
                AddControllpoint(hit.point + hit.normal * 0.05f);
            }
        }
        else if (deltaDirection.magnitude > minDistance)
        {
            AddControllpoint(mousePos);
        }

        currentMousePos = mousePos;
    }

    /// <summary>
    /// Adds a controllpoint to the bees path.
    /// </summary>
    /// <param name="pos"></param>
    private void AddControllpoint(Vector2 pos)
    {
        controllpoints.Add(pos);
        path.positionCount = controllpoints.Count;
        path.SetPositions(controllpoints.ToArray());
    }

    /// <summary>
    /// Sends the bee along the path.
    /// </summary>
    private void SendBee()
    {
        bee.SendBee(controllpoints.ToArray(), ClearPath);
        controllpoints = null;
    }
}
