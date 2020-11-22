using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using Random = UnityEngine.Random;

namespace SheepCounting
{
    public class TileMapper : MonoBehaviour
    { 
        public Tilemap Tilemap;      // Use this for initialization     
        void Start()

        {

            Tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
            // grid = GameObject.Find("Grid").GetComponent<Grid>();
            Debug.Log("Tilemap size: " + Tilemap.size);
            Debug.Log("Tilename: " + Tilemap.GetTile(new Vector3Int (-11, -13, 0)).ToString());
            Debug.Log("Tilename is: " + (Tilemap.GetTile(new Vector3Int(-11, -13, 0)).ToString() == "bottom_corner (UnityEngine.Tilemaps.Tile)"));
            Vector3 tilePosition;
            Vector3Int coordinate = new Vector3Int(-11, -13, 0);
            for (int i = -11; i <= Tilemap.size.x; i++)
            {
                for (int j = -13; j <= Tilemap.size.y; j++)
                {
                    for (int k = 0; k <= Tilemap.size.z; k++)
                    {
                        coordinate.x = i; coordinate.y = j; coordinate.z = k;
                        tilePosition = Tilemap.CellToWorld(coordinate);
                        if (Tilemap.HasTile(coordinate))
                        { 
                            if (Tilemap.GetTile(coordinate).ToString() == "bottom_corner (UnityEngine.Tilemaps.Tile)")
                            {
                                Debug.Log(string.Format("Position of bottom_corner [{0}, {1}, {2}] = ({3}, {4}, {5})",
                                            coordinate.x, coordinate.y, coordinate.z, tilePosition.x, tilePosition.y, tilePosition.z));
                            }
                            else if (Tilemap.GetTile(coordinate).ToString() == "left_corner (UnityEngine.Tilemaps.Tile)")
                            {
                                Debug.Log(string.Format("Position of left_corner [{0}, {1}, {2}] = ({3}, {4}, {5})",
                                            coordinate.x, coordinate.y, coordinate.z, tilePosition.x, tilePosition.y, tilePosition.z));
                            }
                            else if (Tilemap.GetTile(coordinate).ToString() == "top_corner (UnityEngine.Tilemaps.Tile)")
                            {
                                Debug.Log(string.Format("Position of top_corner [{0}, {1}, {2}] = ({3}, {4}, {5})",
                                            coordinate.x, coordinate.y, coordinate.z, tilePosition.x, tilePosition.y, tilePosition.z));
                            }
                            else if (Tilemap.GetTile(coordinate).ToString() == "right_corner (UnityEngine.Tilemaps.Tile)")
                            {
                                Debug.Log(string.Format("Position of right_corner [{0}, {1}, {2}] = ({3}, {4}, {5})",
                                            coordinate.x, coordinate.y, coordinate.z, tilePosition.x, tilePosition.y, tilePosition.z));
                            }
                            else if (Tilemap.GetTile(coordinate).ToString() == "bottom_edge (UnityEngine.Tilemaps.Tile)")
                            {
                                Debug.Log(string.Format("Position of bottom_edge[{0}, {1}, {2}] = ({3}, {4}, {5})",
                                            coordinate.x, coordinate.y, coordinate.z, tilePosition.x, tilePosition.y, tilePosition.z));
                            }
                            else if (Tilemap.GetTile(coordinate).ToString() == "left_edge (UnityEngine.Tilemaps.Tile)")
                            {
                                Debug.Log(string.Format("Position of left_edge[{0}, {1}, {2}] = ({3}, {4}, {5})",
                                            coordinate.x, coordinate.y, coordinate.z, tilePosition.x, tilePosition.y, tilePosition.z));
                            }
                            else if (Tilemap.GetTile(coordinate).ToString() == "top_edge (UnityEngine.Tilemaps.Tile)")
                            {
                                Debug.Log(string.Format("Position of top_edge[{0}, {1}, {2}] = ({3}, {4}, {5})",
                                            coordinate.x, coordinate.y, coordinate.z, tilePosition.x, tilePosition.y, tilePosition.z));
                            }
                            else if (Tilemap.GetTile(coordinate).ToString() == "right_edge (UnityEngine.Tilemaps.Tile)")
                            {
                                Debug.Log(string.Format("Position of right_edge[{0}, {1}, {2}] = ({3}, {4}, {5})",
                                            coordinate.x, coordinate.y, coordinate.z, tilePosition.x, tilePosition.y, tilePosition.z));
                            }
                        }
                    }
                }
            }
        }
    }

