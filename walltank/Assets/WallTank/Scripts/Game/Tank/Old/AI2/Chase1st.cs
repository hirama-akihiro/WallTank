using UnityEngine;
using System.Collections;

public class Chase1st : Goal
{
    Transform item;
    GameObject GetItemPrefab;
    GameObject firstTank;
    bool isActivated;
    bool isRandom;
    void Start()
    {
        GetItemPrefab = Resources.Load("Prefabs/Tanks/OldAI/AI/GetItem") as GameObject;
        isTerminated = false;
        isActivated = false;
    }
    void Update()
    {
        if (isActivated && !isTerminated)
        {
            if (tank != null)
            {
                NavMeshAgent agent = tank.GetComponent<NavMeshAgent>();
                agent.SetDestination(firstTank.transform.position);
                if (firstTank !=TankManager.I.get1st())
                {
                    isTerminated = true;
                }
            }
        }
    }
    public override void Activate()
    {
       
        brain.GetComponent<AI2Brain>().SetUp(false);
        brain.GetComponent<AI2Brain>().SetLeft(false);
        brain.GetComponent<AI2Brain>().SetRight(false);
        brain.GetComponent<AI2Brain>().SetDown(false);
        firstTank = TankManager.I.get1st();
        if (firstTank != tank)
        {
            NavMeshAgent agent = tank.GetComponent<NavMeshAgent>();
            agent.SetDestination(firstTank.transform.position);
        }
        else
        {
			//GameObject get = Instantiate(GetItemPrefab);
			//get.transform.parent = RuleManager.I.transform;
			//brain.GetComponent<AIBrain>().goalList.Push(get);
			brain.GetComponent<AIBrain>().getItem.GetComponent<Goal>().Activate();
            isTerminated = true;
         }
        isActivated = true;
    }
}
