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
        ARTrackedImageManager manager = FindObjectOfType<ARTrackedImageManager>();

        if(manager != null)
        {
            manager.trackedImagesChanged += OnChangeMission;
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
            _trackedObject = Instantiate(_prefabObject,Vector3.zero,Quaternion.identity,AddedImage.transform); 
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            if (updatedImage.trackingState != UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            { 
                _trackedObject.SetActive(false);
            }
            else
            {
                _trackedObject.SetActive(true); 
            }
        } 
    }
}
