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
using System.Threading;
using System.Windows.Forms;

namespace consoleopentktest1
{

    public sealed class Window : GameWindow
    {
        // Размер окна 800x600, заголовок "OpenGL Tutorial"
        // Версия OpenGL 4.0
        // Без поддержки deprecated-функциональности
        // Остальное оставим по умолчанию
        public Window() : base(700, 600, GraphicsMode.Default, "OpenGL Tutorial", GameWindowFlags.FixedWindow, DisplayDevice.Default, 4, 0, GraphicsContextFlags.ForwardCompatible) { }
        
        public void ddd()
        {
            //while(true)
            {
                //MessageBox.Show("");
                Action n = new Action(ddd);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.Rotate(10, 1, 0, 0);
                
               // Thread.Sleep(100);
                //ddd();
            }
        }


        float a = 100;
        float s = 50;
        float d = 100;
        float ae = 0;
        float se = 0;
        float de = 0;
        float angelx = 0;
        float angely = 0;
        static Window window = new Window();
        public static void Main()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            Bitmap bmp = new Bitmap("123.bmp");
            System.Drawing.Imaging.BitmapData data;
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            data = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            
            // Создаём и запускаем окно
            using (window = new Window()) {
                window.KeyDown += (e, arg) =>
                {
                    switch(arg.Key)
                    {
                        case Key.W:
                            //GL.MatrixMode(MatrixMode.Modelview);
                            //GL.Rotate(-10, 1, 0, 0);
                            window.angely += 5f;
                            break;
                        case Key.S:
                            //GL.MatrixMode(MatrixMode.Modelview);
                            //GL.Rotate(10, 1, 0, 0);
                            window.angely -= 5f;
                            break;
                        case Key.A:
                            //GL.MatrixMode(MatrixMode.Modelview);
                            //GL.Rotate(10, 0, 1, 0);
                            window.angelx += 5f;
                            break;
                        case Key.D:
                            //GL.MatrixMode(MatrixMode.Modelview);
                            //GL.Rotate(-10, 0, 1, 0);
                            window.angelx -= 5f;
                            break;
                        case Key.Q:
                            Environment.Exit(0);
                            break;

                    }
                };
                //при закрытие окна, так же закрываем консоль
                
                window.Closed += (o, e) => { Environment.Exit(0); };
                window.Run(60);
                
                    }
        }

        float rotate = 0;

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            ///////////////////////////
            //Eye = new Vector3(a, s, d);
            a = 0;
            d = 0;
            s = 0;
            Eye = new Vector3(100, 100, 100);
            ae = (float)(55 * Math.Sin(angelx * Math.PI / 180) * Math.Sin(angely * Math.PI / 180)) + 0;
            de = (float)(55 * Math.Cos(angelx * Math.PI / 180) * Math.Sin(angely * Math.PI / 180)) + 0;
            se = (float)(55 * Math.Cos(angely * Math.PI / 180)) + 0;
            window.Title = ae.ToString() + " " + de.ToString() + " " + se.ToString() +"anglex: "+angelx + " angley: " + angely;

