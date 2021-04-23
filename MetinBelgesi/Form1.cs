using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MetinBelgesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // ******************************************************** Developed by caner24 ********************************************************************* //

        // Streamwriter ve Streamreader methodlarımınızın veriyi yazabilmesi ve çekebilmesi için string yol değişkenini tanımladık
        string yol;

        // Yazı yazdığımız durumlarda bir değişikliğin olduğunu bildirmek için formun text'inde uyarı amacıyla yazı yazmak için bu int sayac değişkeni oluşturuldu.
        int sayac;
        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // Dosyayı kaydederken bir hata olursa kullanıcıya bir mesaj vermesi için try-catch bloğu kulanıldı
            try
            {
                sayac = 0;
                StreamWriter yaz = new StreamWriter(yol);
                yaz.WriteLine(richTextBox1.Text);
                this.Text = "Form1";
                yaz.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen önce dosyanın yolunu bulmak için dosyayı farklı kaydeti seçiniz ");
                sayac = 1;
            }
        }

        private void yazıTipiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Kullanici yazı stili ve yazı kalınlığını değiştirmek isterse değişikliği yapabilmesi için FontDialog koyuldu.
            FontDialog dialog = new FontDialog();
            DialogResult cevap = dialog.ShowDialog();
            if (cevap == DialogResult.OK)
            {
                richTextBox1.Font = dialog.Font;
            }
        }
        private void açToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Richtextbox'ın textchanged özelliği ile eğer richtextbox'a birşey yazılırsa sayacın değerini değiştirerek formun textine yazı yazılması için sayaç değişkeni değeri atandı.

            if (sayac == 1)
            {
                // Eğer kullanıcı yazıyı kaydetmeden bir başka metin belgesini açmak istiyorsa kullanıcıya uyarı amaçlı bir metin yansıtıldı
                DialogResult cevap = MessageBox.Show("Yazılan Metni Kaydetmediniz,Kaydetmek için EVET ' e basınız ", "Kaydedilmeyen Metin", MessageBoxButtons.YesNo);
                if (cevap == DialogResult.Yes)
                {
                    // Kaydetme işlemleri için saveFileDialog1 nesnesi kullanıldı 
                    saveFileDialog1.Title = "Kayit Yerini Seçin";
                    saveFileDialog1.Filter = "Metin Belgesi (*.txt) | *.txt";
                    saveFileDialog1.FilterIndex = 2;
                    saveFileDialog1.ShowDialog();
                    yol = saveFileDialog1.FileName;
                    StreamWriter yaz = new StreamWriter(saveFileDialog1.FileName);
                    yaz.WriteLine(richTextBox1.Text);
                    yaz.Close();
                }
                // Eğer kullanıcı kaydetmek istemiyorsa sayac tekrardan 0 'a eşitlendi
                if (cevap == DialogResult.No)
                {
                    sayac = 0;
                }
            }
            // Richtextbox'ın textchanged özelliği ile eğer richtextbox'a birşey yazılırsa sayacın değerini değiştirerek formun textine yazı yazılması için sayaç değişkeni değeri atandı.
            if (sayac == 0)
            {
                // Kullanici açacağı dosyayı seçmesi için OpenFileDialog nesnesi tanımlandı
                OpenFileDialog metniac = new OpenFileDialog();
                metniac.Filter = "Metin Dosyası(*.txt) |*.txt";
                // Kullanici çoklu seçim yapmaması için çoklu seçim özelliği devre dışı bırakıldı
                metniac.Multiselect = false;
                if (metniac.ShowDialog() == DialogResult.OK)
                {
                    // Openfiledialog ile açtığımız dosyanın yolunu string nesnesine aktardık.
                    yol = metniac.FileName;
                    // Bir hata olduğunda bildirmek için try-catch bloğu kullanıldı
                    try
                    {
                        // Kullanici halihazirda olan bir txt metin belgesini açmak isterse StreamReader aracı ile ilgili işlemler yapıldı
                        StreamReader oku = new StreamReader(metniac.FileName);
                        richTextBox1.Text = oku.ReadToEnd();
                        oku.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Bu Dosyayı Okurken Bir Hata Oluştu");
                    }
                }
            }
        }

        private void yeniDosyaOluşturToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Yeni dosya oluştuğu zaman kaydetmemiz için gerekli olan yol değişkeni de yeni sayfaya göre hazırlanması için sıfırlandı.
            yol = "";
            if (sayac == 1)
            {
                // Eğer kullanıcı yazıyı kaydetmeden bir başka metin belgesini açmak istiyorsa kullanıcıya uyarı amaçlı bir metin yansıtıldı
                DialogResult cevap = MessageBox.Show("Yazılan Metni Kaydetmediniz,Kaydetmek için EVET ' e basınız ", "Kaydedilmeyen Metin", MessageBoxButtons.YesNo);
                if (cevap == DialogResult.Yes)
                {
                    // Kaydetme işlemleri için saveFileDialog1 nesnesi kullanıldı 
                    saveFileDialog1.Title = "Kayit Yerini Seçin";
                    saveFileDialog1.Filter = "Metin Belgesi (*.txt) | *.txt";
                    saveFileDialog1.FilterIndex = 2;
                    saveFileDialog1.ShowDialog();
                    yol = saveFileDialog1.FileName;
                    StreamWriter yaz = new StreamWriter(saveFileDialog1.FileName);
                    yaz.WriteLine(richTextBox1.Text);
                    yaz.Close();
                }
                if (cevap == DialogResult.No)
                {
                    sayac = 0;
                }
            }
            richTextBox1.Clear();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // Sayaç değişkeni richtextbox'da herhangi bir değişiklik olduğunda arttırılarak kullanıcıya görsel uyarı vermesi amacıyla '*' yazısı eklendi.
            sayac = 1;
            if (sayac == 1)
            {
                this.Text = "Form1" + "" + " * ";
            }
            // Sayaç değişkeni richtextbox'da herhangi bir değişiklik olmadığında kullanıcıya görsel uyarı vermesi amacıyla '*' yazısı kaldırılarak direk uygulama yazısı yazdırıldı.
            if (richTextBox1.Text == "")
            {
                sayac = 0;
                this.Text = "Form1";
            }
        }

        private void farklıYereKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Eğer kullanici açtiği metin belgesini farklı bir yere kaydetmek isterse savefiledialog nesnesi kullanılarak gerekli işlemler yapıldı
            try
            {
                // Sayac nesnesindeki değişiklikler yapıldı.Kullanıcı yazılarda yapmış olduğu değişikliği kaydetmeyi seçtiği için sıfırlandı.
                sayac = 0;
                saveFileDialog1.Title = "Kayit Yerinizi Seçin";
                saveFileDialog1.Filter = "Metin Dosyası(*.txt) | *.txt";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.ShowDialog();
                yol = saveFileDialog1.FileName;
                StreamWriter kaydet = new StreamWriter(saveFileDialog1.FileName);
                kaydet.WriteLine(richTextBox1.Text);
                kaydet.Close();
                this.Text = "Form1";
                MessageBox.Show("Kaynak Metne Yazdırıldı");

            }
            catch (Exception)
            {
                // Herhangi bir hata gelirse sayaç tekrardan 1 yapılarak kullanıcıya kaydedilmediğini göstermek için uyarı yapıldı
                sayac = 1;
                MessageBox.Show("Hata Oluştu Metin Yazdırılamadı");
            }
        }
    }
}
