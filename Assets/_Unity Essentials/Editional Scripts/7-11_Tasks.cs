// ФАЙЛ: BattleArena.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----- ІНТЕРФЕЙС -----
public interface IDamageable
{
    Vector3 Position { get; }
    void Damage(float damage);
}

// ----- РОЗШИРЕННЯ ДЛЯ ТРАНСФОРМА -----
public static class ExtensionMethods
{
    public static void ResetTransformation(this Transform trans)
    {
        trans.position = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = Vector3.one;
    }
}

// ----- БАЗОВИЙ КЛАС ФРУКТ -----
public class Fruit
{
    public Fruit()
    {
        Debug.Log("Fruit created.");
    }

    public virtual void Chop()
    {
        Debug.Log("Chop: Generic fruit chopped.");
    }

    public virtual void SayHello()
    {
        Debug.Log("Hello! I'm just a fruit.");
    }
}

// ----- ЯБЛУКО, ЩО НАСЛІДУЄ ФРУКТ -----
public class Apple : Fruit, IDamageable
{
    public Apple()
    {
        Debug.Log("Apple created.");
    }

    public override void Chop()
    {
        base.Chop();
        Debug.Log("Chop: Apple chopped into slices!");
    }

    public override void SayHello()
    {
        base.SayHello();
        Debug.Log("Hello! I'm a juicy apple.");
    }

    public Vector3 Position => Vector3.zero;

    public void Damage(float damage)
    {
        Debug.Log($"Apple took {damage} damage, but it's still tasty!");
    }
}

// ----- РОБОТ -----
public class Robot : MonoBehaviour, IDamageable
{
    public float health = 100f;
    public Vector3 Position => transform.position;

    public void Damage(float damage)
    {
        health -= damage;
        Debug.Log($"Robot took {damage} damage. Remaining health: {health}");
    }
}

// ----- ЩИТ, ЩО ВИКОРИСТОВУЄ ІНТЕРФЕЙС -----
public class ProtonShield : IDamageable
{
    public float hitPoints = 25f;
    public Vector3 Position => Vector3.zero;

    public void Damage(float damage)
    {
        hitPoints -= damage;
        Debug.Log($"Shield absorbed {damage} damage. Remaining: {hitPoints}");
    }
}

// ----- КЛАС З [SerializeReference] -----
public class PlayerHealth : MonoBehaviour
{
    [SerializeReference]
    public IDamageable shield = new ProtonShield();
}

// ----- ПЕРЕВАНТАЖЕННЯ МЕТОДІВ -----
public class MathHelper
{
    public int Combine(int a, int b)
    {
        return a + b;
    }

    public string Combine(string a, string b)
    {
        return a + b;
    }
}

// ----- GENERIC МЕТОД І КЛАc
public class Utility
{
    public T Echo<T>(T input)
    {
        Debug.Log($"Echo: {input}");
        return input;
    }
}

public class Storage<T>
{
    private T data;
    public void Set(T item) => data = item;
    public T Get() => data;
}

public class Explosion : MonoBehaviour
{
    public float range = 5f;
    public float damage = 30f;
    List<IDamageable> targets = new List<IDamageable>();

    void Start()
    {
        // шукаємо всі об'єкти, що реалізують IDamageable
        MonoBehaviour[] allScripts = FindObjectsOfType<MonoBehaviour>();
        foreach (var script in allScripts)
        {
            if (script is IDamageable d)
                targets.Add(d);
        }
    }

    public void Explode()
    {
        Debug.Log("💥 Explosion happened!");
        foreach (var t in targets)
        {
            if (Vector3.Distance(t.Position, transform.position) < range)
                t.Damage(damage);
        }
    }
}

// ----- ОСНОВНИЙ КОНТРОЛЕР -----
public class GameController : MonoBehaviour
{
    void Start()
    {
        // Reset трансформації
        transform.ResetTransformation();

        // Перевизначення методів
        Apple myApple = new Apple();
        myApple.SayHello();
        myApple.Chop();

        Fruit upcastedFruit = new Apple();
        upcastedFruit.SayHello();
        upcastedFruit.Chop();

        // Перевантаження методів
        MathHelper math = new MathHelper();
        Debug.Log(math.Combine(2, 3));
        Debug.Log(math.Combine("Fruit", " Salad"));

        // Generic метод і клас
        Utility util = new Utility();
        util.Echo<string>("Generic Hello!");
        Storage<int> storage = new Storage<int>();
        storage.Set(42);
        Debug.Log("Storage contains: " + storage.Get());

        // Виклик вибуху
        Explosion explosion = gameObject.AddComponent<Explosion>();
        explosion.Explode();

        // PlayerHealth з [SerializeReference]
        PlayerHealth player = gameObject.AddComponent<PlayerHealth>();
        player.shield.Damage(10);
    }
}

