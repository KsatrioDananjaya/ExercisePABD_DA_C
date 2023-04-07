using System;
using System.Data;
using System.Data.SqlClient;

namespace Insert_and_Get_Data
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi Ke Database\n");
                    Console.WriteLine("Masukkan User ID :");
                    string user = Console.ReadLine();
                    Console.WriteLine("Masukkan Password :");
                    string pass = Console.ReadLine();
                    Console.WriteLine("Masukkan Database Tujuan :");
                    string db = Console.ReadLine();
                    Console.WriteLine("\nKetik K untuk Terhubung ke Database :");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = Danan-Nitro\\Danan; " +
                                    "initial catalog = {0};" +
                                    "User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Delete Data");
                                        Console.WriteLine("4. Keluar");
                                        Console.Write("\nEnter your choice (1-4): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("DATA BARANG\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);

                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT DATA BARANG\n");
                                                    Console.WriteLine("Masukkan Barang ID :");
                                                    string barang = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Nama Barang :");
                                                    string namabar = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Harga :");
                                                    string harga = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Deskripsi Barang : ");
                                                    string deskripbar = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Kategori :");
                                                    string kategor = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Penjual :");
                                                    string penjual = Console.ReadLine();
                                                    try
                                                    {
                                                        pr.insert(barang, namabar, harga, deskripbar, kategor, penjual, conn);
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki " + "akses untuk menambah data");
                                                    }

                                                }
                                                break;
                                            case '3':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INGING MENGHAPUS DATA BARANG? (Y/N) : ");
                                                    string input = Console.ReadLine();
                                                    if (input.ToLower() == "Y")
                                                    {
                                                        pr.delete(conn);
                                                    }
                                                    else
                                                    {
                                                        break;
                                                    }
                                                }
                                                break;
                                            case '4':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;

                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Menggunakan User Tersebut\n");
                    Console.ResetColor();
                }
            }
        }

        public void delete(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Delete from dbo.Barang(barang, nama barang, harga, deskripbar, kategor, penjual) = values(@bar,@nma,@harga,@deskrip,@kateg, @penjual)",con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@bar,@nma,@harga,@deskrip,@kateg, @penjual", "barang, nama barang, harga, deskripbar, kategor, penjual");
            cmd.ExecuteNonQuery();
        }

        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select * From dbo.Barang", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }

        }
        public void insert(string barang, string namabar, string harga, string deskripbar, string kategor, string penjual, SqlConnection con)
        {
            string str = "";
            str = "insert into DBO.BARANG (barang, nama barang, harga, deskripbar, kategor, penjual)"
                + "values(@bar,@nma,@harga,@deskrip,@kateg, @penjual)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("barang", barang));
            cmd.Parameters.Add(new SqlParameter("nama barang", namabar));
            cmd.Parameters.Add(new SqlParameter("namabarang", harga));
            cmd.Parameters.Add(new SqlParameter("deskripsibar", deskripbar));
            cmd.Parameters.Add(new SqlParameter("kategori", kategor));
            cmd.Parameters.Add(new SqlParameter("penjual", penjual)); 
            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Di Tambahkan");
        }
    }
}