using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauMau.Classes.Utilitario
{
    public static class Code
    {
        private static Random randomval = new Random();
        public static string Generate(int size)
        {
            string toreturn = "";
            for(int i = 0; i < size; i++)
            {
                toreturn += randomval.Next(0, 9);
            }
            return toreturn;
        }
    }
}
