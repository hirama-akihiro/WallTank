using UnityEngine;
using System.Collections;
public class Status
{
    public float rotateSpeed;
    public float moveSpeed;
    public float coolTime;
    public bool isShotable;
	public float invincibleTime;
	public bool isInvincible;
	public float maxHP = 60;
    private float nowHP;
	public float ratioHP = 1.0f;
    public float speed = 10;
    public ArrayList itemHolder = new ArrayList();
    public Status()
    {
        rotateSpeed = 180.0f;
        moveSpeed = 4f;
        coolTime = 1.0f;
		invincibleTime = 1.0f;
		nowHP = maxHP;
        isShotable = true;
		isInvincible = false;
    }
	public Status(float rotateSpeed, float moveSpeed, float coolTime, bool isShotable, bool isInvincible, float speed, ArrayList itemHolder)
	{
		this.rotateSpeed = rotateSpeed;
		this.moveSpeed = moveSpeed;
		this.coolTime = coolTime;
		this.isShotable = isShotable;
		this.isInvincible = isInvincible;
		this.speed = speed;
		this.itemHolder = itemHolder;
	}
	public Status(float rotateSpeed, float moveSpeed, float coolTime, bool isShotable, bool isInvincible, float speed)
	{
		this.rotateSpeed = rotateSpeed;
		this.moveSpeed = moveSpeed;
		this.coolTime = coolTime;
		this.isShotable = isShotable;
		this.isInvincible = isInvincible;
		this.speed = speed;
	}
    /*
    public Status nowStatus()
    {
        Status rtnStatus = this;
        if (itemHolder.Count != 0)
        {
            foreach (GameObject item in itemHolder)
            {

                rtnStatus = rtnStatus + item.GetComponent<Item>().getStatus();

            }
        }
        return rtnStatus;
    }
    */
    public static Status operator +(Status a, Status b)
    {
		//パワーアップアイテムのアタッチで，ステータス上昇→coolTimeは下げる
		return new Status(a.rotateSpeed + b.rotateSpeed, a.moveSpeed + b.moveSpeed, a.coolTime * b.coolTime, a.isShotable & b.isShotable, a.isInvincible & b.isInvincible, a.speed = b.speed, a.itemHolder);
    }
    public static Status operator -(Status a, Status b)
    {
		return new Status(a.rotateSpeed - b.rotateSpeed, a.moveSpeed - b.moveSpeed, a.coolTime / b.coolTime, a.isShotable, a.isInvincible, a.speed - b.speed, a.itemHolder);
    }
    public void Damage(float ap)
    {
        nowHP -= ap;
		ratioHP = nowHP / maxHP;

    }
    public void changeSpeed(float speed)
    {
        moveSpeed = speed;
    }
}
