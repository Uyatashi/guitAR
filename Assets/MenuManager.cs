using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class MenuManager : MonoBehaviour {

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "StartDemoScene")
        {
            VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
            DigitalEyewearARController.Instance.SetEyewearType(DigitalEyewearARController.EyewearType.VideoSeeThrough);
            DigitalEyewearARController.Instance.SetSeeThroughConfiguration(DigitalEyewearARController.SeeThroughConfiguration.Vuforia);
        }
    }

    public void buttonNav(string targetScene)
    {
        SceneManager.LoadSceneAsync(targetScene);
    }
    void OnVuforiaStarted()
    {
        Debug.Log("OnVuforiaStarted() called.");

        if (Device.Instance is EyewearDevice)
        {
            EyewearDevice eyewearDevice = (EyewearDevice)Device.Instance;

            DigitalEyewearARController.Instance.SetEyewearType(DigitalEyewearARController.EyewearType.OpticalSeeThrough);
            DigitalEyewearARController.Instance.SetSeeThroughConfiguration(DigitalEyewearARController.SeeThroughConfiguration.Vuforia);

            // when entering into 3d stereo mode
            if (!eyewearDevice.IsDisplayExtended())
            {
                eyewearDevice.SetDisplayExtended(true);
            }

            // to enable predictive tracking
            if (!eyewearDevice.IsPredictiveTrackingEnabled())
            {
                eyewearDevice.SetPredictiveTracking(true);
            }

            // when exiting 3d stereo mode back to 2d (duplication/mono) mode for menus, etc.
            eyewearDevice.SetDisplayExtended(false);

        }
    }
}


