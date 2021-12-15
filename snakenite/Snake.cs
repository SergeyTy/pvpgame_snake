﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static snakenite.Game;

namespace snakenite
{
    class Snake
    {
        private List<Point> snake;

        private Direction direction;
        private int step = 1;
        private Point tail;
        private Point head;
        public Guid guid;

        bool rotate = true;

        public Snake(int x, int y, int length)
        {
            direction = Direction.RIGHT;

            snake = new List<Point>();
            for (int i = x - length; i < x; i++)
            {
                Point p = (i*10, y*10, '*');
                snake.Add(p);

                p.Draw();
            }

            guid = new Guid();
        }

        public Point GetHead() => snake.Last();

        public Direction GetDirection() => this.direction;

        public void Move()
        {
            head = GetNextPoint();
            snake.Add(head);

            tail = snake.First();
            snake.Remove(tail);

            tail.Clear();
            head.Draw();

            rotate = true;
        }

        public bool Eat(Point p)
        {
            head = GetNextPoint();
            if (head == p)
            {
                snake.Add(head);
                head.Draw();
                return true;
            }
            return false;
        }

        public Point GetNextPoint()
        {
            Point p = GetHead();

            switch (direction)
            {
                case Direction.LEFT:
                    p.x -= step;
                    break;
                case Direction.RIGHT:
                    p.x += step;
                    break;
                case Direction.UP:
                    p.y -= step;
                    break;
                case Direction.DOWN:
                    p.y += step;
                    break;
            }
            return p;
        }

        public void Rotation(ConsoleKey key)
        {
            if (rotate)
            {
                switch (direction)
                {
                    case Direction.LEFT:
                    case Direction.RIGHT:
                        if (key == ConsoleKey.DownArrow)
                        {
                            direction = Direction.DOWN;
                        }
                        else if (key == ConsoleKey.UpArrow)
                        {
                            direction = Direction.UP;
                        }
                        break;
                    case Direction.UP:
                    case Direction.DOWN:
                        if (key == ConsoleKey.LeftArrow)
                        {
                            direction = Direction.LEFT;
                        }
                        else if (key == ConsoleKey.RightArrow)
                        {
                            direction = Direction.RIGHT;
                        }
                        break;
                }
                rotate = false;
            }

        }

        public bool IsHit(Point p)
        {
            for (int i = snake.Count - 2; i > 0; i--)
            {
                if (snake[i] == p)
                {
                    return true;
                }
            }
            return false;
        }
    }//class Snake
}