using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace AutoAccepted
{
    public partial class frmMain : Form
    {
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData,
          int dwExtraInfo);

        public enum MouseEventFlags : uint
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010,
            WHEEL = 0x00000800,
            XDOWN = 0x00000080,
            XUP = 0x00000100
        }

        public frmMain()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // takes a snapshot of the screen
            Bitmap bmpScreenshot = Screenshot();

            // makes the background of the form a screenshot of the screen
            this.BackgroundImage = bmpScreenshot;

            // find the login button and check if it exists
            Point location;
            bool success = FindBitmap(Auto_Accepted.Properties.Resources.bmpLogin, bmpScreenshot, out location);

            // check if it found the bitmap
            if (success == false)
            {
                MessageBox.Show("Couldn't find the accept button");
                return;
            }

            // move the mouse to login button
            Cursor.Position = location;

            // click
            MouseClick();


            /*
             *     [x] Snapshot of the whole screen
             *     [x] Find the login button and check if it exists
             *     [x] Move the mouse to login button
             *     [ ] Click the login button
             */
        }

        /// <summary>
        /// Simulates a mouse click
        /// </summary>
        private void MouseClick()
        {
            mouse_event((uint)MouseEventFlags.LEFTDOWN, 0, 0, 0, 0);
            Thread.Sleep((new Random()).Next(20, 30));
            mouse_event((uint)MouseEventFlags.LEFTUP, 0, 0, 0, 0);
        }

       
        private Bitmap Screenshot()
        {
            
            Bitmap bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);


           
            Graphics g = Graphics.FromImage(bmpScreenshot);

            
            g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);

            
            return bmpScreenshot;
        }

       
        private bool FindBitmap(Bitmap bmpNeedle, Bitmap bmpHaystack, out Point location)
        {
            for(int outerX = 0; outerX < bmpHaystack.Width - bmpNeedle.Width; outerX++)
            {
                for (int outerY = 0; outerY < bmpHaystack.Height - bmpNeedle.Height; outerY++)
                {
                    for (int innerX = 0; innerX < bmpNeedle.Width; innerX++)
                    {
                        for (int innerY = 0; innerY < bmpNeedle.Height; innerY++)
                        {
                            Color cNeedle = bmpNeedle.GetPixel(innerX, innerY);
                            Color cHaystack = bmpHaystack.GetPixel(innerX + outerX, innerY + outerY);

                            if (cNeedle.R != cHaystack.R || cNeedle.G != cHaystack.G || cNeedle.B != cHaystack.B)
                            {
                                goto notFound;
                            }
                        }
                    }
                    location = new Point(outerX, outerY);
                    return true;
                notFound:
                    continue;
                }
            }
            location = Point.Empty;
            return false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
        private void Scan()
        {

            // takes a snapshot of the screen
            Bitmap bmpScreenshot = Screenshot();

            // makes the background of the form a screenshot of the screen
            this.BackgroundImage = bmpScreenshot;

            // find the login button and check if it exists
            Point location;
            bool success = FindBitmap(Auto_Accepted.Properties.Resources.bmpLogin, bmpScreenshot, out location);





            // check if it found the bitmap
            if (success == false)
            {
               
                return;
            }

            // move the mouse to login button
            Cursor.Position = location;

            // if found a image of button Mouse Click
            MouseClick();
            // after mouse click to button application quit
            Application.Exit();
            // after run this applications 

            /*
             *     [x] Snapshot of the whole screen
             *     [x] Find the login button and check if it exists
             *     [x] Move the mouse to login button
             *     [ ] Click the login button
             */
        }
     
        private void timer1_Tick(object sender, EventArgs e)
        {
            Scan();
           
        }
    }
}
