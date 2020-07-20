using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Transform lever1;
    public Transform lever2;
    public Transform lever3;
    public Transform lever4;
    public Transform dropButton;
    public Transform releaseButton;

    public Canvas onOffButtonCanvas;
    public Canvas lever1Canvas;
    public Canvas lever2Canvas;
    public Canvas lever3Canvas;
    public Canvas lever4Canvas;
    public Canvas endPracticeSectioncanvas;


    public void PracticeMode()
    {
        MyRoutine(); // Reload Scene to make sure the orientation of the crane is correct


        ChangeLayersRecursively(lever1, "Practice Mode"); // lever1 is not interactive
        ChangeLayersRecursively(lever2, "Practice Mode"); 
        ChangeLayersRecursively(lever3, "Practice Mode"); 
        ChangeLayersRecursively(lever4, "Practice Mode"); 
        ChangeLayersRecursively(dropButton, "Practice Mode"); 
        ChangeLayersRecursively(releaseButton, "Practice Mode"); 

        // Teleport user into the cab
        GameController.Instance.GotoCab();

        // UI to tell user to click on/off button
        onOffButtonCanvas.gameObject.SetActive(true);        
    }

    public void MoveToFirstLever()
    {
        if (GameController.Instance.engineRunning) // Once the On/Off button is pressed
        {
            ChangeLayersRecursively(lever1, "Grab Ignore Ray"); // first lever active
            // TODO UI instruction for lever 1 appears
            onOffButtonCanvas.gameObject.SetActive(false);
            lever1Canvas.gameObject.SetActive(true);
        }
    }

    public void MoveToSecondLever()
    {
        if ((FirstLeverController.Instance.leverRotation.x <= 10f && FirstLeverController.Instance.leverRotation.x >= 0f) || (FirstLeverController.Instance.leverRotation.x >= 350f && FirstLeverController.Instance.leverRotation.x <= 360f))
        {
            ChangeLayersRecursively(lever1, "Practice Mode");
            ChangeLayersRecursively(lever2, "Grab Ignore Ray"); // lever2 is active
            ChangeLayersRecursively(lever3, "Practice Mode");
            ChangeLayersRecursively(lever4, "Practice Mode");
            ChangeLayersRecursively(dropButton, "Practice Mode");
            ChangeLayersRecursively(releaseButton, "Practice Mode");
            // TODO turn off UI for lever1 and turn on for UI lever2
            lever1Canvas.gameObject.SetActive(false);
            lever2Canvas.gameObject.SetActive(true);
        }
            
    }

    public void MoveToThirdLever()
    {
        if ((SecondLeverController.Instance.leverRotation.x <= 10f && SecondLeverController.Instance.leverRotation.x >= 0f) || (SecondLeverController.Instance.leverRotation.x >= 350f && SecondLeverController.Instance.leverRotation.x <= 360f))
        {
            ChangeLayersRecursively(lever1, "Practice Mode");
            ChangeLayersRecursively(lever2, "Practice Mode");
            ChangeLayersRecursively(lever3, "Grab Ignore Ray"); // lever3 is active
            ChangeLayersRecursively(lever4, "Practice Mode");
            ChangeLayersRecursively(dropButton, "Practice Mode");
            ChangeLayersRecursively(releaseButton, "Practice Mode");
            // TODO turn off UI for lever2 and turn on for UI lever3
            lever2Canvas.gameObject.SetActive(false);
            lever3Canvas.gameObject.SetActive(true);
        }
            
    }

    public void MoveToFourthLever()
    {
        if ((ThirdLeverController.Instance.leverRotation.x <= 10f && ThirdLeverController.Instance.leverRotation.x >= 0f) || (ThirdLeverController.Instance.leverRotation.x >= 350f && ThirdLeverController.Instance.leverRotation.x <= 360f))
        {
            ChangeLayersRecursively(lever1, "Practice Mode");
            ChangeLayersRecursively(lever2, "Practice Mode");
            ChangeLayersRecursively(lever3, "Practice Mode");
            ChangeLayersRecursively(lever4, "Grab Ignore Ray"); // lever4 is active
            ChangeLayersRecursively(dropButton, "Practice Mode");
            ChangeLayersRecursively(releaseButton, "Practice Mode");
            // TODO turn off UI for lever3 and turn on for UI lever4
            lever3Canvas.gameObject.SetActive(false);
            lever4Canvas.gameObject.SetActive(true);
            // Set hidden cargo active for picking up 
        }


    }

    //public void MoveToButton()
    //{
    //    ChangeLayersRecursively(lever1, "Practice Mode");
    //    ChangeLayersRecursively(lever2, "Practice Mode");
    //    ChangeLayersRecursively(lever3, "Practice Mode");
    //    ChangeLayersRecursively(lever4, "Practice Mode"); 
    //    ChangeLayersRecursively(dropButton, "Grab Ignore Ray"); // 2 buttons are active
    //    ChangeLayersRecursively(releaseButton, "Grab Ignore Ray");
    //    // TODO turn off UI for lever4 and turn on for UI buttons
    //    lever4Canvas.gameObject.SetActive(false);
    //    dropButtonCanvas.gameObject.SetActive(true);
    //}

    public void EndPracticeSection()
    {
        if ((FourthLeverController.Instance.leverRotation.x <= 10f && FourthLeverController.Instance.leverRotation.x >= 0f) || (FourthLeverController.Instance.leverRotation.x >= 350f && FourthLeverController.Instance.leverRotation.x <= 360f))
        {
            ChangeLayersRecursively(lever1, "Practice Mode");
            ChangeLayersRecursively(lever2, "Practice Mode");
            ChangeLayersRecursively(lever3, "Practice Mode");
            ChangeLayersRecursively(lever4, "Practice Mode");
            ChangeLayersRecursively(dropButton, "Practice Mode");
            ChangeLayersRecursively(releaseButton, "Practice Mode");

            lever4Canvas.gameObject.SetActive(false);
            endPracticeSectioncanvas.gameObject.SetActive(true);
        }
            
    }

    public IEnumerator MyRoutine()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return new WaitForSeconds(3f);
    }

    public void TrainingMode()
    {
        MyRoutine();

        ChangeLayersRecursively(lever1, "Grab Ignore Ray");
        ChangeLayersRecursively(lever2, "Grab Ignore Ray");
        ChangeLayersRecursively(lever3, "Grab Ignore Ray");
        ChangeLayersRecursively(lever4, "Grab Ignore Ray");
        ChangeLayersRecursively(dropButton, "Grab Ignore Ray");
        ChangeLayersRecursively(releaseButton, "Grab Ignore Ray");
        
        GameController.Instance.GotoCab();
        //Debug.Log("Running");
    }

    public void ChangeLayersRecursively(Transform trans, string name)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in trans)
        {
            child.gameObject.layer = LayerMask.NameToLayer(name);
            ChangeLayersRecursively(child, name);
        }
    }
}
