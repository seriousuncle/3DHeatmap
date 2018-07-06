﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private GameObject visualMappingPanel;
    private VisualMappingUIHandler visualMappingUIHandler;
    private GameObject toolTipPanel;
    private Text toolTipText;

    //The main object for the app
    private HeatVRML heatVRML;
    private DataManager dataMgr;

	// Use this for initialization
	void Start () {
        visualMappingPanel = GameObject.Find("VisualMappingPanel");
        if (visualMappingPanel == null)
            Debug.LogError("visualMappingPanel == null");
        visualMappingUIHandler = visualMappingPanel.GetComponent<VisualMappingUIHandler>();
        if (visualMappingUIHandler == null)
            Debug.LogError("visualMappingUIHandler == null");
        toolTipPanel = GameObject.Find("ToolTipPanel");
        if (toolTipPanel == null)
            Debug.LogError("toolTipPanel == null");
        toolTipText = toolTipPanel.transform.Find("ToolTipText").GetComponent<Text>();
        if( toolTipText == null)
            Debug.LogError("toolTipText == null");
        heatVRML = GameObject.Find("Prefab objectify").GetComponent<HeatVRML>();
        if (heatVRML == null)
            Debug.LogError("heatVRML == null");
        dataMgr = GameObject.Find("DataManager").GetComponent<DataManager>();
        if (dataMgr == null)
            Debug.LogError("dataMgr == null");
        TooltipHide();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Show a tool tip. Caller must also call TooltipHide() when done.
    /// </summary>
    /// <param name="tip"></param>
    /// <param name="position">Desired position of middle left of tooltip window</param>
    public void TooltipShow(string tip, Vector3 position)
    {
        toolTipPanel.SetActive(true);
        position.x += toolTipPanel.GetComponent<RectTransform>().rect.width/2;
        toolTipPanel.transform.position = position;
        toolTipText.text = tip;
    }

    public void TooltipHide()
    { 
        toolTipPanel.SetActive(false);
    }

    /// <summary>
    /// Call this when data has been updated in some way the will need a UI refresh (i.e. DataVariables)
    /// </summary>
    public void DataUpdated()
    {
        visualMappingUIHandler.RefreshUI();
        //Debug
        dataMgr.DebugDumpVariables(false/*verbose*/);   
    }

    /// <summary>
    /// Pass-thru func
    /// </summary>
    /// <returns></returns>
    public int[] GetColorTableAssignments()
    {
        return visualMappingUIHandler.GetColorTableAssignments();
    }

    public void OnRedrawButtonClick()
    {
        heatVRML.NewPrepareAndDrawData();
    }
}
