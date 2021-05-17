using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe___Extended_Version
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        #region Classes
        public class MyButton : Button
        {
            private int outsideX;
            private int outsideY;
            private int insideX;
            private int insideY;
            public void setOutsideX(int x)
            {
                this.outsideX = x;
            }
            public void setOutsideY(int y)
            {
                this.outsideY = y;
            }
            public void setInsideX(int x)
            {
                this.insideX = x;
            }
            public void setInsideY(int y)
            {
                this.insideY = y;
            }
            public int getOutsideX()
            {
                return this.outsideX;
            }
            public int getOutsideY()
            {
                return this.outsideY;
            }
            public int getInsideX()
            {
                return this.insideX;
            }
            public int getInsideY()
            {
                return this.insideY;
            }
            public string MyToString()
            {
                return this.outsideX.ToString() + this.outsideY.ToString() + this.insideX.ToString() + this.insideY.ToString();
            }
        }
        #endregion
        #region Public Variabels
        public Graphics g;
        public Size s;
        List<MyButton> buttons = new List<MyButton>();
        public int turn = 0;
        public int[,,,] board = new int[3, 3, 3, 3];/// <rules>
                                                    /// -1 = empty
                                                    /// 0 = P1
                                                    /// 1 = P2
                                                    /// 2 = empty but the area is closed
                                                    /// </summary>
        public int[,] boards = new int[3, 3];/// <rules>
        /// -1 = empty
        /// 0 = P1
        /// 1 = P2
        /// </summary>
        Graphics gb;
        #endregion
        public MyButton getMyButton(int outx, int outy, int inx, int iny)
        {
            string id = outx.ToString() + outy.ToString() + inx.ToString() + iny.ToString();
            for (int place = 0; place < buttons.Count; place++)
                if (buttons[place].MyToString() == id)
                    return buttons[place];
            return null;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            g = this.CreateGraphics();
            s = new Size((this.Size.Width / 100) * 100, (this.Size.Height / 100) * 100);
            Point start;
            for (int outsideX = 0; outsideX < 3; outsideX++)
                for (int outsideY = 0; outsideY < 3; outsideY++)
                {
                    boards[outsideX, outsideY] = -1;
                    for (int insideX = 0; insideX < 3; insideX++)
                        for (int insideY = 0; insideY < 3; insideY++)
                        {
                            //Initializing the board
                            board[outsideX, outsideY, insideX, insideY] = -1;
                            //adding al of the controls
                            MyButton b = new MyButton();
                            b.Size = new Size(s.Width / 9 - s.Width / 100 * 2, s.Height / 9 - s.Height / 100 * 2);
                            b.setOutsideX(outsideX);
                            b.setOutsideY(outsideY);
                            b.setInsideX(insideX);
                            b.setInsideY(insideY);
                            //b.Text = outsideX.ToString() + outsideY.ToString() + insideX.ToString() + insideY.ToString();
                            b.TabStop = false;
                            b.Click += new EventHandler(MyButton_Click);
                            //b.Paint += new PaintEventHandler(MyButton_Paint);
                            start = new Point(outsideX * s.Width / 3, outsideY * s.Height / 3);
                            b.Location = new Point(start.X + insideX * s.Width / 9 + s.Width / 100, start.Y + insideY * s.Height / 9 + s.Height / 100);
                            buttons.Add(b);
                            this.Controls.Add(b);
                        }
                }
        }
        public void check()
        {
            for (int outsideX = 0; outsideX < 3; outsideX++)
                for (int outsideY = 0; outsideY < 3; outsideY++)
                {
                    if (boards[outsideX, outsideY] == -1)
                    {
                        bool ok = true;
                        for (int insideX = 0; insideX < 3; insideX++)
                            if (board[outsideX, outsideY, insideX, 0] != -1 && board[outsideX, outsideY, insideX, 0] == board[outsideX, outsideY, insideX, 1] && board[outsideX, outsideY, insideX, 0] == board[outsideX, outsideY, insideX, 2])
                            {
                                ok = false;
                                boards[outsideX, outsideY] = board[outsideX, outsideY, insideX, 0];
                            }
                        for (int insideY = 0; insideY < 3 && ok; insideY++)
                            if (board[outsideX, outsideY, 0, insideY] != -1 && board[outsideX, outsideY, 0, insideY] == board[outsideX, outsideY, 1, insideY] && board[outsideX, outsideY, 0, insideY] == board[outsideX, outsideY, 2, insideY])
                            {
                                ok = false;
                                boards[outsideX, outsideY] = board[outsideX, outsideY, 0, insideY];
                            }
                        if (board[outsideX, outsideY, 0, 0] != -1 && ok && board[outsideX, outsideY, 0, 0] == board[outsideX, outsideY, 1, 1] && board[outsideX, outsideY, 0, 0] == board[outsideX, outsideY, 2, 2])
                        {
                            ok = false;
                            boards[outsideX, outsideY] = board[outsideX, outsideY, 0, 0];
                        }
                        else if (board[outsideX, outsideY, 2, 0] != -1 && ok && board[outsideX, outsideY, 0, 2] == board[outsideX, outsideY, 1, 1] && board[outsideX, outsideY, 0, 2] == board[outsideX, outsideY, 2, 0])
                        {
                            ok = false;
                            boards[outsideX, outsideY] = board[outsideX, outsideY, 2, 0];
                        }
                        if (!ok)
                        {
                            for (int x = 0; x < 3; x++)
                                for (int y = 0; y < 3; y++)
                                    if (board[outsideX, outsideY, x, y] == -1)
                                    {
                                        board[outsideX, outsideY, x, y] = 2;
                                        getMyButton(outsideX, outsideY, x, y).Enabled = false;
                                        getMyButton(outsideX, outsideY, x, y).BackColor = Color.DarkGray;
                                    }
                            Bitmap bit = new Bitmap(s.Width / 3, s.Height / 3);
                            gb = Graphics.FromImage(bit);
                            if (boards[outsideX, outsideY] == 0)
                            {
                                gb.DrawLine(new Pen(new SolidBrush(Color.Red), 5), new Point(0, 0), new Point(bit.Size.Width, bit.Size.Height));
                                gb.DrawLine(new Pen(new SolidBrush(Color.Red), 5), new Point(bit.Size.Width, 0), new Point(0, bit.Size.Height));
                            }
                            else
                                gb.DrawEllipse(new Pen(new SolidBrush(Color.Red), 5), new Rectangle(new Point(s.Width / 100, s.Height / 100), new Size(bit.Size.Width - s.Width / 100 * 2 - 1, bit.Size.Height - s.Height / 100 * 2 - 1)));
                            PictureBox pic = new PictureBox();
                            pic.Size = bit.Size;
                            pic.BackgroundImage = bit;
                            pic.Location = new Point(s.Width / 3 * outsideX, s.Height / 3 * outsideY);
                            this.Controls.Add(pic);
                            //pic.BringToFront();
                        }
                    }
                }
        }//check mini end
        public void check2()//check real end
        {
            bool ok = true;
            for (int insideX = 0; insideX < 3; insideX++)
                if (boards[insideX, 0] != -1 && boards[insideX, 0] == boards[insideX, 1] && boards[insideX, 0] == boards[insideX, 2])
                    ok = false;
            for (int insideY = 0; insideY < 3 && ok; insideY++)
                if (boards[0, insideY] != -1 && boards[0, insideY] == boards[1, insideY] && boards[0, insideY] == boards[2, insideY])
                    ok = false;
            if (boards[0, 0] != -1 && ok && boards[0, 0] == boards[1, 1] && boards[0, 0] == boards[2, 2])
                ok = false;
            else if (boards[2, 0] != -1 && ok && boards[0, 2] == boards[1, 1] && boards[0, 2] == boards[2, 0])
                ok = false;
            if (!ok)
            {
                for (int place = 0; place < buttons.Count; place++)
                    buttons[place].Enabled = false;
                if (turn % 2 == 0)
                    MessageBox.Show("X Player Has Won");
                else
                    MessageBox.Show("O Player Has Won");
            }
        }
        public void changeBackgroung(MyButton b)
        {
            Bitmap bit = new Bitmap(b.Size.Width, b.Size.Height);
            gb = Graphics.FromImage(bit);
            if (b.Enabled == false)
                gb.FillRectangle(new SolidBrush(Color.DarkGray), new Rectangle(new Point(0, 0), b.Size));
            else
                gb.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(new Point(0, 0), b.Size));
            b.BackgroundImage = bit;
        }
        private void MyButton_Click(object sender, EventArgs e)
        {
            MyButton b = sender as MyButton;
            board[b.getOutsideX(), b.getOutsideY(), b.getInsideX(), b.getInsideY()] = turn % 2;
            #region Draw
            Bitmap bit = new Bitmap(b.Size.Width, b.Size.Height);
            gb = Graphics.FromImage(bit);
            //gb.FillRectangle(new SolidBrush(Color.DarkGray), new Rectangle(new Point(0, 0), b.Size));
            b.BackColor = Color.DarkGray;
            if (turn % 2 == 0)
            {
                gb.DrawLine(new Pen(new SolidBrush(Color.Black), 5), new Point(0, 0), new Point(b.Size.Width, b.Size.Height));
                gb.DrawLine(new Pen(new SolidBrush(Color.Black), 5), new Point(b.Size.Width, 0), new Point(0, b.Size.Height));
            }
            else
                gb.DrawEllipse(new Pen(new SolidBrush(Color.Black), 5), new Rectangle(new Point(s.Width / 100, s.Height / 100), new Size(b.Size.Width - s.Width / 100 * 2 - 1, b.Size.Height - s.Height / 100 * 2 - 1)));
            b.BackgroundImage = bit;
            #endregion
            check();
            b.Enabled = false;
            string whereTo = b.MyToString().Substring(2);//gets the last two digits of "location"
            int x = int.Parse(whereTo[0].ToString()), y = int.Parse(whereTo[1].ToString());
            if (boards[x, y] != -1)
                for (int place = 0; place < buttons.Count; place++)
                {
                    if (buttons[place].getOutsideX() == x && buttons[place].getOutsideY() == y)
                    {
                        buttons[place].Enabled = false;
                        buttons[place].BackColor = Color.DarkGray;
                    }
                    else if (board[int.Parse(buttons[place].MyToString()[0].ToString()), int.Parse(buttons[place].MyToString()[1].ToString()), int.Parse(buttons[place].MyToString()[2].ToString()), int.Parse(buttons[place].MyToString()[3].ToString())] == -1)
                    {
                        buttons[place].Enabled = true;
                        buttons[place].BackColor = SystemColors.Control;
                    }
                }
            else
            {
                for (int place = 0; place < buttons.Count; place++)
                {
                    int value = board[buttons[place].getOutsideX(), buttons[place].getOutsideY(), buttons[place].getInsideX(), buttons[place].getInsideY()];
                    if ((value == -1 || value == 2) && boards[buttons[place].getOutsideX(), buttons[place].getOutsideY()] == -1)
                    {
                        if (!buttons[place].MyToString().StartsWith(whereTo))
                        {
                            buttons[place].Enabled = false;
                            buttons[place].BackColor = Color.DarkGray;
                        }
                        else
                        {
                            buttons[place].Enabled = true;
                            buttons[place].BackColor = SystemColors.Control;
                        }
                    }
                }
            }
            check2();
            turn += 1;
        }
        private void MyButton_Paint(object sender, PaintEventArgs e)
        {
            /*MyButton b = sender as MyButton;
            if (board[b.getOutsideX(), b.getOutsideY(), b.getInsideX(), b.getInsideY()] != -1)
                draw(b);*/
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //g.DrawRectangle(new Pen(new SolidBrush(Color.DarkRed), 6), new Rectangle(new Point(0, 0), new Size(300, 300)));
            //g.DrawEllipse(new Pen(new SolidBrush(Color.DarkRed), 6), new Rectangle(new Point(0, 0), new Size(300, 300)));
            //drawing the big grid
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 4), new Point(0, s.Width / 3), new Point(this.Size.Width, s.Width / 3));
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 4), new Point(0, s.Width / 3 * 2), new Point(this.Size.Width, s.Width / 3 * 2));
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 4), new Point(s.Height / 3, 0), new Point(s.Height / 3, this.Size.Height));
            g.DrawLine(new Pen(new SolidBrush(Color.Black), 4), new Point(s.Height / 3 * 2, 0), new Point(s.Height / 3 * 2, this.Size.Height));
            //drwaing all of the samll grids
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                {
                    Point start = new Point(x * s.Width / 3, y * s.Height / 3);
                    g.DrawLine(new Pen(new SolidBrush(Color.Black), 1), new Point(start.X + s.Width / 9, start.Y + s.Height / 100), new Point(start.X + s.Width / 9, start.Y + s.Height / 3 - s.Height / 100));
                    g.DrawLine(new Pen(new SolidBrush(Color.Black), 1), new Point(start.X + s.Width / 9 * 2, start.Y + s.Height / 100), new Point(start.X + s.Width / 9 * 2, start.Y + s.Height / 3 - s.Height / 100));
                    g.DrawLine(new Pen(new SolidBrush(Color.Black), 1), new Point(start.X + s.Width / 100, start.Y + s.Height / 9), new Point(start.X + s.Width / 3 - s.Width / 100, start.Y + s.Height / 9));
                    g.DrawLine(new Pen(new SolidBrush(Color.Black), 1), new Point(start.X + s.Width / 100, start.Y + s.Height / 9 * 2), new Point(start.X + s.Width / 3 - s.Width / 100, start.Y + s.Height / 9 * 2));
                }
        }
    }
}