using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : UICanvas
{
    [SerializeField] Text textPoint;
    [SerializeField] Image imageForceBarFill;

    private int currentPoint = 0;

    private void Awake()
    {
        this.RegisterListener(EventID.OnSteppingOnNewPlatform, (param) =>
        {
            currentPoint++;
            UpdateTextPoint(currentPoint);
        });
        this.RegisterListener(EventID.OnChangeJumpForce, (param) =>
        {
            UpdateForceBarFill((float)param);
        });

        UpdateTextPoint(0);
    }

    public override void Open()
    {
        base.Open();
    }

    public override void Close(float delayTime)
    {
        base.Close(delayTime);
    }

    public void UpdateTextPoint(int point)
    {
        textPoint.text = point.ToString();
    }

    public void UpdateForceBarFill(float fillAmount)
    {
        imageForceBarFill.fillAmount = fillAmount;
    }
}
