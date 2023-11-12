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
    public int hallNumber = 1;
    [SerializeField] GameObject player;
    [SerializeField] GameObject startingHall1;
    [SerializeField] GameObject startingHall2;
    [SerializeField] GameObject startingHall3;
    [SerializeField] GameObject startingHall4;
    [SerializeField] GameObject startingHall5;
    [SerializeField] GameObject straightHall;
    [SerializeField] GameObject leftTurnHall;
    [SerializeField] GameObject rightTurnHall;
    public List<GameObject> healthPickups;
    public List<GameObject> halls;
    public List<GameObject> obstacles;
    public List<hallType> hallTypes;
    public direction currentDirection = direction.North;
    playerMovement pm;
    playerHealth ph;
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
        pm = player.GetComponent<playerMovement>();
        ph = player.GetComponent<playerHealth>();
        hallNumber = 1;
        stage = 1;
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
        hallNumber++;
        if (hallNumber%5 == 0) {
            IncreaseStage();
        }
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

        for (int i = 0; i < 4; i++) {
            int obstacleCheck = UnityEngine.Random.Range(0,2);
            if (obstacleCheck > 0) {
                int obstacleID = UnityEngine.Random.Range(0,5);
                if (obstacleID == 4) {
                    int cabinetID = UnityEngine.Random.Range(0,2);
                    if (cabinetID == 0) {
                        obstacleID = 4;
                    } else if (cabinetID == 1) {
                        obstacleID = 5;
                    }
                }
                if (hallTypes[4] == hallType.Straight) {
                        if (currentDirection == direction.North) {
                            Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x + (i * 8), halls[4].transform.position.y, halls[4].transform.position.z), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y,halls[4].transform.rotation.z), halls[4].transform);
                        } else if (currentDirection == direction.East) {
                            Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x, halls[4].transform.position.y, halls[4].transform.position.z - (i * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 90,halls[4].transform.rotation.z), halls[4].transform);
                        } else if (currentDirection == direction.South) {
                            Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x - (i * 8), halls[4].transform.position.y, halls[4].transform.position.z), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 180,halls[4].transform.rotation.z), halls[4].transform);
                        } else if (currentDirection == direction.West) {
                            Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x, halls[4].transform.position.y, halls[4].transform.position.z + (i * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 270,halls[4].transform.rotation.z), halls[4].transform);
                        }
                    } else if (hallTypes[4] == hallType.LeftTurn) {
                        if (currentDirection == direction.North) {
                            if (i < 2) {
                                Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x, halls[4].transform.position.y, halls[4].transform.position.z - (i * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 90,halls[4].transform.rotation.z), halls[4].transform);
                            } else if (i >= 2) {
                                Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x + ((i - 1) * 8), halls[4].transform.position.y, halls[4].transform.position.z - 16), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y,halls[4].transform.rotation.z), halls[4].transform);
                            }
                        } else if (currentDirection == direction.East) {
                            if (i < 2) {
                                Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x - (i * 8), halls[4].transform.position.y, halls[4].transform.position.z), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 180,halls[4].transform.rotation.z), halls[4].transform);
                            } else if (i >= 2) {
                                Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x - 16, halls[4].transform.position.y, halls[4].transform.position.z - ((i - 1) * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 90,halls[4].transform.rotation.z), halls[4].transform);
                            }
                        } else if (currentDirection == direction.South) {
                            if (i < 2) {
                                Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x, halls[4].transform.position.y, halls[4].transform.position.z + (i * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 270,halls[4].transform.rotation.z), halls[4].transform);
                            } else if (i >= 2) {
                                Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x - ((i - 1) * 8), halls[4].transform.position.y, halls[4].transform.position.z + 16), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 180,halls[4].transform.rotation.z), halls[4].transform);
                            }
                        } else if (currentDirection == direction.West) {
                            if (i < 2) {
                                Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x + (i * 8), halls[4].transform.position.y, halls[4].transform.position.z), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y,halls[4].transform.rotation.z), halls[4].transform);
                            } else if (i >= 2) {
                                Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x + 16, halls[4].transform.position.y, halls[4].transform.position.z + ((i - 1) * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 270,halls[4].transform.rotation.z), halls[4].transform);
                            }
                        }
                    } else if (hallTypes[4] == hallType.RightTurn) {
                        if (currentDirection == direction.North) {
                            if (i < 2) {
                                Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x, halls[4].transform.position.y, halls[4].transform.position.z + (i * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 270,halls[4].transform.rotation.z), halls[4].transform);
                            } else if (i >= 2) {
                                Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x + ((i - 1) * 8), halls[4].transform.position.y, halls[4].transform.position.z + 16), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y,halls[4].transform.rotation.z), halls[4].transform);
                            }
                        } else if (currentDirection == direction.East) {
                            if (i < 2) {
                                Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x + (i * 8), halls[4].transform.position.y, halls[4].transform.position.z), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y,halls[4].transform.rotation.z), halls[4].transform);
                            } else if (i >= 2) {
                                Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x + 16, halls[4].transform.position.y, halls[4].transform.position.z - ((i - 1) * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 90,halls[4].transform.rotation.z), halls[4].transform);
                            }
                        } else if (currentDirection == direction.South) {
                            if (i < 2) {
                                Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x, halls[4].transform.position.y, halls[4].transform.position.z - (i * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 90,halls[4].transform.rotation.z), halls[4].transform);
                            } else if (i >= 2) {
                                Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x - ((i - 1) * 8), halls[4].transform.position.y, halls[4].transform.position.z - 16), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 180,halls[4].transform.rotation.z), halls[4].transform);
                            }
                        } else if (currentDirection == direction.West) {
                            if (i < 2) {
                                Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x - (i * 8), halls[4].transform.position.y, halls[4].transform.position.z), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 180,halls[4].transform.rotation.z), halls[4].transform);
                            } else if (i >= 2) {
                                Instantiate(obstacles[obstacleID], new Vector3(halls[4].transform.position.x - 16, halls[4].transform.position.y, halls[4].transform.position.z + ((i - 1) * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 270,halls[4].transform.rotation.z), halls[4].transform);
                            }
                        }
                    }
            } else {
                int healthCheck = UnityEngine.Random.Range(0,4);
                if (healthCheck == 0) {
                    int healthPickupID = UnityEngine.Random.Range(0,3);
                    if (hallTypes[4] == hallType.Straight) {
                        if (currentDirection == direction.North) {
                            Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x + (i * 8), halls[4].transform.position.y, halls[4].transform.position.z), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y,halls[4].transform.rotation.z), halls[4].transform);
                        } else if (currentDirection == direction.East) {
                            Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x, halls[4].transform.position.y, halls[4].transform.position.z - (i * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 90,halls[4].transform.rotation.z), halls[4].transform);
                        } else if (currentDirection == direction.South) {
                            Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x - (i * 8), halls[4].transform.position.y, halls[4].transform.position.z), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 180,halls[4].transform.rotation.z), halls[4].transform);
                        } else if (currentDirection == direction.West) {
                            Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x, halls[4].transform.position.y, halls[4].transform.position.z + (i * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 270,halls[4].transform.rotation.z), halls[4].transform);
                        }
                    } else if (hallTypes[4] == hallType.LeftTurn) {
                        if (currentDirection == direction.North) {
                            if (i < 2) {
                                Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x, halls[4].transform.position.y, halls[4].transform.position.z - (i * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 90,halls[4].transform.rotation.z), halls[4].transform);
                            } else if (i >= 2) {
                                Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x + ((i - 1) * 8), halls[4].transform.position.y, halls[4].transform.position.z - 16), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y,halls[4].transform.rotation.z), halls[4].transform);
                            }
                        } else if (currentDirection == direction.East) {
                            if (i < 2) {
                                Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x - (i * 8), halls[4].transform.position.y, halls[4].transform.position.z), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 180,halls[4].transform.rotation.z), halls[4].transform);
                            } else if (i >= 2) {
                                Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x - 16, halls[4].transform.position.y, halls[4].transform.position.z - ((i - 1) * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 90,halls[4].transform.rotation.z), halls[4].transform);
                            }
                        } else if (currentDirection == direction.South) {
                            if (i < 2) {
                                Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x, halls[4].transform.position.y, halls[4].transform.position.z + (i * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 270,halls[4].transform.rotation.z), halls[4].transform);
                            } else if (i >= 2) {
                                Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x - ((i - 1) * 8), halls[4].transform.position.y, halls[4].transform.position.z + 16), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 180,halls[4].transform.rotation.z), halls[4].transform);
                            }
                        } else if (currentDirection == direction.West) {
                            if (i < 2) {
                                Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x + (i * 8), halls[4].transform.position.y, halls[4].transform.position.z), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y,halls[4].transform.rotation.z), halls[4].transform);
                            } else if (i >= 2) {
                                Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x + 16, halls[4].transform.position.y, halls[4].transform.position.z + ((i - 1) * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 270,halls[4].transform.rotation.z), halls[4].transform);
                            }
                        }
                    } else if (hallTypes[4] == hallType.RightTurn) {
                        if (currentDirection == direction.North) {
                            if (i < 2) {
                                Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x, halls[4].transform.position.y, halls[4].transform.position.z + (i * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 270,halls[4].transform.rotation.z), halls[4].transform);
                            } else if (i >= 2) {
                                Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x + ((i - 1) * 8), halls[4].transform.position.y, halls[4].transform.position.z + 16), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y,halls[4].transform.rotation.z), halls[4].transform);
                            }
                        } else if (currentDirection == direction.East) {
                            if (i < 2) {
                                Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x + (i * 8), halls[4].transform.position.y, halls[4].transform.position.z), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y,halls[4].transform.rotation.z), halls[4].transform);
                            } else if (i >= 2) {
                                Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x + 16, halls[4].transform.position.y, halls[4].transform.position.z - ((i - 1) * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 90,halls[4].transform.rotation.z), halls[4].transform);
                            }
                        } else if (currentDirection == direction.South) {
                            if (i < 2) {
                                Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x, halls[4].transform.position.y, halls[4].transform.position.z - (i * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 90,halls[4].transform.rotation.z), halls[4].transform);
                            } else if (i >= 2) {
                                Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x - ((i - 1) * 8), halls[4].transform.position.y, halls[4].transform.position.z - 16), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 180,halls[4].transform.rotation.z), halls[4].transform);
                            }
                        } else if (currentDirection == direction.West) {
                            if (i < 2) {
                                Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x - (i * 8), halls[4].transform.position.y, halls[4].transform.position.z), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 180,halls[4].transform.rotation.z), halls[4].transform);
                            } else if (i >= 2) {
                                Instantiate(healthPickups[healthPickupID], new Vector3(halls[4].transform.position.x - 16, halls[4].transform.position.y, halls[4].transform.position.z + ((i - 1) * 8)), Quaternion.Euler(halls[4].transform.rotation.x,halls[4].transform.rotation.y + 270,halls[4].transform.rotation.z), halls[4].transform);
                            }
                        }
                    }
                }
            }
        }    
    }

    public void IncreaseStage() {
        stage++;
        UnityEngine.Debug.Log("Stage up: " + stage);
        ph.poisonRate /= 1.5f;
        pm.walkSpeed += 2;
        pm.sprintSpeed += 2;
        pm.crouchSpeed += 2;
    }

    public void EndGame() {
        UnityEngine.Debug.Log("End Game");
    }
}
