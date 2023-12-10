using System;
using System.Threading;
using System.Windows.Forms;

namespace JnatarComFilósofos
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            FormJantarDosFilosofos formJantar = new FormJantarDosFilosofos();

            Thread jantarThread = new Thread(() => IniciarJantar(formJantar));
            jantarThread.Start();

            Application.Run(formJantar);
        }
        private static void IniciarJantar(FormJantarDosFilosofos formJantar)
        {
            while (true)
            {
                for (int i = 0; i < 5; i++)
                {
                    formJantar.FilosofoThread(i);
                }
            }
        }
    }
}
