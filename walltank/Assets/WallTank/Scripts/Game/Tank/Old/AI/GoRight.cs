//using UnityEngine;
//using System.Collections;

//public class GoRight : Goal
//{
//    Vector2 tankPos;
//    Vector3 truePos;
//    // Update is called once per frame
//    void Update()
//    {

//    }
//    public override void Init(GameObject brain, GameObject tank)
//    {
//        this.brain = brain;
//        this.tank = tank;
//        tankPos = StageManager.I.CalcStateMapIndex(tank.transform.position);
//        truePos = StageManager.I.TOP_LEFT_POSITION + new Vector3(tankPos.y * 1.8f,0, -tankPos.x * 1.8f);
//        StartCoroutine(WaitMethod());
//    }
//    public override void Activate()
//    {
//        StartCoroutine(WaitMethod());
//        brain.GetComponent<AIBrain>().SetRight(true);
//        StartCoroutine(DelayMethod(0.45f));
//    }
//    private IEnumerator DelayMethod(float waitTime)
//    {
//        yield return new WaitForSeconds(waitTime);
//        brain.GetComponent<AIBrain>().SetRight(false);
//        truePos = truePos + new Vector3(1.8f, 0, 0);
//        while ((truePos - tank.transform.position).sqrMagnitude > 1)
//        {
//            yield return new WaitForSeconds(1f / 60f);
//            bool _isMoveUp = truePos.z > tank.transform.position.z;
//            bool _isMoveLeft = truePos.x < tank.transform.position.x;
//            bool _isMoveRight = !_isMoveLeft;
//            bool _isMoveDown = !_isMoveUp;
//            brain.GetComponent<AIBrain>().SetUp(_isMoveUp);
//            brain.GetComponent<AIBrain>().SetLeft(_isMoveLeft);
//            brain.GetComponent<AIBrain>().SetRight(_isMoveRight);
//            brain.GetComponent<AIBrain>().SetDown(_isMoveDown);

//        }
//        brain.GetComponent<AIBrain>().SetUp(false);
//        brain.GetComponent<AIBrain>().SetLeft(false);
//        brain.GetComponent<AIBrain>().SetRight(false);
//        brain.GetComponent<AIBrain>().SetDown(false);
//        isTerminated = true;
//        yield break;
//    }
//    private IEnumerator WaitMethod()
//    {
//        while ((truePos - tank.transform.position).sqrMagnitude > 1)
//        {
//            yield return new WaitForSeconds(1f / 60f);
//            bool _isMoveUp = truePos.z > tank.transform.position.z;
//            bool _isMoveLeft = truePos.x < tank.transform.position.x;
//            bool _isMoveRight = !_isMoveLeft;
//            bool _isMoveDown = !_isMoveUp;
//            brain.GetComponent<AIBrain>().SetUp(_isMoveUp);
//            brain.GetComponent<AIBrain>().SetLeft(_isMoveLeft);
//            brain.GetComponent<AIBrain>().SetRight(_isMoveRight);
//            brain.GetComponent<AIBrain>().SetDown(_isMoveDown);

//        }
//        brain.GetComponent<AIBrain>().SetUp(false);
//        brain.GetComponent<AIBrain>().SetLeft(false);
//        brain.GetComponent<AIBrain>().SetRight(false);
//        brain.GetComponent<AIBrain>().SetDown(false);
//        yield break;
//    }
//}