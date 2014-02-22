using System;
using TankBattle.Interfaces;

namespace TankBattle.Tanks
{
    public abstract class Tank : IPrintable, IHitable, ICanShoot, IDestroyable
    {
        private string[] up, down, left, right;
        private readonly ConsoleColor tankColor;
        private int health;
        private int shootPower;
        private int speed;
        private ulong score;
        private int x, y; // coordinats
        private Directions direction; // 0 - UP, 1- LEFT, 2- RIGHT, 3 - DOWN


        public Tank(int x, int y, int health, int speed, int shootPower, ConsoleColor color, Directions direction)
        {
            this.tankColor = color;
            this.health = health;
            this.shootPower = shootPower;
            this.score = 0;
            this.speed = speed;
            this.x = x;
            this.y = y;
            this.direction = direction;
            LoadTankVisuals();
            
        }
        public int X 
        { 
            get { return this.x; }
            set { this.x = value; }
        }
        public int Y 
        { 
            get { return this.y; }
            set { this.y = value; }
        }

        public Directions Direction
        {
            get { return this.direction; }
            set
            { 
                this.direction = value; 
            }
        }

        public ConsoleColor Color { get { return this.tankColor; } }

        public int Health { get { return this.health; } }

        public bool IsHitted { get; set; }

        public int Speed { get { return this.speed; } }

        public int ShootPower { get { return this.shootPower; } }

        public bool IsDestroyed
        {
            get
            {
                if (this.Health <= 0)
                {
                    return true;
                }

                return false;
            }
        }

        public int[] GetTankBarrel()
        {
            int[] barrelcoords = new int[2];

            if (this.Direction == Directions.Left)
            {
                barrelcoords[0] = 1;
                barrelcoords[1] = -1;
            }
            else if (this.Direction == Directions.Right)
            {
                barrelcoords[0] = 1;
                barrelcoords[1] = 3;
            }
            else if (this.Direction == Directions.Up)
            {
                barrelcoords[0] = -1;
                barrelcoords[1] = 2;
            }
            else if (this.Direction == Directions.Down)
            {
                barrelcoords[0] = 2;
                barrelcoords[1] = 2;
            }

            return barrelcoords;
        }

        public string[] GetVisual()
        {
            // 0 - UP, 1- LEFT, 2- RIGHT, 3 - DOWN
            if (this.direction == Directions.Up)
            {
                return this.up;
            }
            else if (this.direction == Directions.Left)
            {
                return this.left;
            }
            else if (this.direction == Directions.Right)
            {
                return this.right;
            }
            else
            {
                return this.down;
            }
        }

        private void LoadTankVisuals()
        {
            //To edit up, left / write Right and Down
            #region Up

            char[] firstLineUp = 
            {
                Symbols.GetChar(177),
                Symbols.GetChar(177),
                '|',
                '|',
                Symbols.GetChar(177),
                Symbols.GetChar(177)
            };
            char[] secondLineUp =
            {
                Symbols.GetChar(177),
                Symbols.GetChar(177),
                Symbols.GetChar(219),
                Symbols.GetChar(219),
                Symbols.GetChar(177),
                Symbols.GetChar(177)
            };
            up = new string[2] { new string(firstLineUp), new string( secondLineUp)};
            #endregion

            #region Left
            //height: 3, width: 3
            char[] firstLine =
                {
                Symbols.GetChar(177),
                Symbols.GetChar(177),
                Symbols.GetChar(177)
                };
            char[] secondLine =
                {
                Symbols.GetChar(205),
                Symbols.GetChar(219),
                Symbols.GetChar(219)
                };
            left = new string[3] { new string(firstLine), new string(secondLine), new string(firstLine) };
            #endregion

            #region Right

            char[] middleLine = 
            {
                Symbols.GetChar(219),
                Symbols.GetChar(219),
                Symbols.GetChar(205)
            };

            right = new string[3] { new string(firstLine),new string(middleLine), new string(firstLine) };
            #endregion

            #region Down

            down = new string[2] { new string(secondLineUp), new string(firstLineUp) };
            #endregion
        }

