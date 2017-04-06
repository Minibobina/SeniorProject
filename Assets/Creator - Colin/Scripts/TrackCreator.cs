using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SectionType {Stright, Left, Right, Up, Down, Custom};
public class TrackCreator : MonoBehaviour
{
	public List<GameObject> sections = new List<GameObject>();
	public List<GameObject> prefabs = new List<GameObject>();
	public GameObject Station;
	private GameObject previous;
	private SectionType currentType;

	private RollerCoasterPlanes rp;

	private bool isComplete = false;

	void Start ()
	{
		previous = Station;
		rp = this.GetComponent<RollerCoasterPlanes>();
	} 
	public void CreateSection ()
	{
		GameObject section = Instantiate(prefabs[(int)currentType]);
		section.transform.parent = this.transform;
		AttachTo(section);
		SectionUpdated(section);

		foreach(GameObject go in section.GetComponent<Section>().railPoints)
		{
			rp.railPoints.Add(go.transform);
		}
		
	}

	public void UndoSection ()
	{
		if(sections.Count - 1 > 0)
		{
			for(int i = 0; i < (sections[sections.Count - 1].GetComponent<Section>().railPoints.Count); i++)
				rp.railPoints.RemoveAt(rp.railPoints.Count - 1);

			Destroy(sections[sections.Count - 1]);
			sections.RemoveAt(sections.Count -1);
			PreviousUpdate();

		}
		else
			return;

	}
	public void TypeUpdated (int index)
	{
		currentType = (SectionType)index;
	}
	
	private void AttachTo (GameObject connector)
	{
		Transform tr1P, tr1C, tr2P, tr2C;
		tr1P = connector.transform;
		tr1C = connector.GetComponent<Section>().start.transform;
		tr2P = previous.transform;
		tr2C = previous.GetComponent<Section>().end.transform;

		Vector3 v1 = tr1P.position - tr1C.position;
     	Vector3 v2 = tr2C.position - tr2P.position;
     	tr1P.rotation = Quaternion.FromToRotation(v1, v2) * tr1P.rotation;
     	tr1P.position = tr2C.position + v2.normalized * v1.magnitude;

	}

	private void SectionUpdated (GameObject section)
	{
		// Adding section to list
		sections.Add(section);
		PreviousUpdate();
	}
	void PreviousUpdate ()
	{
		// Updateing the previous section for connection
		if(sections.Count - 1 > 0)
			previous = sections[sections.Count - 1];
		else
			previous = Station;
	}
}
