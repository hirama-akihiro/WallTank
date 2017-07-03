using UnityEngine;
using System.Collections;

public class AIBrain : MonoBehaviour,Brain {
    public bool _isMoveUp = false;
    public bool _isMoveLeft = false;
    public bool _isMoveRight = false;
    public bool _isMoveDown = false;
    public bool isInitialized = false;
    public bool isTerminated = false;
    protected bool isDead = false;
    protected Tank tank;
    //public Stack goalList;
    protected GameObject GetItemPrefab;
	public GameObject getItem;
    public void Robotomy()
    {
        isDead = true;
    }
    public void Init(Tank tank)
    {
        this.tank = tank;

        isInitialized = true;
    }
	// Use this for initialization
	void Start () {
        gameObject.transform.parent = RuleManager.I.transform;
        //goalList = new Stack();
        GetItemPrefab = Resources.Load("Prefabs/Tanks/OldAI/AI/GetItem") as GameObject;
        getItem = Instantiate(GetItemPrefab);
        getItem.transform.parent = RuleManager.I.transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (isDead) { return; }
        if (isInitialized&&!isTerminated)
        {
            StartCoroutine(think());
            isTerminated = true;
        }
    }
    /*
    public void think()
    {
        if (goalList.Count != 0)
        {
            GameObject goal = goalList.Pop() as GameObject;
            goal.GetComponent<Goal>().Activate();
            StartCoroutine(WaitMethod(goal));
        }
    }
    */
    public bool isShot()
    {
        if (Random.Range(0, 100) > 95)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isMoveLeft()
    {
        return _isMoveLeft;
    }
    public bool isMoveRight()
    {
        return _isMoveRight;
    }
    public bool isMoveUp()
    {
        return _isMoveUp;
    }
    public bool isMoveDown()
    {
        return _isMoveDown;
    }
    public bool isSubWeapon()
    {
        if (tank.GetComponent<Tank>().subWeaponType != SubWeaponType.None)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool IsKanonLeftRotate()
    {
        return false;
    }
    public bool IsKanonRightRotate()
    {
        return false;
    }
    void SetGoal(Goal goal)
    {
        goal.GetComponent<Goal>().Activate();
    }
    public void SetLeft(bool tof)
    {
        _isMoveLeft = tof;
    }
    public void SetRight(bool tof)
    {
        _isMoveRight = tof;
    }
    public void SetUp(bool tof)
    {

        _isMoveUp = tof;
    }
    public void SetDown(bool tof)
    {
        _isMoveDown = tof;
    }
    public IEnumerator think()
    {
       
		while (true)
		{
            if (isDead)
            {
                break;
            }
			yield return new WaitForSeconds(1f / 60f);
			getItem.GetComponent<Goal>().Init(gameObject.GetComponent<AIBrain>(), tank);
			getItem.GetComponent<Goal>().Activate();
			while (!getItem.GetComponent<Goal>().getTerminated())
			{
				yield return new WaitForSeconds(1f / 60f);
			}
			getItem.GetComponent<Goal>().Activate();
		}
    }
    private IEnumerator WaitMethod(GameObject goal)
    {
        Debug.Log(goal.GetComponent<Goal>().getTerminated()+"");
        while (!goal.GetComponent<Goal>().getTerminated())
        {
            yield return new WaitForSeconds(1f / 60f);
            if (goal.GetComponent<Goal>().getTerminated())
            {
                yield break;
            }
        }
     }
}
