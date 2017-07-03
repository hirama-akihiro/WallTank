using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Finger : MonoBehaviour {

	public GamePad.Index gamePadIndex = GamePad.Index.Any;
	public float moveSpeed = 10.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		Click();
	}

	private void Click()
	{
		if (GamePad.GetButtonDown(GamePad.Button.A, gamePadIndex) || GamePad.GetButtonDown(GamePad.Button.B, gamePadIndex))
		{
			//Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0), new Vector3(0, 0, 10), Color.blue, 1);
			RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), new Vector3(0, 0, 1), 100);

			//if (hit) { Debug.LogFormat(hit.collider.name); }
			if (hit) { hit.transform.GetComponent<Button>().onClick.Invoke(); }
		}
	}

	private void Move()
	{
		Vector2 inputDirection = Vector2.zero;
		// ゲームパッドの入力を取得
		inputDirection = GamePad.GetAxis(GamePad.Axis.LeftStick, gamePadIndex);
		transform.position += new Vector3(inputDirection.x, inputDirection.y, 0) * moveSpeed * Time.deltaTime;

		// 画面内に移動を制限
		if(Camera.main)
		{
			Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
			Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

			float x = Mathf.Clamp(transform.position.x, min.x, max.x);
			float y = Mathf.Clamp(transform.position.y, min.y, max.y);
			transform.position = new Vector3(x, y, 0);
		}
	}
}
