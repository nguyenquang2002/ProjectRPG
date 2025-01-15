using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwordSkill : Skill
{
    [Header("Skill info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchDir;
    [SerializeField] private float swordGravity;

    private Vector2 finalDir;
    protected override void Update()
    {
        base.Update();
        if (Input.GetButtonUp("ThrowSword"))
        {
            finalDir = new Vector2(AimDirection().normalized.x * launchDir.x, AimDirection().normalized.y * launchDir.y);
        }
    }
    public override void UseSkill()
    {
        base.UseSkill();
    }
    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab);
        newSword.transform.position = new Vector3(player.transform.position.x + 1, player.transform.position.y + 1, player.transform.position.z);
        SwordController swordController = newSword.GetComponent<SwordController>();
        swordController.SetupSword(finalDir, swordGravity);
    }
    public Vector2 AimDirection()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - playerPos;

        return direction;
    }
}
