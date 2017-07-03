using UnityEngine;
using System.Collections;
public class CPUController : MonoBehaviour, IController {
	private bool isKanonLeftRotate = false;
	private bool isKanonRightRotate = false;
	private bool isShot = false;
	private bool isSubWeapon = false;
	private bool isMoveUp = false;
	private bool isMoveLeft = false;
	private bool isMoveRight = false;
	private bool isMoveDown = false;
    bool isInitialized = false;
	public bool isFreeze;
    bool isStarted = false;
    bool isDead = false;
    Tank myTank;
    AIBrain brain;
	// Use this for initialization
	void Start () {
		if (isFreeze) { return; }
	}
    void Update()
    {
        if (isDead)
        {
            return;
        }
        if (isInitialized&&!isStarted)
        {
            
            StartCoroutine(CPU());
        }
    }
    public void Robotomy()
    {
        brain.Robotomy();
    }
    public void Init(Tank myTank,AIBrain brain)
    {
        this.myTank = myTank;
        this.brain = brain;
		this.brain.Init(myTank);
        this.brain.transform.parent = RuleManager.I.transform;
        Debug.Log(brain);
        isInitialized = true;
    }
	private IEnumerator CPU()
	{
        isStarted = true;
		//yield return new WaitForSeconds(2.0f);
		while (true)
		{
            if (!isDead)
            {
                //yield return new WaitForSeconds(0.45f);
                yield return new WaitForSeconds((1f / 60f));
                isMoveUp = brain.GetComponent<Brain>().isMoveUp();
                isMoveLeft = brain.GetComponent<Brain>().isMoveLeft();
                isMoveRight = brain.GetComponent<Brain>().isMoveRight();
                isMoveDown = brain.GetComponent<Brain>().isMoveDown();
                isSubWeapon = brain.GetComponent<Brain>().isSubWeapon();
                //yield return new WaitForSeconds(2f);
                isShot = brain.GetComponent<Brain>().isShot();
                //yield return new WaitForSeconds(1f);
            }
            else
            {
                break;
            }
		}
	}

	public bool IsKanonLeftRotate(GamePad.Index playerIndex) { return isKanonLeftRotate; }
	public bool IsKanonRightRotate(GamePad.Index playerIndex) { return isKanonRightRotate; }
	public bool IsShot(GamePad.Index playerIndex) { return isShot; }
	public bool IsSubWeapon(GamePad.Index playerIndex) { return isSubWeapon; }
	public bool IsMoveUp(GamePad.Index playerIndex) { return isMoveUp; }
	public bool IsMoveLeft(GamePad.Index playerIndex) { return isMoveLeft; }
	public bool IsMoveRight(GamePad.Index playerIndex) { return isMoveRight; }
	public bool IsMoveDown(GamePad.Index playerIndex) { return isMoveDown; }
}
