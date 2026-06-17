using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Feladat214
{
    internal class KorongÁllapot : AbsztraktÁllapot
    {
        private const bool PIROS = false;
        private const bool KEK = true;

        private bool[][] CEL_ALLAPOT = {
            new[] { PIROS, PIROS, PIROS, PIROS },
            new[] { PIROS, KEK,   KEK,   PIROS },
            new[] { PIROS, KEK,   KEK,   PIROS },
            new[] { PIROS, PIROS, PIROS, PIROS }
        };

        private bool[][] BOARD;

        public KorongÁllapot() {
            BOARD = new bool[][] {
                new[] { PIROS, PIROS, PIROS, PIROS },
                new[] { PIROS, PIROS, PIROS, PIROS },
                new[] { PIROS, PIROS, PIROS, PIROS },
                new[] { PIROS, PIROS, PIROS, PIROS }
            };
        }

        public KorongÁllapot(bool[][] board)
        {
            BOARD = board;
        }

        public override bool ÁllapotE()
        {
            if (BOARD == null || BOARD.Length != 4) return false;

            for (int i = 0; i < 4; i++)
            {
                if (BOARD[i] == null || BOARD[i].Length != 4) return false;
            }

            return true;
        }

        public override bool CélÁllapotE()
        {
            if (!ÁllapotE()) return false;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BOARD[i][j] != CEL_ALLAPOT[i][j]) return false;
                }
            }
            return true;
        }

        public override int OperátorokSzáma()
        {
            return 8;
        }

        public override bool SzuperOperátor(int i)
        {
            switch (i) {
                case 0: return ForgatSor(0);
                case 1: return ForgatSor(1);
                case 2: return ForgatSor(2);
                case 3: return ForgatSor(3);
                
                case 4: return ForgatOszlop(0);
                case 5: return ForgatOszlop(1);
                case 6: return ForgatOszlop(2);
                case 7: return ForgatOszlop(3);

                default: return false;
            }
        }

        private bool ForgatSor(int sorSzam) {
            bool[] sor = BOARD[sorSzam];
            for (int i = 0; i < 4; i++)
            {
                sor[i] = !sor[i];
            }

            return ÁllapotE();
        }

        private bool ForgatOszlop(int oszlopSzam)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j == oszlopSzam) {
                        BOARD[i][j] = !BOARD[i][j];
                    }
                }
            }

            return ÁllapotE();
        }

        public override object Clone()
        {
            bool[][] clonedBoard = new bool[4][];

            for (int i = 0; i < 4; i++)
            {
                clonedBoard[i] = new bool[4];
                for (int j = 0; j < 4; j++)
                {
                    clonedBoard[i][j] = BOARD[i][j];
                }
            }

            return new KorongÁllapot(clonedBoard);
        }

        public override bool Equals(object a)
        {
            KorongÁllapot masik = (KorongÁllapot)a;
            if (masik == null) return false;

            bool[][] masikBoard = masik.BOARD;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (BOARD[i][j] != masikBoard[i][j]) return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            int hash = 17;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    hash = hash * 31 + BOARD[i][j].GetHashCode();
                }
            }

            return hash;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    char korong = BOARD[i][j] ? 'K' : 'P';
                    sb.Append(korong);
                    sb.Append(' ');
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
