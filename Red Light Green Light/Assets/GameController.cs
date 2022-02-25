using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    // The list containing the sequence
    public List<string> Sequence = new List<string>();
    // The CurrentIndex within Sequence that is being played
    public int CurrentIndex = 0;
    // The text box on the screen
    public Text OutputText;
    // If true, the sequence is playing
    public bool IsPlayingSequence;
    // The speed in seconds that each element in the sequence is displayed
    public float Speed = 0.5F; 
    // Specifies when the next element in the sequence should be played
    public float NextDisplayAt = -1;

    // Start is a "magic" Unity method that is called when the scene loads
    void Start()
    {
        ResetSequence();
    }

    // This method is called when the red, yellow, or green button is clicked
    public void AddToSequence(string toAdd) {
        Sequence.Add(toAdd); // Add the element to the sequence
        OutputText.text = toAdd; // Display the sequence on the screen
    }

    // This method is called when the Clear Sequence button is clicked
    public void ResetSequence() {
        Sequence = new List<string>();
        IsPlayingSequence = false;
        CurrentIndex = 0;
        OutputText.text = "Cleared.";
    }

    // This method is called when the Play Sequence button is clicked
    public void StartPlayingSequence() {
        if (Sequence.Count == 0) {
            OutputText.text = "Empty Sequence"; // Don't allow playing empty sequences
        } else {
            OutputText.text = ""; // Clear the output text
            CurrentIndex = 0; // Reset the current index so it is playing from the beginning
            IsPlayingSequence = true; // Set IsPlaying Sequence to true so that it is used in the Update method
            NextDisplayAt = Time.time + Speed; // Set the next display to be the current time plus the speed (essentially 0.5 seconds in the future)
        }
    }

    // This method is called by the Update method when IsPlayingSequence is true
    public bool PlayNextElement() {
        OutputText.text = Sequence[CurrentIndex]; // Display the value at the current index
        NextDisplayAt = Time.time + Speed; // Set the next display at to be the current time + the speed
        CurrentIndex = CurrentIndex + 1; // Increase the index by 1 so we move to the next element in the sequence
        return CurrentIndex < Sequence.Count; // Returns true if the sequence is not finished
    }

    // Update is a "magic" Unity method that is called repeatedly (once per frame)
    void Update()
    {
        // If the sequence is playing AND the current time is greater than when we should display the next element
        if (IsPlayingSequence && Time.time >= NextDisplayAt) {
            bool continuePlayingSequence = PlayNextElement();
            if (continuePlayingSequence == false) {
                IsPlayingSequence = false; // Stop playing the sequence
            }
        }
    }
}
