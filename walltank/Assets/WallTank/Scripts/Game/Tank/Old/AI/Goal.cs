using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
    protected AIBrain brain;
    protected Tank tank;
    public bool isTerminated;
    protected Vector3 pos;
    // Use this for initialization
    void Start() {
        isTerminated = false;
    }

    // Update is called once per frame
    void Update() {

    }
    void Stopper()
    {
        isTerminated = true;
    }
    virtual public void Activate()
    {
    }
    virtual public void Init(AIBrain brain,Tank tank)
    {
        this.brain = brain;
        this.tank= tank;
    }
    virtual public void Init(AIBrain brain, Tank tank,Vector3 pos)
    {
        this.brain = brain;
        this.tank = tank;
        this.pos = pos;
    }
    void Process()
    {

    }
    void Terminate()
    {

    }
    void AddSubgoal(Goal g)
    {

    }
    public bool getTerminated()
    {
        return isTerminated;
    }
}
