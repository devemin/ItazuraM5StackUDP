using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ItazuraM5
{
    public partial class Form1 : Form
    {
        // ソケット生成
        private System.Net.Sockets.UdpClient objSck = new System.Net.Sockets.UdpClient(2002);
        bool imageflag = false;

        public Form1()
        {
            InitializeComponent();

            // フォームのロードイベントメソッドを設定
            this.Load += Form1_Load;
            // フォームのキーダウンイベントメソッドを設定
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);

            //objSck.Connect("127.0.0.1", 2002);

            this.BackgroundImage = null;

            Debug.WriteLine("start.");
        }


        // フォームのロードイベントメソッド
        private void Form1_Load(object sender, EventArgs e)
        {
            // 透過色に背景色を設定
            this.TransparencyKey = this.BackColor;
        }

        // フォームのキーダウンイベントメソッド
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // フォームを移動させるピクセル数
            int movePixel = 10;
            if (e.Shift)
            {
                movePixel = 1;
            }
            switch (e.KeyCode)
            {
                // 上移動
                case Keys.Up:
                    this.Top -= movePixel;
                    break;
                // 下移動
                case Keys.Down:
                    this.Top += movePixel;
                    break;
                // 左移動
                case Keys.Left:
                    this.Left -= movePixel;
                    break;
                // 右移動
                case Keys.Right:
                    this.Left += movePixel;
                    break;
            }
        }



        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // ソケットクローズ
            objSck.Close();
        }



        private async void timer1_Tick_1(object sender, EventArgs e)
        {
            // ソケット受信
            if (objSck.Available > 0)
            {
                System.Net.IPEndPoint ipAny =
                    new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);
                Byte[] dat = objSck.Receive(ref ipAny);
                //Debug.WriteLine("got.");

                uint tmp = BitConverter.ToUInt32(dat, 0);

                if (tmp == 1)
                {
                    Debug.WriteLine("dat == 1");

                    imageflag = !imageflag;

                    if(imageflag)
                    {
                        this.BackgroundImage = ItazuraM5.Properties.Resources._862711;


                        System.Media.SystemSounds.Exclamation.Play();
                        await Task.Delay(10);
                        System.Media.SystemSounds.Hand.Play();
                        await Task.Delay(10);

                        System.Media.SystemSounds.Exclamation.Play();
                        await Task.Delay(10);
                        System.Media.SystemSounds.Hand.Play();
                        await Task.Delay(10);

                        //オーディオリソースを取り出す
                        System.IO.Stream strm = Properties.Resources.aaaa;
                        //同期再生する
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer(strm);
                        player.Play();

                        this.Left += 50;
                        await Task.Delay(100);
                        this.Left += 50;
                        await Task.Delay(100);
                        this.Top -= 50;
                        await Task.Delay(100);
                        this.Top -= 50;
                        await Task.Delay(100);
                        this.Left -= 50;
                        await Task.Delay(100);
                        this.Left -= 50;
                        await Task.Delay(100);
                        this.Top += 50;
                        await Task.Delay(100);
                        this.Top += 50;
                        await Task.Delay(100);

                    }
                    else
                    {
                        this.BackgroundImage = null;
                    }



                }
                else if (tmp == 2)
                {
                    Debug.WriteLine("dat == 2");

                    this.Left -= 50;
                }
                else if (tmp == 3)
                {
                    Debug.WriteLine("dat == 3");

                    this.Left += 50;
                }
            }
            //Debug.WriteLine("no data.");
        }
    }
}
