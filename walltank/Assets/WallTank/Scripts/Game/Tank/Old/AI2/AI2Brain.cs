using UnityEngine;
using System.Collections;

public class AI2Brain :  AIBrain
{
    new public void Init(Tank tank)
    {
        this.tank = tank;

        isInitialized = true;
    }
    // Use this for initialization
    void Start()
    {
        gameObject.transform.parent = RuleManager.I.transform;
        //goalList = new Stack();
        GetItemPrefab = Resources.Load("Prefabs/Tanks/OldAI/AI/Chase1st") as GameObject;
        getItem = Instantiate(GetItemPrefab);
        getItem.transform.parent = RuleManager.I.transform;
        //goalList.Push(GetItem);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) { return; }
        if (isInitialized && !isTerminated)
        {
            StartCoroutine(think());
            isTerminated = true;
        }
    }
    new public IEnumerator think()
    {
		while (true)
		{
            if (isDead)
            {
                break;
            }
            yield return new WaitForSeconds(1f / 60f);

			//GameObject goal = goalList.Pop() as GameObject;
			getItem.GetComponent<Goal>().Init(gameObject.GetComponent<AI2Brain>(), tank);
			getItem.GetComponent<Goal>().Activate();
			while (!getItem.GetComponent<Goal>().getTerminated())
			{
				yield return new WaitForSeconds(1f / 60f);
			}
			getItem.GetComponent<Goal>().Activate();
			//GameObject newGoal = Instantiate(GetItemPrefab);
			//newGoal.transform.parent = RuleManager.I.transform;
			//goalList.Push(newGoal);
			//Destroy(goal);
		}
    }
}
