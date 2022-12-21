using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULTRA2006_EDITION_KafeKeker
{
    static class Arrows
    {
        public static void DisplayArrow(int selectedIndex, int offset = 0)
        {
            Console.CursorVisible = false;
            if (selectedIndex == 0)
            {
                Console.SetCursorPosition(0, selectedIndex + offset);
                Console.WriteLine("->");
                Console.SetCursorPosition(0, selectedIndex + offset + 1);
                Console.WriteLine("  ");
            }
            else
            {
                Console.SetCursorPosition(0, selectedIndex + offset - 1);
                Console.WriteLine("  ");
                Console.SetCursorPosition(0, selectedIndex + offset);
                Console.WriteLine("->");
                Console.SetCursorPosition(0, selectedIndex + offset + 1);
                Console.WriteLine("  ");
            }
        }
    }
}
