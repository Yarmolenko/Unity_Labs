// �������� ���� ����
using UnityEngine;
using System;
using Random = UnityEngine.Random;

// ������� �������� ���� (�� ��� ����)
namespace FantasyGame.Creatures
{
    namespace Allies
    {
        public class Elf : MagicalCreature
        {
            public override void CastSpell()
            {
                Debug.Log("Elf casts a healing spell!");
            }
        }
    }

    namespace Enemies
    {
        public class Goblin : MagicalCreature
        {
            public override void CastSpell()
            {
                Debug.Log("Goblin casts a fireball!");
            }
        }
    }

    // ����������
    [Serializable]
    public class MagicalCreature
    {
        public virtual void CastSpell() { }
    }

    // ����������� � �������� ����
    public class BattleArena
    {
        public MagicalCreature[] fighters;

        public BattleArena()
        {
            fighters = new MagicalCreature[2];
            fighters[0] = new Allies.Elf();
            fighters[1] = new Enemies.Goblin();

            if (fighters[0] is Allies.Elf elf)
            {
                elf.CastSpell();
            }

            Enemies.Goblin goblin = fighters[1] as Enemies.Goblin;
            goblin?.CastSpell();
        }
    }
}

// ���������� + ��������������
public class Wizard
{
    private int mana;

    public int Mana
    {
        get { return mana; }
        set { mana = value; }
    }

    public int Level
    {
        get { return mana / 100; }
        set { mana = value * 100; }
    }

    public int Health { get; set; }
}

// ������� ����
public class CreatureCounter
{
    public static int totalCreatures = 0;

    public CreatureCounter()
    {
        totalCreatures++;
    }
}

// ������������ �����
public class Creature
{
    public virtual void Yell()
    {
        Debug.Log("Creature yells!");
    }
}

public class Beast : Creature
{
    new public void Yell()
    {
        Debug.Log("Beast roars!");
    }
}

public class Dragon : Beast
{
    new public void Yell()
    {
        Debug.Log("Dragon breathes fire!");
    }
}

// �����������
public class Potion
{
    public string effect;

    public Potion()
    {
        effect = "Healing";
        Debug.Log("Default Potion Created");
    }

    public Potion(string customEffect)
    {
        effect = customEffect;
        Debug.Log($"Custom Potion: {effect}");
    }

    public void Drink()
    {
        Debug.Log($"Potion drunk: {effect}");
    }
}

public class ManaPotion : Potion
{
    public ManaPotion()
    {
        effect = "Mana Restore";
        Debug.Log("Mana Potion Created");
    }

    public ManaPotion(string customEffect) : base(customEffect)
    {
        Debug.Log("Custom Mana Potion Created");
    }
}

// ��������� ���� � �������
public static class Utilities
{
    public static int AddXP(int currentXP, int gainedXP)
    {
        return currentXP + gainedXP;
    }
}

// �������� ���� ���
public class GameStarter : MonoBehaviour
{
    void Start()
    {
        // ����������
        Wizard wizard = new Wizard();
        wizard.Mana = 500;
        Debug.Log($"Wizard level: {wizard.Level}");

        // �������
        new CreatureCounter();
        new CreatureCounter();
        Debug.Log($"Total creatures: {CreatureCounter.totalCreatures}");

        // ���������� � �������� ����
        FantasyGame.Creatures.BattleArena arena = new FantasyGame.Creatures.BattleArena();

        // ������������
        Creature c1 = new Creature();
        Creature c2 = new Beast();
        Creature c3 = new Dragon();
        c1.Yell();  // Creature
        c2.Yell();  // Creature (���������)
        c3.Yell();  // Creature (���������)

        // �����������
        Potion p = new Potion();
        ManaPotion mp = new ManaPotion("Super Mana");

        p.Drink();
        mp.Drink();

        // ��������� �����
        int newXP = Utilities.AddXP(100, 50);
        Debug.Log($"New XP: {newXP}");

        // �������� ���� ����
        float speed = Random.value;
        Debug.Log($"Random speed: {speed}");
    }
}

