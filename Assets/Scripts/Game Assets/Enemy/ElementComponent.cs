using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Enum;
using Unity.VisualScripting;
using UnityEngine;

public class ElementComponent : MonoBehaviour
{
    private EnemyBase owner;
    [SerializeField] private List<AttachedElement> elementList;

    private const float ELEMENT_DECREASE_CONSTANT = 1.0f;
    private float electroChargedCD = 0.5f;
    private float electroChargePara = 2.0f;
    private float elementReactionCD = 0.2f;

    private float electroChargedCounter = 0.0f;
    private float elementReactionCounter = 0.0f;

    public EnemyBase Owner
    {
        get { return owner; }
        set { owner = value; }
    }

// Start is called before the first frame update
    void Start()
    {
        owner = GetComponent<EnemyBase>();
        
        //Initialize element list
        elementList = new List<AttachedElement>();
        elementList.Add(new AttachedElement(ElementEnum.Fire));
        elementList.Add(new AttachedElement(ElementEnum.Water));
        elementList.Add(new AttachedElement(ElementEnum.Ice));
        elementList.Add(new AttachedElement(ElementEnum.Thunder));
        elementList.Add(new AttachedElement(ElementEnum.Frozen));
    }

    // Update is called once per frame
    void Update()
    {
        foreach (AttachedElement element in elementList)
        {
            if(element.amount<=0.0f)
                continue;

            element.amount -= Time.deltaTime * element.decreaseSpeed;
            if (element.element == ElementEnum.Frozen)
            {
                //Decrease speed of frozen element increases by time
                element.decreaseSpeed += 0.1f * Time.deltaTime;
            }
            
            if (element.amount <= 0.0f)
            {
                ClearElement(element.element);
            }
        }
        
        if (elementReactionCounter > 0.0f)
        {
            elementReactionCounter -= Time.deltaTime;
        }

        if (electroChargedCounter > 0.0f)
        {
            electroChargedCounter -= Time.deltaTime;
        }

        if (elementList[(int)ElementEnum.Water - 1].amount > 0 &&
            elementList[(int)ElementEnum.Thunder - 1].amount > 0 && electroChargedCounter <= 0)
        {
            //Electro Charged
            owner.ElectroCharged((int)(elementList[(int)ElementEnum.Thunder - 1].power * electroChargePara));
            electroChargedCounter = electroChargedCD;
        }

        
    }

    public void ElementAttack(ElementEnum element, float amount, int power, int damage)
    {
        if (elementReactionCounter > 0.0f)
        {
            Debug.Log("Element reaction cool down");
            return;
        }
            
        
        bool addedElement = false;

        if (elementList[(int)element - 1].amount != 0.0f)
        {
            //Supply element
            elementList[(int)element - 1].power = power;
            if (amount > elementList[(int)element - 1].amount)
            {
                elementList[(int)element - 1].amount = amount;
            }
        }
        else
        {
            if (CauseReaction(element))
            {
                AttachedElement tempElement = new AttachedElement(element, power, amount);
                
                //Element reaction
                switch (element)
                {
                    case ElementEnum.Fire:
                    {
                        if (elementList[(int)ElementEnum.Water - 1].amount > 0.0f)
                        {
                            Vaporize(elementList[(int)ElementEnum.Water - 1],tempElement,damage);
                        }
                        else if (elementList[(int)ElementEnum.Ice - 1].amount > 0.0f)
                        {
                            Melt(elementList[(int)ElementEnum.Ice - 1],tempElement,damage);
                        }
                        else if (elementList[(int)ElementEnum.Thunder - 1].amount > 0.0f)
                        {
                            Overload(elementList[(int)ElementEnum.Thunder - 1], tempElement, damage);
                        }

                        break;
                    }
                    case ElementEnum.Water:
                    {
                        if (elementList[(int)ElementEnum.Fire - 1].amount > 0)
                        {
                            Vaporize(elementList[(int)ElementEnum.Fire-1],tempElement,damage);
                        }
                        else if (elementList[(int)ElementEnum.Ice - 1].amount > 0)
                        {
                            Freeze(elementList[(int)ElementEnum.Ice - 1], tempElement);
                            owner.TakeDamage(damage);
                        }
                        else
                        {
                            owner.TakeDamage(damage);
                        }

                        break;
                    }
                    case ElementEnum.Ice:
                    {
                        if (elementList[(int)ElementEnum.Fire - 1].amount > 0)
                        {
                            Melt(elementList[(int)ElementEnum.Fire-1],tempElement,damage);
                        }
                        else if (elementList[(int)ElementEnum.Water - 1].amount > 0)
                        {
                            Freeze(elementList[(int)ElementEnum.Water - 1], tempElement);
                            owner.TakeDamage(damage);
                        }
                        else
                        {
                            owner.TakeDamage(damage);
                        }

                        break;
                    }
                    case ElementEnum.Thunder:
                    {
                        if (elementList[(int)ElementEnum.Fire - 1].amount > 0)
                        {
                            Overload(elementList[(int)ElementEnum.Fire-1],tempElement,damage);
                        }
                        else
                        {
                            owner.TakeDamage(damage);
                        }

                        break;
                    }
                }
            }
            else
            {
                //Attach new element
                float decreaseSpeed = amount / ELEMENT_DECREASE_CONSTANT;
                elementList[(int)element - 1].SetElement(power, amount, decreaseSpeed);
                owner.TakeDamage(damage);
            }
        }

        elementReactionCounter = elementReactionCD;
    }

