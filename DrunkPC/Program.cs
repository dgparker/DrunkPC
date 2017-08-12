using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Media;

namespace DrunkPC
{
    class Program
    {
        public static Random _random = new Random();

        public static int _startupDelaySeconds = 500;
        public static int _totalDurationSeconds = 500;

        static void Main(string[] args)
        {
            Console.WriteLine("DrunkPC prank application by: Dylan");

            // Check for command line arguments and assign the new values
            if(args.Length >= 2)
            {
                _startupDelaySeconds = Convert.ToInt32(args[0]);
                _totalDurationSeconds = Convert.ToInt32(args[1]);
            }

            Thread drunkMouseThread = new Thread(new ThreadStart(DrunkMouseThread));
            Thread drunkKeyboardThread = new Thread(new ThreadStart(DrunkKeyboardThread));
            Thread drunkSoundThread = new Thread(new ThreadStart(DrunkSoundThread));
            Thread drunkPopupThread = new Thread(new ThreadStart(DrunkPopupThread));

            DateTime future = DateTime.Now.AddSeconds(_startupDelaySeconds);
            Console.WriteLine("Waiting 10 seconds before starting");
            while( future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }


            drunkMouseThread.Start();
            drunkKeyboardThread.Start();
            drunkSoundThread.Start();
            drunkPopupThread.Start();

            future = DateTime.Now.AddSeconds(_totalDurationSeconds);
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }
            Console.WriteLine("Terminating all threads");

            drunkMouseThread.Abort();
            drunkKeyboardThread.Abort();
            drunkSoundThread.Abort();
            drunkPopupThread.Abort();


        }
        // this thread will randomly affect the mouse movements
        public static void DrunkMouseThread()
        {
            Console.WriteLine("DrunkMouseThread started");
            int moveX = 0;
            int moveY = 0;

            while (true)
            {
                moveX = _random.Next(20) - 10;
                moveY = _random.Next(20) - 10;

                Cursor.Position = new System.Drawing.Point(Cursor.Position.X + moveX, Cursor.Position.Y + moveY);
                Thread.Sleep(50);

            }
        }

        public static void DrunkKeyboardThread()
        {
            Console.WriteLine("DrunkKeyboardThread started");
            while (true)
            {
                if (_random.Next(100) > 80)
                {
                    char key = (char)(_random.Next(25) + 65);

                    if (_random.Next(2) == 0)
                    {
                        key = Char.ToLower(key);
                    }

                    SendKeys.SendWait(key.ToString());

                    Thread.Sleep(_random.Next(500));
                }
            }
        }

        public static void DrunkSoundThread()
        {
            Console.WriteLine("DrunkSoundThread started");

            while (true)
            {
                if( _random.Next(100) > 80 )
                {
                    switch ( _random.Next(5) )
                    {
                        case 0:
                            SystemSounds.Asterisk.Play();
                            break;
                        case 1:
                            SystemSounds.Beep.Play();
                            break;
                        case 2:
                            SystemSounds.Exclamation.Play();
                            break;
                        case 3:
                            SystemSounds.Hand.Play();
                            break;
                        case 4:
                            SystemSounds.Question.Play();
                            break;
                    }

                }
                
                Thread.Sleep(500);

            }
        }

        public static void DrunkPopupThread()
        {
            Console.WriteLine("DrunkPopupThread started");
            while (true)
            {

                if (_random.Next(100) > 90)
                {
                    switch (_random.Next(2))
                    {
                        case 0:
                            MessageBox.Show("Internet explorer has stopped working", "Internet Explorer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case 2:
                            MessageBox.Show("Your System is running low on resources", "Microsoft Windows", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                   

                }
                
                Thread.Sleep(500);

            }
        }


    }
}
