using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	public Animator startButton;
	public Animator settingsButton;

	public void OpenVRSettings() 
	{
    	startButton.SetBool("isHidden", true);
    	settingsButton.SetBool("isHidden", true);
	}
}
