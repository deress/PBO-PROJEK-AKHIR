using Microsoft.AspNetCore.Mvc;
using OkOkLa.Shop.Models;
using System.Data;
using System.Diagnostics;
using Npgsql;
using System.Transactions;
using System;

namespace OkOkLa.Shop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        Helper helper;
        // Object DataSet
        DataSet ds;
        // Object List of NpgsqlParameter
        NpgsqlParameter[] param;
        // Query ke Database
        string query;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            // Inisialisasi Object
            helper = new Helper();
            ds = new DataSet();
            param = new NpgsqlParameter[] { };
            query = "";
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult LoginAdmin(string username, string password)
        {
            ds = new DataSet();
            param = new NpgsqlParameter[] { };

            // Query Select
            query = "SELECT * FROM account_admins;";
            // Panggil DBConn untuk eksekusi Query
            helper.DBConn(ref ds, query, param);

            // List of User untuk menampung data user
            List<AccountModel> admins = new List<AccountModel>();
            // Mengambil value dari tabel di index 0
            var data = ds.Tables[0];

            // Perulangan untuk mengambil instance tiap baris dari tabel
            foreach (DataRow u in data.Rows)
            {
                // Membuat object User baru
                AccountModel admin = new AccountModel();
                // Mengisi id dan username dari object user dengan nilai dari database
                admin.Id = u.Field<Int32>(data.Columns[0]);
                admin.Username = u.Field<string>(data.Columns[1]);
                admin.Password = u.Field<string>(data.Columns[2]);
                // Menambahkan user ke users (List of User)
                admins.Add(admin);
            }

            ViewData["data"] = admins;

            bool berhasil = true;
            foreach (var admin in admins) 
            {
                if (admin.Username == username && admin.Password == password)
                {
                    Console.WriteLine("Login Berhasil");
                    break;
                } else 
                {
                    Console.WriteLine("Cek Kembali username dan password Anda!");
                    berhasil = false;
                }
            }

            switch (berhasil)
            {
                case true:
                    return RedirectToAction("History");
                case false:
                    return RedirectToAction("Login");
            }
        }

        public IActionResult History()
        {
            // Reinisialiasi ds dan param agar dataset dan parameter nya kembali null
            ds = new DataSet();
            param = new NpgsqlParameter[] { };

            // Query Select
            query = "SELECT * FROM transactions;";
            // Panggil DBConn untuk eksekusi Query
            helper.DBConn(ref ds, query, param);

            // List of Transaction untuk menampung data user
            List<TransactionModel> transactions = new List<TransactionModel>();
            // Mengambil value dari tabel di index 0
            var data = ds.Tables[0];

            // Perulangan untuk mengambil instance tiap baris dari tabel
            foreach (DataRow u in data.Rows)
            {
                // Membuat object User baru
                TransactionModel transaction = new TransactionModel();
                // Mengisi id dan username dari object user dengan nilai dari database
                transaction.Id = u.Field<Int32>(data.Columns[0]);
                transaction.Akungame_Id = u.Field<string>(data.Columns[1]);
                transaction.Game = u.Field<string>(data.Columns[2]);
                transaction.Game_Money = u.Field<string>(data.Columns[3]);
                transaction.Harga = u.Field<Int32>(data.Columns[4]);
                transaction.Metode_Pembayaran = u.Field<string>(data.Columns[5]);
                transaction.Email = u.Field<string>(data.Columns[6]);
                // Menambahkan user ke users (List of User)
                transactions.Add(transaction);
            }

            ViewData["data"] = transactions;
            return View();
        }

        public IActionResult Berhasil()
        {
            // Reinisialiasi ds dan param agar dataset dan parameter nya kembali null
            ds = new DataSet();
            param = new NpgsqlParameter[] { };

            // Query Select
            query = "SELECT * FROM transactions;";
            // Panggil DBConn untuk eksekusi Query
            helper.DBConn(ref ds, query, param);

            // List of Transaction untuk menampung data user
            List<TransactionModel> transactions = new List<TransactionModel>();
            // Mengambil value dari tabel di index 0
            var data = ds.Tables[0];

            // Perulangan untuk mengambil instance tiap baris dari tabel
            foreach (DataRow u in data.Rows)
            {
                // Membuat object User baru
                TransactionModel transaction = new TransactionModel();
                // Mengisi id dan username dari object user dengan nilai dari database
                transaction.Id = u.Field<Int32>(data.Columns[0]);
                transaction.Akungame_Id = u.Field<string>(data.Columns[1]);
                transaction.Game = u.Field<string>(data.Columns[2]);
                transaction.Game_Money = u.Field<string>(data.Columns[3]);
                transaction.Harga = u.Field<Int32>(data.Columns[4]);
                transaction.Metode_Pembayaran = u.Field<string>(data.Columns[5]);
                transaction.Email = u.Field<string>(data.Columns[6]);
                // Menambahkan user ke users (List of User)
                transactions.Add(transaction);
            }

            ViewData["data"] = transactions;
            return View();
        }

        public IActionResult MobileLegends()
        {
            return View();
        }

        public IActionResult Valorant()
        {
            return View();
        }

        public IActionResult FreeFire()
        {
            return View();
        }

        public IActionResult LoL()
        {
            return View();
        }

        public IActionResult GenshinImpact()
        {
            return View();
        }

        public IActionResult InsertMobileLegends(TransactionModel transaction)
        {
            History();
            var data = (List<TransactionModel>)ViewData["data"]!;
            int idCount = data.Count();

            int Harga = 0;
            switch (transaction.Game_Money)
            {
                case "12 Diamonds":
                    Harga = 3700;
                    break;
                case "44 Diamonds":
                    Harga = 12654;
                    break;
                case "85 Diamonds":
                    Harga = 24254;
                    break;
                case "170 Diamonds":
                    Harga = 48600;
                    break;
                case "316 Diamonds":
                    Harga = 84360;
                    break;
                case "408 Diamonds":
                    Harga = 115995;
                    break;
                case "568 Diamonds":
                    Harga = 158175;
                    break;
                case "2010 Diamonds":
                    Harga = 527250;
                    break;
                case "4830 Diamonds" :
                    Harga = 1265400;
                    break;
            }


            ds = new DataSet();
            param = new NpgsqlParameter[] {
                // Parameter untuk atribut
                new NpgsqlParameter("@id", idCount+1),
                new NpgsqlParameter("@akungame_id", transaction.Akungame_Id),
                new NpgsqlParameter("@game", "Mobile Legends"),
                new NpgsqlParameter("@game_money", transaction.Game_Money),
                new NpgsqlParameter("@harga", Harga),
                new NpgsqlParameter("@metode_pembayaran", transaction.Metode_Pembayaran),
                new NpgsqlParameter("@email", transaction.Email),
            };

            query = "INSERT INTO transactions VALUES (@id, @akungame_id, @game, @game_money, @harga, @metode_pembayaran, @email);";
            helper.DBConn(ref ds, query, param);

            return RedirectToAction("Berhasil");
        }

        public IActionResult InsertValorant(TransactionModel transaction)
        {
            History();
            var data = (List<TransactionModel>)ViewData["data"]!;
            int idCount = data.Count();

            int Harga = 0;
            switch (transaction.Game_Money)
            {
                case "125 Points":
                    Harga = 15000;
                    break;
                case "420 Points":
                    Harga = 50000;
                    break;
                case "700 Points":
                    Harga = 80000;
                    break;
                case "1375 Points":
                    Harga = 150000;
                    break;
                case "2400 Points":
                    Harga = 250000;
                    break;
                case "4000 Points":
                    Harga = 400000;
                    break;
                case "8150 Points":
                    Harga = 800000;
                    break;
            }


            ds = new DataSet();
            param = new NpgsqlParameter[] {
                // Parameter untuk atribut
                new NpgsqlParameter("@id", idCount+1),
                new NpgsqlParameter("@akungame_id", transaction.Akungame_Id),
                new NpgsqlParameter("@game", "VALORANT"),
                new NpgsqlParameter("@game_money", transaction.Game_Money),
                new NpgsqlParameter("@harga", Harga),
                new NpgsqlParameter("@metode_pembayaran", transaction.Metode_Pembayaran),
                new NpgsqlParameter("@email", transaction.Email),
            };

            query = "INSERT INTO transactions VALUES (@id, @akungame_id, @game, @game_money, @harga, @metode_pembayaran, @email);";
            helper.DBConn(ref ds, query, param);

            return RedirectToAction("Berhasil");
        }

        public IActionResult InsertFreeFire(TransactionModel transaction)
        {
            History();
            var data = (List<TransactionModel>)ViewData["data"]!;
            int idCount = data.Count();
           
            int Harga = 0;
            switch (transaction.Game_Money)
            {
                case "140 Diamonds":
                    Harga = 20000;
                    break;
                case "350 Diamonds":
                    Harga = 50000;
                    break;
                case "720 Diamonds":
                    Harga = 100000;
                    break;
                case "1450 Diamonds":
                    Harga = 200000;
                    break;
                case "2180 Diamonds":
                    Harga = 300000;
                    break;
                case "7290 Diamonds":
                    Harga = 1000000;
                    break;
            }


            ds = new DataSet();
            param = new NpgsqlParameter[] {
                // Parameter untuk atribut
                new NpgsqlParameter("@id", idCount+1),
                new NpgsqlParameter("@akungame_id", transaction.Akungame_Id),
                new NpgsqlParameter("@game", "Free Fire"),
                new NpgsqlParameter("@game_money", transaction.Game_Money),
                new NpgsqlParameter("@harga", Harga),
                new NpgsqlParameter("@metode_pembayaran", transaction.Metode_Pembayaran),
                new NpgsqlParameter("@email", transaction.Email),
            };

            query = "INSERT INTO transactions VALUES (@id, @akungame_id, @game, @game_money, @harga, @metode_pembayaran, @email);";
            helper.DBConn(ref ds, query, param);

            return RedirectToAction("Berhasil");
        }

        public IActionResult InsertLoL(TransactionModel transaction)
        {
            History();
            var data = (List<TransactionModel>)ViewData["data"]!;
            int idCount = data.Count();

            int Harga = 0;
            switch (transaction.Game_Money)
            {
                case "125 Wild Cores":
                    Harga = 15000;
                    break;
                case "667 Wild Cores":
                    Harga = 80000;
                    break;
                case "1250 Wild Cores":
                    Harga = 150000;
                    break;
                case "3333 Wild Cores":
                    Harga = 400000;
                    break;
                case "6667 Wild Cores":
                    Harga = 800000;
                    break;
            }


            ds = new DataSet();
            param = new NpgsqlParameter[] {
                // Parameter untuk atribut
                new NpgsqlParameter("@id", idCount+1),
                new NpgsqlParameter("@akungame_id", transaction.Akungame_Id),
                new NpgsqlParameter("@game", "Mobile Legends"),
                new NpgsqlParameter("@game_money", transaction.Game_Money),
                new NpgsqlParameter("@harga", Harga),
                new NpgsqlParameter("@metode_pembayaran", transaction.Metode_Pembayaran),
                new NpgsqlParameter("@email", transaction.Email),
            };

            query = "INSERT INTO transactions VALUES (@id, @akungame_id, @game, @game_money, @harga, @metode_pembayaran, @email);";
            helper.DBConn(ref ds, query, param);

            return RedirectToAction("Berhasil");
        }

        public IActionResult InsertGenshinImpact(TransactionModel transaction)
        {
            History();
            var data = (List<TransactionModel>)ViewData["data"]!;
            int idCount = data.Count();
           
            int Harga = 0;
            switch (transaction.Game_Money)
            {
                case "60 Genesis Crystalss": 
                    Harga = 16000;
                    break;
                case "300 Genesis Crystals":
                    Harga = 79000;
                    break;
                case "980 Genesis Crystals":
                    Harga = 249000;
                    break;
                case "1980 Genesis Crystals":
                    Harga = 479000;
                    break;
                case "3280 Genesis Crystals":
                    Harga = 799000;
                    break;
                case "6480 Genesis Crystals":
                    Harga = 159900;
                    break;
            }


            ds = new DataSet();
            param = new NpgsqlParameter[] {
                // Parameter untuk atribut
                new NpgsqlParameter("@id", idCount+1),
                new NpgsqlParameter("@akungame_id", transaction.Akungame_Id),
                new NpgsqlParameter("@game", "Mobile Legends"),
                new NpgsqlParameter("@game_money", transaction.Game_Money),
                new NpgsqlParameter("@harga", Harga),
                new NpgsqlParameter("@metode_pembayaran", transaction.Metode_Pembayaran),
                new NpgsqlParameter("@email", transaction.Email),
            };

            query = "INSERT INTO transactions VALUES (@id, @akungame_id, @game, @game_money, @harga, @metode_pembayaran, @email);";
            helper.DBConn(ref ds, query, param);

            return RedirectToAction("Berhasil");
        }

        public IActionResult List()
        {
            // Reinisialiasi ds dan param agar dataset dan parameter nya kembali null
            ds = new DataSet();
            param = new NpgsqlParameter[] { };

            // Query Select
            query = "SELECT * FROM users;";
            // Panggil DBConn untuk eksekusi Query
            helper.DBConn(ref ds, query, param);

            // List of User untuk menampung data user
            List<UserModel> users = new List<UserModel>();
            // Mengambil value dari tabel di index 0
            var data = ds.Tables[0];

            // Perulangan untuk mengambil instance tiap baris dari tabel
            foreach (DataRow u in data.Rows)
            {
                // Membuat object User baru
                UserModel user = new UserModel();
                // Mengisi id dan username dari object user dengan nilai dari database
                user.Id = u.Field<Int32>(data.Columns[0]);
                user.Username = u.Field<string>(data.Columns[1]);
                // Menambahkan user ke users (List of User)
                users.Add(user);
            }

            ViewData["data"] = users;
            return View();
        }

        public IActionResult Insert()
        {
            return View();
        }

        public IActionResult Edit(int Id, string Username)
        {
            return View();
        }

        public IActionResult InsertUser(UserModel user)
        {
            ds = new DataSet();
            param = new NpgsqlParameter[] {
            // Parameter untuk id dan username
            new NpgsqlParameter("@id", user.Id),
            new NpgsqlParameter("@username", user.Username),
        };

            query = "INSERT INTO users VALUES (@id, @username);";
            helper.DBConn(ref ds, query, param);

            return RedirectToAction("Index");
        }

        public IActionResult UpdateUser(UserModel user)
        {
            ds = new DataSet();
            param = new NpgsqlParameter[] {
            new NpgsqlParameter("@id", user.Id),
            new NpgsqlParameter("@username", user.Username),
        };

            query = "UPDATE users SET username = @username WHERE id = @id;";
            helper.DBConn(ref ds, query, param);

            return RedirectToAction("Index");
        }

        public IActionResult DeleteUser(int id)
        {
            ds = new DataSet();
            param = new NpgsqlParameter[] {
            new NpgsqlParameter("@id", id)
        };

            query = "DELETE FROM users WHERE id = @id;";
            helper.DBConn(ref ds, query, param);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    // Helper untuk koneksi ke DB
    class Helper
    {
        public void DBConn(ref DataSet ds, string query, NpgsqlParameter[] param)
        {
            // Data Source Name berisi credential dari database
            string dsn = "Host=localhost;Username=postgres;Password=idris9265;Database=coba;Port=5432";
            // Membuat koneksi ke db
            var conn = new NpgsqlConnection(dsn);
            // Command untuk eksekusi query
            var cmd = new NpgsqlCommand(query, conn);

            try
            {
                // Perulangan untuk menyisipkan nilai yang ada pada parameter ke query
                foreach (var p in param)
                {
                    cmd.Parameters.Add(p);
                }
                // Membuka koneksi ke database
                cmd.Connection!.Open();
                // Mengisi ds dengan data yang didapatkan dari database
                new NpgsqlDataAdapter(cmd).Fill(ds);
                Console.WriteLine("Query berhasil dieksekusi");
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                // Menutup koneksi ke database
                cmd.Connection!.Close();
            }

        }
    }
}