            Targ = new Vector3(0f, 0f, 0f);
            GL.ClearColor(Color.SkyBlue);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Normalize);
            /*GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(10f, Width / Height, 1.0f, 100.0f);
            */
            var p = Matrix4.CreatePerspectiveFieldOfView((float)(90 * Math.PI / 180), 1, 20, 5000);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref p);
            
            var modelView = Matrix4.LookAt(Eye, Targ, new Vector3(0, 1, 0));
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelView);
            //////////////////////////////////////
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            /*thread = new Thread(new ThreadStart(window.ddd));
            thread.Start();
            thread.Join();
            thread.Abort();*/
            GL.Light(LightName.Light0, LightParameter.Position, new float[] { 0.0f, 0.0f, 0.0f, 1.0f });


            square(ae, se, de, 20, Color.Red);
            
            GL.Color3(Color.Green);     //X - green
            GL.Begin(BeginMode.Lines);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(500, 0, 0);
            GL.End();

            GL.Color3(Color.Red);       //Y - red
            GL.Begin(BeginMode.Lines);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 500, 0);
            GL.End();

            GL.Color3(Color.Blue);      //Z - blue
            GL.Begin(BeginMode.Lines);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 500);
            GL.End();
            {
                /*
                //GL.Color3(Color.Red);
                GL.Begin(BeginMode.Polygon);
                GL.TexCoord2(-1, -1);
                GL.Vertex3(0, 0, 0);
                GL.Vertex3(0, 0, 10);
                GL.Vertex3(10, 0, 10);
                GL.Vertex3(10, 0, 0);
                GL.End();

                GL.Color3(Color.Green);
                GL.Begin(BeginMode.Polygon);
                GL.Vertex3(0, 0, 0);
                GL.Vertex3(0, 0, 10);
                GL.Vertex3(0, 10, 10);
                GL.Vertex3(0, 10, 0);
                GL.End();

                GL.Color3(Color.Blue);
                GL.Begin(BeginMode.Polygon);
                GL.Vertex3(0, 0, 0);
                GL.Vertex3(0, 10, 0);
                GL.Vertex3(10, 10, 0);
                GL.Vertex3(10, 0, 0);
                GL.End();

                GL.Color3(Color.Orange);
                GL.Begin(BeginMode.Lines);
                GL.Vertex3(Targ);
                GL.Color3(Color.Yellow);
                GL.Vertex3(Eye);
                GL.End();

                GL.FrontFace(FrontFaceDirection.Ccw);
                GL.Begin(BeginMode.Polygon);
                GL.Vertex3(-100, -50, -100);
                GL.Vertex3(-100, -50, 100);
                GL.Vertex3(100, -50, 100);
                GL.Vertex3(100, -50, -100);
                GL.End();

                //SwapBuffers();

                //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit); // Очищаем буфер цвета
                //GL.LoadIdentity();                                                                         // Тут будет распологаться основной код отрисовк
                //GL.Translate(0.0f, 0f, -0f);
                //GL.Rotate(rotate, 0.5f, 1.0f, 0f);

                GL.Begin(PrimitiveType.Triangles);
                GL.Color3(Color.Red);
                GL.Vertex3(50f, 0.5f, 50f);
                GL.Vertex3(50f, 00f, 10f);
                GL.Vertex3(-0.5f, 30f, 0.5f);

                GL.Color3(Color.Yellow);
                GL.Vertex3(0f, 0.5f, 0f);
                GL.Vertex3(0.5f, 0f, -0.5f);
                GL.Vertex3(-0.5f, 0f, -0.5f);

                GL.Color3(Color.Blue);
                GL.Vertex3(0f, 0.5f, 0f);
                GL.Vertex3(0.5f, 0f, 0.5f);
                GL.Vertex3(0.5f, 0f, -0.5f);

                GL.Color3(Color.Green);
                GL.Vertex3(0f, 0.5f, 0f);
                GL.Vertex3(-0.5f, 0f, 0.5f);
                GL.Vertex3(0.5f, 0f, 0.5f);
                GL.End();
                square(40, 10, 10, 20, Color.Red);*/
            }
            sphere(50, 10, 10);
            SwapBuffers(); // Переключаем задний и передний буферы
            Mouse.SetPosition(X+Width/2, Y+Height/2);
            
        }
        Thread thread;
        protected override void OnLoad(EventArgs e)
        {
            
            //GL.Viewport(0, 0, Width, Height); // Зададим область перерисовки размером со всё окно
            //GL.MatrixMode(MatrixMode.Projection);
            //GL.LoadIdentity();
            GL.ClearColor(Color4.Gray); // Зададим цвет очистки окна
            //GL.Translate(0.0f, -0.5f, -2.5f);
            float[] light_ambient = { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] light_diffuse = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] light_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] spotdirection = { 0.0f, 0.0f, -20.0f };

            GL.Light(LightName.Light0, LightParameter.Ambient, light_ambient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, light_diffuse);
            GL.Light(LightName.Light0, LightParameter.Specular, light_specular);

            GL.Light(LightName.Light0, LightParameter.ConstantAttenuation, 1.8f);
            GL.Light(LightName.Light0, LightParameter.SpotCutoff, 45.0f);
            GL.Light(LightName.Light0, LightParameter.SpotDirection, spotdirection);
            GL.Light(LightName.Light0, LightParameter.SpotExponent, 1.0f);

            GL.LightModel(LightModelParameter.LightModelLocalViewer, 1.0f);
            GL.LightModel(LightModelParameter.LightModelTwoSide, 1.0f);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ColorMaterial);
            GL.ShadeModel(ShadingModel.Flat);
            //thread.Start();


        }
        Vector3 Eye, Targ;
        protected override void OnResize(EventArgs e)
        {
            
            /*
            GL.LoadMatrix(ref matrix);
            GL.MatrixMode(MatrixMode.Modelview);*/

        }

        public void square(float x, float y, float z, float size, Color color)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(color);
            x -= size / 2;
            y -= size / 2;
            z -= size / 2;
            //size /= 2;
            GL.Vertex3(x, y, z);
            GL.Vertex3(x, y + size, z);
            GL.Vertex3(x + size, y + size, z);
            GL.Vertex3(x + size, y, z);
            
            GL.Vertex3(x, y, z + size);
            GL.Vertex3(x, y + size, z + size);
            GL.Vertex3(x + size, y + size, z + size);
            GL.Vertex3(x + size, y, z + size);
            
            GL.Vertex3(x, y, z);
            GL.Vertex3(x, y, z + size);
            GL.Vertex3(x, y + size, z + size);
            GL.Vertex3(x, y + size, z);
            
            GL.Vertex3(x + size, y, z);
            GL.Vertex3(x + size, y, z + size);
            GL.Vertex3(x + size, y + size, z + size);
            GL.Vertex3(x + size, y + size, z);
            
            GL.Vertex3(x, y, z);
            GL.Vertex3(x, y, z + size);
            GL.Vertex3(x + size, y, z + size);
            GL.Vertex3(x + size, y, z);
            
            GL.Vertex3(x, y + size, z);
            GL.Vertex3(x, y + size, z + size);
            GL.Vertex3(x + size, y + size, z + size);
            GL.Vertex3(x + size, y + size, z);

            GL.End();
        }

        void sphere(double r, int nx, int ny)
        {
            int i, ix, iy;
            double x, y, z;

            for (iy = 0; iy < ny; ++iy)
            {
                GL.Begin(BeginMode.QuadStrip);
                for (ix = 0; ix <= nx; ++ix)
                {
                    x = r * Math.Sin(iy * Math.PI / ny) * Math.Cos(2 * ix * Math.PI / nx);
                    y = r * Math.Sin(iy * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx);
                    z = r * Math.Cos(iy * Math.PI / ny);
                    GL.Normal3(x, y, z);//нормаль направлена от центра
                    GL.TexCoord2((double)ix / (double)nx, (double)iy / (double)ny);
                    GL.Vertex3(x, y, z);

                    x = r * Math.Sin((iy + 1) * Math.PI / ny) * Math.Cos(2 * ix * Math.PI / nx);
                    y = r * Math.Sin((iy + 1) * Math.PI / ny) * Math.Sin(2 * ix * Math.PI / nx);
                    z = r * Math.Cos((iy + 1) * Math.PI / ny);
                    GL.Normal3(x, y, z);
                    GL.TexCoord2((double)ix / (double)nx, (double)(iy + 1) / (double)ny);
                    GL.Vertex3(x, y, z);
                }
                GL.End();
            }
        }
    }

}
