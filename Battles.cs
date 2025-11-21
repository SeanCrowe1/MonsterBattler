using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;

class Battles
{
    public static void MonsterBattle()
    {
        Random generator = new Random();
        string[] elementNames = ["Normal", "Grass", "Fire", "Water", "Electric", "Bug", "Ground", "Poison", "Psychic", "Flying", "Ghost", "Dragon", "Ice", "Fighting", "Rock"];
        Element[] elements = SetElements(elementNames);
        Monster[] battlers = new Monster[2];
        Monster monster1 = new Monster("Fred", elements[generator.Next(15)], elements[generator.Next(15)], generator.Next(20, 201), generator.Next(5, 76), generator.Next(5, 51), generator.Next(5, 61));
        Monster monster2 = new Monster("George", elements[generator.Next(15)], elements[generator.Next(15)], generator.Next(20, 201), generator.Next(5, 76), generator.Next(5, 51), generator.Next(5, 61));
        battlers[0] = monster1; battlers[1] = monster2;
        int i = 0;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Let's start a fight!!");
        Thread.Sleep(1000);
        Console.WriteLine($"{monster1.Name} has challenged {monster2.Name} to a death match!!");
        Thread.Sleep(1000);
        Console.WriteLine($"Here's an overview of our fighters...");
        Thread.Sleep(1000);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n---- FIGHTER 1 ----\n Name: {monster1.Name}\n Health: {monster1.Health}\n Attack: {monster1.Attack}\n Defense: {monster1.Defense}\n Speed: {monster1.Speed}\n Element: {monster1.Element1.Name}");
        Thread.Sleep(2500);
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n---- FIGHTER 2 ----\n Name: {monster2.Name}\n Health: {monster2.Health}\n Attack: {monster2.Attack}\n Defense: {monster2.Defense}\n Speed: {monster2.Speed}\n Element: {monster2.Element1.Name}");
        Thread.Sleep(2500);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n------ FIGHT! ------\n");
        Thread.Sleep(1000);
        bool isBattling = true;
        do
        {
            if (i == monster1.Speed * monster2.Speed)
            {
                i = 0;
            }
            i++;
            if (monster1.Speed % i == 0 && monster2.Speed % i == 0)
            {
                Monster first = battlers[generator.Next(2)];
                Monster second = monster2;
                if (first == second)
                {                    
                    second = monster1;
                }
                if (first == monster1)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                } else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                Console.WriteLine($"It's {first.Name}'s turn -\n");
                Thread.Sleep(1000);
                isBattling = Attack(first, second);
                if (!isBattling)
                {
                    break;
                }
                if (second == monster1)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                } else
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                }
                Console.WriteLine($"It's {second.Name}'s turn - \n");
                Thread.Sleep(1000);
                isBattling = Attack(second, first);
            }
            else if (monster2.Speed % i == 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"It's {monster1.Name}'s turn - \n");
                Thread.Sleep(1000);
                isBattling = Attack(monster1, monster2);
            }
            else if (monster1.Speed % i == 0)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"It's {monster2.Name}'s turn - \n");
                Thread.Sleep(1000);
                isBattling = Attack(monster2, monster1);
            }
        } while (isBattling);
    }

    public static bool Attack(Monster user, Monster target)
    {
        Console.WriteLine($"{user.Name} is attacking {target.Name}!");
        Thread.Sleep(1000);
        int deduct = target.Defense;
        if (deduct % 2 == 1)
        {
            deduct -= 1;
        }
        int damage = user.Attack - deduct;
        if (damage < 1)
        {
            damage = 1;
        }
        foreach(string e1 in user.Element1.Strengths)
        {
            
            if (target.Element1.Name == e1)
            {
                Console.WriteLine("It's super effective!");
                Thread.Sleep(1000);
                damage *= 2;
                break;
            }
        }
        foreach(string e1 in user.Element1.LowAttack)
        {
            if (target.Element1.Name == e1)
            {
                Console.WriteLine("It's not very effective...");
                Thread.Sleep(1000);
                if (damage % 2 == 1)
                {
                    
                    damage += 1;
                }
                damage /= 2;
                if (damage < 1)
                {
                    damage = 1;
                }
            }
        }
        foreach(string e1 in user.Element1.Immunities)
        {
            if (target.Element1.Name == e1)
            {
                Console.WriteLine("It has no effect...");
                Thread.Sleep(1000); 
                damage = 0;
            }
        }
        target.Health -= damage;
        if (damage == 1)
        {
            Console.WriteLine($"{target.Name} loses {damage} point of health!");
            Thread.Sleep(1000);
        }
        else
        {
            Console.WriteLine($"{target.Name} loses {damage} points of health!");
            Thread.Sleep(1000);
        }
        if (target.Health <= 0)
        {
            target.Health = 0;
            Console.WriteLine($"{target.Name} has died!\n");
            Thread.Sleep(1000);
            return false;
        }
        Console.WriteLine($"{target.Name}'s total health is now {target.Health}\n");
        Thread.Sleep(1000);
        return true;
    }

    public static Element[] SetElements(string[] names)
    {
        Element[] elements = new Element[15];
        int i = 0;
        foreach (string e in names)
        {
            string[] weaknesses = [];
            string[] defenses = [];
            string[] strengths = [];
            string[] lowAttack = [];
            string[] immunities = [];
            if (e == "Normal")
            {
                weaknesses = ["Fighting"];
                lowAttack = ["Rock"];
                immunities = ["Ghost"];
            } else if (e == "Grass")
            {
                weaknesses = ["Fire", "Ice", "Poison", "Flying", "Bug"];
                defenses = ["Water", "Electric", "Grass", "Ground"];
                strengths = ["Water", "Ground", "Rock"];
                lowAttack = ["Fire", "Grass", "Poison", "Flying", "Bug", "Dragon"];
            } else if (e == "Fire")
            {
                weaknesses = ["Water", "Ground", "Rock"];
                defenses = ["Fire", "Grass", "Bug"];
                strengths = ["Grass", "Ice", "Bug"];
                lowAttack = ["Fire", "Water", "Rock", "Dragon"];
            } else if (e == "Water")
            {
                weaknesses = ["Electric", "Grass"];
                defenses = ["Fire", "Water", "Ice"];
                strengths = ["Fire", "Ground", "Rock"];
                lowAttack = ["Water", "Grass", "Dragon"];
            } else if (e == "Electric")
            {
                weaknesses = ["Ground"];
                defenses = ["electric", "Flying"];
                strengths = ["Water", "Flying"];
                lowAttack = ["Electric", "Grass", "Dragon"];
            } else if (e == "Bug")
            {
                weaknesses = ["Fire", "Poison", "Flying", "Rock"];
                defenses = ["Grass", "Fighting", "Ground"];
                strengths = ["Grass", "Poison", "Psychic"];
                lowAttack = ["Fire", "Fighting", "Flying", "Ghost"];
            } else if (e == "Ground")
            {
                weaknesses = ["Water", "Grass", "Ice"];
                defenses = ["Poison", "Rock"];
                strengths = ["Fire", "Electric", "Poison", "Rock"];
                lowAttack = ["Grass", "Bug"];
                immunities = ["Electric"];
            } else if (e == "Poison")
            {
                weaknesses = ["Ground", "Psychic", "Bug"];
                defenses = ["Grass", "Fighting", "Poison"];
                strengths = ["Grass", "Bug"];
                lowAttack = ["Poison", "Ground", "Rock", "Ghost"];
            } else if (e == "Psychic")
            {
                weaknesses = ["Bug"];
                defenses = ["Fighting", "Psychic"];
                strengths = ["Fighting", "Poison"];
                lowAttack = ["Psychic"];
                immunities = ["Ghost"];
            } else if (e == "Flying")
            {
                weaknesses = ["Electric", "Ice", "Rock"];
                defenses = ["Grass", "Fighting", "Bug"];
                strengths = ["Grass", "Fighting", "Bug"];
                lowAttack = ["Electric", "Rock"];
                immunities = ["Ground"];
            } else if (e == "Ghost")
            {
                weaknesses = ["Ghost"];
                defenses = ["Poison", "Bug"];
                strengths = ["Ghost"];
                immunities = ["Normal", "Fighting"];
            } else if (e == "Dragon")
            {
                weaknesses = ["Ice", "Dragon"];
                defenses = ["Fire", "Water", "Electric", "Grass"];
                strengths = ["Dragon"];
            } else if (e == "Ice")
            {
                weaknesses = ["Fire", "Fighting", "Rock"];
                defenses = ["Ice"];
                strengths = ["Grass", "Ground", "Flying", "Ice"];
                lowAttack = ["Water", "Ice"];
            } else if (e == "Fighting")
            {
                weaknesses = ["Flying", "Psychic"];
                defenses = ["Bug", "Rock"];
                strengths = ["Normal", "Ice", "Rock"];
                lowAttack = ["Poison", "Flying", "Psychic", "Bug"];
            } else if (e == "Rock")
            {
                weaknesses = ["Water", "Grass", "Fighting", "Ground"];
                defenses = ["Normal", "Fire", "Poison", "Flying"];
                strengths = ["Fire", "Ice", "Flying", "Bug"];
                lowAttack = ["Fighting", "Ground"];
            }
            elements[i] = new Element(e, weaknesses, defenses, strengths, lowAttack, immunities);
            i++;
        }
        return elements;
    }
}