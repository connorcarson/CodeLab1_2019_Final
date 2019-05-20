using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class AsciiLevelLoader : MonoBehaviour
{
    public NavMeshSurface plane;
    
    public string levelTxtFile;

    public Transform environment;
    
    // Start is called before the first frame update
    void Start()
    {
        string filepath = Application.dataPath + levelTxtFile;

        if (!File.Exists(filepath))
        {
            File.WriteAllText(filepath, "X");
        }

        string[] inputLines = File.ReadAllLines(filepath);

        for (int y = 0; y < inputLines.Length; y++)
        {
            string line = inputLines[y];

            for (int x = 0; x < line.Length; x++)
            {
                GameObject tile;
                
                switch (line[x])
                {
                    case 'A':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/left_right_buildings/building_1"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'B':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/left_right_buildings/building_2"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'C':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/left_right_buildings/building_3"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'D':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/left_right_buildings/building_4"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'E':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/left_right_buildings/building_6"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'F':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/left_right_buildings/building_7"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y - 0.5f);
                        break;
                    case 'G':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/left_right_buildings/building_8"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'a':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/up_down_buildings/building_1"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'b':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/up_down_buildings/building_2"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'c':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/up_down_buildings/building_3"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'd':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/up_down_buildings/building_4"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'e':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/up_down_buildings/building_6"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'f':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/up_down_buildings/building_7"));
                        tile.transform.position = new Vector3(x - line.Length/2f + 0.5f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'g':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/up_down_buildings/building_8"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'x':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/roads/road_left_right"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'y':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/roads/road_up_down"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'T':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/roads/road_T_top"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 't':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/roads/road_T_bottom"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'l':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/roads/road_T_left"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'r':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/roads/road_T_right"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case 'X':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/roads/road_intersection"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case '-':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/picket_fence_1"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    case '|':
                        tile = Instantiate(Resources.Load<GameObject>("Prefabs/picket_fence_2"));
                        tile.transform.position = new Vector3(x - line.Length/2f, 0f, inputLines.Length/2f - y);
                        break;
                    default:
                        tile = null;
                        break;
                }
            }
        }
        
        plane.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
