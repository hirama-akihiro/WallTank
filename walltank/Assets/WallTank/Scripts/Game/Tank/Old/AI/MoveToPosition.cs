using UnityEngine;
using System.Collections;

public class MoveToPosition : Goal {
	// Use this for initialization
	// Update is called once per frame
	void Update () {
        if (tank != null)
        {

            if ((tank.transform.position-pos).sqrMagnitude<0.5)
            {
                
                isTerminated = true;
            }
        }
	}
    public override void Activate()
    {
        brain.GetComponent<AIBrain>().SetUp(false);
        brain.GetComponent<AIBrain>().SetLeft(false);
        brain.GetComponent<AIBrain>().SetRight(false);
        brain.GetComponent<AIBrain>().SetDown(false);
        NavMeshAgent agent = tank.GetComponent<NavMeshAgent>();
        agent.SetDestination(pos);
    }
    public void Stopper()
    {
        isTerminated = true;
        NavMeshAgent agent = tank.GetComponent<NavMeshAgent>();
        agent.SetDestination(new Vector3(-9.3f, 0.3f, 5.5f));
    }
}
