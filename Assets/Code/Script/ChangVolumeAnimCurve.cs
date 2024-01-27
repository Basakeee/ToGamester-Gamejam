using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChangVolumeAnimCurve : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] private AnimationCurve hueShiftAnimationCurve;

    private float hueShiftLasttime;
    private ColorAdjustments colorAdjustments;
    // Start is called before the first frame update
    private void Awake()
    {
        volume.profile.TryGet(out colorAdjustments);
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            hueShiftLasttime = Time.realtimeSinceStartup;
        }

        float hueShift = hueShiftAnimationCurve.Evaluate(Time.smoothDeltaTime - hueShiftLasttime);
        colorAdjustments.hueShift.value = hueShift;

    }
}
