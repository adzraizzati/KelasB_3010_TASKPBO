using System;
using System.Collections.Generic;

namespace SistemPerpustakaan
{
    //===Kelas Dasar===
    public class Item
    {
        public string Judul { get; set; }
        public int Tahun { get; set; }

        public Item(string judul, int tahun)
        {
            Judul = judul;
            Tahun = tahun;
        }

        //virtual agar bisa di override oleh subclass
        public virtual void Deskripsi()
        {
            Console.WriteLine("Item:" + Judul + " (" + Tahun + ")");
        }

        public void InfoItem()
        {
            Console.WriteLine("Judul: " + Judul + ", Tahun: " + Tahun);
        }
    }

    //===Buku dan turunannya===
    public class Buku : Item
    {
        public string Penulis { get; set; }

        public Buku(string judul, int tahun, string penulis) : base(judul, tahun)
        {
            Penulis = penulis;
        }

        public void CekPenulis()
        {
            Console.WriteLine("Penulis buku \"" + Judul + "\" adalah " + Penulis);
        }

        public override void Deskripsi()
        {
            Console.WriteLine("Buku: " + Judul + " (" + Tahun + ") oleh " + Penulis);
        }
    }

    public class Novel : Buku
    {
        public Novel(string judul, int tahun, string penulis) : base(judul, tahun, penulis) { }

        public void BacaSinopsis()
        {
            Console.WriteLine("Sinopsis novel \"" + Judul + "\": (ringkasan cerita novel ini)");
        }

        public override void Deskripsi()
        {
            Console.WriteLine("Novel: " + Judul + " (" + Tahun + ") oleh " + Penulis);
        }
    }

    public class Komik : Buku
    {
        public Komik(string judul, int tahun, string penulis) : base(judul, tahun, penulis) { }

        public void TampilkanIlustrasi()
        {
            Console.WriteLine("Menampilkan ilustrasi dari komik \"" + Judul + "\"");
        }

        public override void Deskripsi()
        {
            Console.WriteLine("Komik: " + Judul + " (" + Tahun + ") oleh " + Penulis);
        }
    }

    //===Majalah dan turunannya===
    public class Majalah : Item
    {
        public int Edisi { get; set; }

        public Majalah(string judul, int tahun, int edisi) : base(judul, tahun)
        {
            Edisi = edisi;
        }

        public void InfoEdisi()
        {
            Console.WriteLine("Majalah \"" + Judul + "\" edisi ke-" + Edisi);
        }

        public override void Deskripsi()
        {
            Console.WriteLine("Majalah: " + Judul + " (" + Tahun + "), Edisi " + Edisi);
        }
    }

    public class MajalahAnak : Majalah
    {
        public MajalahAnak(string judul, int tahun, int edisi) : base(judul, tahun, edisi) { }

        public void KategoriAnak()
        {
            Console.WriteLine("Kategori: majalah untuk anak-anak");
        }

        public override void Deskripsi()
        {
            Console.WriteLine("Majalah Anak: " + Judul + " (" + Tahun + "), Edisi " + Edisi);
        }
    }

    public class MajalahTeknologi : Majalah
    {
        public MajalahTeknologi(string judul, int tahun, int edisi) : base(judul, tahun, edisi) { }

        public void TopikTeknologi()
        {
            Console.WriteLine("Topik: gadget, software, dan perkembangan teknologi terbaru");
        }

        public override void Deskripsi()
        {
            Console.WriteLine("Majalah Teknologi: " + Judul + " (" + Tahun + "), Edisi " + Edisi);
        }
    }

    //===Perpustakaan (agregasi: memiliki koleksi item)===
    public class Perpustakaan
    {
        private List<Item> daftarItem = new List<Item>();

        public void TambahItem(Item item)
        {
            daftarItem.Add(item);
            Console.WriteLine("Item \"" + item.Judul + "\" ditambahkan ke perpustakaan.");
        }

        public void DaftarItem()
        {
            Console.WriteLine("=== Daftar Item Perpustakaan ===");
            int no = 1;
            foreach (Item item in daftarItem)
            {
                Console.WriteLine(no + ". ");
                item.Deskripsi(); //polymorphism
                no++;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //buat objek perpustakaan
            Perpustakaan perpustakaan = new Perpustakaan();

            //buat beberapa objek item
            Novel novel = new Novel("Laskar Pelangi", 2005, "Andrea Hirata");
            Komik komik = new Komik("Doraemon Vol. 1", 1970, "Fujiko F. Fujio");
            MajalahAnak majalahAnak = new MajalahAnak("Bobo", 2023, 15);
            MajalahTeknologi majalahTek = new MajalahTeknologi("PC Magazine", 2024, 8);

            //tambahkan ke perpustakaan
            perpustakaan.TambahItem(novel);
            perpustakaan.TambahItem(komik);
            perpustakaan.TambahItem(majalahAnak);
            perpustakaan.TambahItem(majalahTek);
            Console.WriteLine();

            //tampilkan semua data
            perpustakaan.DaftarItem();
            Console.WriteLine();

            //demonstrasi polymorphism
            Console.WriteLine("=== Demonstrasi Polymorphism ===");
            List<Item> semuaItem = new List<Item> { novel, komik, majalahAnak, majalahTek };
            foreach (Item item in semuaItem)
            {
                item.Deskripsi();
            }
            Console.WriteLine();

            //panggil method khusus
            Console.WriteLine("=== Method Khusus ===");
            novel.CekPenulis();
            novel.BacaSinopsis();
            komik.TampilkanIlustrasi();
            majalahAnak.InfoEdisi();
            majalahTek.TopikTeknologi();
            Console.WriteLine();

            //===kasus soal no 1===
            Console.WriteLine("=== Soal 1: Deskripsi() pada Novel dan Majalah ===");
            novel.Deskripsi();
            Majalah majalah = new Majalah("National Geographic", 2022, 3);
            majalah.Deskripsi();
            Console.WriteLine();

            //===soal no 2===
            Console.WriteLine("=== Soal 2: BacaSinopsis() pada Novel ===");
            novel.BacaSinopsis();
            Console.WriteLine();

            //===soal no 3===
            Console.WriteLine("=== Soal 3: Informasi lengkap Novel ===");
            novel.InfoItem();
            Console.WriteLine("Judul   : " + novel.Judul);
            Console.WriteLine("Tahun   : " + novel.Tahun);
            Console.WriteLine("Penulis : " + novel.Penulis);
            novel.Deskripsi();
            Console.WriteLine();

            //===soal no 4===
            Console.WriteLine("=== Soal 4: TopikTeknologi() pada MajalahTeknologi ===");
            majalahTek.TopikTeknologi();
            Console.WriteLine();

            //===soal no 5===
            Console.WriteLine("=== Soal 5: Variabel bertipe Item berisi Komik ===");
            Item itemKomik = new Komik("One Piece Vol. 1", 1997, "Eiichiro Oda");
            itemKomik.Deskripsi();

            ((Komik)itemKomik).TampilkanIlustrasi(); // bisa setelah di-cast ke Komik

            Console.ReadKey();
        }
    }
}