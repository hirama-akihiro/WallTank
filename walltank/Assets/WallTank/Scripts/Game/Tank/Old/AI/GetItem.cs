using UnityEngine;
using System.Collections;

public class GetItem : Goal {
    Transform item;
    GameObject GetItemPrefab;
    bool isActivated;
    bool isRandom;

    GameObject firstTank;
    void Start()
    {
        isTerminated = false;
        isActivated = false;
    
        GetItemPrefab = Resources.Load("Prefabs/Tanks/AI/GetItem") as GameObject;
    }
    void Update()
    {
        if (isActivated&&!isTerminated)
        {
            if (tank != null)
            {
                if (item == null)
                {
                    firstTank = TankManager.I.get1st();
                    if (firstTank != tank)
                    {
                        NavMeshAgent agent = tank.GetComponent<NavMeshAgent>();
                        agent.SetDestination(firstTank.transform.position);
                    }
                    isTerminated = true;
                }
                if ((tank.transform.position - pos).sqrMagnitude < 0.2)
                {
                    isTerminated = true;
                }
            }
        }
    }
    public override void Activate()
    {
        foreach (Transform child in SubWeaponManager.I.transform)
        {
			if(child.GetComponent<GetableObject>() == null) { continue; }
            if(Random.Range(0, 2) > 0)
            {
                item = child;
                break;
            }
        }
        brain.GetComponent<AIBrain>().SetUp(false);
        brain.GetComponent<AIBrain>().SetLeft(false);
        brain.GetComponent<AIBrain>().SetRight(false);
        brain.GetComponent<AIBrain>().SetDown(false);
        NavMeshAgent agent = tank.GetComponent<NavMeshAgent>();
        if (item == null)
        {
            firstTank = TankManager.I.get1st();
            if (firstTank != tank)
            {
                //agent = tank.GetComponent<NavMesh;Agent>();
                agent.SetDestination(firstTank.transform.position);
            }
            isTerminated = true;
        }
        else
        {
            agent.SetDestination(item.position);
        }
        isActivated = true;
    }
}
