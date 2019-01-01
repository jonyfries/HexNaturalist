using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurtleController : MonoBehaviour
{
    public CharacterMovement characterMovement;
    public MentalMap mentalMap;
    public float moveTime;
    public bool isDeciding;

	void Start ()
    {
        Invoke("LateStart", .11f);
        enabled = false;
    }

    void LateStart()
    {
        mentalMap = characterMovement.mentalMap;
        enabled = true;
    }

    void Update()
    {
        if (characterMovement.movementPath.Count == 0 && !isDeciding)
        {
            Invoke("Move", moveTime);
            isDeciding = true;
        }
    }

    void Move()
    {
        List<Hex> path = mentalMap.GetPath(mentalMap.map.ElementAt(Random.Range(0, mentalMap.map.Count)).Value.hex);
        characterMovement.SetPath(path);
        isDeciding = false;
    }
}
