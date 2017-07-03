using UnityEngine;
using System.Collections;

/// <summary>
/// プレイヤー番号
/// </summary>
public enum PlayerIndex { P1 = 1, P2 = 2, P3 = 3, P4 = 4 }

public class Tank : MonoBehaviour
{
    public Status myStatus;
	//public float hp = 100;
    protected Rigidbody myRigidBody;
    public GameObject kanon;
    public GameObject bullet;
    public Transform shotPosition;
    public GameObject shotBullet;
    public enum ControllerType { Player, CPU };
    public ControllerType controllerType;
    private IController _controller;
    public SubWeaponType subWeaponType;
    GameObject healEffect;
    private GamePad.Index _playerIndex;

	public bool isInitialized = false;
	public bool isFreeze = true;

    private MeshRenderer[] renderers;
    private bool isBlink = false;
    private float startTime = 0.0f;
    private float nextTime = 0.0f;
    public float interval = 0.1f;	// 点滅周期
    public float blinkTime = 1.0f;  // 点滅時間

	private int gameStartTime;

    public GameObject explosion;    // HP0の時の爆発

    // Use this for initialization
    void Start()
    {
		gameStartTime = (int)RuleManager.I.gameTime;

		myStatus = new Status();
        myRigidBody = GetComponent<Rigidbody>();
        subWeaponType = SubWeaponType.None;
		isFreeze = false;
    }

	/// <summary>
	/// 外部設定後、S明示的に行う初期化メソッド
	/// </summary>
	public void Init(int playerIndex)
	{
        GameObject brainPrefab = Resources.Load("Prefabs/Tanks/OldAI/Brain/AIBrain") as GameObject;
        GameObject brainPrefab2 = Resources.Load("Prefabs/Tanks/OldAI/Brain/AI2Brain") as GameObject;
        healEffect = Resources.Load("Prefabs/Items/Effects/Heal/Prefab/Eff_Heal_1") as GameObject;
        _playerIndex = (GamePad.Index)playerIndex;
		switch (controllerType)
		{
			case ControllerType.Player:
				_controller = GetComponent<PlayerController>();
                try
                {
                    GetComponent<NavMeshAgent>().enabled=false;
                }
                catch(System.Exception e)
                {

                }
				break;
			case ControllerType.CPU:
				_controller = GetComponent<CPUController>();
				switch(playerIndex)
				{
					case 1:
					case 3:
						_controller.Init(gameObject.GetComponent<Tank>(), (Instantiate(brainPrefab) as GameObject).GetComponent<AIBrain>());
						break;
					case 2:
                        _controller.Init(gameObject.GetComponent<Tank>(), (Instantiate(brainPrefab) as GameObject).GetComponent<AIBrain>());
                        break;
                    case 4:
						_controller.Init(gameObject.GetComponent<Tank>(), (Instantiate(brainPrefab2) as GameObject).GetComponent<AIBrain>());
						break;
				}
				break;
		}
		isInitialized = true;
	}

