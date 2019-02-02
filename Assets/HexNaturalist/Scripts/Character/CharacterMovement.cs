using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public MentalMap mentalMap;
    public CharacterVision vision;
    public CharacterController controller;
    public Animator animator;

    public List<Hex> movementPath { get; private set; }

    [SerializeField] private Hex _location;

    //Used for moving
    private Vector3 targetPosition;
    private Vector3 startPosition;
    public float moveTime = 3f;
    private float moveTimer = 0f;

    private Quaternion targetRotation;
    private Quaternion startRotation;
    public float rotateTime = .5f;
    private float rotateTimer = 0f;

    public Hex location
    {
        get
        {
            return _location;
        }

        set
        {
            _location = value;
        }
    }

    /// <summary>
    /// Sets up the character movement to be ready to process.
    /// </summary>
    void Start()
    {
        movementPath = new List<Hex>();
        enabled = false;
        Invoke("LateStart", .1f);
    }

    /// <summary>
    /// Creates the MentalMap for this character.
    /// </summary>
    void LateStart()
    {
        mentalMap = new MentalMap(this);
        transform.position = location.GetWorldPosition();
        vision.mentalMap = mentalMap;
        vision.OnMove(location);
    }

    void Update()
    {
        if (transform.rotation != targetRotation)
        {
            rotateTimer += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, rotateTimer / rotateTime);
        }
        else
        {
            rotateTimer = 0;
        }

        if (transform.position != targetPosition)
        {
            moveTimer += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, moveTimer / moveTime);
        }
        else
        {
            moveTimer = 0;
            enabled = false;
            if (movementPath.Count != 0)
            {
                Move();
            }
            else
            {
                StopWalking();
            }
        }
    }

    /// <summary>
    /// Move character to next location in the path.
    /// </summary>
    public void Move()
    {
        Hex nextHex = movementPath[0];

        if (nextHex.entryCost > controller.remainingActionPoints)
        {
            StopWalking();
            return;
        }

        StartWalking(nextHex);

        controller.remainingActionPoints -= nextHex.entryCost;
        location = nextHex;
        movementPath.RemoveAt(0);
        vision.OnMove(location);
    }

    /// <summary>
    /// Used to set the characters current position. Should only be used to teleport a character or to place them at the start of the game.
    /// </summary>
    /// <param name="target">Hex where the character should now be located.</param>
    public void SetCurrentHex(Hex target)
    {
        transform.position = target.GetWorldPosition();
        location = target;
        Invoke("LateStart", .1f);
    }

    /// <summary>
    /// Handles everything relavent to when the character starts moving.
    /// </summary>
    private void StartWalking(Hex target)
    {
        targetPosition = target.GetWorldPosition();
        startPosition = transform.position;

        //Turn to face the destination
        Vector3 relativePos = targetPosition - transform.position;
        targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);
        startRotation = transform.rotation;

        //Start walking animation
        animator.SetTrigger("startWalking");

        //Start Walking
        enabled = true;
    }

    /// <summary>
    /// Handles everything relavent when the character stops moving.
    /// </summary>
    private void StopWalking()
    {
        controller.hasActions = false;
        animator.SetTrigger("stopWalking");
    }

    /// <summary>
    /// Sets the path for the character to follow.
    /// </summary>
    /// <param name="newPath">Path for character to follow.</param>
    public void SetPath(List<Hex> newPath)
    {
        if (newPath.Count == 0)
        {
            movementPath = newPath;
            return;
        }

        if (newPath[0] == location)
        {
            newPath.RemoveAt(0);
        }

        movementPath = newPath;

        if (movementPath.Count != 0)
        {
            // If the character is already moving don't call Move again.
            if (!enabled)
            {
                Move();
            }
        }
    }
}
