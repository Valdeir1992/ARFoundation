using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PuzzleC : MonoBehaviour
{
    private GameObject _trackedObject;
    [SerializeField] private GameObject _prefabObject;
    private void OnEnable()
    {
        try
        { 
            ARTrackedImageManager manager = FindObjectOfType<ARTrackedImageManager>();
            if (manager != null)
            {
                manager.trackedImagesChanged += OnChangeMission;
                Debug.LogError("Success find");
            }
            else
            {
                Debug.LogError("Erro manager");
            }
        }
        catch
        {
            Debug.LogError("Erro Find");
        }

        
    }

    private void OnDisable()
    {
        ARTrackedImageManager manager = FindObjectOfType<ARTrackedImageManager>();

        if (manager != null)
        {
            manager.trackedImagesChanged -= OnChangeMission;
        }
    }
    private void OnChangeMission(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var AddedImage in eventArgs.added)
        {
            try
            {
                _trackedObject = Instantiate(_prefabObject, Vector3.zero, Quaternion.identity, AddedImage.transform);
                Debug.LogError("success instantiate");
            }
            catch
            {
                Debug.LogError("Fail instantiate");
            }
         }

        foreach (var updatedImage in eventArgs.updated)
        {
            try
            {

                if (updatedImage.trackingState != UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
                {
                    _trackedObject.SetActive(false);
                    Debug.LogError("Success Desactive"); 
                }
                else
                {
                    _trackedObject.SetActive(true);
                }
                Debug.LogError("Success Update");
            }
            catch
            {
                Debug.LogError("Fail Update");
            } 
        } 
    }
}
