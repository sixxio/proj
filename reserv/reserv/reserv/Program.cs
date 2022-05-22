using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace reserv {
    internal class Program
    {
        public class User
        {
            [Key]
            public int UserId { get; set; }
            [Required]
            public string Login { get; set; }
            [Required]
            public string Password { get; set; }
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            public List<Rent> Rent { get; set; }
        }

        public class Car
        {
            [Key]
            public int CarId { get; set; }
            public string Type { get; set; }
            public int Seats { get; set; }
            [Required]
            public string Manufacturer { get; set; }
            [Required]
            public string Model { get; set; }
            [Required]
            public double PricePerDay { get; set; }
            [Required]
            public List<Rent> Rent { get; set; }
        }
        public class Rent
        {
            [Required]
            public int CarId { get; set; }
            [Required]
            public int UserId { get; set; }
            [Required]
            public DateTime DayStart { get; set; }
            [Required]
            public DateTime DayEnd { get; set; }

            public User User { get; set; }
            public Car Car { get; set; }
        }

        public class ApplicationContext : DbContext
        {
            public DbSet<User> Users { get; set; }
            public DbSet<Car> Cars { get; set; }
            public DbSet<Rent> Rents { get; set; }

            public ApplicationContext()
            {
                //Database.EnsureDeleted();
                Database.EnsureCreated();
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=taskforpdb;Trusted_Connection=True;");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

                modelBuilder.Entity<Rent>()
                    .HasOne(p => p.User)
                    .WithMany(t => t.Rent)
                    .HasForeignKey(p => p.UserId);

                modelBuilder.Entity<Rent>()
                    .HasOne(p => p.Car)
                    .WithMany(t => t.Rent)
                    .HasForeignKey(p => p.CarId);

                modelBuilder.Entity<User>()
                    .HasIndex(u => u.Login)
                    .IsUnique();

                modelBuilder.Entity<Rent>().HasKey(u => new { u.CarId, u.UserId, u.DayStart });
               // modelBuilder.Entity<User>().HasKey(u => new { u.UserId, u.Login });
            }
        }

        static void Main(string[] args)
        {
            User currentUser = new User();
            ConsoleKeyInfo str;
            string login;
            string pass;

            using (ApplicationContext db = new ApplicationContext())
            {
                // создаем два объекта User
                User user1 = new User { Login = "admin", Password = "admin", FirstName = "Leonardo", LastName = "Da Vinchi" };

                Car c1 = new Car { Type = "седан", Model = "Solaris", Manufacturer = "Hyundai", Seats = 4, PricePerDay = 2500 };
                Car c2 = new Car { Type = "седан", Model = "Logan", Manufacturer = "Renault", Seats = 4, PricePerDay = 3000 };
                Car c3 = new Car { Type = "седан", Model = "Rio", Manufacturer = "Kia", Seats = 4, PricePerDay = 2500 };
                Car c4 = new Car { Type = "седан", Model = "Rapid", Manufacturer = "Skoda", Seats = 4, PricePerDay = 3000 };

                Rent r1 = new Rent { CarId = 1, UserId = 1, DayStart = DateTime.Parse("20.05.2022"), DayEnd = DateTime.Parse("25.05.2022") };

                //добавляем их в бд
                db.Users.Add(user1);

                db.Cars.Add(c1);
                db.Cars.Add(c2);
                db.Cars.Add(c3);
                db.Cars.Add(c4);

                db.SaveChanges();

                db.Rents.Add(r1);
                db.SaveChanges();

                mainMenu();

                Console.ReadLine();


                void mainMenu()
                {

                    Console.Clear();
                    Console.WriteLine("Выберите пункт меню");
                    Console.WriteLine("1 - Регистрация\n2 - Авторизация");
                    str = Console.ReadKey();
                    switch (str.KeyChar)
                    {
                        case '1':
                            Console.Clear();
                            User newUser = new User();
                            string loginReg;

                            Console.WriteLine("Введите логин");
                            loginReg = Console.ReadLine();

                            if (db.Users.Where(a => a.Login == loginReg).FirstOrDefault() != null || loginReg.Length < 2)
                            {
                                Console.WriteLine("Пользователь с данным логином уже существует, введите другой.");
                                loginReg = Console.ReadLine();

                                while (db.Users.Where(a => a.Login == loginReg).FirstOrDefault() != null || loginReg.Length < 2)
                                {
                                    Console.WriteLine("Пользователь с данным логином уже существует, введите другой.");
                                    loginReg = Console.ReadLine();
                                }

                            }

                            newUser.Login = loginReg;
                            Console.WriteLine("Введите пароль");
                            newUser.Password = Console.ReadLine();
                            Console.WriteLine("Введите имя");
                            newUser.FirstName = Console.ReadLine();
                            Console.WriteLine("Введите фамилию");
                            newUser.LastName = Console.ReadLine();

                            db.Users.Add(newUser);
                            db.SaveChanges();

                            currentUser = newUser;

                            Console.WriteLine("Поздравляем, вы успешно зарегистрировались!\nНажмите любую клавишу, чтобы перейти в ЛК");
                            Console.ReadKey();
                            viewAccount();

                            break;
                        case '2':
                            loginAccount();
                            break;
                        default:
                            mainMenu();
                            break;
                    }
                }
                void loginAccount() {
                    Console.Clear();
                    Console.WriteLine("Введите логин");
                    login = Console.ReadLine();

                    Console.WriteLine("Введите пароль");
                    pass = Console.ReadLine();

                    currentUser = db.Users.Where(a => a.Login == login && a.Password == pass).FirstOrDefault();

                    if (currentUser != null)
                        viewAccount();
                    else {
                        Console.WriteLine("Неправильно введен логин и/или пароль.");
                        Console.WriteLine("1 - Повторить попытку\n2 - Вернуться в главное меню");

                        str = Console.ReadKey();
                        switch (str.KeyChar)
                        {
                            case '1':
                                loginAccount();
                                break;
                            case '2':
                                mainMenu();
                                break;
                            default:
                                loginAccount();
                                break;
                        }
                    }

                }

                void viewAccount()
                {
                    if (currentUser.Login == "admin")
                    {
                        Console.Clear();
                        Console.WriteLine("****************************************");
                        Console.WriteLine($"Здравствуйте, {currentUser.FirstName}!");
                        Console.WriteLine("****************************************");
                        Console.WriteLine("1 - Удалить пользователей\n2 - Изменить данные об автомобилях\n3 - Выйти из профиля");

                        str = Console.ReadKey();
                        switch (str.KeyChar)
                        {
                            case '1':
                                Console.Clear();
                                var userList = db.Users.ToList();
                                string userIDstring;
                                int userID;

                                Console.WriteLine("Список пользователей:");

                                if (!db.Users.ToList().Any())
                                    Console.WriteLine("Пока нет зарегистрированных пользователей");

                                foreach (var u in userList)
                                    Console.WriteLine($"{u.UserId}. {u.FirstName} {u.LastName}");


                                Console.WriteLine("Выберите пользователя, которого хотите удалить (0, чтобы выйти):");
                                userIDstring = Console.ReadLine();

                                if (userIDstring != "0")
                                {
                                    if (!Int32.TryParse(userIDstring, out userID) || !db.Users.Where(u => u.UserId == userID).Any() || db.Users.Where(u => u.Login == "admin").Any())
                                        while (!Int32.TryParse(userIDstring, out userID) || !db.Users.Where(u => u.UserId == userID).Any() || !db.Users.Where(u => u.Login == "admin").Any())
                                        {
                                            Console.WriteLine("Такого пользователя не существует или вы хотите удалить админа, повторите попытку");
                                            userIDstring = Console.ReadLine();
                                        }

                                    User userToDelete = db.Users.Where(u => u.UserId == userID).First();

                                    db.Users.Remove(userToDelete);
                                    db.SaveChanges();

                                    Console.WriteLine("Пользователь успешно удален. Нажмите любую клавишу, чтобы перейти в ЛК.");
                                    Console.ReadKey();
                                }

                                viewAccount();

                                break;
                            case '2':
                                changeCottageData();

                                break;
                            case '3':
                                mainMenu();
                                break;
                            default:
                                viewAccount();
                                break;
                        }

                    }
                    else
                    {

                        Console.Clear();
                        Console.WriteLine($"Здравствуйте, {currentUser.FirstName}!");
                        Console.WriteLine("--------------------");
                        Console.WriteLine("1 - Посмотреть доступные автомобили\n2 - Изменить данные профиля\n3 - Посмотреть текущие брони\n4 - Выйти из профиля");

                        str = Console.ReadKey();
                        switch (str.KeyChar)
                        {
                            case '1':
                                viewCars();
                                break;
                            case '2':
                                ConsoleKeyInfo key;
                                changePersonalData();

                                Console.WriteLine("\n1 - Продолжить изменения\n2 - Перейти в ЛК");
                                key = Console.ReadKey();

                                while (key.KeyChar == '1')
                                {
                                    switch (key.KeyChar)
                                    {
                                        case '1':
                                            changePersonalData();
                                            break;
                                        case '2':
                                            viewAccount();
                                            break;
                                    }

                                    Console.WriteLine("\n1 - Продолжить изменения\n2 - Перейти в ЛК");
                                    key = Console.ReadKey();
                                }

                                viewAccount();

                                break;
                            case '3':
                                Console.Clear();

                                var curReservs = db.Rents.Where(r => r.UserId == currentUser.UserId).Join(db.Cars, // второй набор
                                 p => p.CarId, // свойство-селектор объекта из первого набора
                                 c => c.CarId, // свойство-селектор объекта из второго набора
                                 (p, c) => new
                                 {
                                     Type = c.Type,
                                     Model = c.Model,
                                     Manufacturer = c.Manufacturer,
                                     Seats = c.Seats,
                                     PricePerDay = c.PricePerDay,
                                     DayStart = p.DayStart,
                                     DayEnd = p.DayEnd
                                 }); ;

                                foreach (var r in curReservs)
                                    Console.WriteLine($"{r.DayStart.ToString("dd/MM/yyyy")} - {r.DayEnd.ToString("dd/MM/yyyy")}: {r.Type} | {r.Model} | {r.Seats} человек | {((uint)(r.DayEnd - r.DayStart).Days) * r.PricePerDay} рублей за {((uint)(r.DayEnd - r.DayStart).Days)} суток");

                                if (!curReservs.Any())
                                    Console.WriteLine("У вас пока нет броней.");

                                Console.WriteLine("Нажмите любую клавишу, чтобы перейти в ЛК.");
                                Console.ReadKey();
                                viewAccount();

                                break;
                            case '4':
                                mainMenu();
                                break;
                            default:
                                viewAccount();
                                break;
                        }
                    }
                }

                void changeCottageData()
                {
                    Console.Clear();
                    Console.WriteLine("1 - Удалить автомобиль\n2 - Добавить автомобиль\n3 - Изменить данные об автомобиле\n4 - в ЛК");


                    str = Console.ReadKey();
                    switch (str.KeyChar)
                    {
                        case '1':
                            Console.Clear();
                            var cotList = db.Cars.ToList();
                            string cotIDstring;
                            int cotID;

                            Console.WriteLine("Список автомобилей:");

                            if (!db.Users.ToList().Any())
                                Console.WriteLine("Пока нет добавленных автомобилей");

                            foreach (var r in cotList)
                                Console.WriteLine($"{r.CarId} - Тип: {r.Type}, Модель: {r.Model}, Производитель: {r.Manufacturer}, Мест: {r.Seats}, Цена: {r.PricePerDay}");


                            Console.WriteLine("Выберите автомобиль, который хотите удалить (0, чтобы выйти):");
                            cotIDstring = Console.ReadLine();

                            if (cotIDstring != "0")
                            {
                                if (!Int32.TryParse(cotIDstring, out cotID) || !db.Cars.Where(u => u.CarId == cotID).Any())
                                    while (!Int32.TryParse(cotIDstring, out cotID) || !db.Cars.Where(u => u.CarId == cotID).Any())
                                    {
                                        Console.WriteLine("Такого автомобиля не существует, повторите попытку");
                                        cotIDstring = Console.ReadLine();
                                    }

                                Car cotToDelete = db.Cars.Where(u => u.CarId == cotID).First();

                                db.Cars.Remove(cotToDelete);
                                db.SaveChanges();

                                Console.WriteLine("Автомобиль успешно удален. Нажмите любую клавишу, чтобы перейти назад.");
                                Console.ReadKey();
                                changeCottageData();
                            }
                            viewAccount();
                            break;
                        case '2':
                            Console.Clear();
                            Car cotToCreate = new Car();

                            Console.WriteLine("Введите тип автомобиля:");
                            cotToCreate.Type = Console.ReadLine();

                            Console.WriteLine("Введите модель:");
                            cotToCreate.Model = Console.ReadLine();

                            Console.WriteLine("Введите производителя:");
                            cotToCreate.Manufacturer = Console.ReadLine();

                            int peopleIn2c;
                            string peopleIncString2;

                            Console.WriteLine("Введите вместимость:");
                            peopleIncString2 = Console.ReadLine();

                            if (!Int32.TryParse(peopleIncString2, out peopleIn2c) || peopleIn2c < 1 || peopleIn2c > 6)
                                while (!Int32.TryParse(peopleIncString2, out peopleIn2c) || peopleIn2c < 1 || peopleIn2c > 6)
                                {
                                    Console.WriteLine("Неверное кол-во человек, повторите попытку");
                                    peopleIncString2 = Console.ReadLine();
                                }

                            cotToCreate.Seats = peopleIn2c;

                            double pricePerDay2;
                            string pricePerDayString2;

                            Console.WriteLine("Введите цену за 1 день аренды:");
                            pricePerDayString2 = Console.ReadLine();

                            if (!Double.TryParse(pricePerDayString2, out pricePerDay2) || pricePerDay2 < 0)
                                while (!Double.TryParse(pricePerDayString2, out pricePerDay2) || pricePerDay2 < 0)
                                {
                                    Console.WriteLine("Неверная цена, повторите попытку");
                                    pricePerDayString2 = Console.ReadLine();
                                }

                            cotToCreate.PricePerDay = pricePerDay2;

                            db.Cars.Add(cotToCreate);
                            db.SaveChanges();

                            Console.WriteLine("Добавлен следующий автомобиль:");
                            Console.WriteLine($"{cotToCreate.CarId} - Тип: {cotToCreate.Type}, Модель: {cotToCreate.Model}, Производитель: {cotToCreate.Manufacturer}, Мест: {cotToCreate.Seats}, Цена: {cotToCreate.PricePerDay}");

                            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться.");
                            Console.ReadKey();
                            changeCottageData();

                            break;
                        case '3':
                            Console.Clear();
                            var cotList2 = db.Cars.ToList();
                            string cotIDstring2;
                            int cotID2;

                            Console.WriteLine("Список автомобилей:");

                            if (!db.Users.ToList().Any())
                                Console.WriteLine("Пока нет добавленных автомобилей");

                            foreach (var r in cotList2)
                                Console.WriteLine($"{r.CarId} - Тип: {r.Type}, Модель: {r.Model}, Производитель: {r.Manufacturer}, Мест: {r.Seats}, Цена: {r.PricePerDay}");

                            Console.WriteLine("Выберите автомобиль, который хотите изменить (0, чтобы выйти):");
                            cotIDstring2 = Console.ReadLine();

                            if (cotIDstring2 != "0")
                            {
                                if (!Int32.TryParse(cotIDstring2, out cotID2) || !db.Cars.Where(u => u.CarId == cotID2).Any())
                                    while (!Int32.TryParse(cotIDstring2, out cotID2) || !db.Cars.Where(u => u.CarId == cotID2).Any())
                                    {
                                        Console.WriteLine("Такого автомобиля не существует, повторите попытку");
                                        cotIDstring2 = Console.ReadLine();
                                    }

                                Car cotToChange = db.Cars.Where(u => u.CarId == cotID2).First();

                                Console.WriteLine("Введите характеристику, которую хотите изменить:\n1 - Тип автомобиля\n2 - Модель\n3 - Вместимость\n4 - Цена за 1 день проживания\n");

                                str = Console.ReadKey();
                                switch (str.KeyChar)
                                {
                                    case '1':
                                        Console.WriteLine("Введите тип автомобиля:");
                                        cotToChange.Type = Console.ReadLine();
                                        break;
                                    case '2':
                                        Console.WriteLine("Введите модель:");
                                        cotToChange.Model = Console.ReadLine();
                                        break;
                                    case '3':
                                        int peopleInc;
                                        string peopleIncString;

                                        Console.WriteLine("Введите вместимость:");
                                        peopleIncString = Console.ReadLine();

                                        if (!Int32.TryParse(peopleIncString, out peopleInc) || peopleInc < 1 || peopleInc > 6)
                                            while (!Int32.TryParse(peopleIncString, out peopleInc) || peopleInc < 1 || peopleInc > 6)
                                            {
                                                Console.WriteLine("Неверное кол-во человек, повторите попытку");
                                                peopleIncString = Console.ReadLine();
                                            }

                                        cotToChange.Seats = peopleInc;

                                        break;

                                    case '4':
                                        double pricePerDay;
                                        string pricePerDayString;

                                        Console.WriteLine("Введите цену за 1 день аренды:");
                                        pricePerDayString = Console.ReadLine();

                                        if (!Double.TryParse(pricePerDayString, out pricePerDay) || pricePerDay < 0)
                                            while (!Double.TryParse(pricePerDayString, out pricePerDay) || pricePerDay < 0)
                                            {
                                                Console.WriteLine("Неверная цена, повторите попытку");
                                                pricePerDayString = Console.ReadLine();
                                            }

                                        cotToChange.PricePerDay = pricePerDay;

                                        break;
                                    case '0':
                                        changeCottageData();
                                        break;
                                    default:
                                        break;
                                }

                                Console.WriteLine("\nИзменения применены.");
                                Console.WriteLine($"{cotToChange.CarId} - Тип: {cotToChange.Type}, Модель: {cotToChange.Model}, Производитель: {cotToChange.Manufacturer}, Мест: {cotToChange.Seats}, Цена: {cotToChange.PricePerDay}");

                                db.SaveChanges();

                                Console.WriteLine("\nНажмите любую клавишу, чтобы вернуться.");
                                Console.ReadKey();
                                changeCottageData();
                            }
                            changeCottageData();
                            break;
                        case '4':
                            viewAccount();
                            break;
                        default:
                            changeCottageData();
                            break;
                    }
                }

                void changePersonalData() {
                    Console.Clear();
                    var CurDBuser = db.Users.Where(a => a.Login == currentUser.Login).FirstOrDefault();
                    string changedValue;
                    Console.WriteLine("1 - Изменить логин\n2 - Изменить пароль\n3 - Изменить имя\n4 - Изменить фамилию\n5 - ЛК");
                    str = Console.ReadKey();
                    switch (str.KeyChar)
                    {
                        case '1':
                            Console.Clear();
                            Console.WriteLine("Введите новый логин");
                            changedValue = Console.ReadLine();

                            if (db.Users.Where(a => a.Login == changedValue).FirstOrDefault() != null)
                            {
                                Console.WriteLine("Пользователь с данным логином уже существует, введите другой.");
                                changedValue = Console.ReadLine();

                                while (db.Users.Where(a => a.Login == changedValue).FirstOrDefault() != null)
                                {
                                    Console.WriteLine("Пользователь с данным логином уже существует, введите другой.");
                                    changedValue = Console.ReadLine();
                                }
                            }
                            CurDBuser.Login = changedValue;
                            db.SaveChanges();
                            break;
                        case '2':
                            Console.Clear();
                            Console.WriteLine("Введите новый пароль");
                            changedValue = Console.ReadLine();
                            CurDBuser.Password = changedValue;
                            db.SaveChanges();
                            break;
                        case '3':
                            Console.Clear();
                            Console.WriteLine("Введите новое имя");
                            changedValue = Console.ReadLine();
                            CurDBuser.FirstName = changedValue;
                            db.SaveChanges();
                            break;
                        case '4':
                            Console.Clear();
                            Console.WriteLine("Введите новую фамилию");
                            changedValue = Console.ReadLine();
                            CurDBuser.LastName = changedValue;
                            db.SaveChanges();
                            break;
                        case '5':
                            viewAccount();
                            break;
                        default:
                            changePersonalData();
                            break;
                    }

                    Console.WriteLine("Данные были успешно изменены.");
                }

                void viewCars() {

                    Console.Clear();
                    string dateStartString;
                    DateTime dateStart;

                    string daysCountString = "1";
                    int daysCount = 1;

                    string selectedCarstring;
                    int selectedCottage;

                    Console.WriteLine("Выберите дату начала аренды (пример: 20.06.2022):");
                    dateStartString = Console.ReadLine();

                    if (!DateTime.TryParse(dateStartString, out dateStart) || dateStart <= DateTime.Now)
                        while (!DateTime.TryParse(dateStartString, out dateStart) || dateStart <= DateTime.Now)
                        {
                            Console.WriteLine("Неверно введена дата, попробуйте снова.");
                            dateStartString = Console.ReadLine();
                        }


                    Console.WriteLine("Введите кол-во дней:");
                    daysCountString = Console.ReadLine();

                    if (!Int32.TryParse(daysCountString, out daysCount) || daysCount <= 0 || daysCount >= 20)
                        while (!Int32.TryParse(daysCountString, out daysCount) || daysCount <= 0 || daysCount >= 20)
                        {
                            Console.WriteLine("Неверно введено кол-во дней, повторите попытку.");
                            daysCountString = Console.ReadLine();
                        }

                    var cots = db.Rents.Where(r => (dateStart >= r.DayStart && dateStart <= r.DayEnd) || (dateStart.AddDays(daysCount) >= r.DayStart && dateStart.AddDays(daysCount) <= r.DayEnd))
                        .Join(db.Cars, // второй набор
                         p => p.CarId, // свойство-селектор объекта из первого набора
                         c => c.CarId, // свойство-селектор объекта из второго набора
                         (p, c) => new {
                             CarId = c.CarId,
                             Type = c.Type,
                             Model = c.Model,
                             Manufacturer = c.Manufacturer,
                             Seats = c.Seats,
                             PricePerDay = c.PricePerDay
                         });
                    
                    var cotInfo = db.Cars.Select(c => new {
                        CarId = c.CarId,
                        Type = c.Type,
                        Model = c.Model,
                        Manufacturer = c.Manufacturer,
                        Seats = c.Seats,
                        PricePerDay = c.PricePerDay
                    }).Except(cots);

                    Console.WriteLine("Выберите автомобиль:");

                    foreach (var r in cotInfo)
                    {
                        Console.WriteLine($"{r.CarId} - Тип: {r.Type}, Модель: {r.Model}, Мест: {r.Seats}, Цена: {r.PricePerDay}");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Введите номер автомобиля:");

                    selectedCarstring = Console.ReadLine();

                    

                    if (!Int32.TryParse(selectedCarstring, out selectedCottage) || selectedCottage <= 0 || !cotInfo.Where(c => c.CarId == selectedCottage).Any())
                        while (!Int32.TryParse(selectedCarstring, out selectedCottage) || selectedCottage <= 0 || !cotInfo.Where(c => c.CarId == selectedCottage).Any())
                        {
                            Console.WriteLine("Неверно введен номер автомобиля попробуйте снова.");
                            selectedCarstring = Console.ReadLine();
                        }

                    var newReserv = new Rent();
                    newReserv.UserId = currentUser.UserId;
                    newReserv.CarId = selectedCottage;
                    newReserv.DayStart = dateStart;
                    newReserv.DayEnd = dateStart.AddDays(daysCount);

                    db.Rents.Add(newReserv);
                    db.SaveChanges();

                    Console.WriteLine("Бронь оформлена. Спасибо, что вы с нами!");
                    Console.WriteLine("\n1 - Продолжить просмотр автомобилей\n2 - Перейти в ЛК");

                    str = Console.ReadKey();
                    switch (str.KeyChar)
                    {
                        case '1':
                            viewCars();
                            break;
                        case '2':
                            viewAccount();
                            break;
                        default:
                            viewCars();
                            break;
                    }
                }
            }
        }
    }
}