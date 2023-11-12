using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    public int stage = 1;

    [SerializeField] GameObject startingHall1;
    [SerializeField] GameObject startingHall2;
    [SerializeField] GameObject startingHall3;
    [SerializeField] GameObject startingHall4;
    [SerializeField] GameObject startingHall5;
    [SerializeField] GameObject straightHall;
    [SerializeField] GameObject leftTurnHall;
    [SerializeField] GameObject rightTurnHall;
    public List<GameObject> halls;
    public List<GameObject> obstacles;
    public List<hallType> hallTypes;
    public direction currentDirection = direction.North;
    public enum hallType {
        Straight = 0,
        LeftTurn = 1,
        RightTurn = 2
    }
    public enum direction {
        North = 0,
        East = 90,
        South = 180,
        West = 270
    }
    
    void Start()
    {
        halls = new List<GameObject>(5);
        halls.Add(startingHall1);
        halls.Add(startingHall2);
        halls.Add(startingHall3);
        halls.Add(startingHall4);
        halls.Add(startingHall5);
        hallTypes = new List<hallType>(5);
        hallTypes.Add(hallType.Straight);
        hallTypes.Add(hallType.Straight);
        hallTypes.Add(hallType.Straight);
        hallTypes.Add(hallType.Straight);
        hallTypes.Add(hallType.Straight);
    }

    
    void Update()
    {
        
    }

    public void GenerateNewHall() {
        Destroy(halls[0]);
        for (int i = 0; i < 4; i++) {
            halls[i] = halls[i+1];
            hallTypes[i] = hallTypes[i+1];
        }
        int hallID = UnityEngine.Random.Range(0, 3);
        switch(hallID) {
            case 0: 
                //Generate straight hall
                hallTypes[4] = hallType.Straight;
                UnityEngine.Debug.Log("Generating Straight Hall");
                if (hallTypes[3] == hallType.Straight) {
                    if (currentDirection == direction.North) {
                        halls[4] = Instantiate(straightHall, new Vector3(halls[3].transform.position.x + 32, halls[3].transform.position.y, halls[3].transform.position.z), halls[3].transform.rotation);
                    } else if (currentDirection == direction.East) {
                        halls[4] = Instantiate(straightHall, new Vector3(halls[3].transform.position.x, halls[3].transform.position.y, halls[3].transform.position.z - 32), halls[3].transform.rotation);
                    } else if (currentDirection == direction.South) {
                        halls[4] = Instantiate(straightHall, new Vector3(halls[3].transform.position.x - 32, halls[3].transform.position.y, halls[3].transform.position.z), halls[3].transform.rotation);
                    } else if (currentDirection == direction.West) {
                        halls[4] = Instantiate(straightHall, new Vector3(halls[3].transform.position.x, halls[3].transform.position.y, halls[3].transform.position.z + 32), halls[3].transform.rotation);
                    }
                } else if (hallTypes[3] == hallType.LeftTurn) {
                    if (currentDirection == direction.North) {
                        halls[4] = Instantiate(straightHall, new Vector3(halls[3].transform.position.x + 24, halls[3].transform.position.y, halls[3].transform.position.z - 16), new Quaternion(0,0,0,1));
                    } else if (currentDirection == direction.East) {
                        halls[4] = Instantiate(straightHall, new Vector3(halls[3].transform.position.x - 16, halls[3].transform.position.y, halls[3].transform.position.z - 24), Quaternion.Euler(0,90,0));
                    } else if (currentDirection == direction.South) {
                        halls[4] = Instantiate(straightHall, new Vector3(halls[3].transform.position.x - 24, halls[3].transform.position.y, halls[3].transform.position.z + 16), Quaternion.Euler(0,180,0));
                    } else if (currentDirection == direction.West) {
                        halls[4] = Instantiate(straightHall, new Vector3(halls[3].transform.position.x + 16, halls[3].transform.position.y, halls[3].transform.position.z + 24), Quaternion.Euler(0,270,0));
                    }
                } else if (hallTypes[3] == hallType.RightTurn) {
                    if (currentDirection == direction.North) {
                        halls[4] = Instantiate(straightHall, new Vector3(halls[3].transform.position.x + 24, halls[3].transform.position.y, halls[3].transform.position.z + 16), new Quaternion(0,0,0,1));
                    } else if (currentDirection == direction.East) {
                        halls[4] = Instantiate(straightHall, new Vector3(halls[3].transform.position.x + 16, halls[3].transform.position.y, halls[3].transform.position.z - 24), Quaternion.Euler(0,90,0));
                    } else if (currentDirection == direction.South) {
                        halls[4] = Instantiate(straightHall, new Vector3(halls[3].transform.position.x - 24, halls[3].transform.position.y, halls[3].transform.position.z - 16), Quaternion.Euler(0,180,0));
                    } else if (currentDirection == direction.West) {
                        halls[4] = Instantiate(straightHall, new Vector3(halls[3].transform.position.x - 16, halls[3].transform.position.y, halls[3].transform.position.z + 24), Quaternion.Euler(0,270,0));
                    }
                } 
                break;
            case 1:
                //Generate Left Turn
                hallTypes[4] = hallType.LeftTurn;
                UnityEngine.Debug.Log("Generating Left Turn");
                if (hallTypes[3] == hallType.Straight) {
                    if (currentDirection == direction.North) {
                        halls[4] = Instantiate(leftTurnHall, new Vector3(halls[3].transform.position.x + 32, halls[3].transform.position.y, halls[3].transform.position.z), halls[3].transform.rotation);
                    } else if (currentDirection == direction.East) {
                        halls[4] = Instantiate(leftTurnHall, new Vector3(halls[3].transform.position.x, halls[3].transform.position.y, halls[3].transform.position.z - 32), halls[3].transform.rotation);
                    } else if (currentDirection == direction.South) {
                        halls[4] = Instantiate(leftTurnHall, new Vector3(halls[3].transform.position.x - 32, halls[3].transform.position.y, halls[3].transform.position.z), halls[3].transform.rotation);
                    } else if (currentDirection == direction.West) {
                        halls[4] = Instantiate(leftTurnHall, new Vector3(halls[3].transform.position.x, halls[3].transform.position.y, halls[3].transform.position.z + 32), halls[3].transform.rotation);
                    }
                } else if (hallTypes[3] == hallType.LeftTurn) {
                    if (currentDirection == direction.North) {
                        halls[4] = Instantiate(leftTurnHall, new Vector3(halls[3].transform.position.x + 24, halls[3].transform.position.y, halls[3].transform.position.z - 16), new Quaternion(0,0,0,1));
                    } else if (currentDirection == direction.East) {
                        halls[4] = Instantiate(leftTurnHall, new Vector3(halls[3].transform.position.x - 16, halls[3].transform.position.y, halls[3].transform.position.z - 24), Quaternion.Euler(0,90,0));
                    } else if (currentDirection == direction.South) {
                        halls[4] = Instantiate(leftTurnHall, new Vector3(halls[3].transform.position.x - 24, halls[3].transform.position.y, halls[3].transform.position.z + 16), Quaternion.Euler(0,180,0));
                    } else if (currentDirection == direction.West) {
                        halls[4] = Instantiate(leftTurnHall, new Vector3(halls[3].transform.position.x + 16, halls[3].transform.position.y, halls[3].transform.position.z + 24), Quaternion.Euler(0,270,0));
                    }
                } else if (hallTypes[3] == hallType.RightTurn) {
                    if (currentDirection == direction.North) {
                        halls[4] = Instantiate(leftTurnHall, new Vector3(halls[3].transform.position.x + 24, halls[3].transform.position.y, halls[3].transform.position.z + 16), new Quaternion(0,0,0,1));
                    } else if (currentDirection == direction.East) {
                        halls[4] = Instantiate(leftTurnHall, new Vector3(halls[3].transform.position.x + 16, halls[3].transform.position.y, halls[3].transform.position.z - 24), Quaternion.Euler(0,90,0));
                    } else if (currentDirection == direction.South) {
                        halls[4] = Instantiate(leftTurnHall, new Vector3(halls[3].transform.position.x - 24, halls[3].transform.position.y, halls[3].transform.position.z - 16), Quaternion.Euler(0,180,0));
                    } else if (currentDirection == direction.West) {
                        halls[4] = Instantiate(leftTurnHall, new Vector3(halls[3].transform.position.x - 16, halls[3].transform.position.y, halls[3].transform.position.z + 24), Quaternion.Euler(0,270,0));
                    }
                } 
                
                if (currentDirection == direction.North) {
                    currentDirection = direction.West;
                } else if (currentDirection == direction.West) {
                    currentDirection = direction.South;
                } else if (currentDirection == direction.South) {
                    currentDirection = direction.East;
                } else if (currentDirection == direction.East) {
                    currentDirection = direction.North;
                }
                break;
            case 2:
                //Generate Right Turn
                hallTypes[4] = hallType.RightTurn;
                UnityEngine.Debug.Log("Generating Right Turn");
                if (hallTypes[3] == hallType.Straight) {
                    if (currentDirection == direction.North) {
                        halls[4] = Instantiate(rightTurnHall, new Vector3(halls[3].transform.position.x + 32, halls[3].transform.position.y, halls[3].transform.position.z), halls[3].transform.rotation);
                    } else if (currentDirection == direction.East) {
                        halls[4] = Instantiate(rightTurnHall, new Vector3(halls[3].transform.position.x, halls[3].transform.position.y, halls[3].transform.position.z - 32), halls[3].transform.rotation);
                    } else if (currentDirection == direction.South) {
                        halls[4] = Instantiate(rightTurnHall, new Vector3(halls[3].transform.position.x - 32, halls[3].transform.position.y, halls[3].transform.position.z), halls[3].transform.rotation);
                    } else if (currentDirection == direction.West) {
                        halls[4] = Instantiate(rightTurnHall, new Vector3(halls[3].transform.position.x, halls[3].transform.position.y, halls[3].transform.position.z + 32), halls[3].transform.rotation);
                    }
                } else if (hallTypes[3] == hallType.LeftTurn) {
                    if (currentDirection == direction.North) {
                        halls[4] = Instantiate(rightTurnHall, new Vector3(halls[3].transform.position.x + 24, halls[3].transform.position.y, halls[3].transform.position.z - 16), new Quaternion(0,0,0,1));
                    } else if (currentDirection == direction.East) {
                        halls[4] = Instantiate(rightTurnHall, new Vector3(halls[3].transform.position.x - 16, halls[3].transform.position.y, halls[3].transform.position.z - 24), Quaternion.Euler(0,90,0));
                    } else if (currentDirection == direction.South) {
                        halls[4] = Instantiate(rightTurnHall, new Vector3(halls[3].transform.position.x - 24, halls[3].transform.position.y, halls[3].transform.position.z + 16), Quaternion.Euler(0,180,0));
                    } else if (currentDirection == direction.West) {
                        halls[4] = Instantiate(rightTurnHall, new Vector3(halls[3].transform.position.x + 16, halls[3].transform.position.y, halls[3].transform.position.z + 24), Quaternion.Euler(0,270,0));
                    }
                } else if (hallTypes[3] == hallType.RightTurn) {
                    if (currentDirection == direction.North) {
                        halls[4] = Instantiate(rightTurnHall, new Vector3(halls[3].transform.position.x + 24, halls[3].transform.position.y, halls[3].transform.position.z + 16), new Quaternion(0,0,0,1));
                    } else if (currentDirection == direction.East) {
                        halls[4] = Instantiate(rightTurnHall, new Vector3(halls[3].transform.position.x + 16, halls[3].transform.position.y, halls[3].transform.position.z - 24), Quaternion.Euler(0,90,0));
                    } else if (currentDirection == direction.South) {
                        halls[4] = Instantiate(rightTurnHall, new Vector3(halls[3].transform.position.x - 24, halls[3].transform.position.y, halls[3].transform.position.z - 16), Quaternion.Euler(0,180,0));
                    } else if (currentDirection == direction.West) {
                        halls[4] = Instantiate(rightTurnHall, new Vector3(halls[3].transform.position.x - 16, halls[3].transform.position.y, halls[3].transform.position.z + 24), Quaternion.Euler(0,270,0));
                    }
                } 
                
                if (currentDirection == direction.North) {
                    currentDirection = direction.East;
                } else if (currentDirection == direction.East) {
                    currentDirection = direction.South;
                } else if (currentDirection == direction.South) {
                    currentDirection = direction.West;
                } else if (currentDirection == direction.West) {
                    currentDirection = direction.North;
                }
                break;
        }
        halls[4].GetComponentInChildren<loadHallway>().gm = this;
        GenerateObstacles();
    }

    public void GenerateObstacles() {

        for (int i = 0; i < 5; i++) {
            int obstacleCheck = UnityEngine.Random.Range(0,2);
            if (obstacleCheck == 1) {
                int obstacleID = UnityEngine.Random.Range(0,4);
                if (obstacleID == 3) {
                    int cabinetID = UnityEngine.Random.Range(0,2);
                    if (cabinetID == 0) {
                        obstacleID = 3;
                    } else if (cabinetID == 1) {
                        obstacleID = 4;
                    }
                }
                    //Determine location
                    //Instantiate obstacle
            }
        }
    }
}
