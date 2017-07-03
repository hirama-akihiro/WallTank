using UnityEngine;
using System.Collections;

public interface Brain{
    void Robotomy();
    IEnumerator think();
    void Init(Tank tank);
    bool isShot();
    bool isMoveLeft();
    bool isMoveRight();
    bool isMoveUp();
    bool isMoveDown();
    bool isSubWeapon();
    bool IsKanonLeftRotate();
    bool IsKanonRightRotate();
}
