public class AmuletEffect
{
    public string name;
    public string description;
    public System.Action applyEffect;

    public AmuletEffect(string name, string description, System.Action applyEffect)
    {
        this.name = name;
        this.description = description;
        this.applyEffect = applyEffect;
    }
}
