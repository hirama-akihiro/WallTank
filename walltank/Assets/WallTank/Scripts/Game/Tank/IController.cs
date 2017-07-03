using UnityEngine;
using System.Collections;

public interface IController
{
	bool IsKanonLeftRotate(GamePad.Index playerIndex);
	bool IsKanonRightRotate(GamePad.Index playerIndex);
	bool IsShot(GamePad.Index playerIndex);
	bool IsSubWeapon(GamePad.Index playerIndex);
	bool IsMoveUp(GamePad.Index playerIndex);
	bool IsMoveLeft(GamePad.Index playerIndex);
	bool IsMoveRight(GamePad.Index playerIndex);
	bool IsMoveDown(GamePad.Index playerIndex);
    void Init(Tank tank,AIBrain brain);
}
