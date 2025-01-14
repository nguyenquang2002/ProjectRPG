using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;
    [SerializeField] public bool isUnlock = false;

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(cooldownTimer < 0)
        {
            
            return true;
        }
        return false;
    }

    public virtual void UseSkill()
    {
        cooldownTimer = cooldown;
    }
}
