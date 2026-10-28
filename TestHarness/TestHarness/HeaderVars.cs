using System;
using System.Collections.Generic;
using System.Text;

namespace TestHarness
{
    public class HeaderVars
    {
        public char HeaderChar { get; set; } = '+';
        public char HeaderSpaceChar { get; set; } = ' ';

        public ConsoleColor ColourBorder { get; set; } = ConsoleColor.Green;

        public ConsoleColor ColourText { get; set; } = ConsoleColor.Yellow;

        public int HeaderHeight { get; set; } = 1;
        public int HeaderHeightSpacing { get; set; } = 2;

        public int HeaderWidth { get; set; } = 3;
        public int HeaderWidthSpacing { get; set; } = 3;
    }
}
