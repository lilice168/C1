
public enum UITypeName
{
	UIManager,
	Menu,
	Deck,
    BattleUI,
    SkillUI,
}

public class UIName 
{


	public static string GetTypeName(UITypeName _TypeName )
	{
		return _TypeName.ToString();
	}

}
