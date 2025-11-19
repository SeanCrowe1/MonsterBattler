public class Element(string name, string[] weaknesses, string[] defenses, string[] strengths, string[] lowAttack, string[] immunities)
{
    public string Name { get; set; } = name;
    public string[] Weaknesses { get; set; } = weaknesses;
    public string[] Defenses { get; set; } = defenses;
    public string[] Strengths { get; set; } = strengths;
    public string[] LowAttack { get; set; } = lowAttack;
    public string[] Immunities { get; set; } = immunities;
}
