using UnityEngine;

public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}

public class PlayerSwordSkill : Skill
{
    public SwordType swordType = SwordType.Regular;

    [Header("Bounce info")]
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceGravity;

    [Header("Pierce info")]
    [SerializeField] private int pierceAmount;
    [SerializeField] private float pierceGravity;

    [Header("Spin info")]
    [SerializeField] private float maxTravelDistance = 6f;
    [SerializeField] private float spinDuration = 2f;
    [SerializeField] private float spinGravity = 2.5f;
    [SerializeField] private float hitCooldown = 0.35f;

    [Header("Skill info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;

    private Vector2 finalDir;

    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotParent;

    private GameObject[] dots;

    protected override void Start()
    {
        base.Start();
        GenerateDots();

        SetUpGravity();
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetButtonUp("ThrowSword"))
        {
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);
        }

        if (Input.GetButton("ThrowSword"))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
            }
        }
    }
    public override void UseSkill()
    {
        base.UseSkill();
    }
    private void SetUpGravity()
    {
        if (swordType == SwordType.Bounce)
            swordGravity = bounceGravity;
        else if (swordType == SwordType.Pierce)
            swordGravity = pierceGravity;
        else if (swordType == SwordType.Spin)
            swordGravity = spinGravity;
    }
    public void CreateSword()
    {
        GameObject newSword = Instantiate(swordPrefab);
        newSword.transform.position = new Vector3(player.transform.position.x + 0.5f * player.facingDir, player.transform.position.y + 0.2f, player.transform.position.z);
        SwordController swordController = newSword.GetComponent<SwordController>();
        if(swordType == SwordType.Bounce)
        {
            swordController.SetupBounce(true, bounceAmount);
        }
        else if(swordType == SwordType.Pierce)
        {
            swordController.SetupPierce(pierceAmount);
        }
        else if(swordType == SwordType.Spin)
        {
            swordController.SetupSpin(true, maxTravelDistance, spinDuration, hitCooldown);
        }
        SetUpGravity();
        swordController.SetupSword(finalDir, swordGravity, player);
        player.AssignNewSword(newSword);
        DotsActive(false);
    }
    #region Aim region
    public Vector2 AimDirection()
    {
        Vector2 playerPos = player.transform.position;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - playerPos;

        return direction;
    }

    public void DotsActive(bool _isActive)
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(_isActive);
        }
    }

    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        Vector2 position = new Vector2(player.transform.position.x + 0.5f * player.facingDir, player.transform.position.y + 0.2f) + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t + .5f * (Physics2D.gravity * swordGravity) * (t * t);
        return position;
    }
    #endregion
}
