using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace consoleopentktest1
{
        class MyApplication
        {
            [STAThread]
            public static void Main()
            {
                Random rnd = new Random();
                int color1 = rnd.Next(-2000000000,200000000);
                int color2 = rnd.Next(-2000000000, 200000000);
                int color3 = rnd.Next(-2000000000, 200000000);
                float a, b;
                a = 200;
                b = 200;
                GraphicsMode g = new GraphicsMode(new ColorFormat(1203));
                using (var game = new GameWindow(640,380, g, "Blunder Buzz"))
                    {
                        game.Load += (sender, e) =>
                        {
                            // setup settings, load textures, sounds
                            game.VSync = VSyncMode.On;
                            game.WindowBorder = WindowBorder.Hidden;

                            //fullscreen
                            game.Width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                            game.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                            game.X = 0;
                            game.Y = 0;
                        };

                        game.Resize += (sender, e) =>
                        {
                            GL.Viewport(0, 0, game.Width, game.Height);
                        };
                    
                        game.UpdateFrame += (sender, e) =>
                        {
                        
                            // add game logic, input handling
                            //game.KeyPress
                            game.KeyPress += (s, arg) =>
                            {
                                //Console.WriteLine(arg.KeyChar.ToString(), arg.ToString());
                                switch(arg.KeyChar)
                                {
                                    case 't':
                                        game.Close();
                                        break;
                                    case 'e':
                                        color1 = rnd.Next(-2000000000, 200000000);
                                        color2 = rnd.Next(-2000000000, 200000000);
                                        color3 = rnd.Next(-2000000000, 200000000);
                                        break;
                                    case 'w':
                                        b += 0.01f;
                                        break;
                                    case 's':
                                        b -= 0.01f;
                                        break;
                                    case 'a':
                                        a -= 0.01f;
                                        break;
                                    case 'd':
                                        a += 0.01f;
                                        break;
                                }
                            };

                        };

                        game.RenderFrame += (sender, e) =>
                        {
                            // render graphics
                            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                            GL.MatrixMode(MatrixMode.Projection);
                            GL.LoadIdentity();
                            GL.Ortho(0, 380, 0, 640, 0.0, 4.0);
                        
                            GL.Begin(PrimitiveType.Triangles);
                            //GL.Begin(PrimitiveType.Polygon);
                            //GL.Begin(PrimitiveType.Quads);
                            GL.Color3(Color.FromArgb(color1));
                            //GL.Vertex2(-1.0f, 0.5f);
                            GL.Vertex2(-10+a, 5+b);
                            GL.Color3(Color.FromArgb(color2));
                            GL.Vertex2(10.0f+a, -5f+b);
                            GL.Color3(Color.FromArgb(color3));
                            GL.Vertex2(5f + a, 10.0f + b);
                            GL.End();
                            GL.Begin(PrimitiveType.Triangles);
                            GL.Color3(Color.FromArgb(color3^color1));
                            GL.Vertex2(-10.0f + a, -5f + b);
                            GL.Color3(Color.FromArgb(color3 ^ color2));
                            GL.Vertex2(10.0f + a, 10.0f + b);
                            GL.Color3(Color.FromArgb(color2 ^ color1));
                            GL.Vertex2(-10.0f + a, 10.0f + b);

                            GL.End();

                            game.SwapBuffers();
                        };

                        // Run the game at 60 updates per second
                        game.Run(20.0);
                    }
            }
        }
    
}
