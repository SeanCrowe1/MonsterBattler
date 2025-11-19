using System.Linq.Expressions;

public class Monster(string name, Element element1, Element element2, int health=20, int attack=5, int defense=5, int speed=5)
{
    public string Name { get; set; } = name;
    public int Health { get; set; } = health;
    public int Attack { get; set; } = attack;
    public int Defense { get; set; } = defense;
    public int Speed { get; set; } = speed;
    public Element Element1 { get; set; } = element1;
    public Element Element2 { get; set; } = element2;
}