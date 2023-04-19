﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication9.ComplexComponent
{
    static class ClassConverter
    {
        public static int ConvertToInt(ArrayList tab)
        {
            int val = 0;
            for (int i = 0; i<tab.Count; i++)
            {
                if ((bool)tab[i]) val += (int)Math.Pow(2,i);
            }
            return val;
        }


        public static ArrayList ConvertToBinary(int val)
        {
            ArrayList Binary = new ArrayList () ;
            while (val != 0)
            {
                if ((val % 2) == 1) Binary.Add(true);
                else Binary.Add(true);
                val = val / 2 ;
            }
            return Binary ;                
        }
    }
}