    private void ClearElement(ElementEnum element)
    {
        elementList[(int)element - 1].power = 0;
        elementList[(int)element - 1].amount = 0.0f;
        elementList[(int)element - 1].decreaseSpeed = 0.0f;

        if (element == ElementEnum.Frozen)
        {
            //Unfreeze ememy
            owner.Unfreeze();
        }
    }
    

    //蒸发
    private void Vaporize(AttachedElement firstElement, AttachedElement secondElement, int damage)
    {
        CalculateElement(firstElement, secondElement);

        if (firstElement.element == ElementEnum.Fire)
        {
            owner.TakeDamage((int)((float)damage*2.0f));
        }
        else
        {
            owner.TakeDamage((int)((float)damage*1.5f));
        }
    }
    
    //超载
    private void Overload(AttachedElement firstElement, AttachedElement secondElement, int damage)
    {
        CalculateElement(firstElement, secondElement);
        
        //OverLoad
        owner.Overload(damage);
    }
    
    //融化
    private void Melt(AttachedElement firstElement, AttachedElement secondElement, int damage)
    {
        CalculateElement(firstElement, secondElement);

        if (firstElement.element == ElementEnum.Fire)
        {
            owner.TakeDamage((int)((float)damage*1.5f));
        }
        else
        {
            owner.TakeDamage((int)((float)damage*2.0f));
        }
    }

    private void Freeze(AttachedElement firstElement, AttachedElement secondElement)
    {
        float tempAmount = Mathf.Min(firstElement.amount, secondElement.amount);
        elementList[(int)ElementEnum.Frozen - 1].amount = tempAmount * 2.0f;
        elementList[(int)ElementEnum.Frozen - 1].power = 1;
        elementList[(int)ElementEnum.Frozen - 1].decreaseSpeed = 0.4f;
        owner.Freeze();
    }

    private void CalculateElement(AttachedElement firstElement, AttachedElement secondElement)
    {
        Debug.Log(
            $"Before: attached {firstElement.element} {firstElement.amount} {firstElement.power} attaching {secondElement.element} {secondElement.amount} {secondElement.power}");
        if (firstElement.amount * firstElement.power <= secondElement.amount * secondElement.power *
            TypeChart.GetReactionPara(firstElement.element, secondElement.element))
        {
            //Attached element is consumed totally
            ClearElement(firstElement.element);
        }
        else
        {
            firstElement.amount = (firstElement.amount * firstElement.power - secondElement.amount *
                secondElement.power *
                TypeChart.GetReactionPara(firstElement.element, secondElement.element)) / (float)firstElement.power;
        }
        Debug.Log(
            $"After: attached {firstElement.element} {firstElement.amount} {firstElement.power} attaching {secondElement.element} {secondElement.amount} {secondElement.power}");
    }

    private bool CauseReaction(ElementEnum element)
    {
        switch (element)
        {
            case ElementEnum.Fire:
            {
                if (elementList[(int)ElementEnum.Water - 1].amount != 0 ||
                    elementList[(int)ElementEnum.Ice - 1].amount != 0 ||
                    elementList[(int)ElementEnum.Thunder - 1].amount != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                break;
            }
            case ElementEnum.Water:
            {
                if (elementList[(int)ElementEnum.Fire - 1].amount != 0 ||
                    elementList[(int)ElementEnum.Ice - 1].amount != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                break;
            }
            case ElementEnum.Ice:
            {
                if (elementList[(int)ElementEnum.Fire - 1].amount != 0 ||
                    elementList[(int)ElementEnum.Water - 1].amount != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                break;
            }
            case ElementEnum.Thunder:
            {
                if (elementList[(int)ElementEnum.Fire - 1].amount != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                break;
            }
            case ElementEnum.Null:
            case ElementEnum.Frozen:
            {
                return false;
                break;
            }
            default: break;    
        }

        Debug.Log("Casue Reaction Error");
        return false;
    }
    
    public bool HasSameElement(ElementEnum element)
    {
        foreach (AttachedElement existElement in elementList)
        {
            if (existElement.element == element)
            {
                return true;
            }
        }

        return false;
    }
}

[System.Serializable]
public class AttachedElement
{
    public ElementEnum element;
    public int power;
    public float amount;
    public float decreaseSpeed;

    public AttachedElement(ElementEnum element, int power = 0, float amount = 0.0f, float decreaseSpeed = 0.0f)
    {
        this.element = element;
        this.power = power;
        this.amount = amount;
        this.decreaseSpeed = decreaseSpeed;
    }

    public void SetElement(int power = 0, float amount = 0.0f, float decreaseSpeed = 0.0f)
    {
        this.power = power;
        this.amount = amount;
        this.decreaseSpeed = decreaseSpeed;
    }
}

public class TypeChart
{
    static float[][] chart =
    {
         //                         Nul    Fir   Wat   Ice   Thu   
        /*Null*/        new float[] {1f,   1f,   1f,   1f,   1f},
        /*Fire*/        new float[] {1f,   1f,   2f,   0.5f, 1f},
        /*Water*/       new float[] {1f,   0.5f, 1f,   1f,   1f},
        /*Ice*/         new float[] {1f,   2f,   1f,   1f,   1f},
        /*Thunder*/     new float[] {1f,   1f,   1f,   1f,   1f}
        
    };

    public static float GetReactionPara(ElementEnum firstElement, ElementEnum secondElement)
    {
        if (firstElement == ElementEnum.Null || secondElement == ElementEnum.Null)
            return 1f;

        int row = (int)firstElement;
        int col = (int)secondElement;

        Debug.Log($"{firstElement} is hit by {secondElement} {chart[row][col]}");
        return chart[row][col];
    }
}