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
            int books_number = 30000;
            string bookName;
            int total;
            DateTime exitDate;
            decimal basecost;
            decimal finishcost;
            decimal salary;
            DateTime date;
            DateTime deadLine;
            int contract_number = 30000;
            int customers_number = 150;
            string customername;
            int orders_number = 30000;
            string orderName;
            DateTime startdate;
            DateTime finishDate;
            int exemplairs;
            decimal totalSum;


            Random randObj = new Random(1);

            string[] author_voc = { "Янка Купала_", "Васіль Быкаў_", "Кузьма Чорны_",
                                    "Якуб Колас_", "Алаіза Пашкевіч_", "Ян Баршчэўскі_",
                                    "Анатоль Сыс_", "Янка Брыль_","Рыгор Барадулін_","Ніл Гілевіч_",
                                    "Уладзімір Караткевіч_","Максім Танк_","Кастусь Каліноўскі_","Лев Толстой_",
                                    "Анна Ахматова_","Марина Цветаева_","Борис Пастернак_","Михаил Булгаков_",  
                                    "Адам Мицкевич","Александр Пушкин",};
            int count_author_voc = author_voc.GetLength(0);
            for (int authorId = 1; authorId <= authors_number; authorId++)
            {
                authorname = author_voc[randObj.Next(count_author_voc)] + authorId.ToString();
                db.Authors.Add(new Author { Fio = authorname });
            }
            db.SaveChanges();

            string[] book_voc = { "Магіла Львва_", "Знак Бяды_", "Сотнікаў_", "Доўгая дарога да дому_",
                                    "Казкі Жыцця_", "Шляхціц Завальня_", "Мы, Беларусы_", "Маці_",
                                    "Дзікае паляванне караля Стаха_", "Паром на бурнай рацэ_", "Цёмны замак Альшанскі_", "Новая Зямля_",
                                    "Ты чуеш брат?_", "Лісты з-пад шыбеніцы_", "Война и мир_", "После бала_",
                                    "Доктор Живаго_", "Магіла Львва_", "Собачье сердце_", "Мастер и Маргаритта_",
                                    "Гражына_", "Руслан и Людмила_", "Повесть о царе Салтане_"};
            int count_book_voc = book_voc.GetLength(0);
            for (int bookId = 1; bookId <= books_number; bookId++)
            {
                int authorId = randObj.Next(1, authors_number - 1);
                bookName = book_voc[randObj.Next(count_book_voc)] + bookId;
                total = randObj.Next(2000) + 160;
                DateTime today = DateTime.Now.Date;
                exitDate = today.AddDays(-bookId);
                basecost = randObj.Next(200, 2000);
                finishcost = randObj.Next(500, 20000);
                salary = randObj.Next(5000, 20000);
                db.Books.Add(new Book { Name = bookName, Total = total, Exitdate = exitDate,
                Basecost = basecost, Finishcost= finishcost, Salary = salary, AuthorId = authorId});
            }
            db.SaveChanges();

            for (int contractId = 1; contractId <= contract_number; contractId++)
            {
                int authorId = randObj.Next(1, authors_number - 1);
                DateTime today = DateTime.Now.Date;
                date = today.AddDays(-contractId);
                deadLine = today.AddDays(+contractId);
                db.Contracts.Add(new Contract { Date = date, Deadline = deadLine, AuthorId = authorId });
            }
            db.SaveChanges();

            string[] customer_voc = { "Наша Ніва_", "Наша Доля_", "Загляне сонца і ўнаша ваконца_", "Міністэрства адукацыі_",
                                      "Times_", "Міністэрсіва інфармацыі_", "Министерство пропаганды Третьего Рейха_", "Міністэрства ўнутраных спраў_",
                                      "ГГТУ им. Сухого_", "БНТУ", "Беларускі Дзяржаўны ўніверсітэт_", "ГДУ імя Ф. Скарыны_",
                                      "Гомельскі гарвыканкам", "Гомельскі аблвыканкам",
                                      "Отдел по идеологической работе гомельского горисполкома", "Адміністрацыя прэзідэнта Рэспублікі Беларусь"};
            int count_customer_voc = customer_voc.GetLength(0);
            for (int customerId = 1; customerId <= customers_number; customerId++)
            {
                customername = customer_voc[randObj.Next(count_customer_voc)] + customerId;
                db.Customers.Add(new Customer { Customername = customername });
            }
            db.SaveChanges();

            string[] order_voc = { "Заказ на напісанне падручнікаў па беларускай мове_", 
                                    "Заказ на напісанне падручнікаў па гісторыі Беларусі_", "Заказ на написание методичек_", 
                                    "Заказ на написание прозы_",
                                    "Заказ на написание поэзии_", "Заказ на написание од_", "Заказ на написание легенд_", "Заказ на написане пропагандистской литературы_",
                                    "Заказ на перевод книг Ленина_", "Заказ на перевод книг К. Маркса_", "Заказ на перевод книг Ф. Энгельса_", "Заказ на перевод книг И. Сталина_",
                                    "Заказ на перевод книг А. Гитлера_", "Заказ на напісанне дзіцячай літаратуры_", "Заказ на друк падручнікаў па фізіцы_", "Заказ на напісанне падручнікаў па паліталогіі_",};
            int count_order_voc = order_voc.GetLength(0);
            for (int orderId = 1; orderId <= orders_number; orderId++)
            {
                orderName = order_voc[randObj.Next(count_order_voc)] + orderId;
                DateTime today = DateTime.Now.Date;
                startdate = today.AddDays(-orderId);
                finishDate = today.AddDays(+orderId);
                exemplairs = randObj.Next(3000) + 160;
                totalSum = randObj.Next(10000, 25000);
                int customerId = randObj.Next(1, customers_number - 1);
                int bookId = randObj.Next(1, books_number - 1);
                db.Orders.Add(new Order { Ordername = orderName, Startdate = startdate, Finishdate = finishDate,
                                          Exemplairs = exemplairs, Totalsum = totalSum,
                                          CustomerId = customerId, BookId = bookId});
            }
            db.SaveChanges();
        }
    }
}
