using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour, IController
{
    public bool IsKanonLeftRotate(GamePad.Index playerIndex) { return GamePad.GetButton(GamePad.Button.LeftShoulder, playerIndex)|| Input.GetKey(KeyCode.Q); }
    public bool IsKanonRightRotate(GamePad.Index playerIndex) { return GamePad.GetButton(GamePad.Button.RightShoulder, playerIndex)|| Input.GetKey(KeyCode.E); }
    //public bool IsKanonLeftRotate(GamePad.Index playerIndex) { return GamePad.GetButtonDown(GamePad.Button.LeftShoulder, playerIndex) || Input.GetKey(KeyCode.Q); }
    //public bool IsKanonRightRotate(GamePad.Index playerIndex) { return GamePad.GetButtonDown(GamePad.Button.RightShoulder, playerIndex) || Input.GetKey(KeyCode.E); }
    public bool IsShot(GamePad.Index playerIndex) { return GamePad.GetButtonDown(GamePad.Button.A, playerIndex) || Input.GetKeyDown(KeyCode.Space); }
	public bool IsSubWeapon(GamePad.Index playerIndex) { return GamePad.GetButtonDown(GamePad.Button.B, playerIndex) || Input.GetKeyDown(KeyCode.S); }
	public bool IsMoveUp(GamePad.Index playerIndex) { return GamePad.GetAxis(GamePad.Axis.LeftStick, playerIndex).y > 0.6 || Input.GetKey(KeyCode.UpArrow); }
	public bool IsMoveLeft(GamePad.Index playerIndex) { return GamePad.GetAxis(GamePad.Axis.LeftStick, playerIndex).x < -0.6 || Input.GetKey(KeyCode.LeftArrow); }
	public bool IsMoveRight(GamePad.Index playerIndex) { return GamePad.GetAxis(GamePad.Axis.LeftStick, playerIndex).x > 0.6 || Input.GetKey(KeyCode.RightArrow); }
	public bool IsMoveDown(GamePad.Index playerIndex) { return GamePad.GetAxis(GamePad.Axis.LeftStick, playerIndex).y < -0.6 || Input.GetKey(KeyCode.DownArrow); }
    public void Init(Tank tank,AIBrain brain) { }
}
