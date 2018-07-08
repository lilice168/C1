using UnityEngine;

public class VerDef {

	public const int DeckCardMax = 30;
	public const int SceneGridMax = 15;


	public const int CardWidth = 252; // 卡片Width
	public const int ChardHeight = 353; // 卡片Height



    public const string Tag_Player = "Player";
    public const string Tag_Block = "Block";
    public const string Tag_Monster = "Monster";
    public const string Tag_Skill = "Skill";


    public const RigidbodyConstraints MoveX = RigidbodyConstraints.FreezeRotation |
                                        RigidbodyConstraints.FreezePositionY |
                                        RigidbodyConstraints.FreezePositionZ;
}
