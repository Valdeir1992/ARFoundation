using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PuzzleC : MonoBehaviour
{

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
            Instantiate(_prefabObject,Vector3.zero,Quaternion.identity, AddedImage.transform);
            Debug.Log(AddedImage.gameObject.transform.GetChild(0).gameObject.transform.position);
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            if (updatedImage.trackingState != UnityEngine.XR.ARSubsystems.TrackingState.Tracking)
            {
                updatedImage.gameObject.transform.GetChild(0).gameObject.SetActive(false); 
            }
            else
            { 
                updatedImage.gameObject.transform.GetChild(0).gameObject.SetActive(true); 
            }
        }
        foreach (var updatedImage in eventArgs.removed)
        {
            updatedImage.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }

    }
}
