using UnityEngine;

[CreateAssetMenu(fileName = "NewCommunityObject", menuName = "Scriptable Objects/Community Object", order = 1)]
public class CommunityObject : ScriptableObject
{
	//Face
	[TextArea(1, 10)]
	public string CommunityName;

	//Top
	public Sprite CommunityLogo;

	//Bottom
	[TextArea(1, 10)]
	public string CommunityDescription;

	//Right
	[TextArea(1, 10)]
	public string CommunityHighlights;

	//Left
	[TextArea(1, 10)]
	public string CommunityLeadName;
	public Sprite CommunityLeadPicture;

	//Back
	[TextArea(1, 10)]
	public string CommunityLeadInfo;
}
