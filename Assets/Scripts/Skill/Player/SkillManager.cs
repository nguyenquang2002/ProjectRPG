using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    
    public PlayerCloneSkill cloneSkill { get; private set; }
    public PlayerSwordSkill swordSkill { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
            instance = this;
        
    }
    private void Start()
    {
        cloneSkill = GetComponent<PlayerCloneSkill>();
        swordSkill = GetComponent<PlayerSwordSkill>();
    }
}
