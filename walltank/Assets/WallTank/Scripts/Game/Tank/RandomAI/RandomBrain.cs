using UnityEngine;
using System.Collections;

public class RandomBrain :MonoBehaviour,Brain{
    private bool _isMoveUp = false;
    private bool _isMoveLeft = false;
    private bool _isMoveRight = false;
    private bool _isMoveDown = false;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(think());
    }
    Tank tank;
    public void Init(Tank tank)
    {
        this.tank = tank;
    }
    public bool isShot() { return true; }
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
    public IEnumerator think()
    {
        while (true)
        {
            _isMoveUp = Random.Range(0, 2) > 0;
            _isMoveLeft = Random.Range(0, 2) > 0;
            _isMoveRight = !_isMoveLeft;
            _isMoveDown = !_isMoveUp;
        }
    }

    public void Robotomy()
    {
    }
}