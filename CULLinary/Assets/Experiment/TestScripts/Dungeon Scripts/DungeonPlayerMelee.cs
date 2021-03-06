using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DungeonPlayerMelee : DungeonPlayerAction
{
    public delegate void PlayerMeleeDelegate();
    public delegate void PlayerStopDelegate();
    public event PlayerMeleeDelegate OnPlayerMelee;

    //Actions prevented
    private DungeonPlayerRange dungeonPlayerRange;
    private void Start()
    {
        dungeonPlayerRange = GetComponent<DungeonPlayerRange>();
    }

    private void Update()
    {
        bool isRangeInvoked = dungeonPlayerRange ? dungeonPlayerRange.GetIsInvoking() : false;
        if (Input.GetMouseButton(0) && !isRangeInvoked && !(EventSystem.current.IsPointerOverGameObject()))
        {
            this.SetIsInvoking(true);
            OnPlayerMelee?.Invoke();
        }
    }

    public void StopInvoking()
    {
        this.SetIsInvoking(false);
    }
}
