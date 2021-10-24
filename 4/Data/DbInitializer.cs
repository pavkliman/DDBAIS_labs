using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using _4.Models;

namespace _4.Data
{
    public class DbInitializer
    {
        private static char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
        private static Random random = new Random();

        public static void Initialize(publishing_houseContext db)
        {
            db.Database.EnsureCreated();

            int rowCount;
            int rowIndex;

            int minStringLength;
            int maxStringLength;

            if (!db.Authors.Any())
            {
                string fio;

                rowCount = 700;
                rowIndex = 0;
                while (rowIndex < rowCount)
                {
                    minStringLength = 8;

                    maxStringLength = 16;
                    fio = GetString(minStringLength, maxStringLength);

                    db.Authors.Add(new Author { Fio = fio });
                    rowIndex++;
                }

                db.SaveChanges();
            }

            if (!db.Books.Any())
            {
                string name;
                int total;
                DateTime exitDate;
                decimal basecost;
                decimal finishcost;
                decimal salary;
                int authorId;

                rowCount = 25000;
                rowIndex = 0;
                while (rowIndex < rowCount)
                {
                    minStringLength = 8;

                    maxStringLength = 32;
                    name = GetString(minStringLength, maxStringLength);
                    total = random.Next(20, 500);
                    basecost = random.Next(200, 2000);
                    finishcost = random.Next(500, 5000);
                    salary = random.Next(200, 5000);
                    authorId = random.Next(1, 701);
                    do
                    {
                        exitDate = GetDateTime();
                    }
                    while (exitDate.Year < random.Next(2000, 2025));

                    db.Books.Add(new Book
                    {
                        Name = name,
                        Total = total,
                        Exitdate = exitDate,
                        Basecost = basecost,
                        Finishcost = finishcost,
                        Salary = salary,
                        AuthorId = authorId
                    });

                    rowIndex++;
                }
                db.SaveChanges();
            }

            if (!db.Contracts.Any())
            {
                DateTime date;
                DateTime deadline;
                int authorId;

                rowCount = 25000;
                rowIndex = 0;

                while (rowIndex < rowCount)
                {
                    do
                    {
                        date = GetDateTime();
                        deadline = GetDateTime();
                    }
                    while (date.Year < random.Next(2000, 2025) &&
                    deadline.Year < random.Next(2000, 2025));

                    authorId = random.Next(1, 701);

                    db.Contracts.Add(new Contract
                    {
                        Date = date,
                        Deadline = deadline,
                        AuthorId = authorId
                    });

                    rowIndex++;
                }

                db.SaveChanges();
            }

            if (!db.Customers.Any())
            {
                string customerName;

                rowCount = 700;
                rowIndex = 0;
                while (rowIndex < rowCount)
                {
                    minStringLength = 8;

                    maxStringLength = 16;
                    customerName = GetString(minStringLength, maxStringLength);
                    db.Customers.Add(new Customer { Customername = customerName });
                    rowIndex++;
                }
                db.SaveChanges();
            }

            if (!db.Orders.Any())
            {
                string orderName;
                DateTime startDate;
                DateTime finishDate;
                int exemplairs;
                decimal totalSum;
                int customerId;
                int bookId;

                rowCount = 20000;
                rowIndex = 0;
                while (rowIndex < rowCount)
                {
                    minStringLength = 8;

                    maxStringLength = 32;
                    orderName = GetString(minStringLength, maxStringLength);
                    do
                    {
                        startDate = GetDateTime();
                        finishDate = GetDateTime();
                    }
                    while (startDate.Year < random.Next(2000, 2025) && 
                    finishDate.Year < random.Next(2000, 2025));

                    exemplairs = random.Next(100, 2000);
                    totalSum = random.Next(10000, 2000000);
                    customerId = random.Next(1, 701);
                    bookId = random.Next(1, 25001);

                    db.Orders.Add(new Order
                    {
                        Ordername = orderName,
                        Startdate = startDate,
                        Finishdate = finishDate,
                        Exemplairs = exemplairs,
                        Totalsum = totalSum,
                        CustomerId = customerId,
                        BookId = bookId
                    });

                    rowIndex++;
                }

                db.SaveChanges();
            }
        }

        private static string GetString(int minStringLength, int maxStringLength)
        {
            string result = "";

            int stringLimit = minStringLength + random.Next(maxStringLength - minStringLength);

            int stringPosition;
            for (int i = 0; i < stringLimit; i++)
            {
                stringPosition = random.Next(letters.Length);

                result += letters[stringPosition];
            }

            return result;
        }

        private static DateTime GetDateTime()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;

            return start.AddDays(random.Next(range));
        }
    }
}
