using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public struct Point
{
    public int x;
    public int y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

public class PlayerBuild : MonoBehaviour
{

    public Transform buildPrefab;
    public Transform pendingPrefab;
    public Dictionary<Point, bool> buildGrid;


    //Gotta be a better way to do this....
    private Point HIDDEN = new Point(100, -100);


    // Start is called before the first frame update
    void Start()
    {
        buildGrid = new Dictionary<Point, bool>();

        //Begin with a platform at 0,0
        buildGrid[new Point(0, 0)] = true;


    }

    /*
        Return the grid point at which a player would be building a platform
     */
    private Point PlayerDesiredPositionToGrid(Vector3 loc, PlayerDirection dir)
    {
        Vector3 dest = new Vector3(loc.x, loc.y, 0);

        // Get location in "front" of player using current direction
        int delta;
        if (dir == PlayerDirection.LEFT)
        {
            delta = -1;
        }
        else
        {
            delta = 1;
        }

        float destXGrid = Mathf.Round((loc.x + delta));
        float destYGrid = Mathf.Round((loc.y));
        return new Point((int)destXGrid, (int)destYGrid);
    }

    private bool CanPlace(Point p)
    {
        if (buildGrid.ContainsKey(p))
        {
            return false;
        } else if (p.y > 0) {
            if (buildGrid.ContainsKey(new Point(p.x, p.y - 1)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }


    private Vector3 GridPosToLocation(Point p)
    {
        return new Vector3(p.x *0.5f, p.y * 0.5f + 0.5f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {

        // Convert player position to grid
        Vector3 loc = PlayerMovement.Instance.gameObject.transform.position;
        Point placementPoint = PlayerDesiredPositionToGrid(loc, PlayerMovement.Instance.currentDirection);

        if (buildGrid.ContainsKey(placementPoint))
        {
            if (!buildGrid.ContainsKey(new Point(placementPoint.x, placementPoint.y + 1)))
            {
                placementPoint.y += 1;
            }
            else
            {
                placementPoint = HIDDEN;
            }
        }

        if (!CanPlace(placementPoint))
        {
            placementPoint = HIDDEN;
        }

        Vector3 placementLoc = GridPosToLocation(placementPoint);
        pendingPrefab.position = placementLoc;


        if (Input.GetKeyDown(KeyCode.B))
        {

            if(CanPlace(placementPoint))
            {
                // Spawn new instance of the crate
                Instantiate(buildPrefab, placementLoc, Quaternion.identity);
                buildGrid[placementPoint] = true;
            }
        }
    }
}
