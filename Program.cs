using System;
using System.Threading;
using Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_bot
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new TelegramBotClient("6336846722:AAFzJz6isidDLLhtd-4oyzLwGiaEt781wcs");
            client.StartReceiving(Update, Error);
            Console.ReadLine();
        }

        private static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            Console.WriteLine(exception);
            return Task.CompletedTask;
        }


        private async static Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            Random rand = new Random();
            var message = update.Message;
            int chance;

            Mobs hero = new Mobs(100,19, "Слизень-маг", "Я готов сражаться!");
            Mobs mob1 = new Mobs(rand.Next(80,100),rand.Next(15, 20), "Смерть", "Я вас уничтожу!");
            Mobs mob2 = new Mobs(rand.Next(90,110),rand.Next(13, 20), "Приведение", "Я готов к победе!");

            Console.WriteLine($"Вы получили сообщение {message.Text} от {message.Chat.FirstName}");
            if (message.Text != null)
            {
                //client.SendTextMessageAsync(message.Chat.Id, "Бот на техобслуживании. Могут быть сбои в работе");

                switch (message.Text.ToLower())
                {
                    case "/start":
                        client.SendTextMessageAsync(message.Chat.Id, "Привет. Мой список команд:" +
                            "\n1 - привет\n2 - как дела\n3 - что любишь делать\n4 - как погода\n5 - персонаж\n6 - твои любимые игры\n7 - будущие функции\n8 - генерация рандомного числа от 10 до 100\n9 - игра бой героев" +
                            "\n для использования команд пишите цифры." +
                            "Бот пока обновляется почти ежедневно, добавлются новые команды");
                        break;
                    case "1":
                        client.SendTextMessageAsync(message.Chat.Id, $"Привет {message.Chat.FirstName}");
                        client.SendStickerAsync(message.Chat.Id, sticker: InputFile.FromUri("https://chpic.su/_data/stickers/s/Slime_AFilinkov/Slime_AFilinkov_017.webp"));
                        break;
                    case "2":
                        chance = rand.Next(0,3);
                        if(chance == 0)
                        {
                            client.SendTextMessageAsync(message.Chat.Id, "Все хорошо");
                        }         
                        if(chance == 1)
                        {
                            client.SendTextMessageAsync(message.Chat.Id, "Все отлично!");
                        }     
                        if(chance == 2)
                        {
                            client.SendTextMessageAsync(message.Chat.Id, "Все нормально и я надеюсь" +
                                " мне дадут больше функционала");
                            client.SendStickerAsync(message.Chat.Id, sticker: InputFile.FromUri("https://chpic.su/_data/stickers/s/Slime_AFilinkov/Slime_AFilinkov_020.webp"));
                        }
                        break;
                    case "3":
                        chance = rand.Next(0, 3);
                        if (chance == 0)
                        {
                            client.SendTextMessageAsync(message.Chat.Id, "Я люблю играть");
                        }
                        if (chance == 1)
                        {
                            client.SendTextMessageAsync(message.Chat.Id, "Обожаю программировать");
                        }
                        if (chance == 2)
                        {
                            client.SendTextMessageAsync(message.Chat.Id, "Люблю кушать");
                        }
                        break;
                    case "4":
                        client.SendTextMessageAsync(message.Chat.Id, "Погода просто сказка!");

                        break;
                    case "5":
                        client.SendTextMessageAsync(message.Chat.Id, $"Ваш персонаж - {hero.Name}\n" +
                            $"Здоровье: {hero.Health}\n{hero.Description}");
                        client.SendStickerAsync(message.Chat.Id, sticker: InputFile.FromUri("https://chpic.su/_data/stickers/s/Slime_AFilinkov/Slime_AFilinkov_010.webp"));
                        break;
                    case "6":
                        chance = rand.Next(0, 3);
                        if (chance == 0)
                        {
                            client.SendTextMessageAsync(message.Chat.Id, "Конечно же Terraria");
                        }
                        if (chance == 1)
                        {
                            client.SendTextMessageAsync(message.Chat.Id, "Core Keeper и the binding of Isaac");
                        }
                        if (chance == 2)
                        {
                            client.SendTextMessageAsync(message.Chat.Id, "Моя самая любимая игра happy wars и Endless adventure");
                        }
                        break;
                    case "7":
                        client.SendTextMessageAsync(message.Chat.Id, "В планах на добавление идет" +
                            "\n1 - текстовая игра бой геров\n2 - разнообразность присылаемых ботом сообщений");
                        break;
                    case "8":
                        int randomNumber;
                        randomNumber = rand.Next(10, 100);
                        client.SendTextMessageAsync(message.Chat.Id, $"Рандомное число - {randomNumber}");
                        break;
                    case "9":
                        chance = rand.Next(0,2);
                        if(chance == 0)
                        {
                            client.SendTextMessageAsync(message.Chat.Id, $"Вы начинаете игру.");
                            client.SendTextMessageAsync(message.Chat.Id, $"Ваш персонаж - {hero.Name}\n" +
        $"Здоровье: {hero.Health}, урон: {hero.Damage}\n{hero.Description}");
                            Thread.Sleep(500);
                            client.SendStickerAsync(message.Chat.Id, sticker: InputFile.FromUri("https://chpic.su/_data/stickers/s/Slime_AFilinkov/Slime_AFilinkov_010.webp"));
                            Thread.Sleep(1000);
                            client.SendTextMessageAsync(message.Chat.Id, $"Ваш противник - {mob1.Name}\n" +
        $"Здоровье: {mob1.Health}, урон: {mob1.Damage}\n{mob1.Description}");
                            client.SendStickerAsync(message.Chat.Id, sticker: InputFile.FromUri("https://chpic.su/_data/stickers/b/bonemaaan/bonemaaan_006.webp?v=1698"));
                            client.SendTextMessageAsync(message.Chat.Id, "Вы вступаете в бой");
                            while (hero.Health > 0 && mob1.Health > 0)
                            {
                                hero.TakeDamage(mob1.Damage);
                                mob1.TakeDamage(hero.Damage);
                                client.SendTextMessageAsync(message.Chat.Id, $"Вы нанесли {hero.Damage} урона, у {mob1.Name} осталось {mob1.Health} здоровья");
                                client.SendTextMessageAsync(message.Chat.Id, $"Вам нанесли {mob1.Damage} урона, у вас осталось {hero.Health} здоровья");
                                Thread.Sleep(1700);
                            }
                            if (mob1.Health <= 0 && hero.Health <= 0)
                            {
                                client.SendTextMessageAsync(message.Chat.Id, "Никто не победил");
                            }
                            else if (mob1.Health <= 0)
                            {
                                client.SendTextMessageAsync(message.Chat.Id, $"Вы убили {mob1.Name}");
                                client.SendStickerAsync(message.Chat.Id, sticker: InputFile.FromUri("https://chpic.su/_data/stickers/s/Slime_AFilinkov/Slime_AFilinkov_008.webp"));
                                Thread.Sleep(700);
                            }
                            else if (hero.Health <= 0)
                            {
                                client.SendTextMessageAsync(message.Chat.Id, $"Вас убило {mob1.Name}");
                                break;
                            }
                            client.SendTextMessageAsync(message.Chat.Id, $"Игра бой героев находится в стадии разработки. Многое будет меняться");
                        }
                        else
                        {
                            client.SendTextMessageAsync(message.Chat.Id, $"Вы начинаете игру.");
                            client.SendTextMessageAsync(message.Chat.Id, $"Ваш персонаж - {hero.Name}\n" +
        $"Здоровье: {hero.Health}, урон: {hero.Damage}\n{hero.Description}");
                            Thread.Sleep(500);
                            client.SendStickerAsync(message.Chat.Id, sticker: InputFile.FromUri("https://chpic.su/_data/stickers/s/Slime_AFilinkov/Slime_AFilinkov_010.webp"));
                            Thread.Sleep(1000);
                            client.SendTextMessageAsync(message.Chat.Id, $"Ваш противник - {mob2.Name}\n" +
        $"Здоровье: {mob2.Health}, урон: {mob2.Damage}\n{mob2.Description}");
                            client.SendStickerAsync(message.Chat.Id, sticker: InputFile.FromUri("https://chpic.su/_data/stickers/p/PumpkinGhost/PumpkinGhost_018.webp?v=1699126022"));
                            client.SendTextMessageAsync(message.Chat.Id, "Вы вступаете в бой");
                            while (hero.Health > 0 && mob2.Health > 0)
                            {
                                hero.TakeDamage(mob1.Damage);
                                mob2.TakeDamage(hero.Damage);
                                client.SendTextMessageAsync(message.Chat.Id, $"Вы нанесли {hero.Damage} урона, у {mob2.Name} осталось {mob2.Health} здоровья");
                                client.SendTextMessageAsync(message.Chat.Id, $"Вам нанесли {mob2.Damage} урона, у вас осталось {hero.Health} здоровья");
                                Thread.Sleep(1700);
                            }
                            if (mob2.Health <= 0 && hero.Health <= 0)
                            {
                                client.SendTextMessageAsync(message.Chat.Id, "Никто не победил");
                            }
                            else if (mob2.Health <= 0)
                            {
                                client.SendTextMessageAsync(message.Chat.Id, $"Вы убили {mob2.Name}");
                                client.SendStickerAsync(message.Chat.Id, sticker: InputFile.FromUri("https://chpic.su/_data/stickers/s/Slime_AFilinkov/Slime_AFilinkov_008.webp"));
                                Thread.Sleep(700);
                            }
                            else if (hero.Health <= 0)
                            {
                                client.SendTextMessageAsync(message.Chat.Id, $"Вас убил {mob2.Name}");
                                break;
                            }
                            client.SendTextMessageAsync(message.Chat.Id, $"Игра бой героев находится в стадии разработки. Многое будет меняться");
                        }
                        break;
                }
            }

        }
    }
    public class Mobs
    {
        public int Health { get; private set; }
        public int Damage { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Mobs(int health,int damage, string name, string description)
        {
            Health = health;
            Damage = damage;
            Name = name;
            Description = description;
        }
        public void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }
}