        public void Move(ConsoleKeyInfo pressedKey)
        {
            ///*
            //UP ARROW
            #region UpArrow
            if (pressedKey.Key == ConsoleKey.UpArrow)
            {
                if (this.Y - 1 <= 0)
                {
                    //do nothing
                }
                else
                {
                    if (this.Direction != Directions.Up)
                    {
                        if (this.Direction == Directions.Down)
                        {
                            ConsoleAction.Clear(this.X, this.Y, 6, 2);
                        }
                        else
                        {
                            ConsoleAction.Clear(this.X, this.Y, 3, 3);

                            if (this.Direction == Directions.Right && (this.x % 6) > 1)
                            {
                                this.x = this.x - (this.x % 6) + 1;
                            }

                            else if (this.Direction == Directions.Left && (this.x % 6) > 1 && this.x % 6 < 5)
                            {
                                this.x = this.x - (this.x % 6) + 1;
                            }
                        }

                        this.Direction = Directions.Up;
                        this.Print();
                    }
                    else
                    {
                        Console.MoveBufferArea(this.X, this.Y, 6, 2, this.X, this.Y - 1);
                        this.Y--;
                    }
                }
            }
            #endregion

            //LEFT ARROW
            #region LeftArrow
            else if (pressedKey.Key == ConsoleKey.LeftArrow)
            {
                if (this.X - 1 <= 0)
                {
                    //do nothing
                }
                else
                {
                    if (this.Direction != Directions.Left)
                    {
                        if (this.Direction == Directions.Right)
                        {
                            ConsoleAction.Clear(this.X, this.Y, 3, 3);
                        }
                        else
                        {
                            ConsoleAction.Clear(this.X, this.Y, 6, 2);
                            this.x++;

                            if (this.Direction == Directions.Down)
                            {
                                if (y % 3 != 1)
                                {
                                    y--;
                                }
                            }
                            else if (this.Direction == Directions.Up && this.y % 3 == 2)
                            {
                                this.y--;
                            }
                        }

                        this.Direction = Directions.Left;
                        this.Print();
                    }

                    Console.MoveBufferArea(this.X, this.Y, 3, 3, this.X - 1, this.Y);
                    this.X--;
                }
            }
            #endregion

            //RIGHT ARROW
            #region RightArrow
            else if (pressedKey.Key == ConsoleKey.RightArrow)
            {
                if (this.X + 4 >= Level.PlaygroundWidth())
                {
                    //do nothing
                }
                else
                {
                    if (this.Direction != Directions.Right)
                    {
                        if (this.Direction == Directions.Left)
                        {
                            ConsoleAction.Clear(this.X, this.Y, 3, 3);
                        }
                        else
                        {
                            ConsoleAction.Clear(this.X, this.Y, 6, 2);

                            if (this.Direction == Directions.Down)
                            {
                                if (y % 3 != 1)
                                {
                                    y--;
                                }
                            }
                            else if (this.Direction == Directions.Up && this.y % 3 == 2)
                            {
                                this.y--;
                            }
                        }

                        this.Direction = Directions.Right;
                        this.Print();
                    }

                    Console.MoveBufferArea(this.X, this.Y, 3, 3, this.X + 1, this.Y);
                    this.X++;
                }
            }
            #endregion

            //DOWN ARROW
            #region DownArrow

            else if (pressedKey.Key == ConsoleKey.DownArrow)
            {
                if (this.Y + 4 >= Level.PlaygroundHeight())
                {
                    //do nothing
                }
                else
                {
                    if (this.Direction != Directions.Down)
                    {
                        if (this.Direction == Directions.Up)
                        {
                            ConsoleAction.Clear(this.X, this.Y, 6, 2);
                        }
                        else
                        {
                            ConsoleAction.Clear(this.X, this.Y, 3, 3);

                            if (this.Direction == Directions.Right && (this.x % 6) > 1)
                            {
                                this.x = this.x - (this.x % 6) + 1;
                            }
                            else if (this.Direction == Directions.Left && (this.x % 6) > 1 && this.x % 6 < 5)
                            {
                                this.x = this.x - (this.x % 6) + 1;
                            }
                        }

                        this.Direction = Directions.Down;
                        this.Print();
                    }

                    Console.MoveBufferArea(this.X, this.Y, 6, 2, this.X, this.Y + 1);
                    this.Y++;
                }
            }
            #endregion
            //*/
        }

        public void LooseHealth(int amount)
        {
            this.health -= amount;
        }

        public void Print()
        {
            ConsoleAction.PrintOnPos(this.GetVisual(), this.x, this.y, this.Color);
        }
    }
}
