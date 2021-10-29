using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _5.Models;

namespace _5.Data
{
    public static class DbInitializer
    {
        public static void Initialize(PublishingLabContext db)
        {
            db.Database.EnsureCreated();

            int authors_number = 150;
            string authorname;

            Random randObj = new Random(1);

            string[] author_voc = { "Янка Купала_", "Васіль Быкаў_", "Кузьма Чорны_",
                                    "Якуб Колас_", "Алаіза Пашкевіч_", "Ян Баршчэўскі_",
                                    "Анатоль Сыс_", "Янка Брыль_","Рыгор Барадулін_","Ніл Гілевіч_",
                                    "Уладзімір Караткевіч_","Максім Танк_","Кастусь Каліноўскі_","Лев Толстой_",
                                    "Анна Ахматова_","Марина Цветаева_","Борис Пастернак_","Михаил Булгаков_","" +
                                    "Адам Мицкевич","Александр Пушкин",};
            int count_author_voc = author_voc.GetLength(0);
            for (int authorId = 1; authorId <= authors_number; authorId++)
            {
                authorname = author_voc[randObj.Next(count_author_voc)] + authorId.ToString();
                db.Authors.Add(new Author { Fio = authorname });
            }

            db.SaveChanges();
        }
    }
}
