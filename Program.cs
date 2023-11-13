using System.Collections;
using System.Security.Cryptography.X509Certificates;


/// <summary>
///  Интерфейс HeroDefence
///  Интерфейс реализующий защиту 
/// </summary>
interface HeroDefence
{

    /// <summary>
    /// Метод который получает Armour благодоря здоровью и уровню
    /// </summary>
    /// <param name="life">Здоровье</param>
    /// <param name="lvl">Уровень</param>
    /// <returns>Armour</returns>
    int Armour(int life, int lvl);
    /// <summary>
    /// Метод считающий подавление урона
    /// </summary>
    /// <param name="armour">Броня</param>
    /// <param name="life">Здоровье</param>
    /// <returns>Reduction</returns>
    int Reduction(int armour, int life);
}

/// <summary>
/// Интерфейс TakeDamage
///  Интерфейс реализующий урон
/// </summary>
interface TakeDamage
{
    /// <summary>
    /// Вычесление урона
    /// </summary>
    /// <param name="TotalDamage">Количество Урона</param>
    /// <returns></returns>
    int damageCalc(int TotalDamage);
}

/// <summary>
/// класс Hero реализующий два интерфейса : HeroDefence, TakeDamage
/// </summary>
public class Hero : HeroDefence, TakeDamage
{
    /// <summary>
    /// Урон
    /// </summary>
    public int Damage { get; set; }
    /// <summary>
    /// Имя героя
    /// </summary>
    public string name { get; set; }
    /// <summary>
    /// Здоровье
    /// </summary>
    public int life { get; set; }
    /// <summary>
    /// Броня
    /// </summary>
    public int armour { get; set; }
    /// <summary>
    /// Уровень
    /// </summary>
    public int lvl { get; set; }

    /// <summary>
    /// Коструктор героя
    /// </summary>
    /// <param name="life">Здоровье</param>
    /// <param name="armour">Броня</param>
    /// <param name="lvl">Уровень</param>
    /// <param name="name">Имя героя</param>
    /// <param name="Damage">Урон</param>
    public Hero(int life, int armour, int lvl, string name, int Damage)
    {
        this.life = life;
        this.armour = armour;
        this.lvl = lvl;
        this.name = name;
        this.Damage = Damage;
    }

    /// <summary>
    /// Метод брони , взят из HeroDefence
    /// </summary>
    /// <param name="life">Здоровье</param>
    /// <param name="lvl">Уровень</param>
    /// <returns>Количество брони</returns>
    public int Armour(int life, int lvl)
    {
        return armour = life + lvl % 2;
    }

    /// <summary>
    /// Метод Подавление , взят из HeroDefence
    /// </summary>
    /// <param name="armour"></param>
    /// <param name="life"></param>
    /// <returns>Подавление</returns>
    public int Reduction(int armour, int life)
    {
        return armour + life - lvl % 2;
    }

    /// <summary>
    /// Метод урона , взят из TakeDamage
    /// </summary>
    /// <param name="TotalDamage">Урон</param>
    /// <returns>Отнимает здоровье , смотря сколько урона</returns>
    public int damageCalc(int TotalDamage)
    {
        return life -= TotalDamage;
    }


}


/// <summary>
/// Класс в котором реализуется main
/// </summary>
public class MainClass
{

    /// <summary>
    /// То где происходят все события : Создание экземпляров , вывод на консоль все характерикстики героев
    /// </summary>
    public static void Main()
    {
        // Создание экземпляров героев
        Hero hero1 = new Hero(50, 0, 10, "Player1", 15);


        Hero hero2 = new Hero(50, 0, 10, "Player2", 10);



        //Вывод характеристик 
        Console.WriteLine(hero1.name + ", Броня: " + hero1.Armour(hero1.life, hero1.lvl) + ", Подавление урона: " + hero1.Reduction(hero1.armour, hero1.life)
         + ", хп:" + hero1.life + ", Уровень:" + hero1.lvl + ", урон:" + hero1.Damage);

        Console.WriteLine(hero2.name + ", Броня: " + hero2.Armour(hero2.life, hero2.lvl) + ", Подавление урона: " + hero2.Reduction(hero2.armour, hero2.life)
         + ", хп:" + hero2.life + ", Уровень:" + hero2.lvl + ", урон:" + hero2.Damage);
        Console.WriteLine();

        //Если true значит цикл начнет работу , если false то нет.
        Console.Write("Включить сражение?: ");
        bool a = Convert.ToBoolean(Console.ReadLine());

        if (a == true)
        {
            //цикл который закончит свою работу , когда у какого либо из героев не будет больше 0 хп.
            while (hero1.life > 0 && hero2.life > 0)
            {

                hero1.damageCalc(hero2.Damage);
                Console.WriteLine($"{hero2.name} нанес {hero2.Damage} урона {hero1.name}");
                ConsoleLife(hero1, hero2);
                Thread.Sleep(2000);
                if (hero2.life <= 0)
                {
                    break;
                }

                hero2.damageCalc(hero1.Damage);
                Console.WriteLine($"{hero1.name} нанес {hero1.Damage} урона {hero2.name}");
                ConsoleLife(hero1, hero2);
            }


            //Логика , которая определяет кто из героев выиграл , путем просмотра их здоровья
            if (hero1.life > 0)
            {
                Console.WriteLine($"{hero1.name} победил");
            }
            else if (hero2.life > 0)
            {
                Console.WriteLine($"{hero2.name} победил");
            }
            else
            {
                Console.WriteLine($"Ничья");
            }
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("пока");
        }
    }

    /// <summary>
    /// Функция Цикл , который преобразует жизни героев из int в string ввиде сердечек
    /// </summary>
    /// <param name="life">Здоровье</param>
    /// <returns>Количество сердец</returns>
    static string GetHeart(int life)
    {
        string heart = string.Empty;
        for (int i = 0; i < life; i++)
        {
            heart += "❤";
        }
        return heart;
    }
    /// <summary>
    /// Фукнция, которая выводит жизни , путем использования фунции GetHeart(hero.life)
    /// </summary>
    /// <param name="hero1">Герой1</param>
    /// <param name="hero2">Герой2</param>
    static void ConsoleLife(Hero hero1, Hero hero2)
    {
        Console.WriteLine($"{hero1.name} жизни: {GetHeart(hero1.life)}");
        Console.WriteLine($"{hero2.name} жизни: {GetHeart(hero2.life)}");
        Console.WriteLine();
    }
}