    // Update is called once per frame
    void Update()
    {
		// Initializeメソッドが呼ばれるまでは更新しない
		if (!isInitialized) { return; }
		if (isFreeze) { return; }
		if(gameStartTime == (int)RuleManager.I.gameTime) { return; }

		float _x = Mathf.Clamp(transform.position.x, -11, 11);
		float _y = 0.3f;
		float _z = Mathf.Clamp(transform.position.z, -6.5f, 6.5f);
		transform.position = new Vector3(_x, _y, _z);

		// 砲台回転
		if (_controller.IsKanonLeftRotate(_playerIndex))
        {
            kanon.transform.eulerAngles -= new Vector3(0, myStatus.rotateSpeed * Time.deltaTime, 0);
        }
        else if (_controller.IsKanonRightRotate(_playerIndex))
        {
            kanon.transform.eulerAngles += new Vector3(0, myStatus.rotateSpeed * Time.deltaTime, 0);
        }
        
		// 攻撃
		if (myStatus.isShotable && _controller.IsShot(_playerIndex))
		{
			AudioManager.I.PlayAudio("se_maoudamashii_battle12", 0.6f);
			StartCoroutine(StartCoolTime());
			shotBullet = Instantiate(bullet, shotPosition.transform.position, kanon.transform.rotation) as GameObject;
			shotBullet.GetComponent<MainWeapon>().Shot(myStatus.speed);
			shotBullet.transform.parent = transform.parent;
		}
        // サブ攻撃
		if(_controller.IsSubWeapon(_playerIndex))
		{
			// 攻撃生成ポイントが壁の場合は攻撃を発動しない
			if (StageManager.I.GetStateMapElement(shotPosition.transform.position) == StateMapElement.NormalPlane && subWeaponType != SubWeaponType.Shield)
			{
				SubWeaponManager.I.ActivateSubWeapon(subWeaponType, shotPosition.transform, _playerIndex);
				subWeaponType = SubWeaponType.None;
			}

			// シールドだけ別枠で対処：要対応
			if(subWeaponType == SubWeaponType.Shield)
			{
				SubWeaponManager.I.ActivateSubWeapon(subWeaponType, transform, _playerIndex);
				subWeaponType = SubWeaponType.None;

				// UI部分の更新
				RuleManager.I.IsCheckGameFinish();
			}
		}
        // 移動
                Vector3 newVelocity = Vector3.zero;
                Vector3 newRotation = transform.eulerAngles;
                float ang = 0.0f;
                if (_controller.IsMoveUp(_playerIndex))
                {
                    ang = 360.0f;
                    newVelocity += new Vector3(0, 0, myStatus.moveSpeed);
                }
                else if (_controller.IsMoveDown(_playerIndex))
                {
                    ang = 180.0f;
                    newVelocity += new Vector3(0, 0, -myStatus.moveSpeed);
                }
                if (_controller.IsMoveLeft(_playerIndex))
                {
                    if (ang == 360.0f)
                        ang = 315.0f;
                    else if (ang == 180.0f)
                        ang = 225.0f;
                    else
                        ang = 270.0f;
                    newVelocity += new Vector3(-myStatus.moveSpeed, 0, 0);
                }
                else if (_controller.IsMoveRight(_playerIndex))
                {
                    if (ang == 360.0f)
                        ang = 45.0f;
                    else if (ang == 180.0f)
                        ang = 135.0f;
                    else
                        ang = 90.0f;
                    newVelocity += new Vector3(myStatus.moveSpeed, 0, 0);
                }
                myRigidBody.velocity = newVelocity;
        if (ang != 0.0f)
            for (int i = 0; i < 10; i++)
            {
                newRotation = new Vector3(0f, Mathf.LerpAngle(newRotation.y, ang, Time.deltaTime), 0f);
            }
        //Vector3 KanonRotate = transform.eulerAngles - newRotation;
        //kanon.transform.eulerAngles = kanon.transform.eulerAngles + KanonRotate;
        transform.eulerAngles = newRotation;
                



        #region 古川君の移動処理
        /*
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.UpArrow))
            myRigidBody.AddForce(0.0f, 0.0f, 300.0f);
        else if (Input.GetKey(KeyCode.UpArrow))
            myRigidBody.AddForce(0.0f, 0.0f, v * 10.0f);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            myRigidBody.AddForce(0.0f, 0.0f, -300.0f);
        if (Input.GetKey(KeyCode.DownArrow))
            myRigidBody.AddForce(0.0f, 0.0f, v * 10.0f);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            myRigidBody.AddForce(-300.0f, 0.0f, 0.0f);        
        if (Input.GetKey(KeyCode.LeftArrow))
            myRigidBody.AddForce(h * 10.0f, 0.0f, 0.0f);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            myRigidBody.AddForce(300.0f, 0.0f, 0.0f);
        if (Input.GetKey(KeyCode.RightArrow))
            myRigidBody.AddForce(h * 10.0f, 0.0f, 0.0f);
        */
        #endregion

        /* 点滅処理  */
        if (isBlink && (nextTime < Time.time)) {
            renderers = GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].enabled = !renderers[i].enabled;
            }
            nextTime += interval;
            if (blinkTime < (nextTime - startTime))
            {
                startTime = 0.0f;
                nextTime = 0.0f;
                isBlink = false;
                for (int i = 0; i < renderers.Length; i++)
                {
                    renderers[i].enabled = true;
                }
            }
        }

        /* HP0時の処理 */
        if (myStatus.ratioHP <= 0.0f)
        {
            isFreeze = true;
            GameObject explosionObject = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            explosionObject.transform.parent = gameObject.transform.parent;
            Destroy(explosionObject.gameObject, 1.5f);

            myRigidBody.transform.position = new Vector3(0, -100, 0);

            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].enabled = false;
            }
            if (controllerType == ControllerType.CPU)
            {
                CPUController cpuc = (CPUController)_controller;
                cpuc.Robotomy();
                NavMeshAgent nav = gameObject.GetComponent<NavMeshAgent>();
                StartCoroutine(WaitToDeath(nav));

            }
        }

    }

    void DeleteRigid()
    {
        myRigidBody.isKinematic = true;
        myRigidBody.isKinematic = false;
    }

    public void Damage(float attack)
    {
        if (myStatus.isInvincible) { return; }

        // ダメージの点滅処理はここに記述・・・かな
        isBlink = true;
        startTime = Time.time;
        nextTime = Time.time;

		// ゲーム終了チェック
		RuleManager.I.IsCheckGameFinish();

        myStatus.Damage(attack);
		StartCoroutine(StartInvincibleTime());
	}
    public void Heal(float mass)
    {
        myStatus.Damage(-mass);
        GameObject obj = Instantiate(healEffect);
        obj.transform.position = gameObject.transform.position;
        obj.transform.parent = gameObject.transform;
        StartCoroutine(HealEffectDuration(obj));
    }
    //ステータス（移動スピード）の変化処理
    public void changeSpeed(float speed)
    {
        //if (myStatus.isInvincible) { return; }

        myStatus.changeSpeed(speed);
    }

	public void ActivateInvincible(float mag = 1.0f)
	{
		StartCoroutine(StartInvincibleTime(mag));
	}

	IEnumerator StartInvincibleTime(float mag = 1.0f)
	{
		myStatus.isInvincible = true;
		yield return new WaitForSeconds(myStatus.invincibleTime * mag);
		myStatus.isInvincible = false;
	}

	IEnumerator StartCoolTime()
    {
        myStatus.isShotable = false;
        yield return new WaitForSeconds(myStatus.coolTime);
        myStatus.isShotable = true;
    }
    IEnumerator HealEffectDuration(GameObject obj)
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(obj);
    }
    IEnumerator WaitToDeath(NavMeshAgent nav)
    {
        yield return new WaitForSeconds(1f);
        Destroy(nav);
        myRigidBody.transform.position = new Vector3(0, -100, 0);
    }
}
