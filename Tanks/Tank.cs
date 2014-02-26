using System;
using TankBattle.Interfaces;
using TankBattle.LevelObjects;

namespace TankBattle.Tanks
{
    public abstract class Tank : LevelObject, IPrintable, IHitable, ICanShoot, IDestroyable
    {
        private string[] up, down, left, right;
        private int health;
        private int shootPower;
        private int speed;
        private Directions direction; // 0 - UP, 1- LEFT, 2- RIGHT, 3 - DOWN


        public Tank(int x, int y, int health, int speed, int shootPower, ConsoleColor color, Directions direction)
            : base(x, y, color)
        {
            this.health = health;
            this.shootPower = shootPower;
            this.speed = speed;
            this.direction = direction;
            LoadTankVisuals();

        }

        public Directions Direction
        {
            get { return this.direction; }
            set
            {
                this.direction = value;
            }
        }

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

        public override string[] LoadVisual()
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
            up = new string[2] { new string(firstLineUp), new string(secondLineUp) };
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

            right = new string[3] { new string(firstLine), new string(middleLine), new string(firstLine) };
            #endregion

            #region Down

            down = new string[2] { new string(secondLineUp), new string(firstLineUp) };
            #endregion
        }

        // Made it virtual, so we can override it in the enemy tank class
        public virtual void Move(ConsoleKeyInfo pressedKey)
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

                            if (this.Direction == Directions.Right && (this.X % 6) > 1)
                            {
                                this.X = this.X - (this.X % 6) + 1;
                            }

                            else if (this.Direction == Directions.Left && (this.X % 6) > 1 && this.X % 6 < 5)
                            {
                                this.X = this.X - (this.X % 6) + 1;
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
                            this.X++;

                            if (this.Direction == Directions.Down)
                            {
                                if (this.Y % 3 != 1)
                                {
                                    this.Y--;
                                }
                            }
                            else if (this.Direction == Directions.Up && this.Y % 3 == 2)
                            {
                                this.Y--;
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
                                if (this.Y % 3 != 1)
                                {
                                    this.Y--;
                                }
                            }
                            else if (this.Direction == Directions.Up && this.Y % 3 == 2)
                            {
                                this.Y--;
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
                            if (this.Y % 3 == 2)
                            {
                                this.Y--;
                            }
                        }
                        else
                        {
                            ConsoleAction.Clear(this.X, this.Y, 3, 3);

                            if (this.Direction == Directions.Right && (this.X % 6) > 1)
                            {
                                this.X = this.X - (this.X % 6) + 1;
                            }
                            else if (this.Direction == Directions.Left && (this.X % 6) > 1 && this.X % 6 < 5)
                            {
                                this.X = this.X - (this.X % 6) + 1;
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

        public override void Print()
        {
            ConsoleAction.PrintOnPos(this.LoadVisual(), this.X, this.Y, this.Color);
        }
    }
}
