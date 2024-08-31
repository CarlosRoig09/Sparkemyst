using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{

    // AIM MODES
    /// 0 = Line        A line from the player character towards mouse position
    /// 1 = Box         A box from the player character towards mouse position
    /// 2 = Arc         An arc/cone/radius from the player character towards mouse position

    public enum AimModes
    {
        Line,
        Box,
        Arc,
    }


    // AIMS STATES
    /// 0 = Enabled     LMB cooldown is ready
    /// 1 = Charging    LMB is on cooldown, the indicator will display semi-transparent and a cooldown indicator will be shown.
    /// 2 = Disabled    LMB is disabled and can't be used
    /// 3 = Hidden      Completely hides the indicator

    public enum AimStates
    {
        Enabled,
        Charging,
        Disabled,
        Hidden,
    }
    

    [Header("Preferences")]
    public bool ShowLine;               // Show/Hide line indicator
    public bool ShowTip;                // Show/Hide line tip indicator

    [Header("Settings")]
    public float MaxAimRange;           // Max range for the aim indicator
    public float MinAimRange;           // Min range for the aim indicator
    public AimModes AimMode;            // Aim indicator shape
    public AimStates AimState;          // Aim indicator state

    [Header("Config")]
    public float LMBCooldown;           // LMB Cooldown converter to 0~1 value
    public float ClampedDotDistance;    // Dot distance if target is below MaxAimRange
    public float BoxWidth;              // Box width for Box AimMode 
    public float ConeAngle;             // Cone angle for Cone AimMode

    public Transform AimHandler;        // Empty gameobject for pivoting aiming graphics
    public Transform AimTip;            // Aiming tip displayed at the end
    public Transform AimAuxTip;         // Auxiliar aiming tip displayed on contact point when aiming over something in range

    // Start is called before the first frame update
    void Start()
    {
        // Check if AimHandler null
        if (AimHandler == null)
        {
            Debug.LogWarning("AimHandler is missing, please add it to the PlayerAim script on the Player");
        }

        // Check if AimTip null
        if (AimTip == null)
        {
            Debug.LogWarning("AimTip is missing, please add it to the PlayerAim script on the Player");
        }

        // Check if AimAuxTip null
        if (AimAuxTip == null)
        {
            Debug.LogWarning("AimAuxTip is missing, please add it to the PlayerAim script on the Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        FaceMousePosition();
        UpdateAuxTip();
    }

    // Instantly rotates AimHandler towards mouse position
    void FaceMousePosition()
    {
        // Obtain mouse world position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Restrict position to 2D coords
        mousePosition.z = 0;

        // Set mouse direction
        Vector3 direction = (mousePosition - transform.position).normalized;

        // Set rotation angle
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation
        AimHandler.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // Displays
    void UpdateAuxTip()
    {
        // Raycast
        RaycastHit2D hit = Physics2D.Raycast(AimHandler.position, AimTip.position);

        // Check collision and:
        if (hit.distance < MaxAimRange && hit.distance > 0)
        {
            // If collided before max range enable AimClampedTip graphics...
            AimAuxTip.gameObject.SetActive(true);
            AimTip.gameObject.SetActive(false);

            // ... and move it towards hit point
            AimAuxTip.position = hit.point;
        }
        else
        {
            // If doesn't collide or out of range, just hide AimClampedTip graphics
            AimAuxTip.gameObject.SetActive(false);
            AimTip.gameObject.SetActive(true);
        }
    }

    // Dynamic range setup
    void UpdateAimRange(float newMaxRange)
    {
        // NOT IMPLEMENTED
    }

    // Dynamic Aim state
    public void SetAimState(AimStates _state)
    {
        AimState = _state;
    }
}