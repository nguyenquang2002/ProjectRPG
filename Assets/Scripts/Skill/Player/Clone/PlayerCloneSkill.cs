using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloneSkill : Skill
{
    [Header("Clone info")]
    [SerializeField] private GameObject clone;
    [SerializeField] private float cloneDuration = 1f;
    [SerializeField] private bool canAttack;

    public override void UseSkill()
    {
        base.UseSkill();
        
    }

    public void CreateClone(Transform _clonePosition, float _facingDir)
    {
        GameObject newClone = Instantiate(clone);
        newClone.GetComponent<PlayerCloneSetup>().SetupClone(_clonePosition, cloneDuration, canAttack, _facingDir);
        UseSkill();
    }
}
