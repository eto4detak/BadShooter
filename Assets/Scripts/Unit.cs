using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    [HideInInspector] public static List<Unit> allUnits = new List<Unit>();
    [HideInInspector] public static GameObject prefab;
    [HideInInspector] public float armor = 1f;
    [HideInInspector] public float aggression = 0f;
    [HideInInspector] public float attackRadius = 2f;
    [HideInInspector] public float domage = 1f;
    [HideInInspector] public float maxHealth = 100f;
    [HideInInspector] public float speed = 2f;
    [HideInInspector] public bool canDamage = true;
    [HideInInspector] public bool canRun = true;
    [HideInInspector] public float jumpForce = 20.0f;
    [HideInInspector] public bool isGrounded = true;
    [HideInInspector] public float morale = 1f;
    [HideInInspector] public float maxMorale = 1f;
    [HideInInspector] public float maxAggression = 1f;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public CharacterManager manager;
    [HideInInspector] public Collider body;

    private float health = 100f;

    protected List<float> allTypeAggression = new List<float>();
    protected UnitCommand command;
    protected List<Unit> commandTargets = new List<Unit>();
    protected UnitStatus status = UnitStatus.Norm;
    protected float slowDown = 1f;
    protected Faction faction = Faction.Neutral;

    [HideInInspector] public Vector3? NewPosition { get; set; }

    [HideInInspector]
    public float Health { get => health;
        set
        {
            health = value;
            if(health <= 0)
            {
                Die();
            }
        }
    }

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        body = GetComponentInChildren<Collider>();
        allUnits.Add(this);
    }

    private void OnDestroy()
    {
        allUnits.Remove(this);
    }


    protected virtual void Start()
    {
        command = new MoveCommand(this, transform.position);
        agent.destination = transform.position;
    }

    protected virtual void Update()
    {
        CheckErrorPositionY();
        UpdateAction();
    }

    private void OnCollisionStay(Collision other)
    {
        Unit target = other.gameObject.GetComponent<Unit>();
    }

    public virtual Sprite GetSprite()
    {
       return Resources.Load<Sprite>("Sprite/unit");
    }
    public void Damage(Unit target, float val)
    {
        target.Health = Mathf.Max(target.Health - val, 0);
    }

    protected virtual void AimCounterpoise()
    {
        transform.position = Vector3.MoveTowards(transform.position, (Vector3)NewPosition, speed * Time.deltaTime);
    }

    private void CheckErrorPositionY()
    {
        if (transform.position.y < GManager.minPositionY)
        {
            transform.position = new Vector3(transform.position.x, GManager.startPositionY, transform.position.z);
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
    public virtual void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private float GetDistanceToTarget()
    {
        if (commandTargets[0] != null)
        {
            return Vector3.Distance(commandTargets[0].transform.position, transform.position);
        }
        return float.PositiveInfinity;
    }

    public static void SetAttackTarget(List<Unit> units, List<Unit> target)
    {
        foreach (var selectedUnit in units)
        {
            selectedUnit.SetAttackTarget(target);
        }
    }

    public void SetAttackTarget(List<Unit> target)
    {
        commandTargets.Clear();
       // command = UnitCommand.Attack;
        commandTargets.AddRange(target);

        if (commandTargets.Count > 0)
        {
            agent.destination = commandTargets[0].transform.position;
        }
    }

    public virtual void ReceiveDamage(float damage = 1f)
    {
        Health -= damage;
    }
    public virtual void RunToPoint()
    {
        if ( NewPosition == null || !canRun) return;
        if ( NewPosition == transform.position)
        {
            NewPosition = null;
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, (Vector3)NewPosition, speed * Time.deltaTime);
    }

    public void MoveToPoint(Vector3 point)
    {
        status = UnitStatus.Run;
        Ray ray = Camera.main.ScreenPointToRay(point);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            agent.destination = hit.point;
        }
    }

    public void MoveToPoint3D(Vector3 newPosition)
    {
         agent.destination = newPosition;
    }


    public static void SetMoveCommand(List<Unit> executers, Vector3 newPosition)
    {
        for (int i = 0; i < executers.Count; i++)
        {
            executers[i].command = new MoveCommand(executers[i], newPosition);
        }
    }
    public void SetCommand(UnitCommand newCommand)
    {
        command = newCommand;
    }

    private void UpdateAction()
    {

        //if (command == UnitCommand.Attack)
        //{
        //    if (attackRadius > GetDistanceToTarget())
        //    {
        //      //  Debug.Log(name + " Attack " + commandTargets[0].name);
        //        Damage(commandTargets[0], domageVal);
        //    }
        //    else
        //    {
        //    }

        //    if (status == UnitStatus.Battle)
        //    {

        //    }else if (status == UnitStatus.Run)
        //    {

        //    }
        //    else
        //    {
        //    }
        //}
    }

}

public enum Faction : int
{
    Hero = 0,
    Red,
    Blue,
    Back,
    Green,
    White,
    Brown,
    Orange,
    Neutral,
    Passive,
    Hostile,
    Boss,
}

public enum UnitStatus : int
{
    Norm = 0,
    Run,
    Immobilized,
    Battle,

}
