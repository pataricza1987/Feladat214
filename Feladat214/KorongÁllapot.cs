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

        private static readonly bool[][] CEL_ALLAPOT = {
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
            return 16;
        }

        public override bool SzuperOperátor(int i)
        {
            bool eredmeny;

            switch (i) {
                case 0: eredmeny = ForgatSor(0); break;
                case 1: eredmeny = ForgatSor(1); break;
                case 2: eredmeny = ForgatSor(2); break;
                case 3: eredmeny = ForgatSor(3); break;

                case 4: eredmeny = ForgatOszlop(0); break;
                case 5: eredmeny = ForgatOszlop(1); break;
                case 6: eredmeny = ForgatOszlop(2); break;
                case 7: eredmeny = ForgatOszlop(3); break;

                case 8: eredmeny = FlipSor(0); break;
                case 9: eredmeny = FlipSor(1); break;
                case 10: eredmeny = FlipSor(2); break;
                case 11: eredmeny = FlipSor(3); break;

                case 12: eredmeny = FlipOszlop(0); break;
                case 13: eredmeny = FlipOszlop(1); break;
                case 14: eredmeny = FlipOszlop(2); break;
                case 15: eredmeny = FlipOszlop(3); break;

                default: return false;
            }

            //Console.WriteLine(ToString());

            return eredmeny;
        }

        private bool ForgatSor(int sorSzam)
        {
            Csere(sorSzam, 0, sorSzam, 3);
            Csere(sorSzam, 1, sorSzam, 2);

            return ÁllapotE();
        }

        private bool ForgatOszlop(int oszlopSzam)
        {
            Csere(0, oszlopSzam, 3, oszlopSzam);
            Csere(1, oszlopSzam, 2, oszlopSzam);

            return ÁllapotE();
        }

        private void Csere(int i1, int j1, int i2, int j2)
        {
            bool temp = BOARD[i1][j1];
            BOARD[i1][j1] = BOARD[i2][j2];
            BOARD[i2][j2] = temp;
        }

        private bool FlipSor(int sorSzam) {
            for (int i = 0; i < 4; i++)
            {
                BOARD[sorSzam][i] = !BOARD[sorSzam][i];
            }

            return ÁllapotE();
        }

        private bool FlipOszlop(int oszlopSzam)
        {
            for (int i = 0; i < 4; i++)
            {
                BOARD[i][oszlopSzam] = !BOARD[i][oszlopSzam];
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
