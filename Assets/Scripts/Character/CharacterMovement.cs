using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class CharacterMovement : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent agent;
    public float turnSmoothing = 15f;
    public float speedDampTime = 0.1f;
    public float slowingSpeed = 0.175f;
    public float turnSpeedThreshold = 0.5f;
    public float inputHoldDelay = 0.5f;

    private Vector3 destinationPosition;
    private bool handleInput = true;
    private WaitForSeconds inputHoldWait;
    private readonly int hashSpeedPara = Animator.StringToHash("Speed");
    private const float stopDistanceProportion = 0.1f;
    private const float navMeshSampleDistance = 4f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.updateRotation = false;
        inputHoldWait = new WaitForSeconds(inputHoldDelay);
    }


    private void Update()
    {
        if (agent.pathPending)
            return;
        float speed = agent.desiredVelocity.magnitude;

        if (agent.remainingDistance <= agent.stoppingDistance * stopDistanceProportion)
            Stopping(out speed);
        else if (speed > turnSpeedThreshold)
            Moving();
        animator.SetFloat(hashSpeedPara, speed, speedDampTime, Time.deltaTime);
    }


    public void MoveToCameraPoint(Vector3 point)
    {
        Ray ray = Camera.main.ScreenPointToRay(point);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            MoveToPoint(hit.point);
        }
    }


    public void MoveToPoint(Vector3 newPosition)
    {
        agent.SetDestination(newPosition);
        agent.isStopped = false;
    }

    public void Stop()
    {
       // agent.SetDestination(agent.transform.position);
        agent.isStopped = true;
    }

    private void Stopping(out float speed)
    {
        agent.isStopped = true;
        speed = 0f;
    }


    private void Moving()
    {
        Quaternion targetRotation = Quaternion.LookRotation(agent.desiredVelocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);
    }



}