    /*


       public Tilemap tileMap;         //the level tilemap
       public Grid grid;               //the level grid
       private Vector3Int pos1;
       private Vector3Int pos2;
       private Vector3Int pos3;
       private Vector3Int pos4;
       private Vector3Int pos5;
       private Vector3Int pos6;
       private Vector3Int pos7;
       private int x;
       private int y;
       private int z;
       private int xMin;
       private int xMax;
       private int yMin;
       private int yMax;
       private int zMin;
       private int zMax;
    //private Vector3Int pos1 = Vector3Int.Set(11, 11, 0);
    //private Vector3Int pos2 = Vector3Int.Set(-11, 11, 0);
    //private Vector3Int pos3 = Vector3Int.Set(11 , -13, 0);
    //private Vector3Int pos4 = Vector3Int.Set(11, 11, 1);
    //private Vector3Int pos5 = Vector3Int.Set(-11, 11, 1);
    //private Vector3Int pos6 = Vector3Int.Set(11, -13, 1);


    // Start is called before the first frame update
    void Start()
    {

    //Assign tilemap and grid from level
    tileMap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
    grid = GameObject.Find("Grid").GetComponent<Grid>();
    tileMap.CompressBounds();
    Debug.Log("TileMap cellBounds output: " + tileMap.cellBounds);
    Vector3Int posBottom = tileMap.cellBounds.position;
    Debug.Log("TileMap cellBounds position output: " + tileMap.cellBounds.position);
    Debug.Log("TileMap cellBounds position var: " + posBottom);
    Debug.Log("PosBottom has cell: " + tileMap.HasTile(posBottom));
    Debug.Log("TileMap cellBounds posBottom cell: " + tileMap.GetTile(posBottom).ToString());
    x = tileMap.cellBounds.position.x;
    Debug.Log("X pos: " + x);
    y = tileMap.cellBounds.position.y;
    Debug.Log("Y pos: " + y);
    z = tileMap.cellBounds.position.z;
    Debug.Log("Z pos: " + z);
    xMin = tileMap.cellBounds.xMin;
    Debug.Log("xMin pos: " + xMin);
    xMax = tileMap.cellBounds.xMax - 1;
    Debug.Log("xMax pos: " + xMax);
    yMin = tileMap.cellBounds.yMin;
    Debug.Log("yMin pos: " + yMin);
    yMax = tileMap.cellBounds.yMax - 1;
    Debug.Log("yMax pos: " + yMax);
    zMin = tileMap.cellBounds.zMin;
    Debug.Log("zMin pos: " + zMin);
    zMax = tileMap.cellBounds.zMax - 1;
    Debug.Log("zMax pos: " + zMax);

    pos1.Set(x, y, z);
    pos2.Set(xMin, yMin, zMin);
    pos3.Set(xMax, yMin, zMin);
    pos4.Set(xMax, yMax, zMin);
    pos5.Set(xMin, yMax, zMax);
    pos6.Set(xMax, yMax, zMax);
    pos7.Set(xMax, yMin, zMax);
    Debug.Log("Pos1 has cell: " + tileMap.HasTile(pos1));
    Debug.Log("Pos2 has cell: " + tileMap.HasTile(pos2));
    Debug.Log("Pos3 has cell: " + tileMap.HasTile(pos3));
    Debug.Log("Pos4 has cell: " + tileMap.HasTile(pos4));
    Debug.Log("Pos5 has cell: " + tileMap.HasTile(pos5));
    Debug.Log("Pos6 has cell: " + tileMap.HasTile(pos6));
    Debug.Log("Pos7 has cell: " + tileMap.HasTile(pos7));

}

// Update is called once per frame
void Update()
{

}
}

*/
}
