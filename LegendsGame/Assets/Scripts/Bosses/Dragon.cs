using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    // This gameobject also acts as the target for the IK
    // This script will control the target's movement
    public GameObject dragonTarget;

    public Transform baseOfNeck;

    public GameObject player;

    //Enum to control 
    DragonState dragonState;
    DragonState lastState;

    int repeatedStateCount = 0;
    int numberOfStates = 3;

    float minIdleTime = 2.5f;
    float maxIdleTime = 4;
    float idleTime;

    float lungeTimer, maxLungeTimer = 1.5f;
    float waitTimer, maxWaitTimer = 1.5f;
    float fireTimer, maxFireTimer = 3;
    public GameObject fireBreath;

    private bool isFireAttacking = false;

    LungeState lungeState;
    FireState fireState;

    //Health health;
    public DragonController1 dragonController;

    //All the same for now
    public int touchDamage = 1;
    //public int lungeDamage;
    //public int fireDamage;

    void Start()
    {
        //lungeDamage = touchDamage = fireDamage;

        dragonTarget.transform.position = gameObject.transform.position;

        dragonState = DragonState.IDLING;
        lastState = dragonState;

        player = GameObject.FindGameObjectWithTag("Player");
        dragonController = GameObject.FindGameObjectWithTag("DragonController").GetComponent<DragonController1>();
        //health = GetComponent<Health>();

        lungeTimer = maxLungeTimer;
        waitTimer = maxWaitTimer;
        fireTimer = maxFireTimer;

        lungeState = LungeState.Anticipation;
        fireState = FireState.Fire;
        //Set initial dragon target position
    }

    void Update()
    {
        if(isFireAttacking)
        {
            fireTimer -= Time.deltaTime;
        }

        //Debug.Log(fireTimer);

        switch (dragonState)
        {
            case DragonState.IDLING:
                Idle();
                break;
            case DragonState.FIRE_BREATH:
                FireAttack();
                break;
            case DragonState.LUNGE:
                LungeAttack();
                break;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            setState(DragonState.FIRE_BREATH);
        }
    }

    float alpha = 0f;
    float variation = 0;
    void Idle()
    {
        //I hate that this is hard coded but we don't have a lot of time atm
        // this if/else is used to ease back to a neutral position after state switching
        //xPos = baseOfNeck.transform.position.x - 8;
        //yPos = baseOfNeck.transform.position.y;

        idleTime -= Time.deltaTime;
        if (idleTime <= 0)
        {
            idleTime = Random.Range(minIdleTime, maxIdleTime);
            changeState();
        }


        float height = 1.75f;
        float width = height / 2;
        //float xOffset = 0;
        float xOffset = baseOfNeck.transform.position.x - 8;
        float yOffset = baseOfNeck.transform.position.y;

        //transform.position = new Vector2(center.x + (semiMajor * Mathf.Sin(AngleX)),
        //                                 center.y + (semiMinor * Mathf.Cos(AngleY)));

        //Below is stolen code to move the dragon in an ellipse.
        // Edit the alpha value to edit the speed it rotates in (variable located above Idle function)
        // edit the height and width values to edit height and width of the ellipse
        // edit the offset to edit the ellipses center relative to 0,0
        Vector2 move = dragonTarget.transform.position;
        move.x = Mathf.Lerp(move.x, xOffset + (height * Mathf.Sin(Mathf.Deg2Rad * alpha)), rate * Time.deltaTime);
        move.y = Mathf.Lerp(move.y, yOffset + (height * Mathf.Sin(Mathf.Deg2Rad * alpha)), rate * Time.deltaTime);
        dragonTarget.transform.position = move;//new Vector2(xOffset + (height * Mathf.Sin(Mathf.Deg2Rad * alpha)),
                                            //yOffset + (width / 2 * Mathf.Cos(Mathf.Deg2Rad * alpha)));
        variation = Random.Range(0.9f, 1.2f);
        alpha += 2f * variation;
        //if (isCloseEnough(xPos, yPos, distanceThreshold))
        //{

        //}
        //else
        //{
        // dragonTarget.transform.position = new Vector2(xPos, yPos);
        //}

        //can be used as speed
    }

    void setState(DragonState state)
    {
        dragonState = state;

        idleTime = maxIdleTime;
        alpha = 0;
        distanceThreshold = 0.1f;

        lungeTimer = maxLungeTimer;
        waitTimer = maxWaitTimer;
        fireTimer = maxFireTimer;

//        Debug.Log("Set state to: " + dragonState);

    }

    private void changeState()
    {
        //If we're calling this method its safe to reset the lunge and waits timer
        xPos = 100;
        yPos = 100;
        alpha = 0;

        lastState = dragonState;
        setState((DragonState)Random.Range(0, numberOfStates));
        if (repeatedStateCount != 1)
        {
            if (dragonState==lastState)
            {
                repeatedStateCount++;
//                Debug.Log("Set state to: " + dragonState);
                return;
            }
            repeatedStateCount = 0;
            Debug.Log("Set state to: " + dragonState);
            return;
        }

        while (dragonState == lastState)
        {
            Debug.Log("The same state has been set for the last 3 times, entering loop to break this cycle");
            setState((DragonState)Random.Range(0, numberOfStates));
            repeatedStateCount = 0;
        }
        Debug.Log("Set state to: " + dragonState);
    }

    GameObject fire;
    Vector2 temp;
    void FireAttack()
    {
        move = dragonTarget.transform.position;

        xPos = player.transform.position.x;
        yPos = player.transform.position.y;
        rate = 0.7f;
        //This if statement MUST be in this exact order
        if (fireState == FireState.Fire && isCloseEnough(xPos, yPos))
        {
            GetComponents<IKManager>()[0].enabled = false;
            GetComponents<IKManager>()[1].enabled = false;
            //Vector2 pos = new Vector2(gameObject.transform.Find("FireBreathLocation").transform.position.x, gameObject.transform.Find("FireBreathLocation").transform.position.x);
            fire = Instantiate(fireBreath, new Vector3(0, 100, 0), fireBreath.transform.rotation);
            fire.transform.SetParent(this.transform);
            fire.transform.localPosition = gameObject.transform.Find("FireBreathLocation").transform.localPosition;
            Vector3 eulerRotation = fire.transform.rotation.eulerAngles;
            fire.transform.localRotation = Quaternion.Euler(0, eulerRotation.y, eulerRotation.z);
            //fire.transform.localRotation = new Quaternion(fire.transform.localRotation.z, fire.transform.localRotation.y, fire.transform.localRotation.z, fire.transform.localRotation.w);

            isFireAttacking = true;

            temp = dragonTarget.transform.position;
            //fireTimer -= Time.deltaTime;
        }

        if (fireTimer <= 0 && fireState == FireState.Return)
        {
            Destroy(fire);
            GetComponents<IKManager>()[0].enabled = true;
            GetComponents<IKManager>()[1].enabled = true;

            xPos = temp.x + 2;
            yPos = this.transform.position.y;

            distanceThreshold = 0.25f;
            rate = 2f;

            if (isCloseEnough(xPos, yPos))
            {
                xPos = baseOfNeck.transform.position.x - 8;
                yPos = baseOfNeck.transform.position.y;
            }
            fire = null;
            isFireAttacking = false;

            if (fireState == FireState.Neutral)
            {
                fireState = FireState.Fire;
                setState(DragonState.IDLING);
            }
        }

        move.x = Mathf.Lerp(move.x, xPos, rate * Time.deltaTime);
        move.y = Mathf.Lerp(move.y, yPos, rate * Time.deltaTime);
        dragonTarget.transform.position = move;
    }

    float rate;
    float distanceThreshold = 0.75f;
    float xPos = 100, yPos = 100; //set them so far away its unreasonable
    Vector2 move;
    //TODO (Aiden) add easing functions

    //TODO I don't like the way these methods are setup... but it may be too late to change these now as I have too much to do
    void LungeAttack()
    {
        //TODO if time, separate this method into 2 separate methods... one to anticipate, one to lunge
        move = dragonTarget.transform.position;
        distanceThreshold = 0.75f;
        //Use anticipation to telegraph the lunge
        if (!isCloseEnough(xPos, yPos, distanceThreshold) && lungeState == LungeState.Anticipation)
        {
            //bring target to near base... then shoot out to player's position
            xPos = baseOfNeck.position.x - 2;
            yPos = baseOfNeck.position.y + 3;
            rate = 0.75f;
        }
        else
        {
            if (lungeTimer >= 0)
            {
                //Move it in a tiny ellipse so the player doesn't think its frozen
                float height = 0.5f;
                float width = height / 2;
                //Technically assigning the variable like this doesn't achieve the desired result
                // however the result actual looks fine so i'm leaving it
                float offset = dragonTarget.transform.position.x;

                xPos = offset + (height * Mathf.Sin(Mathf.Deg2Rad * alpha));
                yPos = offset + (width / 2 * Mathf.Cos(Mathf.Deg2Rad * alpha));

                dragonTarget.transform.position = new Vector2(xPos, yPos);

                alpha += 2f;//can be used as speed

                //Freeze it in place to simulate lunge
                lungeTimer -= Time.deltaTime;
                GetComponents<IKManager>()[0].enabled = false;

            }
            else
            {
                xPos = player.transform.position.x;
                yPos = player.transform.position.y;
                rate = 5f;

                if (isCloseEnough(xPos, yPos) && waitTimer >=0)
                {
                    waitTimer -= Time.deltaTime;
                }
            }
        }

        //Break out of the lunge state
        if(waitTimer <= 0 && lungeState == LungeState.Neutral)
        {
            GetComponents<IKManager>()[0].enabled = true;
            xPos = baseOfNeck.transform.position.x - 8;
            yPos = baseOfNeck.transform.position.y;
            distanceThreshold = 0.25f;
            if (isCloseEnough(xPos, yPos, distanceThreshold))
            {
                lungeState = LungeState.Anticipation;
                setState(DragonState.IDLING);
            }
        }
        //After anticipating the attack, set the dragon to lunge towards the player
        move.x = Mathf.Lerp(move.x, xPos, rate * Time.deltaTime);
        move.y = Mathf.Lerp(move.y, yPos, rate * Time.deltaTime);
        dragonTarget.transform.position = move;
    }

    bool isCloseEnough(float xPos, float yPos)
    {
        if (xPos - dragonTarget.transform.position.x < distanceThreshold && yPos - dragonTarget.transform.position.y < distanceThreshold)
        {
            if (dragonState == DragonState.LUNGE)
            {
                if (lungeState == LungeState.Anticipation)
                {
                    lungeState = LungeState.Lunge;
                }
                if (lungeState == LungeState.Lunge)
                {
                    lungeState = LungeState.Neutral;
                }
            }
            else if (dragonState == DragonState.FIRE_BREATH)
            {
                if (fireState == FireState.Fire)
                {
                    fireState = FireState.Return;
                }
                else if (fireState == FireState.Return)
                {
                    fireState = FireState.Neutral;
                }
            }
            return true;
        }
        return false;
    }

    bool isCloseEnough(float xPos, float yPos, float distanceThreshold)
    {
        if (xPos - dragonTarget.transform.position.x < distanceThreshold && yPos - dragonTarget.transform.position.y < distanceThreshold)
        {
            if(dragonState == DragonState.LUNGE)
            {
                if (lungeState == LungeState.Anticipation)
                {
                    lungeState = LungeState.Lunge;
                }
                if (lungeState == LungeState.Lunge)
                {
                    lungeState = LungeState.Neutral;
                }
            }

            if (dragonState == DragonState.FIRE_BREATH)
            {
                if (fireState == FireState.Fire)
                {
                    fireState = FireState.Return;
                }
                else if (fireState == FireState.Return)
                {
                    fireState = FireState.Neutral;
                }
            }
            return true;
        }
        return false;
    }
}

enum DragonState
{
    IDLING,
    FIRE_BREATH,
    LUNGE
}

enum LungeState
{
    Anticipation,
    Lunge,
    Neutral
}

enum FireState
{
    Fire,
    Return,
    Neutral
}