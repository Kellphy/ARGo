using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommunityPanelsInfo : MonoBehaviour
{
    public CommunityObject communityObject;

	//Front
	public TextMeshProUGUI frontText;

	//Top
	public Image topImage;

	//Bottom
	public TextMeshProUGUI bottomText;

	//Right
	public TextMeshProUGUI rightText;

	//Left
	public TextMeshProUGUI leftText;
	public Image leftImage;

	//Back
	public TextMeshProUGUI backText;

	void Start()
    {
		if(communityObject != null)
		{
			frontText.text = communityObject.CommunityName;
			topImage.sprite = communityObject.CommunityLogo;
			bottomText.text = communityObject.CommunityDescription;
			rightText.text = communityObject.CommunityHighlights;
			leftText.text = communityObject.CommunityLeadName;
			leftImage.sprite = communityObject.CommunityLeadPicture;
			backText.text = communityObject.CommunityLeadInfo;
		}
	}
}
