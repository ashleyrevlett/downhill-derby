using UnityEngine;
using System.Collections;

public class ShowPanels : MonoBehaviour {


	[System.Serializable]
	public struct PanelInfo {
		public GameObject panelObject;
		public GameObject panelTint;
	}
		
	public PanelInfo[] panels;

	private PanelInfo GetPanel(string panelName) {
		foreach (PanelInfo panel in panels) {
			if (panel.panelObject.name == panelName) {
				return panel;
			}
		}
		PanelInfo p = new PanelInfo ();
		p.panelObject = null;
		p.panelTint = null;
		return p;
	}

	public void ShowPanel(string thePanel) {
		PanelInfo panel = GetPanel (thePanel);
		if (panel.panelObject != null) panel.panelObject.SetActive (true);
		if (panel.panelTint != null) panel.panelTint.SetActive (true);
	}

	public void HidePanel(string thePanel) {
		PanelInfo panel = GetPanel (thePanel);
		if (panel.panelObject != null) panel.panelObject.SetActive (false);
		if (panel.panelTint != null) panel.panelTint.SetActive (false);
	}

}
