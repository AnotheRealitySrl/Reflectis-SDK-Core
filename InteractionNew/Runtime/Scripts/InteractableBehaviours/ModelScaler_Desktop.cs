using Reflectis.SDK.InteractionNew;
using Reflectis.SDK.Utilities.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ModelScaler_Desktop : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float scaleMultiplier;
    [SerializeField, Tooltip("Lowest drag movement value that can be reached while scaling downward")]
    private float lowestDragPointDistance;
    [SerializeField, Tooltip("Scaling should be negated if it would get any of the three axes lower " +
        "than these values")]
    private Vector3 lowestScaleAxisValues;
    [SerializeField] private float scalingCornerfixedSize;

    //[Header("References")]


    public enum ScaleType
    {
        X_ONLY,
        Y_ONLY,
        Z_ONLY,
    }

    private GameObject boundingBox;
    [SerializeField] private List<GameObject> scalingCorners;
    [SerializeField] private List<GameObject> scalingFaces;
    //private Downloaded3DModel thisModelController;
    //private ScalerRaycaster_Desktop thisRaycaster;
    // Has the user pressed left mouse button while over a scaling corner? 
    public bool isScalingCornerSelected = false;
    // Has the user pressed left mouse button while over a scaling face?
    public bool isScalingFaceSelected = false;
    public ScaleType scaleType;
    // This will store the scale value before each drag movement;
    private Vector3 scaleStartValue;
    // This will store the position of the bounding box center before each drag movement;
    private Vector2 scaleStartModelCenterOnScreen;
    // The general direction between the model's center and the selected scaling point.
    // This is needed to prevent the scaling from happening when the scaling movement 
    // goes in the opposite direction than intended.
    private Vector2 scaleDirection;

    public bool isNPScaleActivated = false;

    //private GameObject BoundingBox => thisModelController.BoundingBox;

    public GameObject BoundingBox { 
        get {
            if (boundingBox == null)
            {
                boundingBox = GetComponent<BaseInteractable>().InteractionColliders[0].gameObject;
            }
            return boundingBox;
        } 
    } 
    //private List<GameObject> ScalingCorners => thisModelController.ScalingCorners;
    public List<GameObject> ScalingCorners => scalingCorners;
    //private List<GameObject> ScalingFaces => thisModelController.ScalingFaces;
    public List<GameObject> ScalingFaces => scalingFaces;


    public void OnHoverEnter(GameObject hit)
    {
        //Debug.Log($"<color=yellow>OnHoverEnter</color> | OnHoverEnter launched");
        if (isNPScaleActivated)
        {

        }
        else
        {

        }
    }

    public void OnHoverStay(GameObject hit)
    {
        //Debug.Log($"<color=yellow>OnHoverStay</color> | OnHoverStay launched");
        if (isNPScaleActivated)
        {

        }
        else
        {

        }
    }

    public void OnHoverExit(GameObject hit)
    {
        //Debug.Log($"<color=yellow>OnHoverExit</color> | OnHoverExit launched");
        if (isNPScaleActivated)
        {

        }
        else
        {

        }
    }

    public void OnLeftMouseDown(GameObject hit)
    {
        //Debug.Log($"<color=yellow>OnLeftMouseDown</color> | OnLeftMouseDown launched");
        //if (isNPScaleActivated)
        //{
            if (ScalingFaces.IndexOf(hit) != -1)
            {
                isScalingFaceSelected = true;
                SaveScaleStartValues(hit);

                // Detects the intended scale axis
                Vector3 scaleDirection = (hit.transform.position -
                    BoundingBox.transform.position).normalized;
                Vector3 boxRight = BoundingBox.transform.right;
                Vector3 boxUp = BoundingBox.transform.up;
                Vector3 boxForward = BoundingBox.transform.forward;
                //Debug.Log($"<color=red>NPScaling start</color>  scale direction: {scaleDirection}, " +
                //    $"box right: {boxRight}, box up: {boxUp}, box forward {boxForward}");
                if (scaleDirection == boxRight || scaleDirection == -boxRight)
                {
                    scaleType = ScaleType.X_ONLY;
                }
                else if (scaleDirection == boxUp || scaleDirection == -boxUp)
                {
                    scaleType = ScaleType.Y_ONLY;
                }
                else if (scaleDirection == boxForward || scaleDirection == -boxForward)
                {
                    scaleType = ScaleType.Z_ONLY;
                }
            }
        //}
        //else  // Proportional scaling mode
        //{
            if (ScalingCorners.IndexOf(hit) != -1)
            {
                isScalingCornerSelected = true;
                SaveScaleStartValues(hit);
            }
        //}
    }

    public void OnClick(GameObject hit)
    {
        //Debug.Log($"<color=yellow>OnHoverEnter</color> | OnClick launched");
    }

    public void OnDrag(GameObject hit, Vector2 dragStartPoint, Vector2 currentDragPoint)
    {
        //Debug.Log($"<color=yellow>OnDrag</color> | OnDrag launched, hitObject: {hit.name}, " +
        //    $"dragStartPoint: {dragStartPoint}, currentDragPoint: {currentDragPoint}");
        float currentDragPointDistance, dragStartPointDistance, scaleDelta;
        bool isDragPointInRightDirection;

        //if (isNPScaleActivated)
        //{
            if (ScalingFaces.Contains(hit))
            {
                currentDragPointDistance = (currentDragPoint - scaleStartModelCenterOnScreen).magnitude;
                dragStartPointDistance = (dragStartPoint - scaleStartModelCenterOnScreen).magnitude;
                scaleDelta = currentDragPointDistance - dragStartPointDistance;

                // Is the current drag point in front of the scale direction?
                isDragPointInRightDirection = Vector2.Angle(scaleDirection,
                    currentDragPoint - scaleStartModelCenterOnScreen) < 90;

                if (currentDragPointDistance > lowestDragPointDistance &&
                    isDragPointInRightDirection)
                {
                    Vector3 newScale = scaleStartValue;
                    switch (scaleType)
                    {
                        case ScaleType.X_ONLY:
                            newScale += new Vector3(newScale.x, 0, 0) * scaleDelta * scaleMultiplier;
                            break;
                        case ScaleType.Y_ONLY:
                            newScale += new Vector3(0, newScale.y, 0) * scaleDelta * scaleMultiplier;
                            break;
                        case ScaleType.Z_ONLY:
                            newScale += new Vector3(0, 0, newScale.z) * scaleDelta * scaleMultiplier;
                            break;
                    }


                    // The new scale value is applied, but only if none of the scale
                    // axes will get below the lowest allowed values
                    if (newScale.x > lowestScaleAxisValues.x &&
                        newScale.y > lowestScaleAxisValues.y &&
                        newScale.z > lowestScaleAxisValues.z)
                    {
                        transform.localScale = newScale;
                    }

                    //thisModelController.UpdateScalingCornersSize();
                    //thisModelController.UpdateScalingFacesSize();
                }
            }
        //}
        //else  // Proportional scaling mode
        //{
            if (ScalingCorners.Contains(hit))
            {
                currentDragPointDistance = (currentDragPoint - scaleStartModelCenterOnScreen).magnitude;
                dragStartPointDistance = (dragStartPoint - scaleStartModelCenterOnScreen).magnitude;
                scaleDelta = currentDragPointDistance - dragStartPointDistance;

                // Is the current drag point in front of the scale direction?
                isDragPointInRightDirection = Vector2.Angle(scaleDirection,
                    currentDragPoint - scaleStartModelCenterOnScreen) < 90;

                if (currentDragPointDistance > lowestDragPointDistance &&
                    isDragPointInRightDirection)
                {
                    Vector3 newScale = scaleStartValue + (scaleStartValue * scaleDelta * scaleMultiplier);
                    // The new scale value is applied, but only if none of the scale
                    // axes will get below the lowest allowed values
                    if (newScale.x > lowestScaleAxisValues.x &&
                        newScale.y > lowestScaleAxisValues.y &&
                        newScale.z > lowestScaleAxisValues.z)
                    {
                        transform.localScale = newScale;
                    }



                //Debug.Log($"<color=yellow>OnHoverEnter</color> | OnDrag launched, scale delta: {scaleDelta}, " +
                //    $"hit name: {hit.name}, " +
                //    $"dragStartPoint: {dragStartPoint}, " +
                //    $"currentDragPoint: {currentDragPoint}");


                // Changes the scaling corners' size to keep a specific world scale
                }
            //}
            } 
    }

    public void OnDragEnd()
    {
        //Debug.Log($"<color=yellow>OnDragEnd</color> | OnDragEnd launched");
        if (isNPScaleActivated)
        {
            if (isScalingFaceSelected)
            {
                isScalingFaceSelected = false;
            }
        }
        else  // Proportional scaling mode
        {
            if (isScalingCornerSelected)
            {
                isScalingCornerSelected = false;
            }
        }
    }

    public void ToggleProportionalScaling(bool enable)
    { 
        foreach (GameObject scalingCorner in ScalingCorners)
        {
            scalingCorner.SetActive(enable);
        }
    }

    public void ToggleNonProportionalScaling(bool enable)
    {
        foreach (GameObject scalingFace in ScalingFaces)
        {
            scalingFace.SetActive(enable);
        }
    } 

    private void SaveScaleStartValues(GameObject selectedScalerPoint)
    {
        scaleStartModelCenterOnScreen = Camera.main.WorldToScreenPoint(BoundingBox.transform.position);
        scaleStartValue = transform.localScale;
        Vector2 scalerPointScreenPosition = Camera.main.WorldToScreenPoint(selectedScalerPoint.transform.position);
        scaleDirection = scalerPointScreenPosition - scaleStartModelCenterOnScreen;
    }
}
